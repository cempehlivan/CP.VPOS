using CP.VPOS.Enums;
using System.ComponentModel.DataAnnotations;

namespace CP.VPOS.Models
{
    public class CustomerInfo : ModelValidation
    {
        /// <summary>
        /// Adı
        /// </summary>
        [Required(ErrorMessage = "'Ad' alanı zorunludur")]
        public string name { get; set; }

        /// <summary>
        /// Soyadı
        /// </summary>
        [Required(ErrorMessage = "'Soyad' alanı zorunludur")]
        public string surname { get; set; }

        /// <summary>
        /// E-Posta Adresi
        /// </summary>
        [Required(ErrorMessage = "'E-Posta Adresi' alanı zorunludur")]
        public string emailAddress { get; set; }

        /// <summary>
        /// Telefon Numarası
        /// </summary>
        [Required(ErrorMessage = "'Telefon Numarası' alanı zorunludur")]
        public string phoneNumber { get; set; }

        /// <summary>
        /// Vergi/TC No
        /// </summary>
        [Required(ErrorMessage = "'Vergi/TC No' alanı zorunludur")]
        public string taxNumber { get; set; }

        /// <summary>
        /// Vergi Dairesi
        /// </summary>
        public string taxOffice { get; set; }

        /// <summary>
        /// Ülke Kodu, ISO 3166-1 standardında yer alan bir alpha-3 kodu gönderilmelidir.
        /// </summary>
        public Country country { get; set; }

        /// <summary>
        /// İl Adı
        /// </summary>
        public string cityName { get; set; }

        /// <summary>
        /// İlçe Adı
        /// </summary>
        public string townName { get; set; }

        /// <summary>
        /// Açık Adres
        /// </summary>
        public string addressDesc { get; set; }

        /// <summary>
        /// Posta Kodu
        /// </summary>
        public string postCode { get; set; }
    }

}
