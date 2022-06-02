using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class ThreedsPayment : PaymentResource
    {
        public static ThreedsPayment Create(CreateThreedsPaymentRequest request, Options options)
        {
            return RestHttpClient.Create().Post<ThreedsPayment>(options.BaseUrl + "/payment/3dsecure/auth", GetHttpHeaders(request, options), request);
        }
    }
}
