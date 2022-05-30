using CP.VPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class CreditCardBinQueryRequest : ModelValidation
    {
        [Required(ErrorMessage = "binNumber alanı zorunludur")]
        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "binNumber alanı kart numarasının ilk 6 hanesi olması gerekmektedir")]
        [RegularExpression(pattern: "[0-9]+", ErrorMessage = "binNumber alanı sadece rakamlardan oluşmalıdır")]
        public string binNumber { get; set; }
    }

    public class CreditCardBinQueryResponse
    {
        /// <summary>
        /// Kredi kartı 6 haneli bin numarası
        /// </summary>
        public string binNumber { get; set; }
        /// <summary>
        /// Banka kodu
        /// </summary>
        public string bankCode { get; set; }
        /// <summary>
        /// Kart tipi
        /// </summary>
        public CreditCardType cardType { get; set; }
        /// <summary>
        /// Kart markası
        /// </summary>
        public CreditCardBrand cardBrand { get; set; }
        /// <summary>
        /// Ticari kart
        /// </summary>
        public bool commercialCard { get; set; }
    }
}

