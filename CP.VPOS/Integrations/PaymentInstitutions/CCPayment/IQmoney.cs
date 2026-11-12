using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.IQmoney
{
    internal class IQmoneyVirtualPOSService : CCPaymentVirtualPOSService
    {
        public IQmoneyVirtualPOSService() : base(
            urlAPILive: "https://app.iqmoneytr.com/ccpayment",
            urlAPITest: "https://provisioning.iqmoneytr.com/ccpayment",
            bankCode: "9986",
            displayName: "IQmoney")
        {
        }
    }
}
