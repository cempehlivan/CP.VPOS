using CP.VPOS.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CP.VPOS.Models
{
    public class SaleRequest : ModelValidation
    {
        /// <summary>
        /// Sipariş Numarası
        /// </summary>
        [Required(ErrorMessage = "orderNumber alanı zorunludur")]
        [StringLength(maximumLength: 150, MinimumLength = 3, ErrorMessage = "orderNumber alanı {2}-{1} karakter olabilir")]
        public string orderNumber { get; set; }

        /// <summary>
        /// Ödeme işlemi yapan client IP adresi
        /// </summary>
        public string customerIPAddress { get; set; }

        /// <summary>
        /// Satış Bilgisi
        /// </summary>
        [Required(ErrorMessage = "saleInfo alanı zorunludur")]
        public SaleInfo saleInfo { get; set; }

        /// <summary>
        /// Fatura Adresi Bilgisi
        /// </summary>
        [Required(ErrorMessage = "invoiceInfo alanı zorunludur")]
        public CustomerInfo invoiceInfo { get; set; }

        /// <summary>
        /// Teslimat Adresi Bilgisi
        /// </summary>
        [Required(ErrorMessage = "shippingInfo alanı zorunludur")]
        public CustomerInfo shippingInfo { get; set; }

        /// <summary>
        /// 3D Çekim bilgisi
        /// </summary>
        [Required(ErrorMessage = "payment3D alanı zorunludur")]
        public Payment3D payment3D { get; set; }
    }

    public class SaleResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// Error = 0,
        /// Success = 1,
        /// RedirectURL = 2,
        /// RedirectHTML = 3,
        /// </summary>
        public SaleResponseStatu? statu { get; set; }

        /// <summary>
        /// İşlem sonucu; Mesaj, link veya HTML döner
        /// </summary>
        public string message { get; set; }

        /// <summary>
        ///  Sipariş Numarası, (Sizin verdiğiniz)
        /// </summary>
        public string orderNumber { get; set; }

        /// <summary>
        /// Banka tarafındaki işlem numarası. iptal, iade işlemlerinde kullanılır. İptal, iade yapabilmeniz için kaydetmeniz gerekir.
        /// </summary>
        public string transactionId { get; set; }

        /// <summary>
        /// Bankalardan verilen cevapların ham datası
        /// </summary>
        public Dictionary<string, object> privateResponse { get; set; }
    }

    public class Payment3D
    {
        /// <summary>
        /// 3D ödeme isteniyorsa true gönderilmelidir
        /// </summary>
        public bool confirm { get; set; }

        /// <summary>
        /// Başarılı veya başarısız ödeme ardından dönüş URL değeri.
        /// </summary>
        public string returnURL { get; set; }

        /// <summary>
        /// Masaüstünde kullanıyorsanız true yapmanız gerekir. Masaüstünde PORT açılır ve response beklenir. 
        /// </summary>
        public bool isDesktop { get; set; }
    }

    public class Sale3DResponseRequest : ModelValidation
    {
        /// <summary>
        /// HttpContext.Request.Form içerisindeki veri, bankadan gelen post datası
        /// </summary>
        [Required(ErrorMessage = "responseArray alanı zorunludur")]
        public Dictionary<string, object> responseArray { get; set; }

        /// <summary>
        /// Yapı kredi bankası için bu alan zorunludur
        /// </summary>
        public Currency? currency { get; set; }

        /// <summary>
        /// Yapı kredi bankası için bu alan zorunludur
        /// </summary>
        public decimal? amount { get; set; }
    }
}
