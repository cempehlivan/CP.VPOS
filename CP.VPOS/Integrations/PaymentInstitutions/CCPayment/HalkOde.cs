using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.HalkOde
{
    internal class HalkOdeVirtualPOSService : CCPaymentVirtualPOSService
    {
        public HalkOdeVirtualPOSService() : base(
            urlAPILive: "https://app.halkode.com.tr/ccpayment",
            urlAPITest: "https://testapp.halkode.com.tr/ccpayment",
            bankCode: "9979",
            displayName: "HalkOde")
        {
        }
    }
}
