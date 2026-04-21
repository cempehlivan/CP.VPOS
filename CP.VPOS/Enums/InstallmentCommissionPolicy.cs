namespace CP.VPOS.Enums
{
    public enum InstallmentCommissionPolicy
    {
        Default = 0,
        /// <summary>
        /// Komisyon müşteriye yansıtılır
        /// </summary>
        ChargeToCustomer = 1,
        /// <summary>
        /// Komisyonu satıcı karşılar
        /// </summary>
        AbsorbByMerchant = 2
    }
}
