using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class AdditionalInstallmentQueryRequest : ModelValidation
    {
        /// <summary>
        /// Satış Bilgisi
        /// </summary>
        [Required(ErrorMessage = "saleInfo alanı zorunludur")]
        public SaleInfo saleInfo { get; set; }
    }

    public class AdditionalInstallmentQueryResponse
    {
        /// <summary>
        /// İşlem Durumu
        /// </summary>
        public bool confirm { get; set; }

        /// <summary>
        /// Taksit listesi
        /// </summary>
        public List<AdditionalInstallment> installmentList { get; set; }
    }

    public class AdditionalInstallment
    {
        /// <summary>
        /// Taksit sayısı
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Kampanya Kodu
        /// </summary>
        public string campaignCode { get; set; }

        /// <summary>
        /// Kampanya adı
        /// </summary>
        public string campaignName { get; set; }

        /// <summary>
        /// Kampanya açıklama
        /// </summary>
        public string campaignDescription { get; set; }

        /// <summary>
        /// Taksit zorunlu olarak yapılacak
        /// </summary>
        public bool required { get; set; }
    }
}
