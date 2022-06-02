using System;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class ConvertedPayout
    {
        public String PaidPrice { get; set; }
        public String IyziCommissionRateAmount { get; set; }
        public String IyziCommissionFee { get; set; }
        public String BlockageRateAmountMerchant { get; set; }
        public String BlockageRateAmountSubMerchant { get; set; }
        public String SubMerchantPayoutAmount { get; set; }
        public String MerchantPayoutAmount { get; set; }
        public String IyziConversionRate { get; set; }
        public String IyziConversionRateAmount { get; set; }
        public String Currency { get; set; }
    }
}
