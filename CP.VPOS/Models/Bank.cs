using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CP.VPOS.Models
{
    public class Bank
    {
        /// <summary>
        /// Banka EFT kodu
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// Banka adı
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Ortak sanal pos mu?
        /// </summary>
        public bool CollectiveVPOS { get; set; }

        /// <summary>
        /// Taksit komisyonu tutara otomatik eklenir.
        /// </summary>
        public bool CommissionAutoAdd { get; set; }

        /// <summary>
        /// Taksit API'den sorgulanır
        /// </summary>
        public bool InstallmentAPI { get; set; }
    }
}
