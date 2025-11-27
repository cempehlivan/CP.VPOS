using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using CP.VPOS.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace CP.VPOS.Banks.Tami
{
    internal class TamiVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://sandbox-paymentapi.tami.com.tr";
        private readonly string _urlAPILive = "https://paymentapi.tami.com.tr";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();
            response.statu = SaleResponseStatu.Error;

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;
            request.saleInfo.amount = Math.Round(request.saleInfo.amount, 2);

            response.orderNumber = request.orderNumber;

            Dictionary<string, object> saleDic = new Dictionary<string, object>
            {
                { "amount", request.saleInfo.amount},
                { "orderId", request.orderNumber },
                { "currency", request.saleInfo.currency.ToString() },
                { "installmentCount", request.saleInfo.installment },
                { "paymentGroup", "OTHER" },
                { "card", new Dictionary<string, object>
                    {
                        { "holderName", request.saleInfo.cardNameSurname },
                        { "cvv", request.saleInfo.cardCVV },
                        { "number", request.saleInfo.cardNumber },
                        { "expireMonth", request.saleInfo.cardExpiryDateMonth },
                        { "expireYear", request.saleInfo.cardExpiryDateYear },
                    }
                },
                { "buyer", new Dictionary<string, object>
                    {
                        { "buyerId", request.invoiceInfo.taxNumber },
                        { "ipAddress", request.customerIPAddress },
                        { "name", request.invoiceInfo.name },
                        { "surName", request.invoiceInfo.surname },
                        { "identityNumber", request.invoiceInfo.taxNumber },
                        { "city", request.invoiceInfo.cityName },
                        { "country", request.invoiceInfo.country.ToString() },
                        { "zipCode", request.invoiceInfo.postCode },
                        { "emailAddress", request.invoiceInfo.emailAddress },
                        { "phoneNumber", request.invoiceInfo.phoneNumber },
                        { "registrationAddress", request.invoiceInfo.addressDesc }
                    }
                },
                { "shippingAddress", new Dictionary<string, object>
                    {
                        { "emailAddress", request.shippingInfo.emailAddress },
                        { "address", request.shippingInfo.addressDesc },
                        { "city", request.shippingInfo.cityName },
                        { "companyName", $"{request.shippingInfo.name} {request.shippingInfo.surname}" },
                        { "country", request.shippingInfo.country.ToString() },
                        { "district", request.shippingInfo.townName },
                        { "contactName", $"{request.shippingInfo.name} {request.shippingInfo.surname}" },
                        { "phoneNumber", request.shippingInfo.phoneNumber },
                        { "zipCode", request.shippingInfo.postCode },
                    }
                },
                { "billingAddress", new Dictionary<string, object>
                    {
                        { "emailAddress", request.invoiceInfo.emailAddress },
                        { "address", request.invoiceInfo.addressDesc },
                        { "city", request.invoiceInfo.cityName },
                        { "companyName", $"{request.invoiceInfo.name} {request.invoiceInfo.surname}" },
                        { "country", request.invoiceInfo.country.ToString() },
                        { "district", request.invoiceInfo.townName },
                        { "contactName", $"{request.invoiceInfo.name} {request.invoiceInfo.surname}" },
                        { "phoneNumber", request.invoiceInfo.phoneNumber },
                        { "zipCode", request.invoiceInfo.postCode },
                    }
                },
            };

            if (request.payment3D?.confirm == true)
                saleDic.Add("callbackUrl", request.payment3D.returnURL);

            string jwkSignature = GenerateJWKSignature(auth, saleDic);

            saleDic.Add("securityHash", jwkSignature);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/payment/auth";

            string responseStr = Request(saleDic, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;


            if (request.payment3D?.confirm == true)
            {
                if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true && responseDic?.ContainsKey("threeDSHtmlContent") == true)
                {
                    response.statu = SaleResponseStatu.RedirectHTML;
                    response.message = Encoding.UTF8.GetString(Convert.FromBase64String(responseDic["threeDSHtmlContent"].cpToString()));

                    return response;
                }
                else
                {
                    string errorMessage = "İşlem başarısız";

                    if (responseDic?.ContainsKey("errorMessage") == true)
                        errorMessage = responseDic["errorMessage"].cpToString();

                    response.statu = SaleResponseStatu.Error;
                    response.message = errorMessage;

                    return response;
                }
            }
            else
            {
                if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true)
                {
                    string transactrionId = "";

                    if (responseDic?.ContainsKey("bankReferenceNumber") == true)
                        transactrionId = responseDic["bankReferenceNumber"].cpToString();

                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                    response.transactionId = transactrionId;

                    return response;
                }
                else
                {
                    string errorMessage = "İşlem başarısız";

                    if (responseDic?.ContainsKey("errorMessage") == true)
                        errorMessage = responseDic["errorMessage"].cpToString();

                    response.statu = SaleResponseStatu.Error;
                    response.message = errorMessage;

                    return response;
                }
            }
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.privateResponse = new Dictionary<string, object>();

            response.privateResponse.Add("response_1", request.responseArray);

            if (request?.responseArray?.ContainsKey("orderId") == true && request.responseArray["orderId"].cpToString() != "")
                response.orderNumber = request.responseArray["orderId"].cpToString();

            if (request?.responseArray?.ContainsKey("success") == true && request?.responseArray["success"].cpToBool() == true)
            {
                Dictionary<string, object> saleDic = new Dictionary<string, object>
                {
                    { "orderId", response.orderNumber },
                };

                string jwkSignature = GenerateJWKSignature(auth, saleDic);

                saleDic.Add("securityHash", jwkSignature);

                string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/payment/complete-3ds";

                string responseStr = Request(saleDic, link, auth);

                Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

                response.privateResponse.Add("response_2", responseDic);

                if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true)
                {
                    string transactrionId = "";

                    if (responseDic?.ContainsKey("bankReferenceNumber") == true)
                        transactrionId = responseDic["bankReferenceNumber"].cpToString();

                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                    response.transactionId = transactrionId;

                    return response;
                }
                else
                {
                    string errorMessage = "İşlem başarısız";

                    if (responseDic?.ContainsKey("errorMessage") == true)
                        errorMessage = responseDic["errorMessage"].cpToString();

                    response.statu = SaleResponseStatu.Error;
                    response.message = errorMessage;

                    return response;
                }

            }
            else
            {
                string errorMessage = "3D doğrulama işlemi başarısız";

                if (request?.responseArray?.ContainsKey("errorMessage") == true)
                    errorMessage = request?.responseArray["errorMessage"].cpToString();

                response.statu = SaleResponseStatu.Error;
                response.message = errorMessage;

                return response;
            }
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, object> saleDic = new Dictionary<string, object>
            {
                {"orderId", request.orderNumber },
            };

            string jwkSignature = GenerateJWKSignature(auth, saleDic);

            saleDic.Add("securityHash", jwkSignature);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/payment/reverse";

            string responseStr = Request(saleDic, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true)
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";

                return response;
            }
            else
            {
                string errorMessage = "İşlem başarısız";

                if (responseDic?.ContainsKey("errorMessage") == true)
                    errorMessage = responseDic["errorMessage"].cpToString();

                response.statu = ResponseStatu.Error;
                response.message = errorMessage;

                return response;
            }
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            Dictionary<string, object> saleDic = new Dictionary<string, object>
            {
                {"orderId", request.orderNumber },
                {"amount", request.refundAmount }
            };

            string jwkSignature = GenerateJWKSignature(auth, saleDic);

            saleDic.Add("securityHash", jwkSignature);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/payment/reverse";

            string responseStr = Request(saleDic, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true)
            {
                response.refundAmount = request.refundAmount;
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";

                return response;
            }
            else
            {
                string errorMessage = "İşlem başarısız";

                if (responseDic?.ContainsKey("errorMessage") == true)
                    errorMessage = responseDic["errorMessage"].cpToString();

                response.statu = ResponseStatu.Error;
                response.message = errorMessage;

                return response;
            }
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            // Tami, tüm taksit listesini API üzerinden sağlamamaktadır. Bu nedenle, burada elimizdeki bin listesindeki kart programlarını listeleyip her kart programı için bir kartı tek tek sorguluyoruz.
            // Destek eklenirse API üzerinden taksit bilgileri de çekilebilir.

            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse { confirm = false, installmentList = new List<AllInstallment>() };

            List<CreditCardBinQueryResponse> binList = BinService.GetBinList();

            var bankCreditCardBrandList = binList.Where(s => s.cardType == CreditCardType.Credit && s.cardProgram != CreditCardProgram.Unknown).GroupBy(s => s.cardProgram).Select(s => new { cardProgram = s.Key, binNumber = s.First().binNumber }).ToList();

            foreach (var brand in bankCreditCardBrandList)
            {
                var installmentsInfo = BINInstallmentQuery(new BINInstallmentQueryRequest
                {
                    BIN = brand.binNumber,
                    amount = request.amount,
                    currency = request.currency ?? Currency.TRY,
                }, auth);

                if (installmentsInfo.confirm && installmentsInfo.installmentList?.Any() == true)
                {
                    foreach (var ins in installmentsInfo.installmentList)
                    {
                        response.installmentList.Add(new AllInstallment
                        {
                            bankCode = "9980",
                            cardProgram = brand.cardProgram,
                            count = ins.count,
                            customerCostCommissionRate = ins.customerCostCommissionRate,
                        });
                    }
                }
            }

            if (response.installmentList?.Any() == true)
                response.confirm = true;

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            List<installment> installments = new List<installment>();


            Dictionary<string, object> saleDic = new Dictionary<string, object>
            {
                {"binNumber", request.BIN },
            };

            string jwkSignature = GenerateJWKSignature(auth, saleDic);

            saleDic.Add("securityHash", jwkSignature);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/installment/installment-info";

            string responseStr = Request(saleDic, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            if (responseDic?.ContainsKey("success") == true && responseDic["success"].cpToBool() == true && responseDic?.ContainsKey("isInstallment") == true && responseDic["isInstallment"].cpToBool() == true && responseDic?.ContainsKey("installments") == true)
            {
                List<int> installmentList = JsonConvertHelper.Convert<List<int>>(JsonConvertHelper.Json<object>(responseDic["installments"]));

                if (installmentList?.Any() == true)
                {
                    foreach (int installment in installmentList)
                    {
                        int installments_number = installment;
                        decimal payable_amount = request.amount;
                        float commissionRate = 0;

                        if (installments_number > 1)
                        {
                            if (payable_amount > request.amount)
                                commissionRate = ((((decimal)100 * payable_amount) / request.amount) - (decimal)100).cpToSingle();

                            installments.Add(new installment
                            {
                                count = installments_number,
                                customerCostCommissionRate = commissionRate
                            });
                        }
                    }
                }
            }

            return new BINInstallmentQueryResponse
            {
                confirm = installments?.Count > 0,
                installmentList = installments?.Any() == true ? installments : null
            };
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }


        private string Request(Dictionary<string, object> param, string link, VirtualPOSAuth auth)
        {
            string responseString = "";

            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.ServicePointManager.Expect100Continue = false;

                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff" } },
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                string jsonContent = JsonConvert.SerializeObject(param, jsonSerializerSettings);

                using (HttpClient client = new HttpClient())
                using (var req = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") { CharSet = Encoding.UTF8.WebName };

                    client.DefaultRequestHeaders.Add("Accept-Language", "tr");
                    client.DefaultRequestHeaders.Add("PG-Api-Version", "v3");
                    client.DefaultRequestHeaders.Add("PG-Auth-Token", GetPGAuthToken(auth));
                    client.DefaultRequestHeaders.Add("correlationId", $"Correlation{Guid.NewGuid().ToString("N")}");

                    var response = client.PostAsync(link, req).Result;

                    byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;

                    responseString = Encoding.UTF8.GetString(responseByte);
                }
            }
            catch (Exception ex)
            {

            }

            return responseString;
        }

        private string GenerateJWKSignature(VirtualPOSAuth auth, Dictionary<string, object> requestBody)
        {
            string[] splitPassword = auth.merchantPassword.Split('|');

            string kidValue = splitPassword[0];
            string kValue = splitPassword.Length > 1 ? splitPassword[1] : splitPassword[0];

            string bodyJson = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings() { Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff" } }, NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });


            var headerObj = new { alg = "HS512", typ = "JWT", kidValue };
            string headerB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvertHelper.Json(headerObj)));

            string payloadB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes((bodyJson)));

            string signingInput = $"{headerB64}.{payloadB64}";
            byte[] key = Convert.FromBase64String(Base64UrlNormalizer(kValue));

            string signatureB64 = GetSha512(signingInput, key);

            return $"{headerB64}.{payloadB64}.{signatureB64}";
        }

        private string GetSha512(string message, byte[] key)
        {
            using (var sha512 = new HMACSHA512(key))
            {
                byte[] b = sha512.ComputeHash(Encoding.UTF8.GetBytes(message));

                return Convert.ToBase64String(b);
            }
        }

        private string GetSha256(string originalString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));

                return Convert.ToBase64String(bytes);
            }
        }

        private string GetPGAuthToken(VirtualPOSAuth auth)
        {
            string hash = GetSha256($"{auth.merchantID}{auth.merchantUser}{auth.merchantStorekey}");

            string key = $"{auth.merchantID}:{auth.merchantUser}:{hash}";

            return key;
        }

        private string Base64UrlNormalizer(string base64Url)
        {
            string base64 = base64Url.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return base64;
        }
    }
}
