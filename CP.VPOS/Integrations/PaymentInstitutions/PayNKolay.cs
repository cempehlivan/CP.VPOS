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
using System.Security.Cryptography;
using System.Text;

namespace CP.VPOS.Banks.PayNKolay
{
    internal class PayNKolayVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://paynkolaytest.nkolayislem.com.tr";
        private readonly string _urlAPILive = "https://paynkolay.nkolayislem.com.tr";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "Bilinmeyen bir hata oluştu",
                orderNumber = request.orderNumber,
            };

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;
            string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".");

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"sx", auth.merchantID },
                {"clientRefCode", request.orderNumber },
                {"amount", totalStr },
                {"installmentNo", request.saleInfo.installment.ToString() },
                {"cardHolderName", request.saleInfo.cardNameSurname },
                {"month", request.saleInfo.cardExpiryDateMonth.ToString() },
                {"year", request.saleInfo.cardExpiryDateYear.ToString() },
                {"cvv",request.saleInfo.cardCVV},
                {"cardNumber", request.saleInfo.cardNumber},
                {"transactionType", "SALES" },
                {"rnd", DateTime.UtcNow.AddHours(3).ToString("dd.MM.yyyy HH:mm:ss") },
                {"environment", "API" },
                {"currencyNumber", ((int)request.saleInfo.currency).ToString() },
                {"cardHolderIP", request.customerIPAddress},
                {"successUrl", "" },
                {"failUrl", "" },
                {"customerKey", "" },
                {"use3D", "false" },
            };

            if (request.payment3D.confirm == true)
            {
                req["use3D"] = "true";
                req["successUrl"] = request.payment3D.returnURL;
                req["failUrl"] = request.payment3D.returnURL;
            }


            string hashString = req["sx"] + "|"
            + req["clientRefCode"] + "|"
            + req["amount"] + "|"
            + req["successUrl"] + "|"
            + req["failUrl"] + "|"
            + req["rnd"] + "|"
            + req["customerKey"] + "|"
            + auth.merchantStorekey;

            string hash = GetHash(hashString);


            req.Add("hashDatav2", hash);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/Vpos/v1/Payment";

            string responseStr = RequestForm(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;


            if (responseDic?.ContainsKey("RESPONSE_CODE") == true && responseDic["RESPONSE_CODE"].cpToInt() == 2)
            {
                if (responseDic["USE_3D"].cpToString() == "true")
                {
                    response.statu = SaleResponseStatu.RedirectHTML;
                    response.message = responseDic["BANK_REQUEST_MESSAGE"].cpToString();

                    return response;
                }
                else if (responseDic["USE_3D"].cpToString() == "false" && responseDic.ContainsKey("AUTH_CODE") == true && responseDic["AUTH_CODE"].cpToString() != "" && responseDic["AUTH_CODE"].cpToString() != "0")
                {
                    string transactionId = "";

                    if (responseDic.ContainsKey("REFERENCE_CODE") == true)
                        transactionId = responseDic["REFERENCE_CODE"].cpToString();

                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                    response.transactionId = transactionId;

                    return response;
                }
            }

            if (responseDic?.ContainsKey("RESPONSE_DATA") == true && responseDic["RESPONSE_DATA"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = responseDic["RESPONSE_DATA"].cpToString();
            }

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse();

            response.privateResponse = request?.responseArray;

            if (request?.responseArray?.ContainsKey("CLIENT_REFERENCE_CODE") == true && request?.responseArray["CLIENT_REFERENCE_CODE"].cpToString() != "")
                response.orderNumber = request?.responseArray["CLIENT_REFERENCE_CODE"].cpToString();

            if (request?.responseArray?.ContainsKey("RESPONSE_CODE") == true && request?.responseArray["RESPONSE_CODE"].cpToInt() == 2 && request?.responseArray.ContainsKey("AUTH_CODE") == true && request?.responseArray["AUTH_CODE"].cpToString() != "" && request?.responseArray["AUTH_CODE"].cpToString() != "0")
            {
                string transactionId = "";

                if (request?.responseArray.ContainsKey("REFERENCE_CODE") == true)
                    transactionId = request?.responseArray["REFERENCE_CODE"].cpToString();

                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
                response.transactionId = transactionId;
            }
            else if (request?.responseArray?.ContainsKey("RESPONSE_DATA") == true && request?.responseArray["RESPONSE_DATA"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request?.responseArray["RESPONSE_DATA"].cpToString();
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"sx", auth.merchantPassword },
                {"referenceCode", request.orderNumber },
                {"type", "cancel" },
                {"amount", "" },
                {"trxDate", "" },
            };

            string hashString = req["sx"] + "|"
            + req["referenceCode"] + "|"
            + req["type"] + "|"
            + req["amount"] + "|"
            + req["trxDate"] + "|"
            + auth.merchantStorekey;

            string hash = GetHash(hashString);


            req.Add("hashDatav2", hash);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/Vpos/v1/CancelRefundPayment";

            string responseStr = RequestForm(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("RESPONSE_CODE") == true && responseDic["RESPONSE_CODE"].cpToInt() == 2)
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (responseDic?.ContainsKey("RESPONSE_DATA") == true && responseDic["RESPONSE_DATA"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["RESPONSE_DATA"].cpToString();
            }
            else
            {
                response.message = "İşlem iptal edilemedi";
            }

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error };

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"sx", auth.merchantPassword },
                {"referenceCode", request.orderNumber },
                {"type", "refund" },
                {"amount", request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"trxDate", "" },
            };


            string hashString = req["sx"] + "|"
            + req["referenceCode"] + "|"
            + req["type"] + "|"
            + req["amount"] + "|"
            + req["trxDate"] + "|"
            + auth.merchantStorekey;

            string hash = GetHash(hashString);


            req.Add("hashDatav2", hash);

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/Vpos/v1/CancelRefundPayment";

            string responseStr = RequestForm(req, link);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("RESPONSE_CODE") == true && responseDic["RESPONSE_CODE"].cpToInt() == 2)
            {
                response.statu = ResponseStatu.Success;
                response.message = "İşlem başarılı";
            }
            else if (responseDic?.ContainsKey("RESPONSE_DATA") == true && responseDic["RESPONSE_DATA"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["RESPONSE_DATA"].cpToString();
            }
            else
            {
                response.message = "İşlem iade edilemedi";
            }

            return response;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            AllInstallmentQueryResponse response = new AllInstallmentQueryResponse { confirm = false, installmentList = new List<AllInstallment>() };

            Dictionary<string, string> req = new Dictionary<string, string>
            {
                { "sx", auth.merchantID },
                { "date", DateTime.UtcNow.AddHours(3).ToString("dd.MM.yyyy") },
                { "hashDatav2", GetHash($"{auth.merchantID}|{DateTime.UtcNow.AddHours(3).ToString("dd.MM.yyyy")}|{auth.merchantStorekey}") },
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/Vpos/Payment/GetMerchandInformation";

            string responseStr = RequestForm(req, link);

            PayNKolayMerchantInformationResponse binResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PayNKolayMerchantInformationResponse>(responseStr);

            if (binResponse?.RESPONSE_CODE == 2 && binResponse?.COMMISSION_LIST?.Any() == true)
            {
                foreach (var item in binResponse.COMMISSION_LIST)
                {
                    if (item?.DATA?.Any() != true)
                        continue;

                    CreditCardProgram creditCardProgram = CreditCardProgram.Unknown;
                    string cardProgram = item.CODE.cpToString().ToUpper(System.Globalization.CultureInfo.GetCultureInfo("en-US"));

                    switch (cardProgram)
                    {
                        case "PARAF": creditCardProgram = CreditCardProgram.Paraf; break;
                        case "AXESS": creditCardProgram = CreditCardProgram.Axess; break;
                        case "BANKKART": creditCardProgram = CreditCardProgram.Bankkart; break;
                        case "MAXIMUM": creditCardProgram = CreditCardProgram.Maximum; break;
                        case "CARDFINANS": creditCardProgram = CreditCardProgram.CardFinans; break;
                        case "BONUS": creditCardProgram = CreditCardProgram.Bonus; break;
                        case "WORLD": creditCardProgram = CreditCardProgram.World; break;
                        case "WINGS": creditCardProgram = CreditCardProgram.Wings; break;
                        case "ADVANT": creditCardProgram = CreditCardProgram.Advantage; break;
                        case "MILES&SMILES": creditCardProgram = CreditCardProgram.MilesAndSmiles; break;

                        default:
                            creditCardProgram = CreditCardProgram.Unknown;
                            break;
                    }


                    if (creditCardProgram == CreditCardProgram.Unknown)
                        continue;


                    foreach (var installment in item.DATA)
                    {
                        if (!(installment?.INSTALLMENT > 1))
                            continue;

                        float commissionRate = 0;

                        if (installment.MERCHANT_COMMISSION > 0)
                        {
                            decimal cost = installment.MERCHANT_COMMISSION / 100m;
                            decimal extraRate = cost / (1 - cost);

                            commissionRate = Math.Round(extraRate * 100m, 2).cpToSingle();
                        }

                        AllInstallment model = new AllInstallment
                        {
                            bankCode = "9978",
                            cardProgram = creditCardProgram,
                            count = installment.INSTALLMENT,
                            customerCostCommissionRate = commissionRate
                        };

                        response.installmentList.Add(model);
                    }
                }
            }

            return response;
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse { confirm = false, installmentList = new List<installment>() };


            Dictionary<string, string> req = new Dictionary<string, string>
            {
                { "sx", auth.merchantID },
                { "amount", request.amount.ToString("N2", CultureInfo.GetCultureInfo("en-US")).Replace(",", "") },
                { "cardNumber", request.BIN },
                { "iscardvalid", "false" },
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/Vpos/Payment/PaymentInstallments";

            string responseStr = RequestForm(req, link);

            PayNKolayBinResponse binResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PayNKolayBinResponse>(responseStr);

            if (binResponse?.RESPONSE_CODE == 2 && binResponse?.PAYMENT_BANK_LIST?.Any() == true)
            {
                foreach (var installment in binResponse.PAYMENT_BANK_LIST)
                {
                    if (installment.INSTALLMENT > 1)
                    {
                        float commissionRate = 0;

                        if (installment.COMMISION > 0)
                        {
                            decimal cost = installment.COMMISION / 100m;
                            decimal extraRate = cost / (1 - cost);

                            commissionRate = Math.Round(extraRate * 100m, 2).cpToSingle();
                        }

                        response.installmentList.Add(new installment
                        {
                            count = installment.INSTALLMENT,
                            customerCostCommissionRate = commissionRate
                        });
                    }
                }
            }

            if (response.installmentList.Any())
                response.confirm = true;

            return response;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        private string GetHash(string hashstr)
        {
            using (var sha = SHA512.Create())
            {
                byte[] inputbytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashstr));

                return Convert.ToBase64String(inputbytes);
            }
        }

        private string RequestJson(Dictionary<string, object> param, string link)
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

        private string RequestForm(Dictionary<string, string> param, string link)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var req = new FormUrlEncodedContent(param))
            {
                req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = Encoding.UTF8.WebName };
                var response = client.PostAsync(link, req).Result;
                byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                responseString = Encoding.UTF8.GetString(responseByte);
            }

            return responseString;
        }
    }


    class PayNKolayBinResponse
    {
        public int RESPONSE_CODE { get; set; }
        public List<PayNKolayBinModelResponse> PAYMENT_BANK_LIST { get; set; }
    }

    class PayNKolayBinModelResponse
    {
        public int INSTALLMENT { get; set; }
        public decimal COMMISION { get; set; }
    }


    class PayNKolayMerchantInformationResponse
    {
        public int RESPONSE_CODE { get; set; }
        public List<PayNKolayMerchantInformationCommissionModel> COMMISSION_LIST { get; set; }

    }

    class PayNKolayMerchantInformationCommissionModel
    {
        public string CODE { get; set; }
        public string KEY { get; set; }
        public List<PayNKolayMerchantInformationCommissionDataModel> DATA { get; set; }
    }

    class PayNKolayMerchantInformationCommissionDataModel
    {
        public int INSTALLMENT { get; set; }
        public decimal MERCHANT_COMMISSION { get; set; }
    }
}