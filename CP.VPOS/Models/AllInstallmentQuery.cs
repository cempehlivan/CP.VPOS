using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class AllInstallmentQueryRequest : ModelValidation
    {
        /// <summary>
        /// Kart tan çekilmek istenen tutar
        /// </summary>
        [Required(ErrorMessage = "Tutar alanı zorunludur")]
        [Range(minimum: 0.0001, maximum: 10000000.00, ErrorMessage = "Tutar alanı sıfırdan büyük olmalıdır")]
        public decimal amount { get; set; }
    }

    public class AllInstallmentQueryResponse
    {
        /// <summary>
        /// İşlem Durumu
        /// </summary>
        public bool confirm { get; set; }

        /// <summary>
        /// Taksit listesi
        /// </summary>
        public List<AllInstallment> installmentList { get; set; }
    }


    public class AllInstallment
    {
        /// <summary>
        /// Ödeme sistemi banka eft kodu
        /// </summary>
        public string bankCode { get; set; }

        /// <summary>
        /// Taksit sayısı
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// Müşteriye yansıtılacak komisyon oranı
        /// </summary>
        public float customerCostCommissionRate { get; set; }
    }
}
