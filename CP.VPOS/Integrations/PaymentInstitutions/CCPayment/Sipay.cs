using CP.VPOS.Banks.CCPayment;

namespace CP.VPOS.Banks.Sipay
{
    internal class SipayVirtualPOSService : CCPaymentVirtualPOSService
    {
        public SipayVirtualPOSService() : base(
            urlAPILive: "https://app.sipay.com.tr/ccpayment",
            urlAPITest: "https://provisioning.sipay.com.tr/ccpayment",
            bankCode: "9991",
            displayName: "Sipay")
        {
        }

        /// <summary>
        /// Sipay taksit listesinde kart programını "getpos_card_program" alanında döner.
        /// </summary>
        protected override string CardProgramKey => "getpos_card_program";

        /// <summary>
        /// Sipay non-3D satış yanıtında payment_status alanını dönmez; işlem status_code
        /// kontrolünden geçtiyse başarılı kabul edilir.
        /// </summary>
        protected override bool ConfirmsWithPaymentStatus => false;
    }
}
