using System.Threading;
using System.Threading.Tasks;
using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class ThreedsPayment : PaymentResource
    {
        public static Task<ThreedsPayment> CreateAsync(CreateThreedsPaymentRequest request, Options options, CancellationToken cancellationToken)
        {
            return RestHttpClient.Create().PostAsync<ThreedsPayment>(options.BaseUrl + "/payment/3dsecure/auth", GetHttpHeaders(request, options), request, cancellationToken);
        }
    }
}
