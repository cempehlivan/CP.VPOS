using System;
using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class Refund : IyzipayResource
    {
        public String PaymentId { get; set; }
        public String PaymentTransactionId { get; set; }
        public String Price { get; set; }
        public String Currency { get; set; }
        public String ConnectorName { get; set; }
        public String AuthCode { get; set; }
        public String HostReference { get; set; }

        public static Refund Create(CreateRefundRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Refund>(options.BaseUrl + "/payment/refund", GetHttpHeaders(request, options), request);
        }

        public static Refund CreateAmountBasedRefundRequest(CreateAmountBasedRefundRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Refund>(options.BaseUrl + "/v2/payment/refund", GetHttpHeaders(request, options), request);
        }

    }
}
