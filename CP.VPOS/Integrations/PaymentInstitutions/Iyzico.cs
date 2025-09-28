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
using CP.VPOS.Infrastructures.Iyzico;
using CP.VPOS.Infrastructures.Iyzico.Request;
using CP.VPOS.Infrastructures.Iyzico.Model;

namespace CP.VPOS.Banks.Iyzico
{
    internal class IyzicoVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://sandbox-api.iyzipay.com";
        private readonly string _urlAPILive = "https://api.iyzipay.com";

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            RetrieveInstallmentInfoRequest requestbin = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                BinNumber = request.BIN,
                Price = request.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".")
            };


            InstallmentInfo installmentInfo = InstallmentInfo.Retrieve(requestbin, GetOptions(auth));


            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse
            {
                confirm = false
            };

            if (installmentInfo.Status == "success" && installmentInfo.InstallmentDetails?.Any() == true)
            {
                var ins = installmentInfo.InstallmentDetails[0];

                if (ins.InstallmentPrices?.Any() == true)
                {
                    response.confirm = true;
                    response.installmentList = new List<installment>();

                    foreach (var item in ins.InstallmentPrices.Where(s => s.InstallmentNumber > 1))
                    {
                        decimal total = decimal.Parse(item.TotalPrice, CultureInfo.InvariantCulture);

                        float comissionRate = Convert.ToSingle(((100 * (total - request.amount)) / request.amount));

                        var model = new installment
                        {
                            count = item.InstallmentNumber ?? 0,
                            customerCostCommissionRate = comissionRate
                        };

                        response.installmentList.Add(model);
                    }
                }
            }

            return response;
        }

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            if (request.invoiceInfo?.taxNumber?.Length == 0)
                request.invoiceInfo.taxNumber = "11111111111";

            if (request.customerIPAddress.Length == 0)
                request.customerIPAddress = "1.1.1.1";


            CreatePaymentRequest req = new CreatePaymentRequest();
            req.Locale = Locale.TR.ToString();
            req.ConversationId = request.orderNumber;
            req.Price = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
            req.PaidPrice = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
            req.Currency = request.saleInfo.currency.ToString();
            req.Installment = request.saleInfo.installment;
            req.BasketId = request.orderNumber;

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = request.saleInfo.cardNameSurname;
            paymentCard.CardNumber = request.saleInfo.cardNumber;
            paymentCard.ExpireMonth = request.saleInfo.cardExpiryDateMonth.ToString("00");
            paymentCard.ExpireYear = request.saleInfo.cardExpiryDateYear.ToString();
            paymentCard.Cvc = request.saleInfo.cardCVV;
            req.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = request.invoiceInfo.emailAddress;
            buyer.Name = request.invoiceInfo.name;
            buyer.Surname = request.invoiceInfo.name;
            buyer.GsmNumber = request.invoiceInfo.phoneNumber;
            buyer.Email = request.invoiceInfo.emailAddress;
            buyer.IdentityNumber = request.invoiceInfo.taxNumber;
            buyer.RegistrationAddress = request.invoiceInfo.addressDesc;
            buyer.Ip = request.customerIPAddress;
            buyer.City = request.invoiceInfo.cityName;
            buyer.Country = request.invoiceInfo.country.ToString();
            buyer.ZipCode = request.invoiceInfo.postCode;
            req.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = request.shippingInfo.name;
            shippingAddress.City = request.shippingInfo.cityName;
            shippingAddress.Country = request.shippingInfo.country.ToString();
            shippingAddress.Description = request.shippingInfo.addressDesc;
            shippingAddress.ZipCode = request.shippingInfo.postCode;
            req.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = request.invoiceInfo.name;
            billingAddress.City = request.invoiceInfo.cityName;
            billingAddress.Country = request.invoiceInfo.country.ToString();
            billingAddress.Description = request.invoiceInfo.addressDesc;
            billingAddress.ZipCode = request.invoiceInfo.postCode;
            req.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "TAHSILAT";
            secondBasketItem.Name = "Cari Tahsilat";
            secondBasketItem.Category1 = "Tahsilat";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
            basketItems.Add(secondBasketItem);
            req.BasketItems = basketItems;

            if (request.payment3D?.confirm == true)
            {
                req.CallbackUrl = request.payment3D.returnURL;

                ThreedsInitialize payment = ThreedsInitialize.Create(req, GetOptions(auth));

                try
                {
                    response.privateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(payment));
                }
                catch { }

                if (payment.Status == "success")
                {
                    response.statu = SaleResponseStatu.RedirectHTML;
                    response.message = payment.HtmlContent;
                }
                else
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = payment.ErrorMessage;
                }

            }
            else
            {
                Payment payment = Payment.Create(req, GetOptions(auth));

                try
                {
                    response.privateResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(payment));
                }
                catch { }

                if (payment.Status == "success")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarıyla tamamlandı";
                    response.transactionId = payment.PaymentId;
                }
                else
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = payment.ErrorMessage;
                }
            }


            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.statu = SaleResponseStatu.Error;

            if (request?.responseArray == null)
                return response;

            response.privateResponse = request.responseArray;

            if (request.responseArray.ContainsKey("conversationId"))
                response.orderNumber = request.responseArray["conversationId"].cpToString();

            if (request.responseArray.ContainsKey("paymentId"))
                response.transactionId = request.responseArray["paymentId"].cpToString();

            if (request.responseArray.ContainsKey("status") && request.responseArray["status"].cpToString() == "success" && request.responseArray.ContainsKey("mdStatus") && request.responseArray["mdStatus"].cpToInt() == 1)
            {
                CreateThreedsPaymentRequest paymentRequest = new CreateThreedsPaymentRequest();
                paymentRequest.Locale = Locale.TR.ToString();
                paymentRequest.ConversationId = request.responseArray["conversationId"].cpToString();
                paymentRequest.PaymentId = request.responseArray["paymentId"].cpToString();
                paymentRequest.ConversationData = request.responseArray["conversationData"].cpToString();

                ThreedsPayment threedsPayment = ThreedsPayment.Create(paymentRequest, GetOptions(auth));

                if (threedsPayment.Status.ToLower() == "success")
                {
                    response.statu = SaleResponseStatu.Success;
                    response.message = "Ödeme başarılı";
                }
                else
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = threedsPayment.ErrorMessage;
                }
            }
            else
            {
                response.statu = SaleResponseStatu.Error;

                switch (request.responseArray["mdStatus"].cpToInt())
                {
                    case 0:
                        response.message = "3D Secure doğrulaması geçersiz";
                        break;
                    case 2:
                        response.message = "Kart sahibi veya bankası sisteme kayıtlı değil";
                        break;
                    case 3:
                        response.message = "Kartın bankası sisteme kayıtlı değil";
                        break;
                    case 4:
                        response.message = "Doğrulama denemesi, kart sahibi sisteme daha sonra kayıt olmayı seçmiş";
                        break;
                    case 5:
                        response.message = "Doğrulama yapılamıyor";
                        break;
                    case 6:
                        response.message = "3D Secure hatası";
                        break;
                    case 7:
                        response.message = "Sistem hatası";
                        break;
                    case 8:
                        response.message = "Bilinmeyen kart numarası";
                        break;
                    default:
                        break;
                }

                return response;
            }


            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            request.customerIPAddress = request.customerIPAddress.cpToString();

            if (request.customerIPAddress.cpToString().Length < 2)
                request.customerIPAddress = "1.1.1.1";

            CreateCancelRequest req = new CreateCancelRequest();
            req.ConversationId = request.orderNumber;
            req.Locale = Locale.TR.ToString();
            req.PaymentId = request.transactionId;
            req.Ip = request.customerIPAddress;

            CP.VPOS.Infrastructures.Iyzico.Model.Cancel _cancelResponse = CP.VPOS.Infrastructures.Iyzico.Model.Cancel.Create(req, GetOptions(auth));

            if (_cancelResponse.Status == "success")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İade işlemi başarılı";
                response.refundAmount = Convert.ToDecimal(_cancelResponse.Price, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = _cancelResponse.ErrorMessage;
            }

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            request.customerIPAddress = request.customerIPAddress.cpToString();

            if (request.customerIPAddress.cpToString().Length < 2)
                request.customerIPAddress = "1.1.1.1";

            CreateAmountBasedRefundRequest req = new CreateAmountBasedRefundRequest();

            req.Locale = Locale.TR.ToString();
            req.ConversationId = request.orderNumber;
            req.Ip = request.customerIPAddress;
            req.Price = request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");
            req.PaymentId = request.transactionId;

            CP.VPOS.Infrastructures.Iyzico.Model.Refund amountBasedRefund = CP.VPOS.Infrastructures.Iyzico.Model.Refund.CreateAmountBasedRefundRequest(req, GetOptions(auth));

            if (amountBasedRefund.Status == "success")
            {
                response.statu = ResponseStatu.Success;
                response.message = "İade işlemi başarılı";
                response.refundAmount = Convert.ToDecimal(amountBasedRefund.Price, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            }
            else
            {
                response.statu = ResponseStatu.Error;
                response.message = amountBasedRefund.ErrorMessage;
            }

            return response;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse();


            return response;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        private Options GetOptions(VirtualPOSAuth auth)
        {
            Options options = new Options();

            options.ApiKey = auth.merchantUser;
            options.SecretKey = auth.merchantPassword;
            options.BaseUrl = auth.testPlatform ? _urlAPITest : _urlAPILive;

            return options;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return new SaleQueryResponse { statu = SaleQueryResponseStatu.Error, message = "Bu sanal pos için satış sorgulama işlemi şuan desteklenmiyor" };
        }
    }
}
