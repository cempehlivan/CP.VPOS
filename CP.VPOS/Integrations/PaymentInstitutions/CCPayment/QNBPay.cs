using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.QNBpay
{
    internal class QNBpayVirtualPOSService : CCPaymentVirtualPOSService
    {
        public QNBpayVirtualPOSService() : base(
            urlAPILive: "https://portal.qnbpay.com.tr/ccpayment",
            urlAPITest: "https://test.qnbpay.com.tr/ccpayment",
            bankCode: "9990",
            displayName: "QNBpay")
        {
        }
    }
}
