using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace CP.VPOS.Banks.QNBFinansbank
{
    internal class QNBFinansbankVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://vpostest.qnbfinansbank.com/Gateway/Default.aspx";
        private readonly string _urlAPILive = "https://vpos.qnbfinansbank.com/Gateway/Default.aspx";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            response.orderNumber = request.orderNumber;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"MbrId", "5" }, //TODO: Değişkenlik gösterebilir
                {"MerchantId", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.saleInfo.currency).ToString() },
                {"OrderId", request.orderNumber },
                {"InstallmentCount", (request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "0") },
                {"TxnType", "Auth" },
                {"SecureType", "NonSecure" },
                {"Pan", request.saleInfo.cardNumber},
                {"Expiry", request.saleInfo.cardExpiryDateMonth.ToString("00") + request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
                {"Cvv2",request.saleInfo.cardCVV},
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

                if (dic?.ContainsKey("ErrMsg") == true && dic["ErrMsg"].cpToString() != "")
                    message = dic["ErrMsg"].cpToString();

                response.statu = SaleResponseStatu.Error;
                response.message = message;
            }

            response.privateResponse = dic;
            response.orderNumber = request.orderNumber;

            if (dic?.ContainsKey("AuthCode") == true)
                response.transactionId = dic["AuthCode"].cpToString();



            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"MbrId", "5" }, //TODO: Değişkenlik gösterebilir
                {"MerchantId", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.saleInfo.currency).ToString() },
                {"OrderId", request.orderNumber },
                {"OkUrl", request.payment3D.returnURL },
                {"FailUrl", request.payment3D.returnURL },
                {"TxnType", "Auth" },
                {"InstallmentCount", (request.saleInfo.installment > 1 ? request.saleInfo.installment.ToString() : "0") },
                {"SecureType", "3DPay" },
                {"Pan", request.saleInfo.cardNumber},
                {"Cvv2",request.saleInfo.cardCVV},
                {"Expiry", request.saleInfo.cardExpiryDateMonth.ToString("00") + request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
                {"Rnd", Guid.NewGuid().ToString().Replace("-", "")},
            };

            string hashText = SHA1Base64(req["MbrId"] + req["OrderId"] + req["PurchAmount"] + req["OkUrl"] + req["FailUrl"] + req["TxnType"] + req["InstallmentCount"] + req["Rnd"] + auth.merchantStorekey);

            req.Add("Hash", hashText);

            string res = this.Request(req, auth);

            var dic = FoundationHelper.getFormParams(res);

            response.privateResponse = dic;

            if (dic?.ContainsKey("ErrMsg") == true || dic?.ContainsKey("ErrorCode") == true)
            {
                string errorMsg = $"{(dic?.ContainsKey("ErrorCode") == true ? dic["ErrorCode"].cpToString() : "")} - {(dic?.ContainsKey("ErrMsg") == true ? dic["ErrMsg"].cpToString() : "")}";

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

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            if (request.currency == null)
                request.currency = Currency.TRY;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"MbrId", "5" }, //TODO: Değişkenlik gösterebilir
                {"MerchantId", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"OrgOrderId", request.orderNumber },
                {"TxnType", "Void" },
                {"SecureType", "NonSecure" },
                {"Currency", ((int)request.currency).ToString() },
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
            else if (dic?.ContainsKey("ErrMsg") == true && dic["ErrMsg"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["ErrMsg"].cpToString();
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
                {"MbrId", "5" }, //TODO: Değişkenlik gösterebilir
                {"MerchantId", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.currency).ToString() },
                {"OrgOrderId", request.orderNumber },
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
            else if (dic?.ContainsKey("ErrMsg") == true && dic["ErrMsg"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["ErrMsg"].cpToString();
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = "İşlem iade edilemedi";
            }


            return response;
        }


        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.privateResponse = request?.responseArray;

            if (request?.responseArray?.ContainsKey("AuthCode") == true)
                response.transactionId = request.responseArray["AuthCode"].cpToString();


            if (request?.responseArray?.ContainsKey("OrderId") == true)
                response.orderNumber = request.responseArray["OrderId"].cpToString();


            if (request?.responseArray?.ContainsKey("ProcReturnCode") == true && request.responseArray["ProcReturnCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (request?.responseArray?.ContainsKey("ErrMsg") == true && request.responseArray["ErrMsg"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request.responseArray["ErrMsg"].cpToString();
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "İşlem sırasında bir hata oluştu";

                if (request?.responseArray?.ContainsKey("ProcReturnCode") == true)
                {
                    string procReturnCode = request.responseArray["ProcReturnCode"].cpToString();

                    string userMessage = getUserMessage(procReturnCode);

                    if (!string.IsNullOrWhiteSpace(userMessage))
                        response.message = userMessage;
                    else
                    {
                        string bankMessage = getPrivateMessage(procReturnCode);

                        if (!string.IsNullOrWhiteSpace(bankMessage) && request?.responseArray?.ContainsKey("ErrMsg") != true)
                            request.responseArray.Add("ErrMsg", bankMessage);
                    }

                }

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

        private string Request2(Dictionary<string, string> param, VirtualPOSAuth auth, string link = null)
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


        private string Request(Dictionary<string, string> param, VirtualPOSAuth auth, string link = null)
        {
            string requueststring = string.Join("&", param.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            link = link ?? (auth.testPlatform ? _urlAPITest : _urlAPILive);

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;
#pragma warning disable SYSLIB0014
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(link);
#pragma warning restore SYSLIB0014
            string postdata = requueststring.ToString();
            byte[] postdatabytes = System.Text.Encoding.UTF8.GetBytes(postdata);
            request.Method = "POST";
            //request.Accept = "application/x-www-form-urlencoded";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postdatabytes.Length;
            System.IO.Stream requeststream = request.GetRequestStream();
            requeststream.Write(postdatabytes, 0, postdatabytes.Length);
            requeststream.Close();

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader responsereader = new System.IO.StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
            string gelenresp = responsereader.ReadToEnd();

            return gelenresp;
        }

        private string getUserMessage(string ErrorCode)
        {
            string desc = "";

            switch (ErrorCode)
            {
                case "00": desc = "Onaylandı"; break;
                case "01": desc = "Bankanizi Arayın"; break;
                case "02": desc = "Bankanızı Arayın (Özel Durum)"; break;
                case "03": desc = "Geçersiz Üye İşyeri"; break;
                case "04": desc = "Karta El Koy"; break;
                case "05": desc = "Red Onaylanmadı"; break;
                case "06": desc = "Stop List"; break;
                case "07": desc = "Karta El Koy (Özel Durum)"; break;
                case "08": desc = "Kimlik Sorgula"; break;
                case "09": desc = "Tekrar Deneyin"; break;
                case "12": desc = "Geçersiz İşlem"; break;
                case "13": desc = "Geçersiz Tutar"; break;
                case "14": desc = "Geçersiz Hesap Numarası"; break;
                case "15": desc = "Tanımsız Issuer"; break;
                case "16": desc = "Asgari ödeme düzenli olarak gercekleştirilmedi"; break;
                case "22": desc = "Pin deneme sayısı aşıldı"; break;
                case "25": desc = "Kayıt Dosyada Bulunamadi"; break;
                case "28": desc = "Orijinal Reddedildi"; break;
                case "29": desc = "Orijinal Bulunmadi"; break;
                case "30": desc = "Mesaj Hatası"; break;
                case "33": desc = "Süresi Dolmuş Kart, El Koy"; break;
                case "34": desc = "CVV2 Hatalı"; break;
                case "36": desc = "Kısıtlı Kart, El Koy"; break;
                case "38": desc = "PIN Deneme Sayısı Aştı"; break;
                case "41": desc = "Kayıp Kart, Kartı Alın"; break;
                case "43": desc = "Çalıntı Kart, Kartı Alın"; break;
                case "51": desc = "Limit Yetersiz"; break;
                case "52": desc = "Tanimlı Hesap Yok"; break;
                case "53": desc = "Tanimlı Hesap Yok"; break;
                case "54": desc = "Süresi Dolmus Kart"; break;
                case "55": desc = "Yanlış PIN"; break;
                case "56": desc = "Desteklenmeyen Kart"; break;
                case "57": desc = "Karta İzin Verilmeyen İşlem"; break;
                case "58": desc = "POSa Izin Verilmeyen İşlem"; break;
                case "61": desc = "Para Çekme Limiti Aşıldı"; break;
                case "62": desc = "Sınırlı Kart"; break;
                case "63": desc = "Güvenlik İhlali"; break;
                case "65": desc = "Para Çekme Limiti Aşıldı"; break;
                case "75": desc = "PIN Deneme Limiti Aşıldı"; break;
                case "76": desc = "Key Senkronizasyon Hatası"; break;
                case "77": desc = "Red, Script Yok"; break;
                case "78": desc = "Güvenli Olmayan PIN"; break;
                case "79": desc = "ARQC hatası"; break;
                case "81": desc = "Aygıt Versiyon Uyusmazlığı"; break;
                case "91": desc = "Issuer çalışmıyor"; break;
                case "92": desc = "Finansal Kurum Tanınmıyor"; break;
                case "95": desc = "POS Günsonu Hatası"; break;
                case "96": desc = "Sistem Hatası"; break;
                case "98": desc = "Çift Ters İşlem"; break;
            }

            if (!string.IsNullOrWhiteSpace(desc))
                desc = $"({ErrorCode}) - {desc}";

            return desc;
        }

        private string getPrivateMessage(string ErrorCode)
        {
            string desc = "";

            switch (ErrorCode)
            {
                case "M001": desc = "Bonus miktari sipariş miktarından büyük olamaz."; break;
                case "M002": desc = "Para birimi kodu geçersiz."; break;
                case "M003": desc = "Para birimi kodu eksik."; break;
                case "M004": desc = "Sıfır veya boş veya hatalı miktar."; break;
                case "M005": desc = "CVV2 boş olamaz."; break;
                case "M006": desc = "Son kullanma tarihi eksik yada hatalı , 4 karakter olmalıdır"; break;
                case "M007": desc = "FailUrl eksik."; break;
                case "M008": desc = "Pan eksik yada geçersiz uzunlukta. Pan 13 ile 19 karakter arasında olmalıdır."; break;
                case "M009": desc = "Şifre eşleşmesi başarılı değil, lütfen şifrenizi onaylayınız."; break;
                case "M010": desc = "OkUrl hatalı."; break;
                case "M011": desc = "Pareq hazırlanamadı."; break;
                case "M012": desc = "Satın alma miktari eksik yada geçersiz uzunlukta."; break;
                case "M013": desc = "Kurum kodu eksik."; break;
                case "M014": desc = "Bilinmeyen Güvenlik tipi."; break;
                case "M015": desc = "Bilinmeyen İşlem tipi."; break;
                case "M016": desc = "Kullanıci kodu eksik."; break;
                case "M017": desc = "Kullanıcı şifresi eksik."; break;
                case "M018": desc = "Hatalı Cvv2 {0}"; break;
                case "M019": desc = "İşlem tutarı 0,01 - 200.000 arasında olmalıdır."; break;
                case "M020": desc = "Full puan kullanımlı işlemde taksit yapılamaz."; break;
                case "M021": desc = "Bu işlem tipi için geçersiz güvenlik tipi"; break;
                case "M022": desc = "Bu işlem tipi için işlem siparış numarası boş olamaz"; break;
                case "M023": desc = "Bu İşlem tipi için İşlem Sipariş numarası gönderilmemelidir."; break;
                case "M024": desc = "Üye işyeri Numarası eksik."; break;
                case "M025": desc = "{0} alani hatali uzunlukta Gelen : {1} Olmasi Gereken : {2}"; break;
                case "M026": desc = "Bu işlem tipi için hatalı güvenlik tipi"; break;
                case "M027": desc = "Aradığınız kayıt bulunamadı"; break;
                case "M028": desc = "Bu İşlem tipi için sipariş numarası boş olamaz"; break;
                case "M030": desc = "HASH Uyusmazlıgı"; break;
                case "M038": desc = "OKUrl en fazla 200 karakter olabilir"; break;
                case "M041": desc = "Geçersiz kart Numarası"; break;
                case "M042": desc = "Plugin bulunamadı"; break;
                case "M043": desc = "Request formu boş olamaz"; break;
                case "M044": desc = "MPI post sırasında hata oluştu"; break;
                case "M045": desc = "Max Miktar Hatası"; break;
                case "M046": desc = "Rapor isteği sırasında hata oluştu"; break;
                case "M047": desc = "Bu kullanıcı için IP kısıtlaması vardır."; break;
                case "M048": desc = "Hatali Deger"; break;
                case "M049": desc = "Sisteme kimlik doğrulaması yapılamadı"; break;
                case "M050": desc = "Para birimi katsayısı eşleşemedi"; break;
                case "M051": desc = "MD de üye işyeri bilgisi eksik"; break;
                case "P001": desc = "Bu kayit zaten tanimlanmistir."; break;
                case "P002": desc = "İlgili kayıt bulunamadı."; break;
                case "V000": desc = "İşlem tamamlanamadı / Devam ediyor"; break;
                case "V001": desc = "Üye isyeri bulma hatası."; break;
                case "V002": desc = "Hata çözümüne yada DS e baglanilamadi. {0}"; break;
                case "V003": desc = "Sistem Kapalı"; break;
                case "V004": desc = "Kullanıcı Doğrulanamadı."; break;
                case "V005": desc = "Bilinmeyen kart tipi."; break;
                case "V006": desc = "Kullanıcıya bu İşlem tipi için izin verilmemiş"; break;
                case "V007": desc = "Terminal aktif değil."; break;
                case "V008": desc = "Üye işyeri bulunamadı."; break;
                case "V009": desc = "Üye isyeri aktif değil."; break;
                case "V010": desc = "Terminal bu işlem tipi için yetkili değil."; break;
                case "V011": desc = "İşlem tipine bu terminal için izin verilmemiş."; break;
                case "V012": desc = "Pareq Hatasi {0}"; break;
                case "V013": desc = "Seçili İşlem Bulunamadı!"; break;
                case "V014": desc = "Günsonundan önce iade yapılamaz. Asıl işlem iptal edilmelidir"; break;
                case "V015": desc = "Girilen iade miktari asıl işlem tutarından büyük olamaz"; break;
                case "V016": desc = "Seçili İşlem ön provizyon işlemi değildir !"; break;
                case "V017": desc = "Bu sipariş zaten iptal edildi."; break;
                case "V018": desc = "Bu işlem iptal edilemez."; break;
                case "V019": desc = "Kısmı iptal'e izin verilemez, işlem değeri değiştirilemez."; break;
                case "V020": desc = "Bu işlem henüz tamamlanmamıştır"; break;
                case "V021": desc = "Asıl işlem boş olmaz."; break;
                case "V022": desc = "Orjinal provizyon no belirtilen işlem tipi için seçilemez."; break;
                case "V023": desc = "Taksit sayısı belirtilmelidir."; break;
                case "V024": desc = "Taksit sayısı belirtilen işlem tipi için sıfırdan büyük olamaz"; break;
                case "V025": desc = "Bu provizyon önceden kapatılmıştır."; break;
                case "V026": desc = "Provizyon süresi dolmuş"; break;
                case "V027": desc = "Girilen tutar orjinal tutarin %15 kadar fazla veya eksik girilebilir."; break;
                case "V028": desc = "Bilinmeyen İşlem türü"; break;
                case "V029": desc = "OrderID tekil olmalıdır"; break;
                case "V030": desc = "Para birimi bulunamadi."; break;
                case "V031": desc = "İade edilecek tutar girilmemiş"; break;
                case "V032": desc = "Batch No bulunamadı"; break;
                case "V033": desc = "3D Doğrulama Başarılı"; break;
                case "V034": desc = "3D Kullanıcı Doğrulama Adımı Başarısız"; break;
                case "V035": desc = "Kullanıcı Yetkili Değil"; break;
                case "V036": desc = "Sistem Hatası"; break;
                case "V037": desc = "Sipariş daha once iade edilmistir."; break;
                case "V038": desc = "Bu işlem tipi için yetkili değilsiniz."; break;
                case "V040": desc = "Tekrar Sayısı alanı hatalı"; break;
                case "V041": desc = "Aralık Tipi Hatalı"; break;
                case "V042": desc = "Hatalı Aralık Süresi"; break;
                case "V043": desc = "Hatalı Müşteri Kodu"; break;
                case "V044": desc = "Ödeme başarısız"; break;
                case "V045": desc = "Eksik Alt Uye Isyeri Parametreleri"; break;
                case "V046": desc = "Ticari Kart parametreleri ayristirilamadi"; break;
                case "V047": desc = "Ticari Kart kullanilan puan 0 olamaz"; break;
                case "V048": desc = "Ticari Kart iki taksit arasi fark tanimlanandan kucuk olamaz"; break;
                case "V049": desc = "Ticari Kart iki taksit arasi fark tanimlanandan buyuk olamaz"; break;
                case "V050": desc = "Ticari Kart toplam taksit tutarlari satis tutarina esit olmalidir"; break;
                case "V051": desc = "Ticari Kart son vade gunu tanimlanan azami vade gun sayisindan buyuk olamaz"; break;
                case "V052": desc = "Ticari Kart ilk vade gunu tanimlanan ilk vade gun sayisindan az olamaz"; break;
                case "V053": desc = "Ticari Kart taksit tutari ve vade tarihi bos olamaz"; break;
                case "V054": desc = "Ticari Kart taksit tutari 0 olamaz"; break;
                case "V055": desc = "Ticari Kart vade tarihi ya da taksit tutari bos"; break;
                case "V056": desc = "Ticari Kart taksit sayisi tanimlanan taksit sayisindan fazla olamaz"; break;
                case "V057": desc = "Ticari Kart taksit sayisi 1 den buyuk olmalidir"; break;
                case "V058": desc = "Ticari Kart vade gun sayisi tanimlanandan fazla olamaz"; break;
                case "V059": desc = "Ticari Kart vade gun sayisi bos"; break;
                case "V060": desc = "Ticari Kart son vade tarihi tanimli asgari gunden once olamaz"; break;
                case "V9196": desc = "Baglantı Hatası"; break;
                case "V9199": desc = "Banka bağlantısında hata oluştu."; break;
            }

            if (!string.IsNullOrWhiteSpace(desc))
                desc = $"({ErrorCode}) - {desc}";

            return desc;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}

