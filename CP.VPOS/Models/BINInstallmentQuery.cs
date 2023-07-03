using CP.VPOS.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class BINInstallmentQueryRequest : ModelValidation
    {
        /// <summary>
        /// Kart BIN numarası, kartın ilk 6 hanesi
        /// </summary>
        [Required(ErrorMessage = "BIN alanı zorunludur")]
        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Kart BIN numarası 6 haneli olmalıdır")]
        [RegularExpression(pattern: "[0-9]+", ErrorMessage = "cardNumber alanı sadece rakamlardan oluşmalıdır")]
        public string BIN { get; set; }

        /// <summary>
        /// Kart tan çekilmek istenen tutar
        /// </summary>
        [Required(ErrorMessage = "Tutar alanı zorunludur")]
        [Range(minimum: 0.0001, maximum: 10000000.00, ErrorMessage = "Tutar alanı sıfırdan büyük olmalıdır")]
        public decimal amount { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        public Currency? currency { get; set; }
    }

    public class BINInstallmentQueryResponse
    {
        /// <summary>
        /// İşlem Durumu
        /// </summary>
        public bool confirm { get; set; }

        /// <summary>
        /// Taksit listesi
        /// </summary>
        public List<installment> installmentList { get; set; }
    }

    public class installment
    {
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
