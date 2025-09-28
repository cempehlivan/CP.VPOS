using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CP.VPOS.Banks.Moka
{
    internal class MokaVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://service.refmoka.com";
        private readonly string _urlAPILive = "https://service.moka.com";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.statu = SaleResponseStatu.Error;

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;

            response.orderNumber = request.orderNumber;

            string checkKey = GetCheckKey(auth);

            string _currency = request.saleInfo.currency == Currency.TRY ? "TL" : request.saleInfo.currency.ToString();

            Dictionary<string, object> saleDic = new Dictionary<string, object>
            {
                { "CardHolderFullName", request.saleInfo.cardNameSurname },
                { "CardNumber", request.saleInfo.cardNumber },
                { "ExpMonth", request.saleInfo.cardExpiryDateMonth.ToString("00") },
                { "ExpYear", request.saleInfo.cardExpiryDateYear.ToString() },
                { "CvcNumber", request.saleInfo.cardCVV },
                { "Amount", Math.Round(request.saleInfo.amount, 2) },
                { "Currency", _currency },
                { "InstallmentNumber", request.saleInfo.installment },
                { "ClientIP", request.customerIPAddress },
                { "OtherTrxCode", request.orderNumber },
                { "IsPoolPayment", 0 },
                { "IsTokenized", 0 },
                { "Software", "cp.vpos" },
                { "IsPreAuth", 0 },
            };

            if (request.payment3D?.confirm == true)
            {
                saleDic.Add("ReturnHash", 1);
                saleDic.Add("RedirectType", 0);
                saleDic.Add("RedirectUrl", request.payment3D.returnURL);
            }

            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "PaymentDealerAuthentication", new Dictionary<string, string>
                    {
                        { "DealerCode", auth.merchantID },
                        { "Username", auth.merchantUser },
                        { "Password", auth.merchantPassword },
                        { "CheckKey", checkKey },
                    }
                },
                { "PaymentDealerRequest",  saleDic}
            };


            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/DoDirectPayment";

            if (request.payment3D?.confirm == true)
                link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/DoDirectPaymentThreeD";

            string responseStr = Request(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString().ToLower() == "success")
            {
                if (responseDic?.ContainsKey("Data") == true && responseDic["Data"] != null)
                {
                    Dictionary<string, object> responseData = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["Data"]));


                    if (request.payment3D?.confirm == true)
                    {
                        if (responseData?.ContainsKey("Url") == true && responseData["Url"].cpToString() != "")
                        {
                            response.statu = SaleResponseStatu.RedirectURL;
                            response.message = responseData["Url"].cpToString();

                            return response;
                        }
                    }
                    else
                    {
                        if (responseData?.ContainsKey("IsSuccessful") == true && responseData["IsSuccessful"].cpToBool() == true)
                        {
                            string transactrionId = "";

                            if (responseData?.ContainsKey("VirtualPosOrderId") == true)
                                transactrionId = responseData["VirtualPosOrderId"].cpToString();

                            response.statu = SaleResponseStatu.Success;
                            response.message = "İşlem başarılı";
                            response.transactionId = transactrionId;

                            return response;
                        }
                    }
                }
            }

            string errorMessage = "İşlem sırasında bilinmeyen bir hata oluştu";

            if (responseDic?.ContainsKey("ResultMessage") == true && responseDic["ResultMessage"].cpToString() != "")
                errorMessage = responseDic["ResultMessage"].cpToString();
            else if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString() != "")
            {
                string resultCode = responseDic["ResultCode"].cpToString().Trim();

                string _errorMessage = ErrorMessageHandler(resultCode);

                if (!string.IsNullOrWhiteSpace(_errorMessage))
                    errorMessage = _errorMessage;
            }

            response.statu = SaleResponseStatu.Error;
            response.message = errorMessage;

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.statu = SaleResponseStatu.Error;
            response.message = "İşlem sırasında bir hata oluştu";

            response.privateResponse = request?.responseArray;

            string orderNumber = "";
            string transactionId = "";
            string resultCode = "";
            string resultMessage = "";

            if (request?.responseArray?.ContainsKey("OtherTrxCode") == true && request.responseArray["OtherTrxCode"].cpToString() != "")
                orderNumber = request.responseArray["OtherTrxCode"].cpToString();

            if (request?.responseArray?.ContainsKey("trxCode") == true && request.responseArray["trxCode"].cpToString() != "")
                transactionId = request.responseArray["trxCode"].cpToString();

            if (request?.responseArray?.ContainsKey("resultCode") == true && request.responseArray["resultCode"].cpToString() != "")
                resultCode = request.responseArray["resultCode"].cpToString();

            if (request?.responseArray?.ContainsKey("resultMessage") == true && request.responseArray["resultMessage"].cpToString() != "")
                resultMessage = request.responseArray["resultMessage"].cpToString();

            response.transactionId = transactionId;
            response.orderNumber = orderNumber;


            if (!string.IsNullOrWhiteSpace(resultCode))
            {
                string errorMessage = "İşlem sırasında bir hata oluştu";

                string _errMessage = ErrorMessageHandler(resultCode);

                if (!string.IsNullOrWhiteSpace(_errMessage))
                    errorMessage = _errMessage;

                response.statu = SaleResponseStatu.Error;
                response.message = errorMessage;

                return response;
            }

            if (!string.IsNullOrWhiteSpace(resultMessage))
            {
                response.statu = SaleResponseStatu.Error;
                response.message = resultMessage;

                return response;
            }

            string checkKey = GetCheckKey(auth);

            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "PaymentDealerAuthentication", new Dictionary<string, string>
                    {
                        { "DealerCode", auth.merchantID },
                        { "Username", auth.merchantUser },
                        { "Password", auth.merchantPassword },
                        { "CheckKey", checkKey },
                    }
                },
                { "PaymentDealerRequest", new Dictionary<string, string>
                    {
                        { "PaymentId", transactionId },
                        { "OtherTrxCode", orderNumber },
                    }
                }
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/GetDealerPaymentTrxDetailList";

            string responseStr = Request(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString().ToLower() == "success" && responseDic?.ContainsKey("Data") == true)
            {
                Dictionary<string, object> responseData = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["Data"]));

                if (responseData?.ContainsKey("IsSuccessful") == true && responseData["IsSuccessful"].cpToBool() == true && responseData?.ContainsKey("PaymentDetail") == true)
                {
                    Dictionary<string, object> paymentDetail = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseData["PaymentDetail"]));

                    if (paymentDetail?.ContainsKey("PaymentStatus") == true && paymentDetail["PaymentStatus"].cpToInt() == 2 && paymentDetail?.ContainsKey("TrxStatus") == true && paymentDetail["TrxStatus"].cpToInt() == 1)
                    {
                        response.statu = SaleResponseStatu.Success;
                        response.message = "İşlem başarılı";

                        return response;
                    }
                }
            }


            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            request.currency = request.currency ?? Currency.TRY;
            request.customerIPAddress = string.IsNullOrWhiteSpace(request.customerIPAddress) ? "1.1.1.1" : request.customerIPAddress;


            string checkKey = GetCheckKey(auth);

            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "PaymentDealerAuthentication", new Dictionary<string, string>
                    {
                        { "DealerCode", auth.merchantID },
                        { "Username", auth.merchantUser },
                        { "Password", auth.merchantPassword },
                        { "CheckKey", checkKey },
                    }
                },
                { "PaymentDealerRequest", new Dictionary<string, object>
                {
                        { "VirtualPosOrderId", request.transactionId },
                        { "OtherTrxCode", request.orderNumber },
                        { "ClientIP", request.customerIPAddress },
                        { "VoidRefundReason", 2 },
                    }
                }
            };


            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/DoVoid";

            string responseStr = Request(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString().ToLower() == "success")
            {
                if (responseDic?.ContainsKey("Data") == true && responseDic["Data"] != null)
                {
                    Dictionary<string, object> responseData = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["Data"]));

                    if (responseData?.ContainsKey("IsSuccessful") == true && responseData["IsSuccessful"].cpToBool() == true)
                    {
                        response.statu = ResponseStatu.Success;
                        response.message = "İşlem başarılı";

                        return response;
                    }
                }
            }

            string errorMessage = "";

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString() != "")
            {
                string resultCode = responseDic["ResultCode"].cpToString().Trim();

                string _errorMessage = ErrorMessageHandler(resultCode);

                if (!string.IsNullOrWhiteSpace(_errorMessage))
                    errorMessage = _errorMessage;
            }

            if (string.IsNullOrWhiteSpace(errorMessage) && responseDic?.ContainsKey("ResultMessage") == true && responseDic["ResultMessage"].cpToString() != "")
                errorMessage = responseDic["ResultMessage"].cpToString();

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = "İşlem iptal edilemedi";

            response.statu = ResponseStatu.Error;
            response.message = errorMessage;

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse();

            request.currency = request.currency ?? Currency.TRY;
            request.customerIPAddress = string.IsNullOrWhiteSpace(request.customerIPAddress) ? "1.1.1.1" : request.customerIPAddress;


            string checkKey = GetCheckKey(auth);

            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "PaymentDealerAuthentication", new Dictionary<string, string>
                    {
                        { "DealerCode", auth.merchantID },
                        { "Username", auth.merchantUser },
                        { "Password", auth.merchantPassword },
                        { "CheckKey", checkKey },
                    }
                },
                { "PaymentDealerRequest", new Dictionary<string, object>
                    {
                        { "VirtualPosOrderId", request.transactionId },
                        { "OtherTrxCode", request.orderNumber },
                        { "Amount", Math.Round(request.refundAmount, 2) },
                    }
                }
            };


            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/DoCreateRefundRequest";

            string responseStr = Request(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString().ToLower() == "success")
            {
                if (responseDic?.ContainsKey("Data") == true && responseDic["Data"] != null)
                {
                    Dictionary<string, object> responseData = JsonConvertHelper.Convert<Dictionary<string, object>>(JsonConvertHelper.Json(responseDic["Data"]));

                    if (responseData?.ContainsKey("IsSuccessful") == true && responseData["IsSuccessful"].cpToBool() == true)
                    {
                        response.statu = ResponseStatu.Success;
                        response.message = "İşlem başarılı";

                        return response;
                    }
                }
            }

            string errorMessage = "";

            if (responseDic?.ContainsKey("ResultCode") == true && responseDic["ResultCode"].cpToString() != "")
            {
                string resultCode = responseDic["ResultCode"].cpToString().Trim();

                string _errorMessage = ErrorMessageHandler(resultCode);

                if (!string.IsNullOrWhiteSpace(_errorMessage))
                    errorMessage = _errorMessage;
            }

            if (string.IsNullOrWhiteSpace(errorMessage) && responseDic?.ContainsKey("ResultMessage") == true && responseDic["ResultMessage"].cpToString() != "")
                errorMessage = responseDic["ResultMessage"].cpToString();

            if (string.IsNullOrWhiteSpace(errorMessage))
                errorMessage = "İşlem iptal edilemedi";

            response.statu = ResponseStatu.Error;
            response.message = errorMessage;

            return response;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse { confirm = false, installmentList = new List<AllInstallment>() };

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse { confirm = false, installmentList = new List<installment>() };

            for (int i = 2; i <= 12; i++)
            {
                if (GetIsInstallment(request, auth, i))
                {
                    response.installmentList.Add(new installment
                    {
                        count = i,
                        customerCostCommissionRate = 0
                    });
                }
            }

            if (response.installmentList.Any())
                response.confirm = true;

            return response;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return new AdditionalInstallmentQueryResponse { confirm = false };
        }

        private bool GetIsInstallment(BINInstallmentQueryRequest request, VirtualPOSAuth auth, int installmentCount)
        {
            request.currency = request.currency ?? Currency.TRY;

            string _currency = request.currency == Currency.TRY ? "TL" : request.currency.ToString();


            string checkKey = GetCheckKey(auth);

            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "PaymentDealerAuthentication", new Dictionary<string, string>
                    {
                        { "DealerCode", auth.merchantID },
                        { "Username", auth.merchantUser },
                        { "Password", auth.merchantPassword },
                        { "CheckKey", checkKey },
                    }
                },
                { "PaymentDealerRequest", new Dictionary<string, object>
                {
                        { "BinNumber", request.BIN },
                        { "Currency", _currency },
                        { "OrderAmount", request.amount },
                        { "InstallmentNumber", installmentCount },
                        { "GroupRevenueRate", 0 },
                        { "GroupRevenueAmount", 0 },
                        { "IsThreeD", 1 },
                    }
                }
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/PaymentDealer/DoCalcPaymentAmount";

            string responseStr = Request(req, link);

            JObject jobj = JObject.Parse(responseStr);

            string creditType = jobj.SelectToken("$.Data.BankCard.CreditType").cpToString();

            if (creditType == "CreditCard")
                return true;

            return false;
        }

        private string ErrorMessageHandler(string resultCode)
        {
            Dictionary<string, string> messageList = new Dictionary<string, string>()
            {
                { "PaymentDealer.CheckPaymentDealerAuthentication.InvalidRequest", "CheckKey hatalı ya da nesne hatalı ya da JSON bozuk olabilir." },
                { "PaymentDealer.CheckPaymentDealerAuthentication.InvalidAccount", "Böyle bir bayi bulunamadı, Bayi kodu, bayi kullanıcı adı ve/veya şifresi yanlış girilmiş." },
                { "PaymentDealer.CheckPaymentDealerAuthentication.VirtualPosNotFound", "Bu bayi için sanal pos tanımı yapılmamış." },
                { "PaymentDealer.CheckDealerPaymentLimits.DailyDealerLimitExceeded", " Bayi için tanımlı günlük limitlerden herhangi biri aşıldı." },
                { "PaymentDealer.CheckDealerPaymentLimits.DailyCardLimitExceeded", "Gün içinde bu kart kullanılarak daha fazla işlem yapılamaz." },
                { "PaymentDealer.CheckCardInfo.InvalidCardInfo", "Kart bilgilerinde hata var" },
                { "PaymentDealer.DoDirectPayment.ThreeDRequired ", "Bayi için 3d ödeme gönderme zorunluluğu vardır, Non-3D ödeme gönderemez." },
                { "PaymentDealer.DoDirectPayment.InstallmentNotAvailableForForeignCurrencyTransaction ", "Yabancı para ile taksit yapılamaz." },
                { "PaymentDealer.DoDirectPayment.ThisInstallmentNumberNotAvailableForDealer ", "Bu taksit sayısı bu bayi için yapılamaz." },
                { "PaymentDealer.DoDirectPayment.InvalidInstallmentNumber", "Taksit sayısı 2 ile 12 arasıdır." },
                { "PaymentDealer.DoDirectPayment.ThisInstallmentNumberNotAvailableForVirtualPos", "Sanal Pos bu taksit sayısına izin vermiyor." },
                { "EX", "Beklenmeyen bir hata oluştu" },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidRequest", "JSON objesi yanlış oluşturulmuş." },
                { "PaymentDealer.DoDirectPayment3dRequest.RedirectUrlRequired", "3D ödeme sonucunun döneceği RedirectURL verilmemiş." },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidCurrencyCode", "Para birimi hatalı.  (TL, USD, EUR  şeklinde olmalı)" },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidInstallmentNumber", "Geçersiz taksit sayısı girilmiş  1-12 arası olmalıdır." },
                { "PaymentDealer.DoDirectPayment3dRequest.InstallmentNotAvailableForForeignCurrencyTransaction ", "Yabancı para ile taksit yapılamaz." },
                { "PaymentDealer.DoDirectPayment3dRequest.ForeignCurrencyNotAvailableForThisDealer", "Bayinin yabancı parayla ödeme izni yok." },
                { "PaymentDealer.DoDirectPayment3dRequest.PaymentMustBeAuthorization", "Ön otorizasyon tipinde ödeme gönderilmeli." },
                { "PaymentDealer.DoDirectPayment3dRequest.AuthorizationForbiddenForThisDealer", "Bayinin ön otorizasyon tipinde ödeme gönderme izni yok." },
                { "PaymentDealer.DoDirectPayment3dRequest.PoolPaymentNotAvailableForDealer", "Bayinin havuzlu ödeme gönderme izni yok." },
                { "PaymentDealer.DoDirectPayment3dRequest.PoolPaymentRequiredForDealer", "Bayi sadece havuzlu ödeme gönderebilir." },
                { "PaymentDealer.DoDirectPayment3dRequest.TokenizationNotAvailableForDealer", "Bayinin kart saklama izni yok." },
                { "PaymentDealer.DoDirectPayment3dRequest.CardTokenCannotUseWithSaveCard", "Kart saklanmak isteniyorsa Token gönderilemez." },
                { "PaymentDealer.DoDirectPayment3dRequest.CardTokenNotFound", "Gönderilen Token bulunamadı." },
                { "PaymentDealer.DoDirectPayment3dRequest.OnlyCardTokenOrCardNumber", "Hem kart numarası hem de Token aynı anda verilemez." },
                { "PaymentDealer.DoDirectPayment3dRequest.ChannelPermissionNotAvailable", "Bayinin bu kanaldan ödeme gönderme izni yok." },
                { "PaymentDealer.DoDirectPayment3dRequest.IpAddressNotAllowed", "Bayinin IP kısıtlaması var, sadece önceden belirtilen IP den ödeme gönderebilir." },
                { "PaymentDealer.DoDirectPayment3dRequest.VirtualPosNotAvailable", "Girilen kart için uygun sanal pos bulunamadı." },
                { "PaymentDealer.DoDirectPayment3dRequest.ThisInstallmentNumberNotAvailableForVirtualPos", "Sanal Pos bu taksit sayısına izin vermiyor." },
                { "PaymentDealer.DoDirectPayment3dRequest.ThisInstallmentNumberNotAvailableForDealer", "Bu taksit sayısı bu bayi için yapılamaz." },
                { "PaymentDealer.DoDirectPayment3dRequest.DealerCommissionRateNotFound", "Bayiye bu sanal pos ve taksit için komisyon oranı girilmemiş." },
                { "PaymentDealer.DoDirectPayment3dRequest.DealerGroupCommissionRateNotFound", "Üst bayiye bu sanal pos ve taksit için komisyon oranı girilmemiş." },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidSubMerchantName", "Gönderilen bayi adı daha önceden Moka sistemine kaydedilmemiş." },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidUnitPrice", "Satılan ürünler sepete eklendiyse, geçerli birim fiyatı girilmelidir." },
                { "PaymentDealer.DoDirectPayment3dRequest.InvalidQuantityValue", "Satılan ürünler sepete eklendiyse, geçerli adet girilmelidir." },
                { "PaymentDealer.DoDirectPayment3dRequest.BasketAmountIsNotEqualPaymentAmount", "Satılan ürünler sepete eklendiyse, sepet tutarı ile ödeme tutarı eşleşmelidir." },
                { "PaymentDealer.DoDirectPayment3dRequest.BasketProductNotFoundInYourProductList", "Satılan ürünler sepete eklendiyse, geçerli ürün seçilmelidir." },
                { "PaymentDealer.DoDirectPayment3dRequest.MustBeOneOfDealerProductIdOrProductCode", "Satılan ürünler sepete eklendiyse, ürün kodu veya moka ürün ID si girilmelidir." },
                { "000", "Genel Hata" },
                { "001", "Kart Sahibi Onayı Alınamadı" },
                { "002", "Limit Yetersiz" },
                { "003", "Kredi Kartı Numarası Geçerli Formatta Değil" },
                { "004", "Genel Red" },
                { "005", "Kart Sahibine Açık Olmayan İşlem" },
                { "006", "Kartın Son Kullanma Tarihi Hatali" },
                { "007", "Geçersiz İşlem" },
                { "008", "Bankaya Bağlanılamadı" },
                { "009", "Tanımsız Hata Kodu" },
                { "010", "Banka SSL Hatası" },
                { "011", "Manual Onay İçin Bankayı Arayınız" },
                { "012", "Kart Bilgileri Hatalı - Kart No veya CVV2" },
                { "013", "Visa MC Dışındaki Kartlar 3D Secure Desteklemiyor" },
                { "014", "Geçersiz Hesap Numarası" },
                { "015", "Geçersiz CVV" },
                { "016", "Onay Mekanizması Mevcut Değil" },
                { "017", "Sistem Hatası" },
                { "018", "Çalıntı Kart" },
                { "019", "Kayıp Kart" },
                { "020", "Kısıtlı Kart" },
                { "021", "Zaman Aşımı" },
                { "022", "Geçersiz İşyeri" },
                { "023", "Sahte Onay" },
                { "024", "3D Onayı Alındı Ancak Para Karttan Çekilemedi" },
                { "025", "3D Onay Alma Hatası" },
                { "026", "Kart Sahibi Banka veya Kart 3D-Secure Üyesi Değil" },
                { "027", "Kullanıcı Bu İşlemi Yapmaya Yetkili Değil" },
                { "028", "Fraud Olasılığı" },
                { "029", "Kartınız e-ticaret İşlemlerine Kapalıdır" },
                { "PaymentDealer.DoVoid.PaymentNotFound", "İptal edilecek ödeme bulunamadı." },
                { "PaymentDealer.DoCreateRefundRequest.InvalidRequest", "CheckKey hatalı ya da nesne hatalı ya da JSON bozuk olabilir." },
                { "PaymentDealer.DoCreateRefundRequest.InvalidAccount", "Böyle bir bayi bulunamadı." },
                { "PaymentDealer.DoCreateRefundRequest.OtherTrxCodeOrVirtualPosOrderIdMustGiven", "Veriler eksik gönderildi. OrderId veya OtherTrxCode girilmeli." },
                { "PaymentDealer.DoCreateRefundRequest.InvalidAmount", "Tutar alanına girilen değer, ödeme tutarından büyük olmamalı." },
                { "PaymentDealer.DoCreateRefundRequest.PaymentNotFound", "İade talebi oluşturulacak bir ödeme kaydı bulunamadı." },
                { "PaymentDealer.DoCreateRefundRequest.OtherTrxCodeAndVirtualPosOrderIdMismatch", "Gönderilen veriler farklı ödemelere ait." },
                { "PaymentDealer.DoCreateRefundRequest.RefundRequestAlreadyExist", "Bu ödemeye ait bekleyen bir iade talebi mevcut. Onun tamamlanması beklenmeli veya talep geri çekilmeli." },
            };

            if (messageList.TryGetValue(resultCode, out var message))
                return message;
            else
                return "";
        }

        private string Request(Dictionary<string, object> param, string link)
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

                    var response = client.PostAsync(link, req).Result;
                    byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                    responseString = Encoding.UTF8.GetString(responseByte);
                }
            }
            catch { }

            return responseString;
        }

        private string GetSha256(string originalString)
        {
            string hashedData = "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));

                foreach (byte bit in bytes)
                {
                    hashedData += bit.ToString("x2");
                }

                return hashedData;
            }
        }

        private string GetCheckKey(VirtualPOSAuth auth)
        {
            string hashText = $"{auth.merchantID}MK{auth.merchantUser}PD{auth.merchantPassword}";

            string hashedText = GetSha256(hashText);

            return hashedText;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
