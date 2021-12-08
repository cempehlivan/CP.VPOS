using CP.VPOS.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class RefundRequest : ModelValidation
    {
        /// <summary>
        /// Ödeme işlemi yapan client IP adresi
        /// </summary>
        [Required(ErrorMessage = "customerIPAddress alanı zorunludur")]
        public string customerIPAddress { get; set; }

        /// <summary>
        /// Sipariş numarası
        /// </summary>
        [Required(ErrorMessage = "orderNumber alanı zorunludur")]
        public string orderNumber { get; set; }

        /// <summary>
        /// Banka tarafındaki işlem numarası.
        /// </summary>
        [Required(ErrorMessage = "transactionId alanı zorunludur")]
        public string transactionId { get; set; }

        /// <summary>
        /// İade edilecek tutar
        /// </summary>
        [Required(ErrorMessage = "refundAmount alanı zorunludur")]
        [Range(minimum: 0.0001, maximum: 10000000.00, ErrorMessage = "refundAmount alanı sıfırdan büyük olmalıdır")]
        public decimal refundAmount { get; set; }

        /// <summary>
        /// İade edilecek tutar para birimi
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "currency alanı zorunludur")]
        public Currency? currency { get; set; }
    }

    public class RefundResponse
    {
        /// <summary>
        /// İşlem sonucu
        /// </summary>
        public ResponseStatu statu { get; set; }

        /// <summary>
        /// İşlem sonucu mesajı
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// İade edilen tutar
        /// </summary>
        public decimal? refundAmount { get; set; }
        /// <summary>
        /// Bankalardan verilen cevapların ham datası
        /// </summary>
        public Dictionary<string, object> privateResponse { get; set; }
    }
}
