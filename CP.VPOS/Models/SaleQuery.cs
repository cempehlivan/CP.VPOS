using CP.VPOS.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CP.VPOS.Models
{
    public class SaleQueryRequest : ModelValidation
    {
        /// <summary>
        /// Sipariş numarası
        /// </summary>
        [Required(ErrorMessage = "orderNumber alanı zorunludur")]
        public string orderNumber { get; set; }
    }

    public class SaleQueryResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// Error = 0,
        /// Success = 1,
        /// NotFound = 2,
        /// </summary>
        public SaleQueryResponseStatu? statu { get; set; }

        /// <summary>
        /// İşlem sonucu
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
        /// İşlem tarihi
        /// </summary>
        public DateTime? transactionDate { get; set; }

        /// <summary>
        /// İşlem son durumu
        /// </summary>
        public SaleQueryTransactionStatu? transactionStatu { get; set; }

        /// <summary>
        /// Karttan çekilen tutar
        /// </summary>
        public decimal? amount { get; set; }

        /// <summary>
        /// Bankalardan verilen cevapların ham datası
        /// </summary>
        public Dictionary<string, object> privateResponse { get; set; }
    }
}
