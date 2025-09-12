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
using System.Text;
using System.Xml;

namespace CP.VPOS.Banks.Vakifbank
{
    internal class VakifbankVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://onlineodemetest.vakifbank.com.tr:4443/VposService/v3/Vposreq.aspx";
        private readonly string _urlAPILive = "https://onlineodeme.vakifbank.com.tr:4443/VposService/v3/Vposreq.aspx";

        private readonly string _url3Dtest = "https://3dsecuretest.vakifbank.com.tr:4443/MPIAPI/MPI_Enrollment.aspx";
        private readonly string _url3DLive = "https://3dsecure.vakifbank.com.tr:4443/MPIAPI/MPI_Enrollment.aspx";


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
                { "MerchantId", auth.merchantID },
                { "Password", auth.merchantPassword },
                { "TerminalNo", auth.merchantUser },
                { "TransactionType", "Sale" },
                { "CurrencyAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                { "CurrencyCode", ((int)request.saleInfo.currency).ToString() },
                { "Pan", request.saleInfo.cardNumber },
                { "Cvv", request.saleInfo.cardCVV },
                { "Expiry", request.saleInfo.cardExpiryDateYear.ToString() + request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "OrderId", request.orderNumber },
                { "ClientIp", request.customerIPAddress },
                { "TransactionDeviceSource", 0 }
            };

            if (request.saleInfo.installment > 1)
                param.Add("NumberOfInstallments", request.saleInfo.installment);

            string xml = param.toXml("VposRequest");

            string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "VposResponse");

            response.privateResponse = respDic;

            if (respDic?.ContainsKey("ResultCode") == true)
            {
                if (respDic["ResultCode"].cpToString() == "0000")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                    response.transactionId = respDic.ContainsKey("TransactionId") ? respDic["TransactionId"].cpToString() : "";
                }
                else
                {
                    string message = "";

                    if (respDic.ContainsKey("ResultDetail") && respDic["ResultDetail"].cpToString() != "")
                        message = respDic["ResultDetail"].cpToString();

                    if (string.IsNullOrWhiteSpace(message))
                        message = getErrorDesc(respDic["ResultCode"].cpToString());


                    if (string.IsNullOrWhiteSpace(message))
                        message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                    response.transactionId = "";
                    response.statu = SaleResponseStatu.Error;
                    response.message = message;
                }
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


            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                { "MerchantId", auth.merchantID },
                { "MerchantPassword", auth.merchantPassword },
                { "VerifyEnrollmentRequestId", Guid.NewGuid().ToString() },
                { "Pan", request.saleInfo.cardNumber },
                { "ExpiryDate", request.saleInfo.cardExpiryDateYear.ToString().Substring(2) + request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "PurchaseAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                { "Currency", ((int)request.saleInfo.currency).ToString() },
                { "SuccessUrl", request.payment3D.returnURL },
                { "FailureUrl", request.payment3D.returnURL },
                { "SessionInfo", request.orderNumber }
            };

            if (request.saleInfo.installment > 1)
                param.Add("InstallmentCount", request.saleInfo.installment.ToString());


            string resp = this.Request(param, (auth.testPlatform ? _url3Dtest : _url3DLive));

            Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "IPaySecure");

            response.privateResponse = respDic;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(resp);


            var status = xmlDocument.SelectSingleNode("IPaySecure/Message/VERes/Status")?.InnerText;
            var pareq = xmlDocument.SelectSingleNode("IPaySecure/Message/VERes/PaReq")?.InnerText;
            var acsUrl = xmlDocument.SelectSingleNode("IPaySecure/Message/VERes/ACSUrl")?.InnerText;
            var termUrl = xmlDocument.SelectSingleNode("IPaySecure/Message/VERes/TermUrl")?.InnerText;
            var md = xmlDocument.SelectSingleNode("IPaySecure/Message/VERes/MD")?.InnerText;
            var messageErrorCode = xmlDocument.SelectSingleNode("IPaySecure/MessageErrorCode")?.InnerText;
            var errorMessage = xmlDocument.SelectSingleNode("IPaySecure/ErrorMessage")?.InnerText;

            if (status == "Y")
            {
                Dictionary<string, string> three3DFormParams = new Dictionary<string, string>
                {
                    { "PaReq", pareq },
                    { "TermUrl", termUrl },
                    { "MD", md },
                };

                string html = FoundationHelper.ToHtmlForm(three3DFormParams, acsUrl);

                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = html;

            }
            else if (status == "N")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "Kartınızda 3D Secure kullanımı aktif değil. Lütfen 3D Secure kullanımı aktif bir kart ile ödeme yapınız.";
            }
            else
            {
                string message = errorMessage;

                if (string.IsNullOrWhiteSpace(message))
                    message = getErrorDesc(messageErrorCode.cpToString());


                if (string.IsNullOrWhiteSpace(message))
                    message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                response.statu = SaleResponseStatu.Error;
                response.message = message;
            }


            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "İşlem sırasında bilinmeyen bir hata oluştu.",
                privateResponse = request.responseArray,
                orderNumber = request?.responseArray?.ContainsKey("SessionInfo") == true ? request.responseArray["SessionInfo"].cpToString() : "",
            };


            if (request?.responseArray?.ContainsKey("Status") == true && request.responseArray["Status"].cpToString() == "Y")
            {
                string amount = (request.responseArray["PurchAmount"].cpToString().Replace(".", "").Replace(",", "").cpToDecimal() / (decimal)100.00).ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    { "TransactionType", "Sale" },
                    { "MerchantId", auth.merchantID },
                    { "Password", auth.merchantPassword },
                    { "TerminalNo", auth.merchantUser },
                    { "Pan", request.responseArray["Pan"].cpToString() },
                    { "Expiry", $"20{request.responseArray["Expiry"].cpToString()}" },
                    { "CurrencyAmount", amount },
                    { "CurrencyCode", request.responseArray["PurchCurrency"].cpToString() },
                    //{ "CardHoldersName", "cem test" },
                    //{ "Cvv", "715" },
                    { "ECI", request.responseArray["Eci"].cpToString() },
                    { "CAVV", request.responseArray["Cavv"].cpToString() },
                    { "MpiTransactionId", request.responseArray["VerifyEnrollmentRequestId"].cpToString() },
                    { "OrderId", response.orderNumber },
                    { "ClientIp", "1.1.1.1" },
                    { "TransactionDeviceSource", 0 },
                };


                if (request.responseArray.ContainsKey("InstallmentCount") == true && request.responseArray["InstallmentCount"].cpToInt() > 1)
                    param.Add("NumberOfInstallments", request.responseArray["InstallmentCount"].cpToInt());


                string xml = param.toXml("VposRequest");

                string resp = this.xmlRequest(xml, (auth.testPlatform ? _urlAPITest : _urlAPILive));

                Dictionary<string, object> respDic = FoundationHelper.XmltoDictionary(resp, "VposResponse");

                response.privateResponse = respDic;
                response.transactionId = respDic?.ContainsKey("TransactionId") == true ? respDic["TransactionId"].cpToString() : "";

                if (respDic?.ContainsKey("ResultCode") == true)
                {
                    if (respDic["ResultCode"].cpToString() == "0000")
                    {
                        response.statu = SaleResponseStatu.Success;
                        response.message = "İşlem başarıyla tamamlandı";
                    }
                    else
                    {
                        string message = respDic.ContainsKey("ResultDetail") ? respDic["ResultDetail"].cpToString() : "";

                        if (string.IsNullOrWhiteSpace(message))
                            message = getErrorDesc(respDic["ResultDetail"].cpToString());


                        if (string.IsNullOrWhiteSpace(message))
                            message = "İşlem sırasında bilinmeyen bir hata oluştu.";

                        response.statu = SaleResponseStatu.Error;
                        response.message = message;
                    }
                }
                else
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = "İşlem sırasında bilinmeyen bir hata oluştu.";
                }
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = "3D doğrulama işlemi başarısız.";
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            return new CancelResponse { statu = ResponseStatu.Error, message = "Bu banka için iptal metodu tanımlanmamış!" };
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            return new RefundResponse { statu = ResponseStatu.Error, message = "Bu banka için iade metodu tanımlanmamış!" };
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
            string postdata = "prmstr=" + xml.ToString();
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

        private string getErrorDesc(string ErrCode)
        {
            string errMsg = "";

            switch (ErrCode)
            {
                case "0000": errMsg = "İşlem Başarılı"; break;
                case "0001": errMsg = "Red-Bankanızı Arayın"; break;
                case "0002": errMsg = "Kategori Yok"; break;
                case "0003": errMsg = "Isyerı Kategorisi Hatalı/Tanımsız"; break;
                case "0004": errMsg = "Karta El Koyunuz/Sakıncalı"; break;
                case "0005": errMsg = "Red/Onaylanmadı"; break;
                case "0006": errMsg = "Hatalı İşlem"; break;
                case "0007": errMsg = "Karta El Koyunuz"; break;
                case "0008": errMsg = "Kimlik Kontrolü/Onaylandı"; break;
                case "0009": errMsg = "Tekrar Deneyiniz"; break;
                case "0010": errMsg = "Tekrar Deneyiniz"; break;
                case "0011": errMsg = "Tekrar Deneyiniz"; break;
                case "0012": errMsg = "Hatalı İşlem / Red"; break;
                case "0013": errMsg = "Gecersız İşlem Tutarı"; break;
                case "0014": errMsg = "Geçersiz Kart Numarası"; break;
                case "0015": errMsg = "Müşteri Yok/Bın Hatalı"; break;
                case "0021": errMsg = "İşlem Yapılamadı"; break;
                case "0030": errMsg = "Format Hatası (Üye İşyeri)"; break;
                case "0032": errMsg = "Dosyasına Ulaşılamadı"; break;
                case "0033": errMsg = "Süresi Bitmiş/İptal Kart"; break;
                case "0034": errMsg = "Sahte Kart"; break;
                case "0038": errMsg = "Şifre Aşımı/Karta El Koy"; break;
                case "0041": errMsg = "Kayıp Kart- Karta El Koy"; break;
                case "0043": errMsg = "Çalıntı Kart-Karta El Koy"; break;
                case "0051": errMsg = "Bakiyesi-Kredi Limiti Yetersiz"; break;
                case "0052": errMsg = "Hesap Noyu Kontrol Edin"; break;
                case "0053": errMsg = "Hesap Yok"; break;
                case "0054": errMsg = "Vade Sonu Geçmiş Kart"; break;
                case "0055": errMsg = "Hatalı Kart Şifresi"; break;
                case "0056": errMsg = "Kart Tanımlı Değil."; break;
                case "0057": errMsg = "Kart İşlem Tipine Kapalı"; break;
                case "0058": errMsg = "İşlem Tipi Terminale Kapalı"; break;
                case "0059": errMsg = "Sahtekarlık Şüphesi"; break;
                case "0061": errMsg = "Para Çekme Tutar Limiti Aşıldı"; break;
                case "0062": errMsg = "Yasaklanmış Kart"; break;
                case "0063": errMsg = "Güvenlik İhlali"; break;
                case "0065": errMsg = "Para Çekme Limiti Aşıldı"; break;
                case "0075": errMsg = "Şifre Deneme Sayısı Aşıldı"; break;
                case "0077": errMsg = "Şifre Scrıpt Talebi Reddedildi"; break;
                case "0078": errMsg = "Şifre Güvenilir Bulunmadı"; break;
                case "0089": errMsg = "İşlem Onaylanmadı"; break;
                case "0091": errMsg = "Kartı Veren Banka Hizmet Dışı"; break;
                case "0092": errMsg = "Bankası Bilinmiyor"; break;
                case "0093": errMsg = "Kartınız E-Ticaret İşlemlerine Kapalıdır. Bankanızı Arayınız."; break;
                case "0096": errMsg = "Bankasının Sistemi Arızalı"; break;
                case "0312": errMsg = "Kartın Cvv2 Değeri Hatalı"; break;
                case "0315": errMsg = "Kartın Sanal Limiti Yeterli Değil"; break;
                case "0320": errMsg = "Önprovizyon Yok"; break;
                case "0323": errMsg = "Önpr. Kapama Tutar Eşlenmedi"; break;
                case "0357": errMsg = "Eksik Ödeme Sayacı:Nakit Red"; break;
                case "0358": errMsg = "Kart Kapalı"; break;
                case "0359": errMsg = "Aylık Ciro Limiti Aşıldı"; break;
                case "0381": errMsg = "Red Karta El Koy"; break;
                case "0382": errMsg = "Sahte Kart-Karta El Koyunuz"; break;
                case "0400": errMsg = "3D Secure Şifre Doğrulaması Yapılamadı"; break;
                case "0501": errMsg = "Geçersiz Taksit/İşlem Tutarı"; break;
                case "0503": errMsg = "Ekstre-Taksit Sayısı Uyumsuz"; break;
                case "0504": errMsg = "İşyerinin Storeu İçin Bu Kartın Bıni Tanımlı Değil"; break;
                case "0540": errMsg = "İade Edilecek İşlemin Orijinali Bulunamadı"; break;
                case "0541": errMsg = "Orj. İşlem Tamamı İade Edildi"; break;
                case "0542": errMsg = "Günlük İade Limiti Aşımı"; break;
                case "0550": errMsg = "İşlem Ykb Pos Undan Yapılmalı"; break;
                case "0570": errMsg = "Yurtdışı Kart İşlem İzni Yok"; break;
                case "0571": errMsg = "İşyeri Amex İşlem İzni Yok"; break;
                case "0572": errMsg = "İşyeri Amex Tanımları Eksik"; break;
                case "0574": errMsg = "Kampüs Karta Uygun İşyeri Değil"; break;
                case "0575": errMsg = "Limitsiz Takip Kart"; break;
                case "0577": errMsg = "Taksite Kapalı Sektör"; break;
                case "0580": errMsg = "Cavv Veya Bkm Expsign değeri Hatalı"; break;
                case "0581": errMsg = "Ecı Veya Cavv Bilgisi Eksik"; break;
                case "0582": errMsg = "Cavv Acs Error"; break;
                case "0583": errMsg = "Bkm Expsign Mükerrer"; break;
                case "0961": errMsg = "Debit Kartla İade Yapılamaz"; break;
                case "0962": errMsg = "Terminalıd Tanımısız"; break;
                case "0963": errMsg = "Üye İşyeri Tanımlı Değil"; break;
                case "0966": errMsg = "Duplicate İşlem Numarası Hatası"; break;
                case "0971": errMsg = "Eşleşmiş (Capture) Bir İşlem İptal Edilemez"; break;
                case "0972": errMsg = "Para Kodu Geçersiz.Onus Kart İle Yp İşlem Yapılamaz."; break;
                case "0973": errMsg = "İşlem Onaylanmadı"; break;
                case "0974": errMsg = "Reversal Farklı Günde Gelmiş."; break;
                case "0975": errMsg = "İşlem İzni Yok"; break;
                case "0976": errMsg = "Onus Kart Tanımlı Değil"; break;
                case "0977": errMsg = "Onus Kart Tanımlı Değil"; break;
                case "0978": errMsg = "Notonus Kart İle Taksitli İşlem"; break;
                case "0980": errMsg = "Son Kur Bilgisi Bulunamadı"; break;
                case "0981": errMsg = "3D Secure Acquiring İle İlgili Eksik Güvenlik Alanı"; break;
                case "0982": errMsg = "İşlem İptal Durumda. İade Edilemez"; break;
                case "0983": errMsg = "İade Edilecek İşlemin Orijinali Bulunamadı"; break;
                case "0984": errMsg = "İade Tutarı Satış Tutarından Büyük Olamaz"; break;
                case "0985": errMsg = "İşyeri Store A Bağlı Olmalıdır"; break;
                case "0986": errMsg = "Gıb Taksit Hata"; break;
                case "0987": errMsg = "İşyeri Mp Taksit Tanımı Bulunamadı"; break;
                case "1001": errMsg = "Sistem Hatasi."; break;
                case "1006": errMsg = "Bu İşlem Numarası İle Daha Önce Bir İslem Gerçekleştirilmiş, İşleme Yeni Bir Numara Verebilir Yada Bu Alanı Boş Bırakabilirsiniz"; break;
                case "1007": errMsg = "Referans Transaction Alinamadi"; break;
                case "1044": errMsg = "Debit Kartlarla Taksitli İslem Yapilamaz"; break;
                case "1046": errMsg = "Toplam İade Tutarı Orjinal Tutarı Aştı."; break;
                case "1047": errMsg = "Islem Tutari Geçersiz."; break;
                case "1049": errMsg = "Geçersiz Tutar."; break;
                case "1050": errMsg = "Cvv Hatali."; break;
                case "1051": errMsg = "Kredi Karti Numarasi Hatali."; break;
                case "1052": errMsg = "Kart Vadesi Hatalı Veya Vade Formatı Hatalı"; break;
                case "1053": errMsg = "Gönderilen Pancode Kayıtlı Değil"; break;
                case "1054": errMsg = "Islem Numarasi Hatali."; break;
                case "1059": errMsg = "İşlemin Tamamı İade Edilmiş."; break;
                case "1060": errMsg = "Hatali Taksit Sayisi."; break;
                case "1061": errMsg = "Ayni Siparis Numarasiyla Daha Önceden Basarili İslem Yapilmis"; break;
                case "1065": errMsg = "Ön Provizyon Daha Önceden Kapatilmis"; break;
                case "1073": errMsg = "Terminal Üzerinde Aktif Olarak Bir Batch Bulunamadi"; break;
                case "1074": errMsg = "Islem Henüz Sonlanmamis Yada Referans İslem Henüz Tamamlanmamis."; break;
                case "1075": errMsg = "Sadakat Puan Tutari Hatali"; break;
                case "1076": errMsg = "Sadakat Puan Kodu Hatali"; break;
                case "1077": errMsg = "Para Kodu Hatali"; break;
                case "1078": errMsg = "Geçersiz Siparis Numarasi"; break;
                case "1079": errMsg = "Geçersiz Siparis Açiklamasi"; break;
                case "1080": errMsg = "Sadakat Tutari Ve Para Tutari Gönderilmemis."; break;
                case "1081": errMsg = "Puanla Satış İşleminde Taksit Sayısı Gönderilemez"; break;
                case "1082": errMsg = "Geçersiz İslem Tipi"; break;
                case "1083": errMsg = "Referans İslem Daha Önceden İptal Edilmis."; break;
                case "1087": errMsg = "Yabanci Para Birimiyle Taksitli Provizyon Kapama İslemi Yapilamaz"; break;
                case "1088": errMsg = "Önprovizyon İptal Edilmis"; break;
                case "1089": errMsg = "Referans İslem Yapilmak İstenen İslem İçin Uygun Degil"; break;
                case "1091": errMsg = "Recurring İslemin Toplam Taksit Sayisi Hatali"; break;
                case "1092": errMsg = "Recurring İslemin Tekrarlama Araligi Hatali"; break;
                case "1093": errMsg = "Sadece Satis (Sale) İslemi Recurring Olarak İsaretlenebilir"; break;
                case "1095": errMsg = "Lütfen Geçerli Bir Email Adresi Giriniz"; break;
                case "1096": errMsg = "Provizyon Talep Mesajına Clientıp Değerini Gönderiniz."; break;
                case "1097": errMsg = "Lütfen Geçerli Bir Cavv Degeri Giriniz"; break;
                case "1098": errMsg = "Lütfen Geçerli Bir Ecı Degeri Giriniz"; break;
                case "1099": errMsg = "Lütfen Geçerli Bir Kart Sahibi İsmi Giriniz"; break;
                case "1100": errMsg = "Lütfen Geçerli Bir Brand Girisi Yapin."; break;
                case "1101": errMsg = "Referans Transaction Reverse Edilmis."; break;
                case "1102": errMsg = "Recurring İslem Araligi Geçersiz."; break;
                case "1103": errMsg = "Taksit Sayisi Girilmeli"; break;
                case "1104": errMsg = "İzinsiz Taksitli İşlem."; break;
                case "1105": errMsg = "Üye İsyeri Ip Si Sistemde Tanimli Degil"; break;
                case "1106": errMsg = "Extract Maksimum 40 Karakter Olmalıdır."; break;
                case "1107": errMsg = "Expsign Alanın Uzunluğu Hatalı"; break;
                case "1108": errMsg = "Mpitransactionıd Alanın Uzunluğu Hatalı"; break;
                case "1109": errMsg = "Valuelist Alanın Uzunluğu Hatalı"; break;
                case "1110": errMsg = "Bu Üye İşyerii 3D İşlem Yapamaz"; break;
                case "1111": errMsg = "Bu Üye İşyeri Non Secure İşlem Yapamaz"; break;
                case "1112": errMsg = "Terminal Aktif Değil"; break;
                case "1113": errMsg = "Terminalde Açık Reversal Bulunuyor"; break;
                case "1114": errMsg = "Mpitransactionıd Alanı Boş Gönderilmiş"; break;
                case "1115": errMsg = "Mpitransactionı Buluanamıyor"; break;
                case "1116": errMsg = "Eci Değeri Mpı İle Uyumsuz"; break;
                case "1117": errMsg = "Cavv Değeri Mpı İle Uyumsuz"; break;
                case "1118": errMsg = "3D Secure İşlemler Mailorder Olarak Gönderilemez"; break;
                case "1119": errMsg = "Otomatik Gün Sonu Tanımlı Üye İşyerleri Manuel Gün Sonu Yapamazlar"; break;
                case "1120": errMsg = "Geçersiz Security Code"; break;
                case "1121": errMsg = "Transactiondevicesource Alanının Gönderilmesi Zorunludur."; break;
                case "1122": errMsg = "Surcharge Tutarı 0 Dan Büyük Olmalı."; break;
                case "1123": errMsg = "Kayıt İade Durumda"; break;
                case "1124": errMsg = "Kayıt İptal Durumda"; break;
                case "1125": errMsg = "Terminal Bulunamadi"; break;
                case "1126": errMsg = "Mpi İşlemindeki Veriler İle Uyumsuz"; break;
                case "1127": errMsg = "3Ds İşlemlerde Kart Bilgisi Veya Tutar Provizyon Mesajında Yer Almamalıdır"; break;
                case "1128": errMsg = "Mpitransactionıd Daha Önce Başka Bir İşlem İçin Kullanılmış"; break;
                case "1129": errMsg = "Express Ve 3D Secure İşlem Aynı Anda Gönderilemez."; break;
                case "1130": errMsg = "Expsign Değeri İletilen İşlem İçin Ecı Değeri Geçerli Değil"; break;
                case "1131": errMsg = "Customıtems Alanının Uzunluğu Hatalı"; break;
                case "1132": errMsg = "İşleme Ait Kanal Bilgisine Göre, Expsign Değeri Boş Olmalı"; break;
                case "1133": errMsg = "Üye İş Yeri Yetkileri Arasında Ekstre Gönderme Yetkisi Bulunmamaktadır."; break;
                case "1134": errMsg = "Custom Items Name Alanının Uzunluğu Maksimum 100 Karakter Olmalı."; break;
                case "1135": errMsg = "Customıtem İçerisinde Tutar Hatalı"; break;
                case "1136": errMsg = "Customıtem İçerisinde Telefon Hatalı"; break;
                case "1137": errMsg = "Customıtem İçerisinde E-Posta Hatalı"; break;
                case "1138": errMsg = "Customıtem İçerisinde Tip Hatalı"; break;
                case "1139": errMsg = "Tanımlı Customıtem Hatalı Customtype"; break;
                case "1140": errMsg = "Sıra Numarası Zorunlu."; break;
                case "1141": errMsg = "Vade Süresi(Ay) Zorunlu."; break;
                case "1142": errMsg = "Ödeme Sıklığı(Ay) Zorunlu."; break;
                case "1143": errMsg = "Öteleme Süresi(Ay) Zorunlu."; break;
                case "1144": errMsg = "Vade Ödeme Sıklığı Zorunlu."; break;
                case "1145": errMsg = "Sgk Tutarı Küsüratlı Olamaz."; break;
                case "1146": errMsg = "Ödeme Planı Bulunamadı."; break;
                case "1147": errMsg = "Üye İş Yeri Yetkileri Arasında Gib Taksit Yetkisi Bulunmamaktadır."; break;
                case "1148": errMsg = "Üye İş Yeri Yetkileri Arasında Tekrarlı Tahsilat Yetkisi Bulunmamaktadır."; break;
                case "1152": errMsg = "Customıtem İçerisindeki Vftbankreferansno Bu İşlem Tipi İçin Geçerli Değildir"; break;
                case "1153": errMsg = "Üye İş Yerinin Bkm Express İzni Yoktur."; break;
                case "2012": errMsg = "Batch Bulunamadı"; break;
                case "2013": errMsg = "Terminal Bulunamadı."; break;
                case "2200": errMsg = "Is Yerinin İslem İçin Gerekli Hakki Yok."; break;
                case "2202": errMsg = "Islem İptal Edilemez. ( Batch Kapali )"; break;
                case "2203": errMsg = "Batch Kapama İsteginden Once Batch E Ait Settlementqueue Daki İslemler Tamamlanmis Olmali."; break;
                case "4000": errMsg = "İşlem Tipi Hatalı"; break;
                case "4001": errMsg = "Bitiş Tarihi, Başlangıç Tarihinden Küçük Olamaz"; break;
                case "4002": errMsg = "Başlangıç Tarihi Zorunlu"; break;
                case "4003": errMsg = "Bitiş Tarihi Zorunlu"; break;
                case "4004": errMsg = "Otorizasyon Kodu Zorunlu"; break;
                case "4005": errMsg = "En Az Bir Sorgu Kriteri Zorunlu"; break;
                case "4006": errMsg = "En Az Bir Sorgu Kriteri Zorunlu"; break;
                case "4007": errMsg = "Arama Kriteri Hatalı"; break;
                case "4008": errMsg = "Mutabakat Tarihi Zorunlu"; break;
                case "5000": errMsg = "En Az 1 Sayfa İçeriği Zorunlu"; break;
                case "5001": errMsg = "Kimlik Doğrulama İşlemi Başarısız."; break;
                case "5002": errMsg = "Is Yeri Aktif Degil."; break;
                case "5003": errMsg = "Sayfanın 1 Adet Tutar Tipinde İçeriği Olmalı"; break;
                case "5004": errMsg = "Sayfanın 1 Den Fazla Tutar Tipinde İçeriği Olamaz"; break;
                case "5005": errMsg = "Tutar Tipindeki Sayfa İçeriğinde Para Birimi Zorunlu"; break;
                case "5006": errMsg = "Sayfa İçerik Başlığı Zorunlu"; break;
                case "5007": errMsg = "Sayfa İçeriği Hatalı"; break;
                case "5008": errMsg = "Bağış Sayfası İçerik Etiket Uzunluğu Hatalı"; break;
                case "5009": errMsg = "Bağış Sayfası İçerik Etiket Değer Uzunluğu Hatalı"; break;
                case "5010": errMsg = "Girilen Değer 200 Karakteri Geçmemeli"; break;
                case "5011": errMsg = "Dikkate Alınacak Tutar Alanı Seçili İse, Para Birimi De Seçilebilmelidir."; break;
                case "5012": errMsg = "Dikkate Alınacak Tutar Alanı Seçili İse, Dikkate Alınacak Adet Alanı Seçilmemelidir."; break;
                case "5013": errMsg = "Dikkate Alınacak Tutar Alanı Seçili İse, Giriş Zorunlu Olmalıdır."; break;
                case "5014": errMsg = "Dikkate Alınacak Adet Alanı Seçili İse, Para Birimi Seçilmemelidir."; break;
                case "5015": errMsg = "Dikkate Alınacak Adet Alanı Seçili İse, Giriş Zorunlu Olmalıdır."; break;
                case "5016": errMsg = "Değer Başlık Alanı Boş Bırakılamaz."; break;
                case "5017": errMsg = "Sayfanın 1 Den Fazla Adet Tipinde İçeriği Olamaz."; break;
                case "5018": errMsg = "İçerik Tipi Liste Yada Radyo Butonu Olan İçeriklere İçerik Değeri Atanabilir"; break;
                case "6000": errMsg = "Merchant Isactive Field Is Invalid"; break;
                case "6001": errMsg = "Merchant Contactaddressline1 Length Is Invalid"; break;
                case "6002": errMsg = "Merchant Contactaddressline2 Length Is Invalid"; break;
                case "6003": errMsg = "Merchant Contactcitylength Is Invalid"; break;
                case "6004": errMsg = "Merchant Contactemail Must Be Valid Email"; break;
                case "6005": errMsg = "Merchant Contactemail Length Is Invalid"; break;
                case "6006": errMsg = "Merchant Contactname Length Is Invalid"; break;
                case "6007": errMsg = "Merchant Contactphone Length Is Invalid"; break;
                case "6008": errMsg = "Merchant Hostmerchantıd Length Is Invalid"; break;
                case "6009": errMsg = "Merchant Hostmerchantıd Is Empty"; break;
                case "6010": errMsg = "Merchant Merchantname Length Is Invalid"; break;
                case "6011": errMsg = "Merchant Merchantpassword Length Is Invalid"; break;
                case "6012": errMsg = "Terminalınfo Hostterminalıd Is Invalid"; break;
                case "6013": errMsg = "Terminalınfo Hostterminalıd Length Is Invalid"; break;
                case "6014": errMsg = "Terminalınfo Hostterminalıd Is Empty"; break;
                case "6015": errMsg = "Terminalınfo Terminalname Is Invalid"; break;
                case "6016": errMsg = "Üye İşyeri Departmanı Hatalı"; break;
                case "6017": errMsg = "Üye İşyeri Departman No Hatalı"; break;
                case "6018": errMsg = "Merchant Not Found"; break;
                case "6019": errMsg = "Invalidrequest"; break;
                case "6020": errMsg = "Birim Zaten Mevcut"; break;
                case "6021": errMsg = "Birim Bulunamadı"; break;
                case "6022": errMsg = "Transaction Type Exist In Merchant Permission"; break;
                case "6023": errMsg = "Merchant Permission Exist In Merchant"; break;
                case "6024": errMsg = "Currency Code Exist In Merchant Currency Codes Permission"; break;
                case "6025": errMsg = "Terminal Exist In Merchantterminals"; break;
                case "6026": errMsg = "Terminal Can Not Be Found In Merchantterminals"; break;
                case "6027": errMsg = "Invalid Login Attempti. Please Check Clientıd And Clientpassword Fields"; break;
                case "6028": errMsg = "Merchant İs Already Exist. You Should Try To Update Method"; break;
                case "6029": errMsg = "Üye İşyeri Eposta Hatalı"; break;
                case "6030": errMsg = "Üye İşyeri Web Adresi Hatalı"; break;
                case "6031": errMsg = "Otomatik Günsonu Zamanı Zorunlu"; break;
                case "6032": errMsg = "Otomatik Günsonu Zamanı Hatalı"; break;
                case "6033": errMsg = "3D Üye İşyeri Tipi Hatalı"; break;
                case "6034": errMsg = "Parenthostmerchantıd Dolu Olmamalıdır."; break;
                case "6035": errMsg = "Parenthostmerchantıd Dolu Olmalıdır."; break;
                case "6036": errMsg = "Yalnızca Ana Bayi,Alt Bayi İşlemlerini Görebilir."; break;
                case "6037": errMsg = "Ana Bayi Sistemde Tanımlı Değil."; break;
                case "6038": errMsg = "Alt Bayi Olan Bir İşyeri Başka Bir İşyerinin Parenthostmerchantıd Si Olamaz."; break;
                case "6039": errMsg = "Tckn Veya Vkn Alanları Dolu Olmalıdır."; break;
                case "6040": errMsg = "Aynı Tckn Ye Sahip Sadece Bir Üye İşyeri Olabilir."; break;
                case "6041": errMsg = "Aynı Vkn Ye Sahip Sadece Bir Üye İşyeri Olabilir."; break;
                case "6042": errMsg = "Tckn 11 Hane Olmalı."; break;
                case "6043": errMsg = "Tckn Numerik Olmalıdır."; break;
                case "6044": errMsg = "Vkn 10 Hane Olmalıdır."; break;
                case "6045": errMsg = "Vkn Numerik Olmalıdır."; break;
                case "6046": errMsg = "Tckn Ve Vkn 10 Veya 11 Hane Olmalıdır."; break;
                case "6047": errMsg = "Tckn Ve Vkn Alanı Boş Olamaz."; break;
                case "6048": errMsg = "Vkn Ve Tckn Boş Olmalıdır."; break;
                case "6049": errMsg = "Merchantıd Boş Olmalıdır."; break;
                case "6050": errMsg = "Merchantıd Boş Olamaz."; break;
                case "6051": errMsg = "Parent Hostmerchant Ana Bayi Olmalıdır."; break;
                case "6052": errMsg = "Kullanıcının Onaylama Yetkisi Yok"; break;
                case "6053": errMsg = "Üye İş Yerinin Vade Ve Ödeme Sıklığı Tekil Olmalıdır."; break;
                case "6054": errMsg = "Ödeme Sıklığı Nümerik Olmalı."; break;
                case "6055": errMsg = "Vade Süresi Nümerik Olmalı."; break;
                case "6056": errMsg = "Sıra Numarası Nümerik Olmalı."; break;
                case "6057": errMsg = "Öteleme Süresi Nümerik Olmalı."; break;
                case "6058": errMsg = "Ödeme Sıklığı Zorunlu."; break;
                case "6059": errMsg = "Vade Süresi Zorunlu."; break;
                case "6060": errMsg = "Sıra Numarası Zorunlu."; break;
                case "6061": errMsg = "Öteleme Süresi Zorunlu."; break;
                case "6063": errMsg = "Na Bayi, Alt Bayi İlişkisi Bulunmuyor"; break;
                case "6064": errMsg = "Hostmerchantıd Veri Uzunluğu Geçersiz"; break;
                case "6065": errMsg = "Hostsubmerchantıd Veri Uzunluğu Geçersiz"; break;
                case "6066": errMsg = "Hostsubmerchantıd Must Not Be Empty."; break;
                case "6067": errMsg = "Hostsubmerchantıd Değeri Boş Olmalı"; break;
                case "7777": errMsg = "Banka Tarafinda Gün Sonu Yapildigindan İslem Gerçeklestirilemedi"; break;
                case "9000": errMsg = "İşlem Yükleme Limit Aşıldı"; break;
                case "9001": errMsg = "İşlem Yükleme Limit Aşıldı"; break;
                case "9025": errMsg = "Hatalı İstek."; break;
                case "9026": errMsg = "İstek Bilgisi Hatalı."; break;
                case "9027": errMsg = "Kullanıcı Adı Veya Şifre Yanlış."; break;
                case "9028": errMsg = "Rol Bulunamadı."; break;
                case "9029": errMsg = "Rol Adı Boş Bırakılamaz."; break;
                case "9030": errMsg = "Rol İçerisinde Tanımlı User Varken Silemezsiniz."; break;
                case "9031": errMsg = "Kullanıcı Adı Veya Şifre Yanlış."; break;
                case "9032": errMsg = "Kullanıcı Pasif"; break;
                case "9033": errMsg = "Kullanıcı Silinmiş"; break;
                case "9034": errMsg = "Üye İşyerinin Yönetici Kullanıcısı Bulunuyor."; break;
                case "9035": errMsg = "Üye İşyerine Ait Bir Yönetici Rolü Bulunuyor"; break;
                case "9036": errMsg = "Bloklanmış Kullanıcı"; break;
                case "9037": errMsg = "Kullanıcının Şifre Süresi Dolmuş"; break;
                case "9038": errMsg = "Yeni Şifre Eski Şifreyle Aynı Olamaz"; break;
                case "9039": errMsg = "Üye İşyeri Bulunamadı."; break;
                case "9041": errMsg = "Üye İşyerine Tanımlanabilecek Maksimum Web Sitesi Sayısına Ulaştınız"; break;
                case "9042": errMsg = "Üye İşyerinin 1 Adet Ekstre Tipinde E-Postası Olmak Zorundadır"; break;
                case "9043": errMsg = "Üye İşyerine Tanımlanabilecek Maksimum E- Posta Sayısına Ulaştınız"; break;
                case "9044": errMsg = "Üye İşyerinin Sadece 1 Adet Ekstre Tipinde E- Postası Olabilir"; break;
                case "9045": errMsg = "Üye İşyeri E-Posta Adreslerinin E-Posta Tipi Değiştirilemez"; break;
                case "9046": errMsg = "Ip Tipi Bulunamadı"; break;
                case "9047": errMsg = "Ip Hatalıdır"; break;
                case "9048": errMsg = "Ip Aralık Sonu Hatalıdır"; break;
                case "9049": errMsg = "Banka Bulunamadı"; break;
                case "9050": errMsg = "Kart Sağlayıcı Alanı Zorunlu"; break;
                case "9051": errMsg = "Bin Boş Bırakılamaz"; break;
                case "9052": errMsg = "Bin 6 Karakter Olmalı"; break;
                case "9053": errMsg = "Kart Tipi Boş Bırakılamaz"; break;
                case "9054": errMsg = "Bin Kaydı Zaten Mevcut"; break;
                case "9055": errMsg = "Bankaya Ait Bin Bulunamıyor"; break;
                case "9056": errMsg = "Dosya Boş"; break;
                case "9057": errMsg = "Zaman 4 Aydan Büyük Olamaz"; break;
                case "9058": errMsg = "Tutar Hatalı"; break;
                case "9059": errMsg = "Para Birimi Hatalı"; break;
                case "9060": errMsg = "Kredi Kartı Numarası Hatalı"; break;
                case "9061": errMsg = "Cvv2 Hatalı"; break;
                case "9062": errMsg = "Brand Hatalı"; break;
                case "9063": errMsg = "Son Kullanma Tarihi Hatalı"; break;
                case "9064": errMsg = "Referans Transaction Numarası Hatalı"; break;
                case "9065": errMsg = "Üye İşyeri Bulunamadı"; break;
                case "9066": errMsg = "Üye İşyeri Pasif"; break;
                case "9067": errMsg = "Transaction Numarası Hatalı"; break;
                case "9068": errMsg = "Sipariş Numarası Hatalı"; break;
                case "9069": errMsg = "Sipariş Açıklaması Hatalı"; break;
                case "9070": errMsg = "Ip Adresi Hatalı"; break;
                case "9071": errMsg = "Kart Sahibi Hatalı"; break;
                case "9072": errMsg = "Referans İşlem İade Edilmiştir."; break;
                case "9073": errMsg = "Ön Provizyon İptal Edilmiş"; break;
                case "9074": errMsg = "İşlem Teknik Sebeplerden Dolayı İptal Edilmiş."; break;
                case "9075": errMsg = "Referans İşlem Geçersiz"; break;
                case "9076": errMsg = "Puan İle Satışta Taksit Sayısı Gönderilemez"; break;
                case "9077": errMsg = "İşlem Zaten İade Edilmiş"; break;
                case "9078": errMsg = "İşlem Zaten İptal Edilmiş"; break;
                case "9079": errMsg = "Geçersiz İade Tutarı"; break;
                case "9080": errMsg = "Puan Tutarı Hatalı"; break;
                case "9081": errMsg = "Terminal Bulunamadı"; break;
                case "9083": errMsg = "Kullanıcı Ldap Bilgileri Hatalı"; break;
                case "9084": errMsg = "Tek Kullanımlık Şifre Girişi Yapılmalıdır"; break;
                case "9085": errMsg = "Kullanıcı Ldap Sisteminde Bulunamadı"; break;
                case "9086": errMsg = "Tek Kullanımlık Şifre Bulunamadı"; break;
                case "9087": errMsg = "Girilen Tek Kullanımlık Şifre Yanlış"; break;
                case "9088": errMsg = "Tek Kullanımlık Şifrenin Süresi Doldu"; break;
                case "9089": errMsg = "Mesaj Servisi Hata Aldı"; break;
                case "9090": errMsg = "Hatalı Kullanıcı E-Posta"; break;
                case "9091": errMsg = "Puan Kodu Zorunlu"; break;
                case "9092": errMsg = "Hatalı E-Posta Tipi"; break;
                case "9093": errMsg = "Rol Pasif Yapılamaz. Bu Role Sahip Aktif Kullanıcılar Bulunmaktadır."; break;
                case "9094": errMsg = "Kullanıcı Rolü Aktif Değil Ya Da Silinmiş."; break;
                case "9096": errMsg = "Üye İşyeri Yönetici Rolü Eklemek İçin Üye İşyeri Seçmelisiniz."; break;
                case "9097": errMsg = "Geçersiz Müşteri Bilgisi"; break;
                case "9098": errMsg = "Geçersiz İşlem Tarihi"; break;
                case "9099": errMsg = "Geçersiz İşlem Tipi"; break;
                case "9100": errMsg = "Geçersiz Tekrar Sayısı"; break;
                case "9101": errMsg = "Geçersiz Tekrarlama Aralığı"; break;
                case "9102": errMsg = "Geçersiz Periyot Tipi"; break;
                case "9103": errMsg = "Kaydın Statüsü Bu İşlem İçin Uygun Değil"; break;
                case "9104": errMsg = "City Information Is Not Found."; break;
                case "9105": errMsg = "Town Information Is Not Found."; break;
                case "9106": errMsg = "Customer Name Information Is Not Found."; break;
                case "9107": errMsg = "Customer Surname Information Is Not Found."; break;
                case "9108": errMsg = "Invalid Customer Number."; break;
                case "9109": errMsg = "Invalid Customer Email Information."; break;
                case "9110": errMsg = "Bu Müşteri Numarasına Sahip Zaten Bir Müşteri Mevcut"; break;
                case "9111": errMsg = "Role Atanan Üye İşyeri Numarası İle Kullanıcıya Atanan Numara Eşleşmiyor."; break;
                case "9112": errMsg = "İşlem Numarası Zaten Mevcut"; break;
                case "9113": errMsg = "Tekrarlama Aralığı Değeri Boş Bırakılamaz."; break;
                case "9114": errMsg = "Pancode Zorunlu"; break;
                case "9115": errMsg = "Pancode Zaten Mevcut"; break;
                case "9116": errMsg = "Müşteri Ad-Soyad Uzunlugu Hatalı"; break;
                case "9117": errMsg = "3Dsecure Islemlerde Ecı Degeri Bos Olamaz"; break;
                case "9118": errMsg = "Bu Batch Daha Önce Kapatılmıştır."; break;
                case "9121": errMsg = "Lütfen Geçerli Bir Tarih Giriniz."; break;
                case "9200": errMsg = "Gib Taksit Yetkisi Sadece Gib Üye İşyerlerine Verilebilir."; break;
                case "9201": errMsg = "Girilen Üye İş Yeri Numarası Zaten Sisteme Kayıtlıdır."; break;
                case "9553": errMsg = "Response Kodu Boş"; break;
                case "9578": errMsg = "Client Request Id Boş"; break;
                case "9579": errMsg = "Client Request Id Çok Uzun"; break;
                case "9580": errMsg = "Client Id Boş"; break;
                case "9581": errMsg = "Client Şifre Boş"; break;
                case "9582": errMsg = "Client Şifre Çok Uzun"; break;
                case "9583": errMsg = "Client Request Zamanı Boş"; break;
                case "9587": errMsg = "Client Id Ya Da Şifre Hatalı"; break;
                case "9595": errMsg = "Kayıt Zaten Mevcut"; break;
                case "9601": errMsg = "Kayıt Bulunamadı"; break;
                case "9602": errMsg = "Yeni Kayıt Silinmiş Olamaz"; break;
                case "9603": errMsg = "Kayıt Bilgisi Boş Olamaz"; break;
                case "9612": errMsg = "Sayfa No En Az 1 Olabilir"; break;
                case "9614": errMsg = "Sayfadaki Kayıt Sayısı En Az 1 Olabilir"; break;
                case "9615": errMsg = "En Fazla 50 Kayıt Listelenebilir"; break;
                case "9993": errMsg = "Url Formatı Yanlış"; break;
                case "9994": errMsg = "Döküman Başlığı Formatı Yanlış"; break;
                case "9995": errMsg = "Dil Kodu Bilinmiyor"; break;
                case "9996": errMsg = "Geçersiz Döküman"; break;
                case "9997": errMsg = "Url Bulunamadı"; break;
                case "9998": errMsg = "Döküman Başlığı Bulunamadı"; break;
                case "9999": errMsg = "Döküman Açıklaması Bulunamadı"; break;
                default:
                    break;
            }

            return errMsg;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
