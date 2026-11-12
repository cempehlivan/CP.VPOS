using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.PayBull
{
    internal class PayBullVirtualPOSService : CCPaymentVirtualPOSService
    {
        public PayBullVirtualPOSService() : base(
            urlAPILive: "https://app.paybull.com/ccpayment",
            urlAPITest: "https://test.paybull.com/ccpayment",
            bankCode: "9988",
            displayName: "PayBull")
        {
        }
    }
}
