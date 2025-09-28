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

namespace CP.VPOS.Banks.Denizbank
{
    internal class DenizbankVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://test.inter-vpos.com.tr/mpi/Default.aspx";
        private readonly string _urlAPILive = "https://inter-vpos.com.tr/mpi/Default.aspx";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ShopCode", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.saleInfo.currency).ToString() },
                {"OrderId", request.orderNumber },
                {"TxnType", "Auth" },
                {"InstallmentCount", (request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "0") },
                {"SecureType", "NonSecure" },
                {"Pan", request.saleInfo.cardNumber},
                {"Cvv2",request.saleInfo.cardCVV},
                {"Expiry", request.saleInfo.cardExpiryDateMonth.ToString("00") + request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
                {"Lang", "TR"},
            };

            string res = this.Request(req, auth);

            Dictionary<string, object> dic = new Dictionary<string, object>();

            string[] responseSplit = res.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < responseSplit.Length; i++)
            {
                string[] keyValue = responseSplit[i].Split('=');

                if (keyValue.Length == 2)
                    dic.Add(keyValue[0], keyValue[1]);
            }

            if (dic?.ContainsKey("ProcReturnCode") == true && dic["ProcReturnCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = " işlem başarıyla tamamlandı";
            }
            else
            {
                string message = "İşlem sırasında bir hata oluştu";

                if (dic?.ContainsKey("ErrorMessage") == true && dic["ErrorMessage"].cpToString() != "")
                    message = dic["ErrorMessage"].cpToString();

                response.statu = SaleResponseStatu.Error;
                response.message = message;
            }

            response.privateResponse = dic;
            response.orderNumber = request.orderNumber;

            if (dic?.ContainsKey("TransId") == true)
                response.transactionId = dic["TransId"].cpToString();

            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ShopCode", auth.merchantID },
                {"PurchAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.saleInfo.currency).ToString() },
                {"OrderId", request.orderNumber },
                {"OkUrl", request.payment3D.returnURL },
                {"FailUrl", request.payment3D.returnURL },
                {"Rnd", Guid.NewGuid().ToString().Replace("-", "")},
                {"TxnType", "Auth" },
                {"InstallmentCount", (request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "0") },
                {"SecureType", "3DPay" },
                {"Pan", request.saleInfo.cardNumber},
                {"Cvv2",request.saleInfo.cardCVV},
                {"Expiry", request.saleInfo.cardExpiryDateMonth.ToString("00") + request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
            };

            string hashText = SHA1Base64(req["ShopCode"] + req["OrderId"] + req["PurchAmount"] + req["OkUrl"] + req["FailUrl"] + req["TxnType"] + req["InstallmentCount"] + req["Rnd"] + auth.merchantStorekey);

            req.Add("Hash", hashText);

            string res = this.Request(req, auth);

            var dic = FoundationHelper.getFormParams(res);

            response.privateResponse = dic;

            if (dic?.ContainsKey("ErrorMessage") == true || dic?.ContainsKey("ErrorCode") == true)
            {
                string errorMsg = $"{(dic?.ContainsKey("ErrorCode") == true ? dic["ErrorCode"].cpToString() : "")} - {(dic?.ContainsKey("ErrorMessage") == true ? dic["ErrorMessage"].cpToString() : "")}";

                response.statu = SaleResponseStatu.Error;
                response.message = errorMsg;
            }
            else
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = res;
            }

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.privateResponse = request?.responseArray;

            if (request?.responseArray?.ContainsKey("TransId") == true)
                response.transactionId = request.responseArray["TransId"].cpToString();


            if (request?.responseArray?.ContainsKey("OrderId") == true)
                response.orderNumber = request.responseArray["OrderId"].cpToString();


            if (request?.responseArray?.ContainsKey("ProcReturnCode") == true && request.responseArray["ProcReturnCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (request?.responseArray?.ContainsKey("ErrorMessage") == true && request.responseArray["ErrorMessage"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request.responseArray["ErrorMessage"].cpToString();
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "İşlem sırasında bir hata oluştu";
            }


            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ShopCode", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"orgOrderId", request.orderNumber },
                {"TxnType", "Void" },
                {"SecureType", "NonSecure" },
                {"Lang", "TR"},
            };

            string res = this.Request(req, auth);

            Dictionary<string, object> dic = new Dictionary<string, object>();

            string[] responseSplit = res.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < responseSplit.Length; i++)
            {
                string[] keyValue = responseSplit[i].Split('=');

                if (keyValue.Length == 2)
                    dic.Add(keyValue[0], keyValue[1]);
            }

            response.privateResponse = dic;

            if (dic?.ContainsKey("ProcReturnCode") == true && dic["ProcReturnCode"].cpToString() == "00")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (dic?.ContainsKey("ErrorMessage") == true && dic["ErrorMessage"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["ErrorMessage"].cpToString();
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = "İşlem iptal edilemedi";
            }


            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ShopCode", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.currency).ToString() },
                {"orgOrderId", request.orderNumber },
                {"TxnType", "Refund" },
                {"SecureType", "NonSecure" },
                {"Lang", "TR"},
            };

            string res = this.Request(req, auth);

            Dictionary<string, object> dic = new Dictionary<string, object>();

            string[] responseSplit = res.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < responseSplit.Length; i++)
            {
                string[] keyValue = responseSplit[i].Split('=');

                if (keyValue.Length == 2)
                    dic.Add(keyValue[0], keyValue[1]);
            }

            response.privateResponse = dic;

            if (dic?.ContainsKey("ProcReturnCode") == true && dic["ProcReturnCode"].cpToString() == "00")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
                response.refundAmount = request.refundAmount;
            }
            else if (dic?.ContainsKey("ErrorMessage") == true && dic["ErrorMessage"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["ErrorMessage"].cpToString();
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = "İşlem iade edilemedi";
            }


            return response;
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

        private string Request(Dictionary<string, string> param, VirtualPOSAuth auth, string link = null)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var req = new FormUrlEncodedContent(param))
            {
                req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = Encoding.UTF8.WebName };
                var response = client.PostAsync(link ?? (auth.testPlatform ? _urlAPITest : _urlAPILive), req).Result;
                byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                responseString = Encoding.UTF8.GetString(responseByte);
            }

            return responseString;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
