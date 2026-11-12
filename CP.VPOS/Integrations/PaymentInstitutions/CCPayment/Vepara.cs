using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.Vepara
{
    internal class VeparaVirtualPOSService : CCPaymentVirtualPOSService
    {
        public VeparaVirtualPOSService() : base(
            urlAPILive: "https://app.vepara.com.tr/ccpayment",
            urlAPITest: "https://test.vepara.com.tr/ccpayment",
            bankCode: "9983",
            displayName: "Vepara")
        {
        }
    }
}
