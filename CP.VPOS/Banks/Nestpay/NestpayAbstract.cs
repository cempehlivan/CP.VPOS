using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace CP.VPOS.Banks
{
    internal abstract class NestpayVirtualPOSService : IVirtualPOSService
    {
        private static string _urlAPITest = "https://entegrasyon.asseco-see.com.tr/fim/api";
        private static string _urlAPILive = "";

        private static string _url3Dtest = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate";
        private static string _url3DLive = "";

        public NestpayVirtualPOSService(string urlAPILive, string url3DLive, string urlAPITest = "", string url3DTest = "")
        {
            _urlAPILive = urlAPILive;
            _url3DLive = url3DLive;

            if (!string.IsNullOrWhiteSpace(urlAPITest))
                _urlAPITest = urlAPITest;

            if (!string.IsNullOrWhiteSpace(url3DTest))
                _url3Dtest = url3DTest;
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
                { "OrderId", request.orderNumber },
                { "Total", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                { "Currency", ((int)request.saleInfo.currency).ToString() },
                { "Number", request.saleInfo.cardNumber },
                { "Expires", request.saleInfo.cardExpiryDateMonth.ToString("00") + "/" +request.saleInfo.cardExpiryDateYear.ToString() },
                { "Cvv2Val", request.saleInfo.cardCVV },
            };

            string xml = param.toXml();

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp);

            response.privateResponse = respDic;

            if (respDic.ContainsKey("Response"))
            {
                if (respDic["Response"].cpToString() == "Error" || respDic["Response"].cpToString() == "Decline" || respDic["Response"].cpToString() == "Declined")
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
                if (request.responseArray["Response"].cpToString() == "Approved")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                    response.transactionId = request.responseArray.ContainsKey("TransId") ? request.responseArray["TransId"].cpToString() : "";
                }
                else
                {
                    string errMsg = "İşlem sırasında bir hata oluştu.";

                    if (request.responseArray.ContainsKey("ErrMsg") && string.IsNullOrWhiteSpace(request.responseArray["ErrMsg"].cpToString()) == false)
                        errMsg = request.responseArray["ErrMsg"].cpToString();
                    else if (request.responseArray.ContainsKey("mdStatus") && request.responseArray["mdStatus"].cpToString() == "0")
                        errMsg = "3D doğrulaması başarısız.";

                    response.statu = SaleResponseStatu.Error;
                    response.message = errMsg;
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

        public virtual SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            SaleQueryResponse response = new SaleQueryResponse
            {
                statu = SaleQueryResponseStatu.NotFound,
                message = "Sipariş bulunamadı",
                orderNumber = request.orderNumber,
            };

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "Name", auth.merchantUser },
                { "Password", auth.merchantPassword },
                { "ClientId", auth.merchantID },
                { "OrderId", request.orderNumber },
                { "Extra", new Dictionary<string, object>{ { "ORDERSTATUS", "QUERY" }  } }
            };

            string xml = param.toXml(rootTag: "CC5Request");

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "CC5Response");

            response.privateResponse = respDic;

            if (respDic.ContainsKey("Response"))
            {
                if (respDic["Response"].cpToString() == "Approved")
                {
                    response.statu = SaleQueryResponseStatu.Found;
                    response.message = "İşlem bulundu";
                    response.transactionId = respDic.ContainsKey("TransId") ? respDic["TransId"].cpToString() : "";
                }
                else
                {
                    response.statu = SaleQueryResponseStatu.Error;
                    response.message = respDic.ContainsKey("ErrMsg") ? respDic["ErrMsg"].cpToString() : "Sipariş bulunamadı";
                }
            }

            if (respDic.ContainsKey("Extra") && respDic["Extra"] != null)
            {
                Dictionary<string, object> extraRespDic = respDic["Extra"] as Dictionary<string, object>;

                if (extraRespDic?.ContainsKey("CAPTURE_AMT") == true)
                    response.amount = extraRespDic["CAPTURE_AMT"].cpToString().cpToFlatStringParseDecimal();

                if (extraRespDic?.ContainsKey("CAPTURE_DTTM") == true)
                {
                    string transactionDateStr = extraRespDic["CAPTURE_DTTM"].cpToString();

                    int dotIndex = transactionDateStr.IndexOf('.');

                    if (dotIndex > 0)
                        transactionDateStr = transactionDateStr.Substring(0, dotIndex);

                    if (DateTime.TryParseExact(transactionDateStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                        response.transactionDate = result;
                }

                if (extraRespDic?.ContainsKey("TRANS_STAT") == true)
                {
                    string transStat = extraRespDic["TRANS_STAT"].cpToString();

                    if (transStat == "S")
                        response.transactionStatu = SaleQueryTransactionStatu.Paid;
                    else if (transStat == "V")
                        response.transactionStatu = SaleQueryTransactionStatu.Voided;
                }
            }

            return response;
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

            string installment = request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "";

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
                { "storetype", "3d_pay" },
                { "lang", "tr" },
                { "currency", ((int)request.saleInfo.currency).ToString() },
                { "installment", installment },
                { "taksit", installment },
                { "islemtipi", "Auth" },
                { "hashAlgorithm", "ver3" }
            };

            string hash = string.Join("|", param.OrderBy(s => s.Key).Select(s => s.Value.Replace("|", "\\|").Replace("\\", "\\\\"))) + "|" + auth.merchantStorekey;

            hash = this.GetHash(hash);

            param.Add("hash", hash);

            string resp = this.Request(param, (auth.testPlatform ? _url3Dtest : _url3DLive));

            Dictionary<string, object> form = FoundationHelper.getFormParams(resp);

            response.privateResponse = form;
            response.orderNumber = request.orderNumber;

            if (form?.ContainsKey("Response") == true && (form["Response"].cpToString() == "Error" || form["Response"].cpToString() == "Decline"))
            {
                response.statu = SaleResponseStatu.Error;
                response.message = form.ContainsKey("ErrMsg") ? form["ErrMsg"].cpToString() : "İşlem sırasında bir hata oluştu.";
            }
            else
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = resp;
            }

            return response;
        }

        private string GetHash(string hashstr)
        {
            using (var sha = SHA512.Create())
            {
                byte[] inputbytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashstr));

                return Convert.ToBase64String(inputbytes);
            }
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
