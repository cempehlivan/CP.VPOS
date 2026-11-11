using System.Threading;
using System.Threading.Tasks;
using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class Payment : PaymentResource
    {
        public static Task<Payment> CreateAsync(CreatePaymentRequest request, Options options, CancellationToken cancellationToken)
        {
            return RestHttpClient.Create().PostAsync<Payment>(options.BaseUrl + "/payment/auth", GetHttpHeaders(request, options), request, cancellationToken);
        }
    }
}
