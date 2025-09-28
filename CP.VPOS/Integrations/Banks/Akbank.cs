using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;

namespace CP.VPOS.Banks.Akbank
{

    internal class AkbankVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://apipre.akbank.com/api/v1/payment/virtualpos/transaction/process";
        private readonly string _urlAPILive = "https://api.akbank.com/api/v1/payment/virtualpos/transaction/process";

        private readonly string _url3DTest = "https://virtualpospaymentgatewaypre.akbank.com/securepay";
        private readonly string _url3DLive = "https://virtualpospaymentgateway.akbank.com/securepay";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;

            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            response.orderNumber = request.orderNumber;

            string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"version", "1.00" },
                {"txnCode", "1000" },
                {"requestDateTime", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") },
                {"randomNumber", getRandomNumberBase16(128) },
                {"terminal", new Dictionary<string, string>{
                    {"merchantSafeId", auth.merchantUser },
                    {"terminalSafeId", auth.merchantPassword },
                    }
                },
                {"order", new Dictionary<string, string>{
                    {"orderId", request.orderNumber },
                }
                },
                {"card", new Dictionary<string, string>{
                    {"cardHolderName", request.saleInfo.cardNameSurname },
                    {"cardNumber", request.saleInfo.cardNumber },
                    {"cvv2", request.saleInfo.cardCVV },
                    {"expireDate", $"{request.saleInfo.cardExpiryDateMonth.ToString("00")}{request.saleInfo.cardExpiryDateYear.ToString().Substring(2)}" },
                }
                },
                {"transaction", new Dictionary<string, object>{
                    {"amount", totalStr },
                    {"currencyCode", (int)request.saleInfo.currency },
                    {"motoInd", 0 },
                    {"installCount", request.saleInfo.installment },
                }
                },
                {"customer", new Dictionary<string, string>{
                    {"emailAddress", string.IsNullOrWhiteSpace(request?.invoiceInfo?.emailAddress) ? "test@test.com" : request.invoiceInfo.emailAddress },
                    {"ipAddress", request.customerIPAddress },
                }
                },
            };

            string link = auth.testPlatform ? _urlAPITest : _urlAPILive;

            string responseStr = Request(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("responseCode") == true && responseDic["responseCode"].cpToString() == "VPS-0000")
            {
                string transactionId = "";

                try
                {
                    if (responseDic.ContainsKey("transaction"))
                    {
                        var dataObj = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["transaction"]));

                        if (dataObj?.ContainsKey("authCode") == true)
                            transactionId = dataObj["authCode"].cpToString();
                    }
                }
                catch { }


                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
                response.transactionId = transactionId;

                return response;
            }

            string errorMsg = "İşlem sırasında bir hata oluştu";

            if (responseDic?.ContainsKey("responseMessage") == true && responseDic["responseMessage"].cpToString() != "")
                errorMsg = responseDic["responseMessage"].cpToString();
            else if (responseDic?.ContainsKey("code") == true && responseDic["code"].cpToString() == "401")
                errorMsg = "Sanal pos üye işyeri bilgilerinizi kontrol ediniz";


            response.statu = SaleResponseStatu.Error;
            response.message = errorMsg;

            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

            string emailAddress = string.IsNullOrWhiteSpace(request?.invoiceInfo?.emailAddress) ? "test@test.com" : request.invoiceInfo.emailAddress;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"paymentModel", "3D" },
                {"txnCode", "3000" },
                {"merchantSafeId", auth.merchantUser },
                {"terminalSafeId", auth.merchantPassword },
                {"orderId", request.orderNumber },
                {"lang", "TR" },
                {"amount", totalStr },
                {"currencyCode", ((int)request.saleInfo.currency).ToString() },
                {"installCount", request.saleInfo.installment.ToString() },
                {"okUrl", request.payment3D.returnURL },
                {"failUrl", request.payment3D.returnURL },
                {"emailAddress", emailAddress },
                {"creditCard", request.saleInfo.cardNumber },
                {"expiredDate", $"{request.saleInfo.cardExpiryDateMonth.ToString("00")}{request.saleInfo.cardExpiryDateYear.ToString().Substring(2)}" },
                {"cvv", request.saleInfo.cardCVV },
                //{"cardholderName", request.saleInfo.cardNameSurname },
                {"randomNumber", getRandomNumberBase16(128) },
                {"requestDateTime", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") },
                {"hash", "" }
            };


            string hashItems = req["paymentModel"].ToString() +
                               req["txnCode"].ToString() +
                               req["merchantSafeId"].ToString() +
                               req["terminalSafeId"].ToString() +
                               req["orderId"].ToString() +
                               req["lang"].ToString() +
                               req["amount"].ToString() +
                               req["currencyCode"].ToString() +
                               req["installCount"].ToString() +
                               req["okUrl"].ToString() +
                               req["failUrl"].ToString() +
                               req["emailAddress"].ToString() +
                               req["creditCard"].ToString() +
                               req["expiredDate"].ToString() +
                               req["cvv"].ToString() +
                               //req["cardholderName"].ToString() +
                               req["randomNumber"].ToString() +
                               req["requestDateTime"].ToString();

            string hash = hashToString(hashItems, Encoding.UTF8.GetBytes(auth.merchantStorekey));

            req["hash"] = hash;


            string link = auth.testPlatform ? _url3DTest : _url3DLive;

            string responseStr = RequestForm(req, link);

            response.privateResponse = new Dictionary<string, object>
            {
                {"stringResponse", responseStr },
            };

            if (responseStr.Contains($"action=\"{req["failUrl"].ToString()}\""))
            {
                Dictionary<string, object> form = Helpers.FoundationHelper.getFormParams(responseStr);

                if (form?.ContainsKey("responseMessage") == true && form["responseMessage"].cpToString() != "")
                {
                    string errorMsg = form["responseMessage"].cpToString();

                    response.statu = SaleResponseStatu.Error;
                    response.message = errorMsg;

                    return response;
                }
            }

            response.statu = SaleResponseStatu.RedirectHTML;
            response.message = responseStr;

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.privateResponse = new Dictionary<string, object>();

            response.privateResponse.Add("response_1", request.responseArray);

            if (request?.responseArray?.ContainsKey("orderId") == true && request.responseArray["orderId"].cpToString() != "")
                response.orderNumber = request.responseArray["orderId"].cpToString();

            if (request?.responseArray?.ContainsKey("responseCode") == true && request.responseArray["responseCode"].cpToString() == "VPS-0000" && request?.responseArray?.ContainsKey("mdStatus") == true && request.responseArray["mdStatus"].cpToString() == "1")
            {
                Dictionary<string, object> req = new Dictionary<string, object> {
                    {"version", "1.00" },
                    {"txnCode", "1000" },
                    {"requestDateTime", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") },
                    {"randomNumber", getRandomNumberBase16(128) },
                    {"terminal", new Dictionary<string, string>{
                        {"merchantSafeId", auth.merchantUser },
                        {"terminalSafeId", auth.merchantPassword },
                        }
                    },
                    {"order", new Dictionary<string, string>{
                        {"orderId", request.responseArray["orderId"].cpToString() },
                    }
                    },
                    {"transaction", new Dictionary<string, object>{
                        {"amount", request.responseArray["amount"].cpToString() },
                        {"currencyCode", (int)request.currency },
                        {"motoInd", 0 },
                        {"installCount", request.responseArray["installCount"].cpToInt() },
                    }
                    },
                    {"secureTransaction", new Dictionary<string, string>{
                        {"secureId", request.responseArray["secureId"].cpToString() },
                        {"secureEcomInd", request.responseArray["secureEcomInd"].cpToString() },
                        {"secureData", request.responseArray["secureData"].cpToString() },
                        {"secureMd", request.responseArray["secureMd"].cpToString() },
                    }
                    },
                };

                string link = auth.testPlatform ? _urlAPITest : _urlAPILive;

                string responseStr = Request(req, link, auth);

                Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

                response.privateResponse.Add("response_2", responseDic);

                if (responseDic?.ContainsKey("responseCode") == true && responseDic["responseCode"].cpToString() == "VPS-0000")
                {
                    string transactionId = "";

                    try
                    {
                        if (responseDic.ContainsKey("transaction"))
                        {
                            var dataObj = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["transaction"]));

                            if (dataObj?.ContainsKey("authCode") == true)
                                transactionId = dataObj["authCode"].cpToString();
                        }
                    }
                    catch { }


                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                    response.transactionId = transactionId;

                    return response;
                }

                string errorMsg = "İşlem sırasında bir hata oluştu";

                if (responseDic?.ContainsKey("responseMessage") == true && responseDic["responseMessage"].cpToString() != "")
                    errorMsg = responseDic["responseMessage"].cpToString();
                else if (responseDic?.ContainsKey("code") == true && responseDic["code"].cpToString() == "401")
                    errorMsg = "Sanal pos üye işyeri bilgilerinizi kontrol ediniz";


                response.statu = SaleResponseStatu.Error;
                response.message = errorMsg;

                return response;

            }
            else if (request?.responseArray?.ContainsKey("responseMessage") == true && request.responseArray["responseMessage"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request.responseArray["responseMessage"].cpToString();
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "3D doğrulaması başarısız";
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"version", "1.00" },
                {"txnCode", "1003" },
                {"requestDateTime", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") },
                {"randomNumber", getRandomNumberBase16(128) },
                {"terminal", new Dictionary<string, string>{
                    {"merchantSafeId", auth.merchantUser },
                    {"terminalSafeId", auth.merchantPassword },
                    }
                },
                {"order", new Dictionary<string, string>{
                    {"orderId", request.orderNumber },
                }
                },
                {"customer", new Dictionary<string, string>{
                    {"ipAddress", request.customerIPAddress },
                }
                },
            };


            string link = auth.testPlatform ? _urlAPITest : _urlAPILive;

            string responseStr = Request(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;


            if (responseDic?.ContainsKey("responseCode") == true && responseDic["responseCode"].cpToString() == "VPS-0000")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (responseDic?.ContainsKey("responseMessage") == true && responseDic["responseMessage"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["responseMessage"].cpToString();
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = "İşlem iade edilemedi";
            }

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            string totalStr = request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"version", "1.00" },
                {"txnCode", "1002" },
                {"requestDateTime", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") },
                {"randomNumber", getRandomNumberBase16(128) },
                {"terminal", new Dictionary<string, string>{
                    {"merchantSafeId", auth.merchantUser },
                    {"terminalSafeId", auth.merchantPassword },
                    }
                },
                {"order", new Dictionary<string, string>{
                    {"orderId", request.orderNumber },
                }
                },
                {"customer", new Dictionary<string, string>{
                    {"ipAddress", request.customerIPAddress },
                }
                },
                {"transaction", new Dictionary<string, object>{
                    {"amount", totalStr },
                    {"currencyCode", (int)request.currency },
                }
                },
            };

            string link = auth.testPlatform ? _urlAPITest : _urlAPILive;

            string responseStr = Request(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;


            if (responseDic?.ContainsKey("responseCode") == true && responseDic["responseCode"].cpToString() == "VPS-0000")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
                response.refundAmount = request.refundAmount;
            }
            else if (responseDic?.ContainsKey("responseMessage") == true && responseDic["responseMessage"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["responseMessage"].cpToString();
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
            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse { confirm = false, installmentList = new List<AllInstallment>() };

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse();

            return response;
        }

        private string Request(Dictionary<string, object> param, string link, VirtualPOSAuth auth)
        {
            string responseString = "";

            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.ServicePointManager.Expect100Continue = false;

                string jsonContent = JsonConvertHelper.Json(param);

                using (HttpClient client = new HttpClient())
                using (var req = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                {
                    req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };

                    string hash = hashToString(jsonContent, Encoding.UTF8.GetBytes(auth.merchantStorekey));

                    req.Headers.Add("auth-hash", hash);

                    var response = client.PostAsync(link, req).Result;
                    byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                    responseString = Encoding.UTF8.GetString(responseByte);
                }
            }
            catch { }

            return responseString;
        }

        private string RequestForm(Dictionary<string, string> param, string link)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var req = new FormUrlEncodedContent(param))
            {
                req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = Encoding.UTF8.WebName };
                var response = client.PostAsync(link, req).Result;
                byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                responseString = Encoding.UTF8.GetString(responseByte);
            }

            return responseString;
        }

        private string hashToString(string message, byte[] key)
        {
            using (var sha512 = new HMACSHA512(key))
            {
                byte[] b = sha512.ComputeHash(Encoding.UTF8.GetBytes(message));

                return Convert.ToBase64String(b);
            }
        }

        private bool checkResponseHash(Dictionary<string, string> requestMap, string secretKey)
        {
            string[] parameters = requestMap["hashParams"].Split('+');
            StringBuilder builder = new StringBuilder();
            foreach (string param in parameters)
            {
                builder.Append(requestMap[param]);
            }



            string hash = hashToString(builder.ToString(), Encoding.UTF8.GetBytes(secretKey));
            Console.WriteLine(hash);
            return requestMap["hash"] == hash;
        }

        private string getRandomNumberBase16(int length)
        {
#pragma warning disable SYSLIB0023
            byte[] data = new byte[length];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append((b % 16).ToString("X"));
            }
            return result.ToString();
#pragma warning restore SYSLIB0023
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
