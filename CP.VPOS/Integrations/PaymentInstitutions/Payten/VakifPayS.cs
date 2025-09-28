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

namespace CP.VPOS.Banks.VakifPayS
{
    internal class VakifPaySVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://testpos.vakifpays.com.tr/vakifpays/api/v2";
        private readonly string _urlAPILive = "https://pos.vakifpays.com.tr/vakifpays/api/v2";

        private readonly string _url3Dtest = "https://testpos.vakifpays.com.tr/vakifpays/api/v2/post/sale3d/{0}";
        private readonly string _url3DLive = "https://pos.vakifpays.com.tr/vakifpays/api/v2/post/sale3d/{0}";

        private readonly string _org_id = "6bmm5c3v"; // VakifPayS için Online Metrix Org ID

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            SaleResponse response = new SaleResponse();

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ACTION", "SALE" },
                {"MERCHANTPAYMENTID", request.orderNumber },
                {"MERCHANTUSER", auth.merchantUser },
                {"MERCHANTPASSWORD", auth.merchantPassword },
                {"MERCHANT", auth.merchantID },
                {"CUSTOMER", request.invoiceInfo.taxNumber},
                {"CUSTOMERNAME",request.invoiceInfo.name},
                {"CUSTOMEREMAIL", request.invoiceInfo.emailAddress},
                {"CUSTOMERIP", request.customerIPAddress},
                {"CUSTOMERUSERAGENT", ""},
                {"CUSTOMERPHONE", request.invoiceInfo.phoneNumber},

                {"BILLTOADDRESSLINE", request.invoiceInfo.addressDesc},
                {"BILLTOCITY", request.invoiceInfo.cityName},
                {"BILLTOCOUNTRY",request.invoiceInfo.country.ToString()},
                {"BILLTOPOSTALCODE",request.invoiceInfo.postCode},
                {"BILLTOPHONE", request.invoiceInfo.phoneNumber},

                {"SHIPTOADDRESSLINE", request.shippingInfo.addressDesc},
                {"SHIPTOCITY", request.shippingInfo.cityName},
                {"SHIPTOCOUNTRY", request.shippingInfo.country.ToString() },
                {"SHIPTOPOSTALCODE", request.shippingInfo.postCode},
                {"SHIPTOPHONE", request.shippingInfo.phoneNumber},

                {"NAMEONCARD", request.saleInfo.cardNameSurname},
                {"CARDPAN", request.saleInfo.cardNumber},
                {"CARDEXPIRY", request.saleInfo.cardExpiryDateMonth.ToString("00") + "." + request.saleInfo.cardExpiryDateYear.ToString()},
                {"CARDCVV",request.saleInfo.cardCVV},
                {"AMOUNT", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"CURRENCY", request.saleInfo.currency.ToString() },
                {"INSTALLMENTS", request.saleInfo.installment.ToString() }
            };

            string res = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(res);

            if (dic["responseCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = dic["responseMsg"].cpToString() + " - işlem başarılı";
            }
            else
            {
                response.statu = SaleResponseStatu.Error;
                response.message = dic["responseMsg"].cpToString() + " - " + dic["errorMsg"].cpToString();
            }

            response.privateResponse = dic;
            response.orderNumber = request.orderNumber;

            if (dic.ContainsKey("pgTranId"))
                response.transactionId = dic["pgTranId"].cpToString();

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse();

            Dictionary<string, string> req = new Dictionary<string, string>
            {
                {"ACTION", "QUERYPAYMENTSYSTEMS" },
                {"MERCHANTUSER", auth.merchantUser },
                {"MERCHANTPASSWORD", auth.merchantPassword },
                {"MERCHANT", auth.merchantID },
                {"BIN", request.BIN }
            };

            string res = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(res);

            if (dic["responseCode"].cpToString() == "00" && dic.ContainsKey("installmentPaymentSystem"))
            {
                response.confirm = true;

                var asd = dic["installmentPaymentSystem"];

                var bak = asd.GetValue("installmentList");

                List<Dictionary<string, object>> keyValuePairs = JsonConvertHelper.Convert<List<Dictionary<string, object>>>(JsonConvertHelper.Json<object>(bak));

                if (keyValuePairs != null && keyValuePairs.Any())
                {
                    response.installmentList = new List<installment>();

                    foreach (var item in keyValuePairs)
                    {
                        response.installmentList.Add(new
                        installment
                        {
                            count = item["count"].cpToInt(),
                            customerCostCommissionRate = item["customerCostCommissionRate"].cpToSingle()
                        });
                    }
                }
            }

            return response;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse();

            Dictionary<string, string> req = new Dictionary<string, string>
            {
                {"ACTION", "QUERYINSTALLMENT" },
                {"MERCHANT", auth.merchantID },
                {"MERCHANTUSER", auth.merchantUser },
                {"MERCHANTPASSWORD", auth.merchantPassword },
                {"STATUS", "OK" },
            };

            if (!auth.merchantStorekey.cpIsNullOrEmpty())
                req.Add("DEALERTYPENAME", auth.merchantStorekey);

            string res = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(res);

            if (dic["responseCode"].cpToString() == "00" && dic.ContainsKey("paymentSystemList"))
            {
                response.confirm = true;

                List<Dictionary<string, object>> paymentSystemList = JsonConvertHelper.Convert<List<Dictionary<string, object>>>(JsonConvertHelper.Json<object>(dic["paymentSystemList"]));

                if (paymentSystemList != null && paymentSystemList.Any())
                {
                    response.installmentList = new List<AllInstallment>();

                    foreach (var paymentSystem in paymentSystemList)
                    {
                        if (paymentSystem["status"].cpToString() != "OK")
                            continue;

                        List<Dictionary<string, object>> installmentList = JsonConvertHelper.Convert<List<Dictionary<string, object>>>(JsonConvertHelper.Json<object>(paymentSystem["installmentList"]));

                        foreach (var installmentt in installmentList)
                        {
                            if (installmentt["status"].cpToString() != "OK")
                                continue;

                            response.installmentList.Add(new AllInstallment
                            {
                                bankCode = paymentSystem["eftCode"].cpToString(),
                                count = installmentt["count"].cpToInt(),
                                customerCostCommissionRate = installmentt["interestRate"].cpToSingle()
                            });
                        }
                    }
                }
            }

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            if (request.responseArray["responseCode"].cpToString() == "00")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = request.responseArray["responseMsg"].cpToString() + " - işlem başarılı";
            }
            else
            {
                string errText = "";

                if (request.responseArray.ContainsKey("errorCode"))
                    errText = this.getErrorDesc(request.responseArray["errorCode"].cpToString());

                if (errText.cpIsNullOrEmpty() && request.responseArray.ContainsKey("pgTranErrorText"))
                    errText = request.responseArray["pgTranErrorText"].cpToString();

                if (string.IsNullOrWhiteSpace(errText))
                    errText = "İşlem sırasında hata oluştu. Lütfen tekrar deneyiniz.";

                response.statu = SaleResponseStatu.Error;
                response.message = request.responseArray["responseMsg"].cpToString() + " - " + errText;
            }

            if (request.responseArray.ContainsKey("merchantPaymentId"))
                response.orderNumber = request.responseArray["merchantPaymentId"].cpToString();


            response.privateResponse = request.responseArray;

            if (request.responseArray.ContainsKey("pgTranId"))
                response.transactionId = request.responseArray["pgTranId"].cpToString();

            return response;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ACTION", "VOID" },
                {"MERCHANTUSER", auth.merchantUser },
                {"MERCHANTPASSWORD", auth.merchantPassword },
                {"MERCHANT", auth.merchantID },
                {"PGTRANID", request.transactionId },
                {"REFLECTCOMMISSION", "No" },
            };

            string res = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(res);

            response.privateResponse = dic;

            if (dic["responseCode"].cpToString() == "00")
            {
                response.statu = ResponseStatu.Success;
                response.message = dic["responseMsg"].cpToString() + " - işlem başarılı";
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["responseMsg"].cpToString() + " - " + dic["errorMsg"].cpToString();
            }


            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ACTION", "REFUND" },
                {"MERCHANTUSER", auth.merchantUser },
                {"MERCHANTPASSWORD", auth.merchantPassword },
                {"MERCHANT", auth.merchantID },
                {"AMOUNT", request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"CURRENCY", request.currency.ToString() },
                {"PGTRANID", request.transactionId },
                {"REFLECTCOMMISSION", "No" },
            };

            string res = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(res);

            response.privateResponse = dic;

            if (dic["responseCode"].cpToString() == "00")
            {
                response.statu = ResponseStatu.Success;
                response.message = dic["responseMsg"].cpToString() + " - işlem başarılı";
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = dic["responseMsg"].cpToString() + " - " + dic["errorMsg"].cpToString();
            }

            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            string sessionToken = "";

            SaleResponse response = new SaleResponse();

            try
            {
                sessionToken = this.getSessionToken(request, auth);
            }
            catch (Exception ex)
            {
                response.statu = SaleResponseStatu.Error;
                response.message = ex.Message;

                return response;
            }

            string link = String.Format((auth.testPlatform ? _url3Dtest : _url3DLive), sessionToken);

            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                {"points", "" },
                {"paymentSystem", "" },
                {"panname", request.saleInfo.cardNameSurname },
                {"cardOwner", request.saleInfo.cardNameSurname },
                {"pan", request.saleInfo.cardNumber },
                {"expiryMonth",  request.saleInfo.cardExpiryDateMonth.ToString("00") },
                {"expiryYear", request.saleInfo.cardExpiryDateYear.ToString() },
                {"cvv", request.saleInfo.cardCVV },
                {"installmentCount", request.saleInfo.installment.ToString() },
            };

            string res = this.Request(pairs, auth, link);

            Uri uri = new Uri(link);

            string baseUri = uri.Scheme + "://" + uri.Host;


            if (res.cpIsNullOrEmpty() == false)
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = "<base href=\"" + baseUri + "\" /> " + res;

                string thmaxrix = Environment.NewLine
                    + $@"
    <script type=""text/javascript"" src=""https://h.online-metrix.net/fp/tags.js?org_id={_org_id}&amp&session_id={sessionToken}&pageid=1"">
    </script>
    <noscript>
        <iframe style=""width: 100px; height: 100px; border: 0; position: absolute; top: -5000px;"" src=""https://h.online-metrix.net/fp/tags.js?org_id={_org_id}&amp&session_id={sessionToken}&pageid=1""></iframe>
    </noscript>
" + Environment.NewLine;

                if (response.message.Contains("</body>"))
                {
                    thmaxrix += "</body>" + Environment.NewLine;
                    response.message = response.message.Replace("</body>", thmaxrix);
                }
                else
                {
                    response.message += thmaxrix;
                }
            }

            return response;
        }

        private string getSessionToken(SaleRequest request, VirtualPOSAuth auth)
        {
            string pItem = "[{\"code\":\"POSCEK\",\"name\":\"Cari Tahsilat\",\"description\":\"CariTahsilat\",\"quantity\":1,\"amount\":" + request.saleInfo.amount.cpToDecimal().ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") + "}]";

            Dictionary<string, string> req = new Dictionary<string, string> {
                                {"ACTION", "SESSIONTOKEN" },
                                {"SESSIONTYPE","PAYMENTSESSION"},
                                {"MERCHANTPAYMENTID", request.orderNumber },
                                {"MERCHANTUSER", auth.merchantUser },
                                {"MERCHANTPASSWORD", auth.merchantPassword },
                                {"MERCHANT", auth.merchantID },
                                {"CUSTOMER", request.invoiceInfo.taxNumber},
                                {"CUSTOMERNAME",request.invoiceInfo.name},
                                {"CUSTOMEREMAIL", request.invoiceInfo.emailAddress},
                                {"CUSTOMERIP", request.customerIPAddress},
                                {"CUSTOMERPHONE", request.invoiceInfo.phoneNumber},
                                {"ORDERITEMS",pItem},
                                {"RETURNURL", request.payment3D.returnURL },

                                {"BILLTOADDRESSLINE", request.invoiceInfo.addressDesc},
                                {"BILLTOCITY", request.invoiceInfo.cityName},
                                {"BILLTOCOUNTRY",request.invoiceInfo.country.ToString()},
                                {"BILLTOPOSTALCODE",request.invoiceInfo.postCode},
                                {"BILLTOPHONE", request.invoiceInfo.phoneNumber},

                                {"SHIPTOADDRESSLINE", request.shippingInfo.addressDesc},
                                {"SHIPTOCITY", request.shippingInfo.cityName},
                                {"SHIPTOCOUNTRY", request.shippingInfo.country.ToString() },
                                {"SHIPTOPOSTALCODE", request.shippingInfo.postCode},
                                {"SHIPTOPHONE", request.shippingInfo.phoneNumber},

                                {"AMOUNT", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                                {"CURRENCY", request.saleInfo.currency.ToString() },
            };

            string resp = this.Request(req, auth);

            var dic = JsonConvertHelper.Convert<Dictionary<string, object>>(resp);

            if (dic["responseCode"].cpToString() == "00")
                return dic["sessionToken"].cpToString();
            else if (dic.ContainsKey("errorCode"))
            {
                string err = getErrorDesc(dic["errorCode"].cpToString());

                if (string.IsNullOrWhiteSpace(err) && dic.ContainsKey("errorMsg") && dic["errorMsg"].cpToString() != "")
                    err = dic["errorMsg"].cpToString();

                if (!string.IsNullOrWhiteSpace(err))
                    throw new Exception(err);
            }

            throw new Exception("VakıfPayS session token oluşturulamadı");
        }

        private string Request(Dictionary<string, string> param, VirtualPOSAuth auth, string link = null)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var vakifPaySRequest = new FormUrlEncodedContent(param))
            {
                var response = client.PostAsync(link ?? (auth.testPlatform ? _urlAPITest : _urlAPILive), vakifPaySRequest).Result;
                responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }

        private string getErrorDesc(string ErrCode)
        {
            string desc = "";

            switch (ErrCode)
            {
                case "ERR10010": desc = "İstekte zorunlu parametrelerden biri bulunamadı"; break;
                case "ERR10011": desc = "Aynı parametre bir defadan fazla olarak gönderilmiş"; break;
                case "ERR10012": desc = "Bu değer için azami büyüklük değeri aşıldı."; break;
                case "ERR10013": desc = "Bu değer için geçersiz veri tipi belirtilmiş"; break;
                case "ERR10014": desc = "Geçersiz güvenlik algoritması belirtilmiş"; break;
                case "ERR10015": desc = "Geçersiz üye iş yeri bilgisi belirtilmiş"; break;
                case "ERR10016": desc = "Geçersiz tutar bilgisi belirtilmiş"; break;
                case "ERR10017": desc = "Geçersiz para birimi belirtilmiş"; break;
                case "ERR10018": desc = "Geçersiz dil seçimi"; break;
                case "ERR10019": desc = "Genel hata"; break;
                case "ERR10020": desc = "Geçersiz kullanıcı bilgileri"; break;
                case "ERR10021": desc = "Boş parametre belirtilmiş, tüm parametreleri kontrol edin"; break;
                case "ERR10022": desc = "Sipariş edilen ürünlerin toplam tutarı gerçek tutarla örtüşmüyor"; break;
                case "ERR10023": desc = "Ödeme tutarı hesaplanan tutarla örtüşmüyor"; break;
                case "ERR10024": desc = "Geçersiz vergi tutarı belirtilmiş"; break;
                case "ERR10025": desc = "Belirtilen durumda vergi tutarı sıfır olmalıdır"; break;
                case "ERR10026": desc = "Geçersiz entegrasyon modeli belirtilmiş"; break;
                case "ERR10027": desc = "Geçersiz kart bilgisi (TOKEN) belirtilmiş"; break;
                case "ERR10028": desc = "Belirtilen ödeme sistemi (sanal POS) bulunamadı"; break;
                case "ERR10029": desc = "Belirtilen ödeme tipi (kampanya) bulunamadı"; break;
                case "ERR10030": desc = "Belirtilen işlem bulunamadı"; break;
                case "ERR10031": desc = "Bu işlem iade edilemez"; break;
                case "ERR10032": desc = "Geçersiz iade tutarı belirtilmiş ya da bu işlem daha önce iade edilmiş"; break;
                case "ERR10033": desc = "Bu işlem iptal edilemez"; break;
                case "ERR10034": desc = "Belirtilen ödeme bulunamadı"; break;
                case "ERR10035": desc = "Bu işlem için ön otorizasyon kaydı bulunmamaktadır"; break;
                case "ERR10036": desc = "Geçersiz son otorizasyon (POSTAUTH) tutarı belirtilmiş"; break;
                case "ERR10037": desc = "Belirtilen Kart Sahibi (Müşteri) kayıtlı değil"; break;
                case "ERR10038": desc = "İlgili ödeme onay beklemektedir"; break;
                case "ERR10039": desc = "Geçersiz ödeme durumu belirtilmiş"; break;
                case "ERR10040": desc = "Geçersiz alt işlem (SUBACTION) belirtilmiş"; break;
                case "ERR10041": desc = "Belirtilen kart daha önce eklenmiş"; break;
                case "ERR10042": desc = "Kart daha önceden silinmiş"; break;
                case "ERR10043": desc = "Geçersiz zaman aralığı belirtilmiş"; break;
                case "ERR10044": desc = "Geçersiz tarih formatı belirtilmiş"; break;
                case "ERR10045": desc = "Belirtilen kart numarası geçersizdir"; break;
                case "ERR10046": desc = "Belirtilen kredi kartı geçerlilik tarihi geçersizdir"; break;
                case "ERR10047": desc = "Kullanıcının API servislerini kullanma yetkisi bulunmamaktadır"; break;
                case "ERR10048": desc = "Başarılı Bir İşlem zaten bu üye iş yeri sipariş numarası ile var"; break;
                case "ERR10049": desc = "Geçersiz üye iş yeri grup numarası"; break;
                case "ERR10050": desc = "Geçersiz HASH değeri"; break;
                case "ERR10051": desc = "Herhangi bir ödeme sistemi (sanal pos) tanımı yok. Lütfen, kontrol ediniz."; break;
                case "ERR10052": desc = "Desteklenmeyen para birimi:"; break;
                case "ERR10053": desc = "Kullanıcının bu üye iş yeri üzerinde işlem yapma yetkisi yok"; break;
                case "ERR10054": desc = "Ödeme geçerlilik süresi maksimum limitin üstündedir."; break;
                case "ERR10055": desc = "Ödeme geçerlilik süresi minimum limitin altındadır."; break;
                case "ERR10056": desc = "Geçersiz API isteği belirtilmiş"; break;
                case "ERR10057": desc = "Kart BIN bilgisi geçersiz"; break;
                case "ERR10058": desc = "Kart daha önce etkinleştirilmiş"; break;
                case "ERR10059": desc = "Kart daha önce kullanım dışı bırakılmış"; break;
                case "ERR10060": desc = "Geçersiz IP Adresi"; break;
                case "ERR10062": desc = "Belirtilen kart henüz aktive edilmemiştir."; break;
                case "ERR10063": desc = "Bu işlem sadece LetsBodrum kart ile yapılabilir."; break;
                case "ERR10064": desc = "Lütfen LetsBodrum kart veya Türkiye İş Bankası kredi kartı kullanınız."; break;
                case "ERR10065": desc = "Belirtilen kart numarası daha önceden tanımlanmış."; break;
                case "ERR10066": desc = "Belirtilen zaman bilgisi geçersiz ya da tutarsızdır"; break;
                case "ERR10067": desc = "Belirtilen period değeri çok yüksek"; break;
                case "ERR10068": desc = "Geçersiz tekrar düzeni parametresi"; break;
                case "ERR10069": desc = "Zamanlayıcı (Quartz) hatası oluştu"; break;
                case "ERR10070": desc = "Başlangı tarihi gelecekteki bir tarih olmalıdır"; break;
                case "ERR10071": desc = "Geçersiz tekrarlı ödeme durum parametresi belirtilmiş"; break;
                case "ERR10072": desc = "Tekrarlı ödeme planı zaten etkin durumda"; break;
                case "ERR10073": desc = "ERR10073"; break;
                case "ERR10074": desc = "Tekrarlı ödeme planının zaten süresi geçmiş"; break;
                case "ERR10075": desc = "Üye iş yeri görsel (logo) bilgisi hatalı"; break;
                case "ERR10076": desc = "Geçersiz tekrarlı ödeme durum parametresi"; break;
                case "ERR10078": desc = "İşlem kilitlidir"; break;
                case "ERR10079": desc = "Bu kart sistemde kayıtlıdır."; break;
                case "ERR10080": desc = "Lütfen, Üye İş Yeri Sipariş numarasını veya Ödeme Oturumu(Token) veriniz"; break;
                case "ERR10081": desc = "Geçersiz işlem durumu"; break;
                case "ERR10082": desc = "Kullanıcısının bu işlem için yetkisi yoktur."; break;
                case "ERR10083": desc = "Geçersiz statü"; break;
                case "ERR10084": desc = "Faiz veya indirim oranı sıfır değeri olmalıdır"; break;
                case "ERR10085": desc = "Geçerli bitiş tarihi ,geçerli başlangıç tarihten daha büyük olamaz"; break;
                case "ERR10086": desc = "Geçerli bitiş tarihi şimdiki tarihten daha büyük olmalıdır"; break;
                case "ERR10087": desc = "Taksit sayı numarası zaten bu ödeme sistemi ile bir ödeme tipi var"; break;
                case "ERR10088": desc = "Taksit bilgisi 1-12 arasında bir değer olmalıdır."; break;
                case "ERR10089": desc = "Tekrarlı ödemeye ait kart silinemez."; break;
                case "ERR10090": desc = "İşlem başarısız"; break;
                case "ERR10091": desc = "Ödeme sistemi devre dışı bırakıldığı için işlem gerçekleştiremiyor. Lütfen Üye İş Yeri Süper Yöneticisiyle iletişime geçiniz."; break;
                case "ERR10092": desc = "Geçersiz offset değeri"; break;
                case "ERR10093": desc = "Geçersiz limit değeri"; break;
                case "ERR10094": desc = "Tanımlı bir kart bulunamadı."; break;
                case "ERR10095": desc = "Kayıtlı bulunan tekrarlayan ödeme planlarından dolayı kart silinemez."; break;
                case "ERR10096": desc = "Geçersiz oturum (session) bilgisi."; break;
                case "ERR10097": desc = "Sonlandırılmış oturum (session) bilgisi."; break;
                case "ERR10098": desc = "Bu oturum anahtarının yapılmak istenen işleme yetkisi yoktur."; break;
                case "ERR10099": desc = "Bu işlem başka bir üye iş yerine ait."; break;
                case "ERR10100": desc = "Bu ödeme için birden fazla başarılı işlem vardır. Lütfen PGTRANID parametresini kullanınız."; break;
                case "ERR10101": desc = "Geçersiz URL parametresi belirtilmiştir."; break;
                case "ERR10102": desc = "Geçersiz BIN değeri belirtilmiştir."; break;
                case "ERR10103": desc = "İşlem isteği Inact RT servisi tarafından raporlanan fraud olasılığı nedeniyle reddedilmiştir."; break;
                case "ERR10104": desc = "Kullanılabilir komisyon şeması bulunmamaktadır."; break;
                case "ERR10105": desc = "Mevcut Ödeme Sistemi havuzda bulunmamaktadır"; break;
                case "ERR10106": desc = "İşlem tutarı üye iş yeri hesabına geçmemiştir, iade yapılamaz."; break;
                case "ERR10107": desc = "Bu ödeme zaten yapılmıştır, verilen Üye İş Yeri Sipariş Numarası ile yeni ödeme oturumu oluşturulamaz."; break;
                case "ERR10108": desc = "Üye iş yeri onaylanmamış"; break;
                case "ERR10109": desc = "Ödeme havuzu üye iş yeri için henüz onaylanmamıştır."; break;
                case "ERR10110": desc = "Kullanilan ödeme sistemi kampanya kullanımını desteklememektedir."; break;
                case "ERR10111": desc = "Puan sorgulama ödeme sistemi tarafından desteklenmemektedir."; break;
                case "ERR10112": desc = "Hatali puan formatı lütfen API Dokümantasyonundan puan kullanım formatını kontrol ediniz."; break;
                case "ERR10113": desc = "Kullanilan ödeme sistemi puan kullanımını desteklememektedir."; break;
                case "ERR10115": desc = "Üye iş yeri tarafından desteklenmeyen taksit sayısı belirtilmiştir."; break;
                case "ERR10116": desc = "Bu işlem kullanımda olmayan üye iş yeri bilgileriyle gerçekleştirilemez."; break;
                case "ERR10117": desc = "Bu sipariş numarası sonlanan bir oturumda kullanılmıştır lütfen farklı bir sipariş numarası ile oturum anahtarı oluşturun."; break;
                case "ERR10118": desc = "İstek ile mevcut sipariş numarasına ait oturumun tutar,kur,oturum tipi, url dönüş değeri ya da yapılmak istenen işlem değerlerinden biri uyuşmamaktadır."; break;
                case "ERR10119": desc = "Tam ve ya noktalı kısımda limit aşımı"; break;
                case "ERR10120": desc = "Bu plan koduna ait bir tekrarlı ödeme bulunuyor"; break;
                case "ERR10121": desc = "Geçersiz tekrarlı ödeme kodu"; break;
                case "ERR10122": desc = "Sonlanmış durumdaki tekrarlı ödeme güncellenemez."; break;
                case "ERR10123": desc = "Geçersiz işlem tipi"; break;
                case "ERR10125": desc = "Mutabakat sorgusu için en az bir parametre geçilmeli."; break;
                case "ERR10126": desc = "Birden fazla işlem bulundu."; break;
                case "ERR10127": desc = "Ödeme sistemi puan parametresi hatalı, işlemin gönderileceği ödeme sisteminde gönderilen puan parametresi tanımlı değildir."; break;
                case "ERR10128": desc = "Geçersiz parametre değeri"; break;
                case "ERR10129": desc = "Parçalı puan kullanımı bu ödeme sistemi tarafından desteklenmemektedir"; break;
                case "ERR10130": desc = "İşlem fraud süphesiyle reddedilmiştir. Detaylı bilgi için destek ekibiyle iletişime geçebilirsiniz. (TMX rejected)"; break;
                case "ERR10133": desc = "İstenen işlem güncellenemez."; break;
                case "ERR10134": desc = "Ödeme sistemi tipi ya da EFT kodu bulunamadı."; break;
                case "ERR10135": desc = "EXTRA parametresi decode edilemiyor."; break;
                case "ERR10136": desc = "Bu üye iş yeri için ortak ödeme sayfası (HPP) kullanılamaz."; break;
                case "ERR10137": desc = "Query Campaign Not Supported By PaymentSystem"; break;
                case "ERR10138": desc = "3D işlem yaparken hata oluştu."; break;
                case "ERR10139": desc = "Üye İşyeri Entegrasyon Modeli Hatalı"; break;
                case "ERR10140": desc = "İşlem tipi bu ödeme sistemi tarafından desteklenmiyor."; break;
                case "ERR10141": desc = "Beklenemedik ödeme sistemi entegrasyonu hatası"; break;
                case "ERR10142": desc = "Geçersiz Yönlendirme Adresi"; break;
                case "ERR10143": desc = "ÖDENMİŞ veya İPTAL EDİLMİŞ ödeme"; break;
                case "ERR10144": desc = "Üye iş yerinin yabancı banka kartları ile işlem yapma yetkisi yoktur"; break;
                case "ERR10145": desc = "Tekrarlı ödeme bulunamadı."; break;
                case "ERR10146": desc = "Tekrarlı ödeme kartı bulunamadı."; break;
                case "ERR10147": desc = "3D doğrulama olmaksızın kart ekleme yetkiniz yoktur. Lütfen HPP entegrasyon modelini kullanarak kart ekleyiniz ya da VakıfPayS destek ekibinden yardım alınız."; break;
                case "ERR10148": desc = "Tekrarlı ödeme planı zaten bu kart daha önce eklenmiş."; break;
                case "ERR10149": desc = "Bu işlem için desteklenmeyen para birimi"; break;
                case "ERR10150": desc = "İndirim tutarı sipariş tutarından yüksek olamaz."; break;
                case "ERR10151": desc = "Satıcı bulunamadı"; break;
                case "ERR10152": desc = "Bu id ile satıcı mevcuttur."; break;
                case "ERR10153": desc = "İade işlemi VakıfPayS Finans ekibi tarafından red edilmiştir"; break;
                case "ERR10154": desc = "İşlem 3D kısıtlamasıyla başarısız olmuştur."; break;
                case "ERR10155": desc = "Satıcı deaktive durumdadir. Bu işlem yapılamaz."; break;
                case "ERR10156": desc = "Unsupported Currency Conversion"; break;
                case "ERR10157": desc = "Aktivasyon tarihi gelecek tarih olmalıdır"; break;
                case "ERR10158": desc = "Geçersiz varsayılan komisyon oranı"; break;
                case "ERR10159": desc = "Geçersiz ödeme sistemi bazlı komisyon oranı"; break;
                case "ERR10160": desc = "Eksik parametre"; break;
                case "ERR10161": desc = "Ödeme sistemi havuzda bulunamadı"; break;
                case "ERR10162": desc = "Sadece 2 ile 12 arasındaki tüm taksitler parametrede sağlanmalıdır"; break;
                case "ERR10163": desc = "Save card parametresi API entegrasyon modeli için kullanılamaz"; break;
                case "ERR10164": desc = "Bu isimde havuzda kartsız işlem destekleyen ödeme sistemi bulunamadı"; break;
                case "ERR10165": desc = "Komisyon şeması bulunamadı"; break;
                case "ERR10166": desc = "ERR10166"; break;
                case "ERR10167": desc = "Invalid sellerId - do not use semicolon"; break;
                case "ERR10168": desc = "Bu kart markası desteklenmemektedir"; break;
                case "ERR10169": desc = "Taksit bu kart markası için uygun değildir"; break;
                case "ERR10170": desc = "Girilen değer geçerli aralığın dışında. Minimum değer 1 olmalı, maksimum değer için lütfen VakıfPayS Operasyon Ekibi'yle iletişime geçiniz."; break;
                case "ERR10171": desc = "Belirtilen MCC bulunamadı."; break;
                case "ERR10172": desc = "Belirtilen MCC daha önce eklenmiş"; break;
                case "ERR10173": desc = "Ürün komisyon tutarları TOTALSELLERCOMMISSIONAMOUNT parametresinde belirtilen komisyon tutarıyla uyuşmamaktadır."; break;
                case "ERR20001": desc = "Manuel onay için bankanızla iletişime geçiniz"; break;
                case "ERR20002": desc = "Sahte onay, bankanızla iletişime geçiniz"; break;
                case "ERR20003": desc = "Geçersiz üye iş yeri ya da servis sağlayıcı"; break;
                case "ERR20004": desc = "Karta el koyunuz"; break;
                case "ERR20005": desc = "İşleme onay verilmedi"; break;
                case "ERR20006": desc = "Hata (Sanal POS ya da banka tarafında sadece kayıt güncelleme cevapları bulundu)"; break;
                case "ERR20007": desc = "Karta el koyunuz - Özel nedenler"; break;
                case "ERR20008": desc = "Sahte onay, bankanızla iletişime geçiniz"; break;
                case "ERR20009": desc = "İşlem yapılan banka kartına taksit uygulanmamaktadır."; break;
                case "ERR20011": desc = "Sahte onay (VIP), bankanızla iletişime geçiniz"; break;
                case "ERR20012": desc = "Sanal POS ya da banka tarafında geçersiz işlem"; break;
                case "ERR20013": desc = "Sanal POS hatası: Geçersiz tutar bilgisi"; break;
                case "ERR20014": desc = "Geçersiz hesap ya da kart numarası belirtilmiş"; break;
                case "ERR20015": desc = "Böyle bir banka (issuer) bulunamadı"; break;
                case "ERR20019": desc = "Sanal POS hatası: Tekrar deneyiniz"; break;
                case "ERR20020": desc = "Sanal POS hatası: Geçersiz / Hatalı tutar"; break;
                case "ERR20021": desc = "Banka / Sanal POS tarafında işlem yapılamıyor"; break;
                case "ERR20025": desc = "Sanal POS hatası: Kayıt oluşturulamadı"; break;
                case "ERR20026": desc = "Sanal POS tarafında işlem bulunamadı"; break;
                case "ERR20027": desc = "Sanal POS hatası: Banka reddetti"; break;
                case "ERR20028": desc = "Sanal POS hatası: Original is denied"; break;
                case "ERR20029": desc = "Sanal POS hatası: Original not found"; break;
                case "ERR20030": desc = "Sanal POS tarafında switch bazlı format hatası"; break;
                case "ERR20032": desc = "Sanal POS tarafında genel yönlendirme hatası"; break;
                case "ERR20033": desc = "Belirtilen kredi kartının geçerlilik süresi bitmiştir"; break;
                case "ERR20034": desc = "İşlemde sahtecilik (fraud) şüphesi"; break;
                case "ERR20036": desc = "Sanal POS hatası: Kısıtlanmış kart"; break;
                case "ERR20037": desc = "Sanal POS hatası: Banka (Issuer) kartı geri çağrıyor"; break;
                case "ERR20038": desc = "Sanal POS hatası: İzin verilen PIN deneme sayısı aşıldı"; break;
                case "ERR20040": desc = "Sanal POS hatası: İade işlemi gün sonundan önce yapılamaz"; break;
                case "ERR20041": desc = "Sanal POS hatası: Kayıp kart, karta el koyunuz"; break;
                case "ERR20043": desc = "Sanal POS hatası: Çalıntı kart, karta el koyunuz"; break;
                case "ERR20045": desc = "Puan kullanılan işlemlerde iade desteklenmemektedir. Lütfen bankanızla iletişime geçiniz."; break;
                case "ERR20051": desc = "Belirtilen kredi kartının limiti bu işlem için yeterli değildir"; break;
                case "ERR20052": desc = "Sanal POS hatası: Çek hesabı bulunamadı"; break;
                case "ERR20053": desc = "Sanal POS hatası: Tasarruf hesabı bulunamadı"; break;
                case "ERR20054": desc = "Kartın kullanım süresi geçmiş"; break;
                case "ERR20055": desc = "Sanal POS hatası: Hatalı / Geçersiz PIN değeri"; break;
                case "ERR20056": desc = "Sanal POS hatası: Kart bilgisi bulunamadı"; break;
                case "ERR20057": desc = "Kart sahibine bu işlem yetkisi verilmemiştir"; break;
                case "ERR20058": desc = "Terminale bu işlem izni verilmemiştir"; break;
                case "ERR20059": desc = "İşlemde sahtecilik (fraud) şüphesi vardır"; break;
                case "ERR20061": desc = "Sanal POS hatası: Beklenen işlem tutar sınırı aşıldı"; break;
                case "ERR20062": desc = "Belirtilen kredi kartı kısıtlanmıştır"; break;
                case "ERR20063": desc = "Sanal POS tarafında güvenlik ihlali durumu"; break;
                case "ERR20065": desc = "Sanal POS hatası: Beklenen işlem sınırı aşıldı"; break;
                case "ERR20075": desc = "Sanal POS hatası: İzin verilen PIN deneme sayısı aşıldı"; break;
                case "ERR20076": desc = "Sanal POS anahtar eşzamanlama hatası"; break;
                case "ERR20077": desc = "Sanal POS hatası: Geçersiz / Tutarsız bilgi gönderildi"; break;
                case "ERR20080": desc = "Geçersiz tarih bilgisi"; break;
                case "ERR20081": desc = "Sanal POS şifreleme hatası"; break;
                case "ERR20082": desc = "Geçersiz / Hatalı CVV değeri"; break;
                case "ERR20083": desc = "PIN değeri doğrulanamıyor"; break;
                case "ERR20084": desc = "Geçersiz / Hatalı CVV değeri"; break;
                case "ERR20085": desc = "Sanal POS tarafında reddedildi (Genel)"; break;
                case "ERR20086": desc = "Doğrulanamadı"; break;
                case "ERR20091": desc = "Banka / Sanal POS şu an işlem gerçekleştiremiyor"; break;
                case "ERR20092": desc = "Zaman aşımı nedeniyle teknik iptal gerçekleşitiriliyor"; break;
                case "ERR20093": desc = "Kartınız e-ticaret işlemlerine kapalıdır. Bankanızı arayınız."; break;
                case "ERR20096": desc = "Sanal POS tarafında genel hata"; break;
                case "ERR20098": desc = "Çoklu iptal (Duplicate reversal)"; break;
                case "ERR20099": desc = "Lütfen yeniden deneyiniz, sorun devam ederse bankanızla iletişime geçiniz."; break;
                case "ERR200YK": desc = "Kart kara listede bulunuyor"; break;
                case "ERR200SF": desc = "Detaylar için sanal POS cevabındaki HOSTMSG alanını kontrol ediniz."; break;
                case "ERR200GK": desc = "Sanal POS hatası: Bu terminalde yanabcı kartlar için yetki bulunmamaktadır."; break;
                case "ERR30002": desc = "3D işlemi başarılı şekilde sonlanmadı."; break;
                case "ERR30004": desc = "Bu istek fraud (sahtecilik) kuralları tarafından reddedilmiştir."; break;
                default:
                    break;
            }

            return desc;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
