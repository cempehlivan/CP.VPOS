using CP.VPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class SaleInfo : ModelValidation
    {
        /// <summary>
        /// Kart Sahibi Adı Soyadı
        /// </summary>
        [Required(ErrorMessage = "cardNameSurname alanı zorunludur")]
        public string cardNameSurname { get; set; }

        /// <summary>
        /// Kart Numarası
        /// </summary>
        [Required(ErrorMessage = "cardNumber alanı zorunludur")]
        [StringLength(maximumLength: 19, MinimumLength = 15, ErrorMessage = "cardNumber alanı {2}-{1} karakter arasında olmalıdır")]
        [RegularExpression(pattern: "[0-9]+", ErrorMessage = "cardNumber alanı sadece rakamlardan oluşmalıdır")]
        public string cardNumber { get; set; }

        /// <summary>
        /// Kart Geçerlilik Tarihi Ayı, Örn: 10
        /// </summary>
        [Required(ErrorMessage = "cardExpiryDateMonth alanı zorunludur")]
        [Range(minimum: 1, maximum: 12, ErrorMessage = "cardExpiryDateMonth alanı {1}-{2} arasında olmalıdır")]
        public short cardExpiryDateMonth { get; set; }

        /// <summary>
        /// Kart Geçerlilik Tarihi Yılı, Örn: 2020
        /// </summary>
        [Required(ErrorMessage = "cardExpiryDateYear alanı zorunludur")]
        [Range(minimum: 2019, maximum: 3100, ErrorMessage = "cardExpiryDateYear alanı geçerlik değil")]
        public ushort cardExpiryDateYear { get; set; }

        /// <summary>
        /// Kart Güvenlik Kodu
        /// </summary>
        [Required(ErrorMessage = "cardCVV alanı zorunludur")]
        [StringLength(maximumLength: 4, MinimumLength = 3, ErrorMessage = "cardCVV alanı {2} veya {1} karakter olmalıdır")]
        [RegularExpression(pattern: "[0-9]+", ErrorMessage = "cardCVV alanı sadece rakamlardan oluşmalıdır")]
        public string cardCVV { get; set; }

        /// <summary>
        /// Para birimi
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "currency alanı zorunludur")]
        public Currency? currency { get; set; }

        /// <summary>
        /// Karttan çekilecek tutar
        /// </summary>
        [Required(ErrorMessage = "amount alanı zorunludur")]
        [Range(minimum: 0.0001, maximum: 10000000.00, ErrorMessage = "amount alanı sıfırdan büyük olmalıdır")]
        public decimal amount { get; set; }

        /// <summary>
        /// Çekim işleminde kullanılacak puan tutarı
        /// </summary>
        [Range(minimum: 0.0000, maximum: 10000000.00, ErrorMessage = "point alanı sıfırdan küçük olamaz")]
        public decimal? point { get; set; }

        /// <summary>
        /// Taksit sayısı
        /// </summary>
        [Required(ErrorMessage = "installment alanı zorunludur")]
        [Range(minimum: 1, maximum: 15, ErrorMessage = "installment alanı {1}-{2} arasında olmalıdır")]
        public sbyte installment { get; set; }

        /// <summary>
        /// Kampanya kodu (varsa)
        /// </summary>
        public string campaignCode { get; set; }
    }
}
