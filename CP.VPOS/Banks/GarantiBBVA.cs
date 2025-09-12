using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace CP.VPOS.Banks.GarantiBBVA
{
    internal class GarantiBBVAVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://sanalposprovtest.garantibbva.com.tr/VPServlet";
        private readonly string _urlAPILive = "https://sanalposprov.garanti.com.tr/VPServlet";

        private readonly string _url3Dtest = "https://sanalposprovtest.garantibbva.com.tr/servlet/gt3dengine";
        private readonly string _url3DLive = "https://sanalposprov.garanti.com.tr/servlet/gt3dengine";

        /*
            test ortamı raporlama için:
            https://sanalposwebtest.garanti.com.tr
 
            işyerino 	: 7000679
            Kullanıcı Adı   : GARANTI
            Parola    	: destek
            Şifre  		: 123456WqE
         */


        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request.payment3D?.confirm == true)
                return Sale3D(request, auth);

            SaleResponse response = null;

            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {
                    "Mode", auth.testPlatform ? "TEST" : "PROD"
                },
                {
                    "Version", "v0.00"
                },
                {
                    "Terminal", new Dictionary<string, object>()
                    {
                        { "ProvUserID", "PROVAUT" },
                        { "HashData", "" },
                        { "MerchantID", auth.merchantID },
                        { "UserID", "PROVAUT" },
                        { "ID", auth.merchantUser },
                    }
                },
                {
                    "Customer", new Dictionary<string, object>()
                    {
                        { "IPAddress", request.customerIPAddress },
                        { "EmailAddress", request.invoiceInfo.emailAddress },
                    }
                },
                {
                    "Card", new Dictionary<string, object>()
                    {
                        { "Number", request.saleInfo.cardNumber },
                        { "ExpireDate", $"{request.saleInfo.cardExpiryDateMonth.ToString("00")}{request.saleInfo.cardExpiryDateYear.ToString().Substring(2)}" },
                        { "CVV2", request.saleInfo.cardCVV },
                    }
                },
                {
                    "Order", new Dictionary<string, object>()
                    {
                        { "OrderID", request.orderNumber },
                        { "GroupID", "" },
                    }
                },
                {
                    "Transaction", new Dictionary<string, object>()
                    {
                        { "Type", "sales" },
                        {"InstallmentCnt", request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte?)null },
                        {"Amount", request.saleInfo.amount.To2Digit() },
                        {"CurrencyCode", (int)request.saleInfo.currency },
                        {"CardholderPresentCode", request.payment3D?.confirm == true ? 13 : 0 },
                        {"MotoInd", "N" }
                    }
                }
            };

            string hashedPassword = GetSHA1(auth.merchantPassword + auth.merchantUser.cpToLong().ToString("000000000")).ToUpper();
            string hash = GetSHA1(request.orderNumber +
                                  auth.merchantUser +
                                  request.saleInfo.cardNumber +
                                  request.saleInfo.amount.To2Digit() +
                                  hashedPassword).ToUpper();

            ((Dictionary<string, object>)param["Terminal"])["HashData"] = hash;

            string xml = param.toXml("GVPSRequest", "utf-8");

            string resp = SendHttpRequest(auth.testPlatform ? _urlAPITest : _urlAPILive, "POST", xml);

            var dic = FoundationHelper.XmltoDictionary(resp, "GVPSResponse");

            if (dic == null)
                dic = new Dictionary<string, object>();

            dic.Add("originalResponseXML", resp);

            if (dic.ContainsKey("Transaction") && ((Dictionary<string, object>)dic["Transaction"]).ContainsKey("Response") && ((Dictionary<string, object>)((Dictionary<string, object>)dic["Transaction"])["Response"]).ContainsKey("Code"))
            {
                Dictionary<string, object> dicResponse = ((Dictionary<string, object>)((Dictionary<string, object>)dic["Transaction"])["Response"]);

                if (dicResponse["Code"].ToString() == "00")
                {
                    response = new SaleResponse
                    {
                        statu = SaleResponseStatu.Success,
                        message = "İşlem başarılı",
                        privateResponse = dic,
                        orderNumber = request.orderNumber,
                        transactionId = ((Dictionary<string, object>)dic["Transaction"])["RetrefNum"].ToString()
                    };

                }
                else
                {
                    response = new SaleResponse
                    {
                        statu = SaleResponseStatu.Error,
                        message = dicResponse["ErrorMsg"].ToString(),
                        privateResponse = dic,
                        orderNumber = request.orderNumber,
                        transactionId = ""
                    };
                }
            }
            else
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = "İşlem sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    privateResponse = dic,
                    orderNumber = request.orderNumber,
                    transactionId = ""
                };
            }

            return response;
        }

        public SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = null;

            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "mode", auth.testPlatform ? "TEST" : "PROD"},
                { "apiversion", "v0.01" },
                { "version", "v0.01" },
                { "secure3dsecuritylevel", "3D" },

                { "terminalprovuserid", "PROVAUT" },
                { "terminaluserid", "PROVAUT" },
                { "terminalmerchantid", auth.merchantID },
                { "terminalid", auth.merchantUser },

                { "txntype", "sales" },
                { "txnamount", request.saleInfo.amount.To2Digit() },
                { "txncurrencycode", ((int)request.saleInfo.currency).ToString() },
                { "txninstallmentcount", request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "" },


                { "customeripaddress", request.customerIPAddress },
                { "customeremailaddress", request.invoiceInfo.emailAddress },
                { "orderid", request.orderNumber },

                { "cardnumber", request.saleInfo.cardNumber },
                { "cardexpiredatemonth",  request.saleInfo.cardExpiryDateMonth.ToString("00")},
                { "cardexpiredateyear",  request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
                { "cardcvv2", request.saleInfo.cardCVV },


                { "successurl", request.payment3D.returnURL },
                { "errorurl", request.payment3D.returnURL },
                { "secure3dhash", "" }
            };


            string hashedPassword = GetSHA1(auth.merchantPassword + auth.merchantUser.cpToLong().ToString("000000000")).ToUpper();
            string hash = GetSHA1(auth.merchantUser +
                                  request.orderNumber +
                                  request.saleInfo.amount.To2Digit() +
                                  request.payment3D.returnURL +
                                  request.payment3D.returnURL +
                                  param["txntype"].ToString() +
                                  param["txninstallmentcount"].ToString() +
                                  auth.merchantStorekey +
                                  hashedPassword).ToUpper();

            param["secure3dhash"] = hash;


            string resp = this.Request(param, auth.testPlatform ? _url3Dtest : _url3DLive);

            string _orginalResp = resp;

            resp = resp.Replace(" value =\"", " value=\"");


            Dictionary<string, object> respdic = new Dictionary<string, object>();

            respdic.Add("originalResponseHTML", _orginalResp);

            Dictionary<string, object> dic = FoundationHelper.getFormParams(resp);

            foreach (var item in dic)
                respdic[item.Key] = item.Value.ToString().Split('"')[0];

            if (respdic == null)
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = "İşlem sırasında hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    orderNumber = request.orderNumber,
                    privateResponse = respdic

                };
            }
            else if (respdic.ContainsKey("response") && respdic["response"].ToString().ToLower() == "error")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = "İşlem sırasında hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    orderNumber = request.orderNumber,
                    privateResponse = respdic
                };

                if (respdic.ContainsKey("errmsg"))
                    response.message = respdic["errmsg"].ToString();
            }
            else if (resp.Contains($"action=\"{request.payment3D.returnURL}\""))
            {
                return Sale3DResponse(new Sale3DResponseRequest
                {
                    currency = request.saleInfo.currency,
                    responseArray = respdic
                }, auth);
            }
            else if (respdic.ContainsKey("TermUrl") && respdic.ContainsKey("MD") && respdic.ContainsKey("PaReq"))
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.RedirectHTML,
                    message = resp,
                    orderNumber = request.orderNumber,
                    privateResponse = respdic
                };
            }
            else if (resp.Contains("<form ") && resp.Contains("action="))
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.RedirectHTML,
                    message = resp,
                    orderNumber = request.orderNumber,
                    privateResponse = respdic
                };
            }
            else
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = "İşlem sırasında hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    orderNumber = request.orderNumber,
                    privateResponse = respdic
                };
            }

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = null;

            if (request.responseArray.ContainsKey("mdstatus") == false || request.responseArray["mdstatus"].ToString() != "1")
            {
                string message = "3-D Secure doğrulanamadı";


                switch (request.responseArray["mdstatus"].ToString())
                {
                    case "0": message = "3-D doğrulama başarısız"; break;
                    case "1": message = "Doğrulama başarılı, işleme devam edebilirsiniz"; break;
                    case "2": message = "Kart sahibi veya bankası sisteme kayıtlı değil"; break;
                    case "3": message = "Kartın bankası sisteme kayıtlı değil"; break;
                    case "4": message = "Doğrulama denemesi, kart sahibi sisteme daha sonra kayıt olmayı seçmiş"; break;
                    case "5": message = "Doğrulama yapılamıyor"; break;
                    case "6": message = "3-D Secure hatası"; break;
                    case "7": message = "Sistem hatası"; break;
                    case "8": message = "Bilinmeyen kart no"; break;
                    case "9": message = "Üye İşyeri 3D-Secure sistemine kayıtlı değil (Bankada işyeri ve terminal numarası 3d olarak tanımlı değil)"; break;
                    default:
                        break;
                }



                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = message,
                    orderNumber = request.responseArray.ContainsKey("oid") ? request.responseArray["oid"].ToString() : "",
                    privateResponse = request.responseArray,
                    transactionId = null
                };

                return response;
            }



            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {
                    "Mode", auth.testPlatform ? "TEST" : "PROD"
                },
                {
                    "Version", "v0.00"
                },
                {
                    "Terminal", new Dictionary<string, object>()
                    {
                        { "ProvUserID", "PROVAUT" },
                        { "HashData", "" },
                        { "MerchantID", auth.merchantID },
                        { "UserID", "PROVAUT" },
                        { "ID", auth.merchantUser },
                    }
                },
                {
                    "Customer", new Dictionary<string, object>()
                    {
                        { "IPAddress", request.responseArray["customeripaddress"].ToString() },
                        { "EmailAddress", request.responseArray["customeremailaddress"].ToString() },
                    }
                },
                {
                    "Card", new Dictionary<string, object>()
                    {
                        { "Number", "" },
                        { "ExpireDate","" },
                        { "CVV2", "" },
                    }
                },
                {
                    "Order", new Dictionary<string, object>()
                    {
                        { "OrderID", request.responseArray["oid"].ToString() },
                        { "GroupID", "" },
                        { "Description", "" },
                    }
                },
                {
                    "Transaction", new Dictionary<string, object>()
                    {
                        { "Type", "sales" },
                        {"InstallmentCnt",  request.responseArray["txninstallmentcount"].ToString()},
                        {"Amount", request.responseArray["txnamount"].ToString()},
                        {"CurrencyCode",  request.responseArray["txncurrencycode"].ToString() },
                        {"CardholderPresentCode", 13 },
                        {"MotoInd", "N" },
                        {
                            "Secure3D", new Dictionary<string, object>()
                            {
                                {"AuthenticationCode", request.responseArray["cavv"].ToString()},
                                {"SecurityLevel", request.responseArray["eci"].ToString()},
                                {"TxnID", request.responseArray["xid"].ToString()},
                                {"Md",request.responseArray["md"].ToString()}
                            }
                        }
                    }
                },

            };


            string hashedPassword = GetSHA1(auth.merchantPassword + auth.merchantUser.cpToLong().ToString("000000000")).ToUpper();

            string hash = GetSHA1(request.responseArray["oid"].ToString() +
                                  auth.merchantUser +
                                  request.responseArray["txnamount"].ToString() +
                                  hashedPassword).ToUpper();

            ((Dictionary<string, object>)param["Terminal"])["HashData"] = hash;

            string xml = param.toXml("GVPSRequest", "utf-8");

            string resp = SendHttpRequest(auth.testPlatform ? _urlAPITest : _urlAPILive, "POST", xml);

            var dic = FoundationHelper.XmltoDictionary(resp, "GVPSResponse");

            if (dic.ContainsKey("Transaction") && ((Dictionary<string, object>)dic["Transaction"]).ContainsKey("Response") && ((Dictionary<string, object>)((Dictionary<string, object>)dic["Transaction"])["Response"]).ContainsKey("Code"))
            {
                Dictionary<string, object> dicResponse = ((Dictionary<string, object>)((Dictionary<string, object>)dic["Transaction"])["Response"]);

                if (dicResponse["Code"].ToString() == "00")
                {
                    response = new SaleResponse
                    {
                        statu = SaleResponseStatu.Success,
                        message = "İşlem başarılı",
                        privateResponse = dic,
                        orderNumber = request.responseArray["oid"].ToString(),
                        transactionId = ((Dictionary<string, object>)dic["Transaction"])["RetrefNum"].ToString()
                    };

                }
                else
                {
                    response = new SaleResponse
                    {
                        statu = SaleResponseStatu.Error,
                        message = dicResponse["ErrorMsg"].ToString(),
                        privateResponse = dic,
                        orderNumber = request.responseArray["oid"].ToString(),
                        transactionId = ""
                    };
                }
            }
            else
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = "İşlem sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    privateResponse = dic,
                    orderNumber = request.responseArray["oid"].ToString(),
                    transactionId = ""
                };
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

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            return new CancelResponse { statu = ResponseStatu.Error, message = "Bu banka için iptal metodu tanımlanmamış!" };
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            return new RefundResponse { statu = ResponseStatu.Error, message = "Bu banka için iptal metodu tanımlanmamış!" };
        }

        private string GetSHA1(string SHA1Data)
        {
#pragma warning disable SYSLIB0021
            using (SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider())
            {
                string HashedPassword = SHA1Data;

                byte[] hashbytes = Encoding.UTF8.GetBytes(HashedPassword);
                byte[] inputbytes = sha.ComputeHash(hashbytes);


                return GetHexaDecimal(inputbytes);
            }
#pragma warning restore SYSLIB0021
        }

        private string GetHexaDecimal(byte[] bytes)
        {
            var s = new StringBuilder();
            var length = bytes.Length;

            for (int n = 0; n <= length - 1; n++)
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));

            return s.ToString();
        }

        private string Request(Dictionary<string, string> param, string link)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(param);
                request.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                request.Headers.ContentType.CharSet = "utf-8";
                var response = client.PostAsync(link, request).Result;
                responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }

        private string SendHttpRequest(string Host, string Method, string Params)
        {
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            var returnSrting = String.Empty;

#pragma warning disable SYSLIB0014
            var request = (HttpWebRequest)WebRequest.Create(Host);
#pragma warning restore SYSLIB0014
            request.Timeout = 30000;
            request.Method = Method;

            var bytes = new ASCIIEncoding().GetBytes(Params);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Params.Length;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                returnSrting = sr.ReadToEnd();
            }

            return returnSrting;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }


    static class helper
    {
        public static string To2Digit(this decimal amount)
        {
            return amount.ToString("N2", CultureInfo.GetCultureInfo("en-US")).Replace(",", "").Replace(".", "");
        }
    }
}
