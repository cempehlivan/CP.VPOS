using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class VirtualPOSAuth : ModelValidation
    {
        /// <summary>
        /// 4 haneli banka global EFT Kodu, Aktif banka listesine GetBankList methodundan ulaşılabilir
        /// </summary>
        [Required(ErrorMessage = "bankCode alanı zorunludur")]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "Banka kodu 4 haneden oluşmalıdır")]
        public string bankCode { get; set; }

        /// <summary>
        /// üye İşyeri Kodu
        /// </summary>
        [Required(ErrorMessage = "merchantID alanı zorunludur")]
        public string merchantID { get; set; }

        /// <summary>
        /// Üye işyeri kullanıcı kodu
        /// </summary>
        [Required(ErrorMessage = "merchantUser alanı zorunludur")]
        public string merchantUser { get; set; }

        /// <summary>
        /// Üye işyeri şifre
        /// </summary>
        [Required(ErrorMessage = "merchantPassword alanı zorunludur")]
        public string merchantPassword { get; set; }

        /// <summary>
        /// Üye işyeri anahtarı, Bazı bankalarda 3D kullanabilmek için zorunludur (Nestpay)
        /// </summary>
        public string merchantStorekey { get; set; }

        /// <summary>
        /// Test ortamı ise true gönderilmelidir.
        /// </summary>
        public bool testPlatform { get; set; }
    }
}
