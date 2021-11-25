using CP.VPOS.Models;
using CP.VPOS.Interfaces;
using System;
using System.Collections.Generic;
using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using System.Net;
using System.Net.Http;
using System.Globalization;

namespace CP.VPOS.Banks
{
    internal abstract class NestpayVirtualPOSService : IVirtualPOSService
    {
        private static string _urlAPITest = "https://entegrasyon.asseco-see.com.tr/fim/api";
        private static string _urlAPILive = "";

        private static string _url3Dtest = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate";
        private static string _url3DLive = "";

        public NestpayVirtualPOSService(string urlAPILive, string url3DLive)
        {
            _urlAPILive = urlAPILive;
            _url3DLive = url3DLive;
        }

        public virtual SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
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
                { "Name", auth.merchantUser },
                { "Password", auth.merchantPassword },
                { "ClientId", auth.merchantID },
                { "Type", "Auth" },
                { "Total", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                { "Currency", ((int)request.saleInfo.currency).ToString() },
                { "Number", request.saleInfo.cardNumber },
                { "Expires", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) + "/" +request.saleInfo.cardExpiryDateMonth.ToString() },
                { "Cvv2Val", request.saleInfo.cardCVV },
            };

            string xml = param.toXml();

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp);

            response.privateResponse = respDic;

            if (respDic.ContainsKey("Response"))
            {
                if (respDic["Response"].cpToString() == "Error" || respDic["Response"].cpToString() == "Decline")
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = respDic.ContainsKey("ErrMsg") ? respDic["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
                }
                else if (respDic["Response"].cpToString() == "Approved")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                    response.transactionId = respDic.ContainsKey("TransId") ? respDic["TransId"].cpToString() : "";
                }
            }


            return response;
        }

        public virtual SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                privateResponse = request.responseArray,
                orderNumber = request.responseArray.ContainsKey("oid") ? request.responseArray["oid"].cpToString() : "",
                message = "İşlem sırasında bilinmeyen bir hata oluştu"
            };


            if (request.responseArray.ContainsKey("Response"))
            {
                if (request.responseArray["Response"].cpToString() == "Error" || request.responseArray["Response"].cpToString() == "Decline")
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = request.responseArray.ContainsKey("ErrMsg") ? request.responseArray["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
                }
                else if (request.responseArray["Response"].cpToString() == "Approved")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                    response.transactionId = request.responseArray.ContainsKey("TransId") ? request.responseArray["TransId"].cpToString() : "";
                }
            }


            return response;
        }

        public virtual BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return new BINInstallmentQueryResponse { confirm = false };
        }

        public virtual AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return new AllInstallmentQueryResponse { confirm = false };
        }

        public virtual AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return new AdditionalInstallmentQueryResponse { confirm = false };
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "Name", auth.merchantUser },
                { "Password", auth.merchantPassword },
                { "ClientId", auth.merchantID },
                { "Type", "Void" },
                { "TransId", request.transactionId },
            };

            string xml = param.toXml();

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp);

            response.privateResponse = respDic;

            if (respDic.ContainsKey("Response"))
            {
                if (respDic["Response"].cpToString() == "Error" || respDic["Response"].cpToString() == "Decline")
                {
                    response.statu = ResponseStatu.Error;
                    response.message = respDic.ContainsKey("ErrMsg") ? respDic["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
                }
                else if (respDic["Response"].cpToString() == "Approved")
                {
                    response.statu = ResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                }
            }

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "Name", auth.merchantUser },
                { "Password", auth.merchantPassword },
                { "ClientId", auth.merchantID },
                { "Type", "Credit" },
                { "TransId", request.transactionId },
                { "Total", request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
            };

            string xml = param.toXml();

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp);

            response.privateResponse = respDic;

            if (respDic.ContainsKey("Response"))
            {
                if (respDic["Response"].cpToString() == "Error" || respDic["Response"].cpToString() == "Decline")
                {
                    response.statu = ResponseStatu.Error;
                    response.message = respDic.ContainsKey("ErrMsg") ? respDic["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
                }
                else if (respDic["Response"].cpToString() == "Approved")
                {
                    response.statu = ResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                }
            }

            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                { "pan", request.saleInfo.cardNumber },
                { "cv2", request.saleInfo.cardCVV },
                { "Ecom_Payment_Card_ExpDate_Year", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) },
                { "Ecom_Payment_Card_ExpDate_Month", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "clientid", auth.merchantID },
                { "amount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                { "oid", request.orderNumber },
                { "okUrl", request.payment3D.returnURL },
                { "failUrl", request.payment3D.returnURL },
                { "rnd", Helpers.FoundationHelper.time().ToString()},
                { "hash", "" },
                { "storetype", "3d_pay" },
                { "lang", "tr" },
                { "currency", ((int)request.saleInfo.currency).ToString() },
                { "installment", request.saleInfo.installment.ToString() },
                { "taksit", request.saleInfo.installment.ToString() },
                { "islemtipi", "Auth" },
           
                // { "cardType", "1" }, // setlenecek
            };

            string hash = this.GetHash(param["clientid"].cpToString()
                                     + param["oid"].cpToString()
                                     + param["amount"].cpToString()
                                     + param["okUrl"].cpToString()
                                     + param["failUrl"].cpToString()
                                     + param["islemtipi"].cpToString()
                                     + param["installment"].cpToString()
                                     + param["rnd"].cpToString()
                                     + auth.merchantStorekey
                                    );

            param["hash"] = hash;

            string resp = this.Request(param, (auth.testPlatform ? _url3Dtest : _url3DLive));

            Dictionary<string, object> form = FoundationHelper.getFormParams(resp);

            response.privateResponse = form;
            response.orderNumber = request.orderNumber;

            if (form.ContainsKey("PaReq"))
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = resp;
            }
            else if (form.ContainsKey("Response") && (form["Response"].cpToString() == "Error" || form["Response"].cpToString() == "Decline"))
            {
                response.statu = SaleResponseStatu.Error;
                response.message = form.ContainsKey("ErrMsg") ? form["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "İşlem sırasında bilinmeyen bir hata oluştu";
            }

            return response;
        }

        private string GetHash(string hashstr)
        {
#pragma warning disable SYSLIB0021
            using (System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider())
            {
                byte[] inputbytes = sha.ComputeHash(System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr));

                return Convert.ToBase64String(inputbytes);
            }
#pragma warning restore SYSLIB0021
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

        private string xmlRequest(string xml, string link)
        {
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
#pragma warning disable SYSLIB0014
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(link);
#pragma warning restore SYSLIB0014
            string postdata = "DATA=" + xml.ToString();
            byte[] postdatabytes = System.Text.Encoding.UTF8.GetBytes(postdata);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
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
}
