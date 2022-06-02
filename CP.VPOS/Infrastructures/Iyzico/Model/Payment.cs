using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class Payment : PaymentResource
    {
        public static Payment Create(CreatePaymentRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Payment>(options.BaseUrl + "/payment/auth", GetHttpHeaders(request, options), request);
        }
    }
}
