using CP.VPOS.Enums;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using CP.VPOS.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CP.VPOS.Banks.Paynet
{
    internal class PaynetVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://pts-api.paynet.com.tr";
        private readonly string _urlAPILive = "https://api.paynet.com.tr";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            if (request?.payment3D?.confirm == true)
                return Sale3D(request, auth);

            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "Bilinmeyen bir hata oluştu",
                orderNumber = request.orderNumber,
            };

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;
            string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "");

            string host = "cp.vpos.local";

            try
            {
                if (!string.IsNullOrWhiteSpace(request?.payment3D?.returnURL))
                {
                    Uri uri = new Uri(request?.payment3D?.returnURL);
                    host = uri.Host;
                }
            }
            catch
            {
                host = "cp.vpos.local";
            }

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"amount", totalStr },
                {"reference_no", request.orderNumber },
                {"domain",  host},
                {"card_holder", request.saleInfo.cardNameSurname },
                {"pan", request.saleInfo.cardNumber},
                {"month", request.saleInfo.cardExpiryDateMonth },
                {"year", request.saleInfo.cardExpiryDateYear },
                {"cvc",request.saleInfo.cardCVV },
                {"card_holder_phone",request.invoiceInfo.phoneNumber },
                {"card_holder_mail",request.invoiceInfo.emailAddress },
                {"instalment", request.saleInfo.installment },
                {"add_commission", request.saleInfo.installment > 1 },
                {"transaction_type", 1 },
            };


            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v2/transaction/payment";

            string responseStr = RequestJson(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("is_succeed") == true && responseDic["is_succeed"].cpToBool() == true)
            {
                string transactionId = responseDic.ContainsKey("xact_id") ? responseDic["xact_id"].cpToString() : "";

                response.statu = SaleResponseStatu.Success;
                response.message = "İşlem başarılı";
                response.transactionId = transactionId;
            }
            else if (responseDic?.ContainsKey("paynet_error_message") == true && responseDic["paynet_error_message"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = responseDic["paynet_error_message"].cpToString();
            }

            return response;
        }

        private SaleResponse Sale3D(SaleRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse
            {
                statu = SaleResponseStatu.Error,
                message = "Bilinmeyen bir hata oluştu",
                orderNumber = request.orderNumber,
            };

            request.saleInfo.currency = request.saleInfo.currency ?? Currency.TRY;
            request.saleInfo.installment = request.saleInfo.installment > 1 ? request.saleInfo.installment : (sbyte)1;
            string totalStr = request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "");

            string host = "cp.vpos.local";

            try
            {
                if (!string.IsNullOrWhiteSpace(request?.payment3D?.returnURL))
                {
                    Uri uri = new Uri(request?.payment3D?.returnURL);
                    host = uri.Host;
                }
            }
            catch
            {
                host = "cp.vpos.local";
            }

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"amount", totalStr },
                {"reference_no", request.orderNumber },
                {"return_url", request.payment3D.returnURL },
                {"domain",  host},
                {"card_holder", request.saleInfo.cardNameSurname },
                {"pan", request.saleInfo.cardNumber},
                {"month", request.saleInfo.cardExpiryDateMonth },
                {"year", request.saleInfo.cardExpiryDateYear },
                {"cvc",request.saleInfo.cardCVV },
                {"card_holder_phone",request.invoiceInfo.phoneNumber },
                {"card_holder_mail",request.invoiceInfo.emailAddress },
                {"instalment", request.saleInfo.installment },
                {"add_commission", request.saleInfo.installment > 1 },
                {"transaction_type", 1 },
            };


            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v2/transaction/tds_initial";

            string responseStr = RequestJson(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("code") == true && (responseDic["code"].cpToInt() == 0 || responseDic["code"].cpToInt() == 100))
            {
                response.statu = SaleResponseStatu.RedirectHTML;
                response.message = responseDic["html_content"].cpToString();
            }
            else if (responseDic?.ContainsKey("message") == true && responseDic["message"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = responseDic["message"].cpToString();
            }

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            SaleResponse response = new SaleResponse { statu = SaleResponseStatu.Error, message = "İşlem sırasında bilinmeyen bir hata oluştu." };

            response.privateResponse = new Dictionary<string, object>();

            response.privateResponse.Add("response_1", request?.responseArray);


            if (request?.responseArray?.ContainsKey("session_id") == true && request?.responseArray?.ContainsKey("token_id") == true)
            {
                Dictionary<string, object> req = new Dictionary<string, object> {
                    {"session_id", request.responseArray["session_id"].cpToString() },
                    {"token_id", request.responseArray["token_id"].cpToString() },
                    {"transaction_type", 1 },
                };

                string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v2/transaction/tds_charge";

                string responseStr = RequestJson(req, link, auth);

                Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

                response.privateResponse.Add("response_2", responseDic);

                if(responseDic?.ContainsKey("reference_no") == true && responseDic["reference_no"].cpToString() != "")
                    response.orderNumber = responseDic["reference_no"].cpToString();

                if (responseDic?.ContainsKey("is_succeed") == true && responseDic["is_succeed"].cpToBool() == true)
                {
                    string transactionId = responseDic.ContainsKey("xact_id") ? responseDic["xact_id"].cpToString() : "";

                    response.statu = SaleResponseStatu.Success;
                    response.message = "İşlem başarılı";
                    response.transactionId = transactionId;
                }
                else if (responseDic?.ContainsKey("paynet_error_message") == true && responseDic["paynet_error_message"].cpToString() != "")
                {
                    response.statu = SaleResponseStatu.Error;
                    response.message = responseDic["paynet_error_message"].cpToString();
                }
            }
            else if (request?.responseArray?.ContainsKey("message") == true && request?.responseArray["message"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request?.responseArray["message"].cpToString();
            }
            else if (request?.responseArray?.ContainsKey("paynet_error_message") == true && request?.responseArray["paynet_error_message"].cpToString() != "")
            {
                response.statu = SaleResponseStatu.Error;
                response.message = request?.responseArray["paynet_error_message"].cpToString();
            }

            return response;
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            CancelResponse response = new CancelResponse { statu = ResponseStatu.Error, message = "İptal işlemi sırasında bir hata oluştu" };

            Dictionary<string, object> req = new Dictionary<string, object> {
                {"xact_id", request.transactionId },
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v1/transaction/reversed_request";

            string responseStr = RequestJson(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("code") == true && (responseDic["code"].cpToInt() == 0 || responseDic["code"].cpToInt() == 100))
            {
                response.statu = ResponseStatu.Success;
                response.message = "İptal işlemi başarılı";
            }
            else if (responseDic?.ContainsKey("message") == true && responseDic["message"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["message"].cpToString();
            }

            return response;
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            RefundResponse response = new RefundResponse { statu = ResponseStatu.Error, message = "İade işlemi sırasında bir hata oluştu" };

            string refundAmount = request.refundAmount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", "");


            Dictionary<string, object> req = new Dictionary<string, object> {
                {"xact_id", request.transactionId },
                {"amount", refundAmount },
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v1/transaction/reversed_request";

            string responseStr = RequestJson(req, link, auth);

            Dictionary<string, object> responseDic = JsonConvertHelper.Convert<Dictionary<string, object>>(responseStr);

            response.privateResponse = responseDic;

            if (responseDic?.ContainsKey("code") == true && (responseDic["code"].cpToInt() == 0 || responseDic["code"].cpToInt() == 100))
            {
                response.statu = ResponseStatu.Success;
                response.message = "İade işlemi başarılı";
                response.refundAmount = request.refundAmount;
            }
            else if (responseDic?.ContainsKey("message") == true && responseDic["message"].cpToString() != "")
            {
                response.statu = ResponseStatu.Error;
                response.message = responseDic["message"].cpToString();
            }

            return response;
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
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
                            bankCode = "9977",
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
            BINInstallmentQueryResponse response = new BINInstallmentQueryResponse { confirm = false, installmentList = new List<installment>() };


            Dictionary<string, object> req = new Dictionary<string, object>
            {
                { "bin", request.BIN },
                { "amount", request.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(",", "").Replace(".", "") },
                { "addcomission_to_amount", true },
            };

            string link = $"{(auth.testPlatform ? _urlAPITest : _urlAPILive)}/v1/ratio/Get";

            string responseStr = RequestJson(req, link, auth);

            PaynetBINInstallmentModel binResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PaynetBINInstallmentModel>(responseStr);


            if (binResponse?.code == 0 && binResponse?.data?.Any() == true && binResponse?.data?.FirstOrDefault()?.ratio?.Any() == true)
            {
                var installment_list = binResponse.data.First().ratio.ToList();

                foreach (var installment in installment_list)
                {

                    int installments_number = installment.instalment;
                    decimal payable_amount = installment.total_amount;
                    float commissionRate = 0;

                    if (installments_number > 1)
                    {
                        if (payable_amount > request.amount)
                            commissionRate = ((((decimal)100 * payable_amount) / request.amount) - (decimal)100).cpToSingle();

                        response.installmentList.Add(new installment
                        {
                            count = installments_number,
                            customerCostCommissionRate = commissionRate
                        });
                    }
                }
            }

            if (response.installmentList.Any())
                response.confirm = true;

            return response;
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        public SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth)
        {
            return null;
        }

        private string RequestJson(Dictionary<string, object> param, string link, VirtualPOSAuth auth)
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

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", auth.merchantPassword);

                    var response = client.PostAsync(link, req).Result;
                    byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                    responseString = Encoding.UTF8.GetString(responseByte);
                }
            }
            catch { }

            return responseString;
        }
    }

    class PaynetBINInstallmentModel
    {
        public bool tds_required { get; set; }
        public string object_name { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<PaynetBINInstallmentData> data { get; set; }
    }

    class PaynetBINInstallmentData
    {
        public string bank_id { get; set; }
        public string bank_logo { get; set; }
        public string bank_name { get; set; }
        public string card_type { get; set; }
        public bool tds_required { get; set; }
        public List<PaynetBINInstallmentDataRatio> ratio { get; set; }
    }

    class PaynetBINInstallmentDataRatio
    {
        public int instalment { get; set; }
        public decimal instalment_amount { get; set; }
        public decimal total_net_amount { get; set; }
        public decimal total_amount { get; set; }
        public decimal commision { get; set; }
    }
}
