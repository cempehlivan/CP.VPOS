using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;

namespace CP.VPOS.Banks.KuveytTurk
{
    internal class KuveytTurkVirtualPOSService : IVirtualPOSService
    {
        private static readonly KuveytTurkUrlModel _urlTest = new KuveytTurkUrlModel
        {
            Non3DPayGateUrl = "https://boatest.kuveytturk.com.tr/boa.virtualpos.services/Home/Non3DPayGate",
            ThreeDModelPayGateUrl = "https://boatest.kuveytturk.com.tr/boa.virtualpos.services/Home/ThreeDModelPayGate",
            ThreeDModelProvisionGateUrl = "https://boatest.kuveytturk.com.tr/boa.virtualpos.services/Home/ThreeDModelProvisionGate",
            WCFServiceUrl = "https://boatest.kuveytturk.com.tr/BOA.Integration.WCFService/BOA.Integration.VirtualPos/VirtualPosService.svc"
        };

        private static readonly KuveytTurkUrlModel _urlLive = new KuveytTurkUrlModel
        {
            Non3DPayGateUrl = "https://sanalpos.kuveytturk.com.tr/ServiceGateWay/Home/Non3DPayGate",
            ThreeDModelPayGateUrl = "https://sanalpos.kuveytturk.com.tr/ServiceGateWay/Home/ThreeDModelPayGate",
            ThreeDModelProvisionGateUrl = "https://sanalpos.kuveytturk.com.tr/ServiceGateWay/Home/ThreeDModelProvisionGate",
            WCFServiceUrl = "https://boa.kuveytturk.com.tr/BOA.Integration.WCFService/BOA.Integration.VirtualPos/VirtualPosService.svc"
        };

        public async Task<SaleResponse> SaleAsync(SaleRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            if (request?.payment3D?.confirm == true)
                return await Sale3DAsync(request, auth, cancellationToken).ConfigureAwait(false);

            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                orderNumber = request.orderNumber,
            };

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "APIVersion", "TDV2.0.0" },
                { "CardNumber", request.saleInfo.cardNumber },
                { "CardExpireDateYear", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) },
                { "CardExpireDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "CardCVV2", request.saleInfo.cardCVV },
                { "CardHolderName", request.saleInfo.cardNameSurname },
                { "BatchID", 0 },
                { "TransactionType", "Sale" },
                { "InstallmentCount", request.saleInfo.installment > 1 ? request.saleInfo.installment : 0 },
                { "Amount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "DisplayAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "CurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "MerchantOrderId", request.orderNumber },
                { "TransactionSecurity", 1 },
                { "UserName", auth.merchantUser },
                { "MerchantId", auth.merchantID },
                { "CustomerId", auth.merchantStorekey },
            };

            string hashText = SHA1Base64(
                param["MerchantId"].ToString() +
                param["MerchantOrderId"].ToString() +
                param["Amount"].ToString() +
                param["UserName"].ToString() +
                SHA1Base64(auth.merchantPassword));

            param.Add("HashData", hashText);


            string xml = param.toXml("KuveytTurkVPosMessage");

            string resp = await this.xmlRequestAsync(xml, (auth.testPlatform ? _urlTest.Non3DPayGateUrl : _urlLive.Non3DPayGateUrl), cancellationToken).ConfigureAwait(false);

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "VPosTransactionResponseContract");

            response.privateResponse = respDic;

            if (respDic?.ContainsKey("ResponseCode") == true && respDic["ResponseCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarıyla tamamlandı";
                response.transactionId = (respDic.ContainsKey("ProvisionNumber") ? respDic["ProvisionNumber"].cpToString() : "") + "|" + (respDic.ContainsKey("OrderId") ? respDic["OrderId"].cpToString() : "");
            }
            else
            {
                string message = "";

                if (respDic.ContainsKey("ResponseMessage") && respDic["ResponseMessage"].cpToString() != "")
                    message = respDic["ResponseMessage"].cpToString();

                if (string.IsNullOrWhiteSpace(message))
                    message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                response.transactionId = "";
                response.statu = SaleResponseStatu.Error;
                response.message = message;
            }


            return response;
        }

        private async Task<SaleResponse> Sale3DAsync(SaleRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                orderNumber = request.orderNumber,
            };

            string phoneNumber = request.invoiceInfo.phoneNumber.cpToString().clearNumber();

            if (phoneNumber.Length > 10)
                phoneNumber = phoneNumber.Substring(phoneNumber.Length - 10);

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "APIVersion", "TDV2.0.0" },
                { "OkUrl", request.payment3D.returnURL },
                { "FailUrl", request.payment3D.returnURL },
                { "HashData", "" },
                { "MerchantId", auth.merchantID },
                { "CustomerId", auth.merchantStorekey },
                { "UserName", auth.merchantUser },
                { "CardNumber", request.saleInfo.cardNumber },
                { "CardExpireDateYear", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) },
                { "CardExpireDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "CardCVV2", request.saleInfo.cardCVV },
                { "CardHolderName", request.saleInfo.cardNameSurname },
                { "BatchID", 0 },
                { "TransactionType", "Sale" },
                { "InstallmentCount", request.saleInfo.installment > 1 ? request.saleInfo.installment : 0 },
                { "Amount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "DisplayAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "CurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "MerchantOrderId", request.orderNumber },
                { "TransactionSecurity", 3 },
                { "DeviceData", new Dictionary<string, object>()
                {
                    { "DeviceChannel", "02" },
                    { "ClientIP", request.customerIPAddress }
                }
                },
                { "CardHolderData", new Dictionary<string, object>()
                {
                    { "BillAddrCity", request.invoiceInfo.cityName.getMaxLength(50) },
                    { "BillAddrCountry", ((int)(request?.invoiceInfo?.country ?? Country.TUR)).ToString("000") },
                    { "BillAddrLine1", request.invoiceInfo.addressDesc.getMaxLength(150) },
                    { "BillAddrPostCode", request.invoiceInfo.postCode },
                    { "Email", request.invoiceInfo.emailAddress },
                    { "MobilePhone", new Dictionary<string, object>()
                    {
                        { "Cc", "90" },
                        { "Subscriber", phoneNumber }
                    }
                    }
                }
                },

            };

            string hashedPassword = SHA1Base64(auth.merchantPassword);

            string hashText = SHA1Base64(
                param["MerchantId"].ToString() +
                param["MerchantOrderId"].ToString() +
                param["Amount"].ToString() +
                param["OkUrl"].ToString() +
                param["FailUrl"].ToString() +
                param["UserName"].ToString() +
                hashedPassword);

            param["HashData"] = hashText;


            string xml = param.toXml("KuveytTurkVPosMessage");

            string resp = await this.xmlRequestAsync(xml, (auth.testPlatform ? _urlTest.ThreeDModelPayGateUrl : _urlLive.ThreeDModelPayGateUrl), cancellationToken).ConfigureAwait(false);

            response.statu = SaleResponseStatu.RedirectHTML;
            response.message = resp;

            return response;
        }

        public async Task<SaleResponse> Sale3DResponseAsync(Sale3DResponseRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                privateResponse = new Dictionary<string, object>(),
                orderNumber = ""
            };

            response.privateResponse.Add("response_1", request.responseArray);

            if (request?.responseArray?.ContainsKey("AuthenticationResponse") == true)
            {
                string authenticationResponse = request.responseArray["AuthenticationResponse"].cpToString();

                if (!string.IsNullOrWhiteSpace(authenticationResponse))
                {
                    authenticationResponse = System.Net.WebUtility.UrlDecode(authenticationResponse);

                    Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(authenticationResponse, "VPosTransactionResponseContract");

                    if (respDic?.ContainsKey("ResponseCode") == true && respDic["ResponseCode"].cpToString() == "00")
                    {
                        response.orderNumber = (respDic.ContainsKey("MerchantOrderId") ? respDic["MerchantOrderId"].cpToString() : "");

                        Dictionary<string, object> vposMessageDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(respDic["VPosMessage"]));


                        Dictionary<string, object> param = new Dictionary<string, object>()
                        {
                            { "APIVersion", "TDV2.0.0" },
                            { "MerchantId", auth.merchantID },
                            { "CustomerId", auth.merchantStorekey },
                            { "UserName", auth.merchantUser },
                            { "TransactionType", "Sale" },
                            { "InstallmentCount", vposMessageDic["InstallmentCount"].cpToString() },
                            { "Amount", vposMessageDic["Amount"].cpToString() },
                            { "CurrencyCode", vposMessageDic["CurrencyCode"].cpToString() },
                            { "MerchantOrderId", response.orderNumber },
                            { "TransactionSecurity", 3 },

                            { "KuveytTurkVPosAdditionalData", new Dictionary<string, object>()
                            {
                                { "AdditionalData", new Dictionary<string, object>()
                                {
                                    { "Key", "MD" },
                                    { "Data", respDic["MD"].cpToString() }
                                }
                                },
                            }
                            },
                        };

                        string hashText = SHA1Base64(
                            param["MerchantId"].ToString() +
                            param["MerchantOrderId"].ToString() +
                            param["Amount"].ToString() +
                            param["UserName"].ToString() +
                            SHA1Base64(auth.merchantPassword));

                        param.Add("HashData", hashText);


                        string xml = param.toXml("KuveytTurkVPosMessage");

                        string resp = await this.xmlRequestAsync(xml, (auth.testPlatform ? _urlTest.ThreeDModelProvisionGateUrl : _urlLive.ThreeDModelProvisionGateUrl), cancellationToken).ConfigureAwait(false);

                        Dictionary<string, object> provisionresDic = FoundationHelper.XmltoDictionary(resp, "VPosTransactionResponseContract");

                        response.privateResponse.Add("response_2", provisionresDic);

                        if (provisionresDic?.ContainsKey("ResponseCode") == true && provisionresDic["ResponseCode"].cpToString() == "00")
                        {
                            response.statu = SaleResponseStatu.Success;
                            response.message = "İşlem başarıyla tamamlandı";
                            response.transactionId = (provisionresDic.ContainsKey("ProvisionNumber") ? provisionresDic["ProvisionNumber"].cpToString() : "") + "|" + (provisionresDic.ContainsKey("OrderId") ? provisionresDic["OrderId"].cpToString() : "");
                        }
                        else
                        {
                            string message = "";

                            if (provisionresDic.ContainsKey("ResponseMessage") && provisionresDic["ResponseMessage"].cpToString() != "")
                                message = provisionresDic["ResponseMessage"].cpToString();

                            if (string.IsNullOrWhiteSpace(message))
                                message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                            response.transactionId = "";
                            response.statu = SaleResponseStatu.Error;
                            response.message = message;
                        }
                    }
                    else
                    {
                        string message = "";

                        if (respDic.ContainsKey("ResponseMessage") && respDic["ResponseMessage"].cpToString() != "")
                            message = respDic["ResponseMessage"].cpToString();

                        if (string.IsNullOrWhiteSpace(message))
                            message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                        response.transactionId = "";
                        response.statu = SaleResponseStatu.Error;
                        response.message = message;
                    }
                }
            }


            return response;
        }

        public Task<CancelResponse> CancelAsync(CancelRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RefundResponse> RefundAsync(RefundRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AdditionalInstallmentQueryResponse> AdditionalInstallmentQueryAsync(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            return Task.FromResult<AdditionalInstallmentQueryResponse>(null);
        }

        public Task<AllInstallmentQueryResponse> AllInstallmentQueryAsync(AllInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            return Task.FromResult<AllInstallmentQueryResponse>(null);
        }

        public Task<BINInstallmentQueryResponse> BINInstallmentQueryAsync(BINInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            return Task.FromResult<BINInstallmentQueryResponse>(null);
        }

        public Task<SaleQueryResponse> SaleQueryAsync(SaleQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken)
        {
            return Task.FromResult<SaleQueryResponse>(null);
        }

        private string SHA1Base64(string text)
        {
            using (var sha = SHA1.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] hashingbytes = sha.ComputeHash(bytes);
                string hash = Convert.ToBase64String(hashingbytes);

                return hash;
            }
        }

        private Task<string> xmlRequestAsync(string xml, string link, CancellationToken cancellationToken)
        {
            byte[] postdatabytes = System.Text.Encoding.UTF8.GetBytes(xml);

            return WebRequestHelper.PostAsync(link, postdatabytes, "application/xml", System.Text.Encoding.UTF8, 100000, cancellationToken);
        }
    }


    internal class KuveytTurkUrlModel
    {
        public string Non3DPayGateUrl { get; set; }
        public string ThreeDModelPayGateUrl { get; set; }
        public string ThreeDModelProvisionGateUrl { get; set; }
        public string WCFServiceUrl { get; set; }
    }
}
