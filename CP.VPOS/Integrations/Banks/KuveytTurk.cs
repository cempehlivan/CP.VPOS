using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Xml;


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

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

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

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlTest.Non3DPayGateUrl : _urlLive.Non3DPayGateUrl));

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

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
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

            string hashText = SHA1Base64(
                param["MerchantId"].ToString() +
                param["MerchantOrderId"].ToString() +
                param["Amount"].ToString() +
                param["OkUrl"].ToString() +
                param["FailUrl"].ToString() +
                param["UserName"].ToString() +
                SHA1Base64(auth.merchantPassword));

            param.Add("HashData", hashText);


            string xml = param.toXml("KuveytTurkVPosMessage");

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlTest.ThreeDModelPayGateUrl : _urlLive.ThreeDModelPayGateUrl));

            response.statu = SaleResponseStatu.RedirectHTML;
            response.message = resp;

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
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
                    authenticationResponse = Uri.UnescapeDataString(authenticationResponse);

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

                        string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlTest.ThreeDModelProvisionGateUrl : _urlLive.ThreeDModelProvisionGateUrl));

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

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
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

        private string xmlRequest(string xml, string link)
        {
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
#pragma warning disable SYSLIB0014
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(link);
#pragma warning restore SYSLIB0014
            byte[] postdatabytes = System.Text.Encoding.UTF8.GetBytes(xml);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = postdatabytes.Length;
            System.IO.Stream requeststream = request.GetRequestStream();
            requeststream.Write(postdatabytes, 0, postdatabytes.Length);
            requeststream.Close();

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader responsereader = new System.IO.StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
            string gelenXml = responsereader.ReadToEnd();

            return gelenXml;
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
