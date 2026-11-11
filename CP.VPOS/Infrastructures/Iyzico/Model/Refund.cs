using System;
using System.Threading;
using System.Threading.Tasks;
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

        public static Task<Refund> CreateAsync(CreateRefundRequest request, Options options, CancellationToken cancellationToken)
        {
            return RestHttpClient.Create().PostAsync<Refund>(options.BaseUrl + "/payment/refund", GetHttpHeaders(request, options), request, cancellationToken);
        }

        public static Task<Refund> CreateAmountBasedRefundRequestAsync(CreateAmountBasedRefundRequest request, Options options, CancellationToken cancellationToken)
        {
            return RestHttpClient.Create().PostAsync<Refund>(options.BaseUrl + "/v2/payment/refund", GetHttpHeaders(request, options), request, cancellationToken);
        }

    }
}
