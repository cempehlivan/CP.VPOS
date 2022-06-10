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

namespace CP.VPOS.Banks.Denizbank
{
    internal class DenizbankVirtualPOSService : IVirtualPOSService
    {
        private readonly string _urlAPITest = "https://test.inter-vpos.com.tr/mpi/Default.aspx";
        private readonly string _urlAPILive = "https://inter-vpos.com.tr/mpi/Default.aspx";

        public SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            //if (request?.payment3D?.confirm == true)
            //    return Sale3D(request, auth);

            SaleResponse response = new SaleResponse();
            response.orderNumber = request.orderNumber;

            Dictionary<string, string> req = new Dictionary<string, string> {
                {"ShopCode", auth.merchantID },
                {"UserCode", auth.merchantUser },
                {"UserPass", auth.merchantPassword },
                {"PurchAmount", request.saleInfo.amount.ToString("N2", CultureInfo.GetCultureInfo("tr-TR")).Replace(".", "").Replace(",", ".") },
                {"Currency", ((int)request.saleInfo.currency).ToString() },
                {"OrderId", request.orderNumber },
                {"TxnType", "Auth" },
                {"InstallmentCount", request.saleInfo.installment.ToString() },
                {"SecureType", "NonSecure" },
                {"Pan", request.saleInfo.cardNumber},
                {"Cvv2",request.saleInfo.cardCVV},
                {"Expiry", request.saleInfo.cardExpiryDateMonth.ToString("00") + request.saleInfo.cardExpiryDateYear.ToString().Substring(2)},
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

            if (dic?.ContainsKey("TxnResult") == true && dic["TxnResult"].cpToString() == "Success")
            {
                response.statu = SaleResponseStatu.Success;
                response.message = " işlem başarıyla tamamlandı";
            }
            else
            {
                string message = "İşlem sırasında bir hata oluştu";

                if (dic?.ContainsKey("ErrorMessage") == true && dic["ErrorMessage"].cpToString() != "")
                    message = dic["ErrorMessage"].cpToString();

                response.statu = SaleResponseStatu.Error;
                response.message = message;
            }

            response.privateResponse = dic;
            response.orderNumber = request.orderNumber;

            if (dic.ContainsKey("TransId"))
                response.transactionId = dic["TransId"].cpToString();

            return response;
        }

        public SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        public BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            throw new NotImplementedException();
        }

        private string Request(Dictionary<string, string> param, VirtualPOSAuth auth, string link = null)
        {
            string responseString = "";

            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.Expect100Continue = false;

            using (HttpClient client = new HttpClient())
            using (var req = new FormUrlEncodedContent(param))
            {
                req.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var response = client.PostAsync(link ?? (auth.testPlatform ? _urlAPITest : _urlAPILive), req).Result;
                byte[] responseByte = response.Content.ReadAsByteArrayAsync().Result;
                responseString = Encoding.UTF8.GetString(responseByte);
            }

            return responseString;
        }
    }
}
