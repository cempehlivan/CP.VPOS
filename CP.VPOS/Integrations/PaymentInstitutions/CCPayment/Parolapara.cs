using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.Parolapara
{
    internal class ParolaparaVirtualPOSService : CCPaymentVirtualPOSService
    {
        public ParolaparaVirtualPOSService() : base(
            urlAPILive: "https://ccpayment.parolapara.com/ccpayment",
            urlAPITest: "https://testccpayment.parolapara.com/ccpayment",
            bankCode: "9987",
            displayName: "Parolapara")
        {
        }
    }
}
