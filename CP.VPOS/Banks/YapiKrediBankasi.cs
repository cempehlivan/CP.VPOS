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
using System.Text;

namespace CP.VPOS.Banks.YapiKrediBankasi
{
    internal class YapiKrediBankasiVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://setmpos.ykb.com/PosnetWebService/XML";
        private readonly string _urlAPILive = "https://posnet.yapikredi.com.tr/PosnetWebService/XML";

        private readonly string _url3Dtest = "https://setmpos.ykb.com/3DSWebService/YKBPaymentService";
        private readonly string _url3DLive = "https://posnet.yapikredi.com.tr/3DSWebService/YKBPaymentService";


        //Panel: https://setmpos.ykb.com/PosnetF1/Login.jsp


        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request.saleInfo.installment == 1)
                request.saleInfo.installment = 0;

            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            SaleResponse response = new SaleResponse();


            Dictionary<string, string> req1 = new Dictionary<string, string>
            {
                {"xmldata", $@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
<posnetRequest>
<mid>{auth.merchantID}</mid>
<tid>{auth.merchantUser}</tid>
<tranDateRequired>1</tranDateRequired>
<sale>
    <amount>{request.saleInfo.amount.To2Digit()}</amount>
    <ccno>{request.saleInfo.cardNumber}</ccno>
    <currencyCode>{request.saleInfo.currency.ToYKBCurrency()}</currencyCode>
    <cvc>{request.saleInfo.cardCVV}</cvc>
    <expDate>{request.saleInfo.cardExpiryDateYear.ToString().Substring(2) + request.saleInfo.cardExpiryDateMonth.ToString("00")}</expDate>
    <orderID>{request.orderNumber.ToOrderNumber(24)}</orderID>
    <installment>{request.saleInfo.installment.ToString("00")}</installment>
    <!-- <koiCode>1</koiCode> -->
</sale>
</posnetRequest> " }
            };

            string resp1 = this.Request(req1, auth.testPlatform ? _urlAPITest : _urlAPILive);


            Dictionary<string, object> respdic = FoundationHelper.XmltoDictionary(resp1, "posnetResponse");

            if (respdic.ContainsKey("approved") && respdic["approved"].ToString() == "1")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Success,
                    message = "İşlem başarıyla tamamlandı",
                    orderNumber = request.orderNumber.ToOrderNumber(24),
                    privateResponse = respdic,
                    transactionId = respdic["hostlogkey"].ToString()
                };
            }
            else if (respdic.ContainsKey("approved") && respdic["approved"].ToString() == "0")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = respdic["respText"].ToString(),
                    orderNumber = request.orderNumber.ToOrderNumber(24),
                    privateResponse = respdic,
                    transactionId = null
                };
            }
            else if (respdic.ContainsKey("approved") && respdic["approved"].ToString() == "2")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = respdic["respText"].ToString(),
                    orderNumber = request.orderNumber.ToOrderNumber(24),
                    privateResponse = respdic,
                    transactionId = respdic["hostlogkey"].ToString()
                };
            }


            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            string firstHash = HASH(auth.merchantStorekey + ';' + auth.merchantUser);

            string amount = request.amount != null && request.amount > 0 ? Convert.ToDecimal(request.amount).To2Digit() : request.responseArray["Amount"].ToString();

            string mac = HASH(request.responseArray["Xid"].ToString() + ';' + amount + ';' + request.currency.ToYKBCurrency() + ';' + auth.merchantID + ';' + firstHash);


            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"xmldata", $@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
<posnetRequest>
<mid>{auth.merchantID}</mid>
<tid>{auth.merchantUser}</tid>
<oosResolveMerchantData>
<bankData>{request.responseArray["BankPacket"].ToString()}</bankData>
<merchantData>{request.responseArray["MerchantPacket"].ToString()}</merchantData>
<sign>{request.responseArray["Sign"].ToString()}</sign>
<mac>{mac}</mac>
</oosResolveMerchantData>
</posnetRequest>" },
            };

            string resp = this.Request(keyValuePairs, auth.testPlatform ? _urlAPITest : _urlAPILive);

            Dictionary<string, object> dic = FoundationHelper.XmltoDictionary(resp, "posnetResponse");

            if (dic.ContainsKey("approved") && dic["approved"].ToString() == "0")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = dic["respText"].ToString(),
                    orderNumber = null,
                    privateResponse = dic,
                    transactionId = null
                };

                return response;
            }

            var oos = dic["oosResolveMerchantDataResponse"] as Dictionary<string, object>;

            if (oos["mdStatus"].ToString() != "1" && !(oos["mdStatus"].ToString() == "9" && auth.testPlatform))
            {
                string message = "";


                switch (oos["mdStatus"].ToString())
                {
                    case "0": message = "Kart doğrulama başarısız, işleme devam etmeyin"; break;
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
                    orderNumber = null,
                    privateResponse = dic,
                    transactionId = null
                };

                return response;
            }

            Dictionary<string, string> req3 = new Dictionary<string, string>
            {
                { "xmldata", $@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
<posnetRequest>
<mid>{auth.merchantID}</mid>
<tid>{auth.merchantUser}</tid>
<oosTranData>
<bankData>{request.responseArray["BankPacket"].ToString()}</bankData>
<mac>{mac}</mac>
</oosTranData>
</posnetRequest>" }

            };

            string resp3 = this.Request(req3, auth.testPlatform ? _urlAPITest : _urlAPILive);

            Dictionary<string, object> respdic = FoundationHelper.XmltoDictionary(resp3, "posnetResponse");

            if (respdic.ContainsKey("approved") && respdic["approved"].ToString() == "1")
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Success,
                    message = "İşlem başarıyla tamamlandı",
                    orderNumber = request.responseArray["Xid"].ToString(),
                    privateResponse = respdic,
                    transactionId = respdic["hostlogkey"].ToString()
                };
            }
            else
            {
                response = new SaleResponse
                {
                    statu = SaleResponseStatu.Error,
                    message = respdic["respText"].ToString(),
                    orderNumber = request.responseArray["Xid"].ToString(),
                    privateResponse = respdic,
                    transactionId = respdic.ContainsKey("hostlogkey") ? respdic["hostlogkey"].ToString() : ""
                };
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            return new CancelResponse { statu = ResponseStatu.Error, message = "Bu banka için iptal metodu tanımlanmamış!" };
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            return new RefundResponse { statu = ResponseStatu.Error, message = "Bu banka için iptal metodu tanımlanmamış!" };
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            //ilk adım
            //ilk adımda posnetData, posnetData2, digest oluşturulmalı

            Dictionary<string, string> req1 = new Dictionary<string, string>{
                {"xmldata",
$@"<?xml version=""1.0"" encoding=""ISO-8859-9""?>
<posnetRequest>
<mid>{auth.merchantID}</mid>
<tid>{auth.merchantUser}</tid>
    <oosRequestData>
    <posnetid>{auth.merchantPassword}</posnetid>
    <XID>{request.orderNumber.ToOrderNumber()}</XID>
    <amount>{request.saleInfo.amount.To2Digit()}</amount>
    <currencyCode>{request.saleInfo.currency.ToYKBCurrency()}</currencyCode>
    <installment>{request.saleInfo.installment.ToString("00")}</installment>
    <tranType>Sale</tranType>
    <cardHolderName>{request.saleInfo.cardNameSurname}</cardHolderName>
    <ccno>{request.saleInfo.cardNumber}</ccno>
    <expDate>{request.saleInfo.cardExpiryDateYear.ToString().Substring(2) + request.saleInfo.cardExpiryDateMonth.ToString("00")}</expDate>
    <cvc>{request.saleInfo.cardCVV}</cvc>
    </oosRequestData>
</posnetRequest>" }
            };

            string resp1 = this.Request(req1, auth.testPlatform ? _urlAPITest : _urlAPILive);

            Dictionary<string, object> xml1 = FoundationHelper.XmltoDictionary(resp1, "posnetResponse");

            if (xml1.ContainsKey("approved") && xml1["approved"].ToString() == "0")
            {
                response.orderNumber = request.orderNumber.ToOrderNumber();
                response.statu = SaleResponseStatu.Error;
                response.privateResponse = xml1;
                response.message = xml1.ContainsKey("respText") ? xml1["respText"].ToString() : "İşlem sırasında hata oluştu";

                return response;
            }

            //ikinci adım
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                {"mid", auth.merchantID },
                {"posnetID", auth.merchantPassword },
                {"posnetData", ((Dictionary<string,object>)xml1["oosRequestDataResponse"])["data1"].ToString() },
                {"posnetData2", ((Dictionary<string,object>)xml1["oosRequestDataResponse"])["data2"].ToString() },
                {"digest", ((Dictionary<string,object>)xml1["oosRequestDataResponse"])["sign"].ToString()},
              //  {"vftCode", "" }, // -vade Farklı İşlem kampanya kodu
                //{"useJokerVadaa", "1" }, //useJokerVadaa Opsiyonel
                {"merchantReturnURL", request.payment3D.returnURL },
                {"lang", "tr" },
            };

            string htmlForm = param.ToHtmlForm(auth.testPlatform ? _url3Dtest : _url3DLive);


            response.statu = SaleResponseStatu.RedirectHTML;
            response.message = htmlForm;
            response.orderNumber = request.orderNumber.ToOrderNumber();
            response.privateResponse = null;
            response.transactionId = null;

            return response;
        }

        private string Request(Dictionary<string, string> param, string link)
        {
            string responseString = "";
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var ykbRequest = new FormUrlEncodedContent(param))
            {
                var response = client.PostAsync(link, ykbRequest).Result;
                
                try
                {
                    responseString = response.Content.ReadAsStringAsync().Result;
                }
                catch
                {
                    using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
            }

            return responseString;
        }

        private string HASH(string originalString)
        {
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));
                return Convert.ToBase64String(bytes);
            }
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
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

        public static string ToOrderNumber(this string orderNumber, int length = 20)
        {
            int fark = length - orderNumber.Length;

            for (int i = 0; i < fark; i++)
                orderNumber = "0" + orderNumber;

            return orderNumber;
        }

        public static string ToYKBCurrency(this Currency? currency)
        {
            string cr = "";

            switch (currency)
            {
                case Currency.TRY:
                    cr = "TL";
                    break;
                case Currency.USD:
                    cr = "US";
                    break;
                case Currency.EUR:
                    cr = "EU";
                    break;
                case Currency.GBP:
                    break;
                case Currency.JPY:
                    break;
                case Currency.RUB:
                    break;
                default:
                    break;
            }

            return cr;
        }
    }
}
