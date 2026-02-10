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

namespace CP.VPOS.Banks.VakifKatilim
{
    internal class VakifKatilimVirtualPOSService : IVirtualPOSService
    {
        private static readonly VakifKatilimUrlModel _urlTest = new VakifKatilimUrlModel
        {
            Non3DPayGateUrl = "",
            ThreeDModelPayGateUrl = "",
            ThreeDModelProvisionGateUrl = "",
            CancelServiceUrl = "",
            RefundServiceUrl = ""
        };

        private static readonly VakifKatilimUrlModel _urlLive = new VakifKatilimUrlModel
        {
            Non3DPayGateUrl = "https://boa.vakifkatilim.com.tr/VirtualPOS.Gateway/Home/Non3DPayGate",
            ThreeDModelPayGateUrl = "https://boa.vakifkatilim.com.tr/VirtualPOS.Gateway/Home/ThreeDModelPayGate",
            ThreeDModelProvisionGateUrl = "https://boa.vakifkatilim.com.tr/VirtualPOS.Gateway/Home/ThreeDModelProvisionGate",
            CancelServiceUrl = "https://boa.vakifkatilim.com.tr/VirtualPOS.Gateway/Home/SaleReversal",
            RefundServiceUrl = "https://boa.vakifkatilim.com.tr/VirtualPOS.Gateway/Home/PartialDrawBack"
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
                { "MerchantId", auth.merchantID },
                { "CustomerId", auth.merchantStorekey },
                { "UserName", auth.merchantUser },
                { "CustomerIPAddress", request.customerIPAddress },
                { "MerchantOrderId", request.orderNumber },
                { "InstallmentCount", request.saleInfo.installment > 1 ? request.saleInfo.installment : 0 },
                { "Amount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "DisplayAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "CurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "FECCurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "CardNumber", request.saleInfo.cardNumber },
                { "CardExpireDateYear", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) },
                { "CardExpireDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "CardCVV2", request.saleInfo.cardCVV },
                { "CardHolderName", request.saleInfo.cardNameSurname },
                { "PaymentType", 1 },
                { "TransactionSecurity", 1 },
            };

            string hashText = SHA1Base64(
                param["MerchantId"].ToString() +
                param["MerchantOrderId"].ToString() +
                param["Amount"].ToString() +
                param["UserName"].ToString() +
                SHA1Base64(auth.merchantPassword));

            param.Add("HashData", hashText);


            string xml = param.toXml("VPosMessageContract");

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

            string hashPassword = SHA1Base64(auth.merchantPassword);

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "OkUrl", request.payment3D.returnURL },
                { "FailUrl", request.payment3D.returnURL },
                { "MerchantId", auth.merchantID },
                { "CustomerId", auth.merchantStorekey },
                { "UserName", auth.merchantUser },
                { "HashPassword", hashPassword },
                { "MerchantOrderId", request.orderNumber },
                { "InstallmentCount", request.saleInfo.installment > 1 ? request.saleInfo.installment : 0 },
                { "Amount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "DisplayAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "") },
                { "APIVersion", "1.0.0" },
                { "CardNumber", request.saleInfo.cardNumber },
                { "CardExpireDateYear", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) },
                { "CardExpireDateMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "CardCVV2", request.saleInfo.cardCVV },
                { "CardHolderName", request.saleInfo.cardNameSurname },
                { "PaymentType", 1 },
                { "CurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "FECCurrencyCode", ((int)request.saleInfo.currency).ToString("0000") },
                { "TransactionSecurity", 3 }
            };

            string hashText = SHA1Base64(
                param["MerchantId"].ToString() +
                param["MerchantOrderId"].ToString() +
                param["Amount"].ToString() +
                param["OkUrl"].ToString() +
                param["FailUrl"].ToString() +
                param["UserName"].ToString() +
                hashPassword);

            param.Add("HashData", hashText);


            string xml = param.toXml("VPosMessageContract");

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlTest.ThreeDModelPayGateUrl : _urlLive.ThreeDModelPayGateUrl));

            //if(resp.Contains("action\""+ request.payment3D.returnURL + "\""))
            //{

            //}

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

            if (request?.responseArray?.ContainsKey("ResponseMessage") == true)
            {
                string authenticationResponse = request.responseArray["ResponseMessage"].cpToString();

                if (!string.IsNullOrWhiteSpace(authenticationResponse))
                {
                    authenticationResponse = Uri.UnescapeDataString(authenticationResponse);

                    Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(authenticationResponse, "VPosTransactionResponseContract");

                    if (respDic?.ContainsKey("ResponseCode") == true && respDic["ResponseCode"].cpToString() == "00")
                    {
                        response.orderNumber = (respDic.ContainsKey("MerchantOrderId") ? respDic["MerchantOrderId"].cpToString() : "");

                        Dictionary<string, object> vposMessageDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(respDic["VPosMessageContract"]));


                        Dictionary<string, object> param = new Dictionary<string, object>()
                        {
                            { "APIVersion", "" },
                            { "MerchantId", auth.merchantID },
                            { "CustomerId", auth.merchantStorekey },
                            { "UserName", auth.merchantUser },
                            { "TransactionType", "Sale" },
                            { "InstallmentCount", vposMessageDic["InstallmentCount"].cpToString() },
                            { "Amount", vposMessageDic["Amount"].cpToString() },
                            { "CurrencyCode", vposMessageDic["CurrencyCode"].cpToString() },
                            { "FECCurrencyCode", vposMessageDic["CurrencyCode"].cpToString() },
                            { "MerchantOrderId", response.orderNumber },
                            { "TransactionSecurity", 3 },
                            { "PaymentType", 1 },
                            { "AdditionalData", new Dictionary<string, object>()
                            {
                                { "AdditionalDataList", new Dictionary<string, object>()
                                {
                                    { "VPosAdditionalData", new Dictionary<string, object>()
                                    {
                                        { "Key", "MD" },
                                        { "Data", respDic["MD"].cpToString() }
                                    }
                                    },
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


                        string xml = param.toXml("VPosMessageContract");

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


    internal class VakifKatilimUrlModel
    {
        public string Non3DPayGateUrl { get; set; }
        public string ThreeDModelPayGateUrl { get; set; }
        public string ThreeDModelProvisionGateUrl { get; set; }
        public string CancelServiceUrl { get; set; }
        public string RefundServiceUrl { get; set; }
    }
}
