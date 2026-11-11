using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CP.VPOS.Enums;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using CP.VPOS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CP.VPOS.Test
{
    /// <summary>
    /// VPOSClient'ın bankaya istek göndermeden önce yaptığı doğrulamalar ve
    /// tamamen yerel çalışan BIN/banka sorguları.
    /// </summary>
    [TestClass]
    public class VPOSClientTests
    {
        private const string UnknownBankCode = "XXXX";
        private const string BankNotFoundMessage = "Banka entegrasyonu bulunamadı.";

        private static CustomerInfo Customer() => new CustomerInfo
        {
            name = "cem",
            surname = "pehlivan",
            emailAddress = "test@test.com",
            phoneNumber = "1111111111",
            taxNumber = "1111111111",
            taxOffice = "maltepe",
            country = Country.TUR,
            cityName = "istanbul",
            townName = "maltepe",
            addressDesc = "adres",
            postCode = "34000"
        };

        private static VirtualPOSAuth Auth(string bankCode) => new VirtualPOSAuth
        {
            bankCode = bankCode,
            merchantID = "1",
            merchantUser = "1",
            merchantPassword = "1",
            merchantStorekey = "1",
            testPlatform = true
        };

        private static SaleRequest Request(string cardNumber) => new SaleRequest
        {
            orderNumber = "TEST-1",
            customerIPAddress = "1.1.1.1",
            invoiceInfo = Customer(),
            shippingInfo = Customer(),
            payment3D = new Payment3D { confirm = false },
            saleInfo = new SaleInfo
            {
                cardNameSurname = "Test Kart",
                cardNumber = cardNumber,
                cardExpiryDateMonth = 1,
                cardExpiryDateYear = 2030,
                cardCVV = "030",
                currency = Currency.TRY,
                installment = 1,
                amount = 100.50m
            }
        };

        /// <summary>
        /// Verilen işlemin fırlattığı hatayı döner; hata fırlatılmazsa testi başarısız yapar.
        /// </summary>
        private static Exception CapturedException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                return ex;
            }

            Assert.Fail("Beklenen hata fırlatılmadı.");
            return null;
        }

        #region Kart numarası doğrulaması (ağ erişimi yok)

        [TestMethod]
        public void Sale_ThrowsValidationExceptionForInvalidCardNumber()
        {
            Exception ex = CapturedException(() => VPOSClient.Sale(Request("1234567812345678"), Auth(UnknownBankCode)));

            Assert.IsInstanceOfType(ex, typeof(ValidationException));
            StringAssert.Contains(ex.Message, "Geçersiz kart numarası");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("424242424")]
        [DataRow("4242424242424242abc")]
        public void Sale_RejectsMalformedCardNumbers(string cardNumber)
        {
            Exception ex = CapturedException(() => VPOSClient.Sale(Request(cardNumber), Auth(UnknownBankCode)));

            Assert.IsInstanceOfType(ex, typeof(ValidationException));
        }

        [TestMethod]
        [DataRow("4242 4242 4242 4242")]
        [DataRow("4242-4242-4242-4242")]
        public void Sale_StripsSpacesAndDashesFromCardNumber(string cardNumber)
        {
            // Numara temizlendiği için kart doğrulamasını geçer ve banka arama adımına kadar ilerler
            Exception ex = CapturedException(() => VPOSClient.Sale(Request(cardNumber), Auth(UnknownBankCode)));

            Assert.IsNotInstanceOfType(ex, typeof(ValidationException), "kart numarası temizlenmemiş");
            Assert.AreEqual(BankNotFoundMessage, ex.Message);
        }

        [TestMethod]
        public void Sale_ThrowsValidationExceptionWhenRequiredFieldMissing()
        {
            SaleRequest request = Request("4242424242424242");
            request.orderNumber = null;

            Exception ex = CapturedException(() => VPOSClient.Sale(request, Auth(UnknownBankCode)));

            Assert.IsInstanceOfType(ex, typeof(ValidationException));
            StringAssert.Contains(ex.Message, "orderNumber");
        }

        [TestMethod]
        public void Sale_ThrowsMeaningfulErrorForUnknownBankCode()
        {
            Exception ex = CapturedException(() => VPOSClient.Sale(Request("4242424242424242"), Auth(UnknownBankCode)));

            Assert.AreEqual(BankNotFoundMessage, ex.Message);
        }

        #endregion

        #region BIN sorgulama (tamamen yerel)

        [TestMethod]
        public void CreditCardBinQuery_ReturnsKnownBin()
        {
            CreditCardBinQueryResponse bin = VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "374421" });

            Assert.IsNotNull(bin);
            Assert.AreEqual("374421", bin.binNumber);
            Assert.AreEqual("0062", bin.bankCode);
        }

        [TestMethod]
        public void CreditCardBinQuery_FallsBackToSixDigitBin()
        {
            CreditCardBinQueryResponse bin = VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "37442100" });

            Assert.IsNotNull(bin);
            Assert.AreEqual("374421", bin.binNumber);
        }

        [TestMethod]
        public void CreditCardBinQuery_ReturnsNullForUnknownBin()
        {
            Assert.IsNull(VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "000000" }));
        }

        [TestMethod]
        public void CreditCardBinQuery_ThrowsValidationExceptionForEmptyBin()
        {
            Exception ex = CapturedException(() => VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = null }));

            Assert.IsInstanceOfType(ex, typeof(ValidationException));
        }

        [TestMethod]
        public void CreditCardBinQuery_PutsOwnBankFirstInInstallmentList()
        {
            CreditCardBinQueryResponse bin = VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "374421" });

            Assert.IsNotNull(bin.banksWithInstallments);
            Assert.IsTrue(bin.banksWithInstallments.Count > 0);
            Assert.AreEqual(bin.bankCode, bin.banksWithInstallments[0]);
        }

        [TestMethod]
        public void CreditCardBinQuery_DoesNotMutateSharedList()
        {
            CreditCardBinQueryResponse birinci = VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "374421" });
            birinci.bankCode = "DEGISTI";

            CreditCardBinQueryResponse ikinci = VPOSClient.CreditCardBinQuery(new CreditCardBinQueryRequest { binNumber = "374421" });

            Assert.AreEqual("0062", ikinci.bankCode);
        }

        [TestMethod]
        public void AllCreditCardBinList_ReturnsPopulatedList()
        {
            List<CreditCardBinQueryResponse> list = VPOSClient.AllCreditCardBinList();

            Assert.IsTrue(list.Count > 100, "BIN listesi beklenenden kısa");
            Assert.IsTrue(list.All(s => !string.IsNullOrEmpty(s.binNumber)));
        }

        #endregion

        #region Banka listesi

        [TestMethod]
        public void AllBankList_AppliesFilter()
        {
            List<Bank> hepsi = VPOSClient.AllBankList(s => true);
            List<Bank> garanti = VPOSClient.AllBankList(s => s.BankCode == "0062");

            Assert.IsTrue(hepsi.Count > 1);
            Assert.AreEqual(1, garanti.Count);
            Assert.AreEqual("Garanti BBVA", garanti[0].BankName);
        }

        [TestMethod]
        public void AllBanks_BankCodesAreUnique()
        {
            List<string> tekrarlayan = BankService.AllBanks
                .GroupBy(s => s.BankCode)
                .Where(s => s.Count() > 1)
                .Select(s => s.Key)
                .ToList();

            Assert.AreEqual(0, tekrarlayan.Count, "tekrarlayan banka kodu: " + string.Join(", ", tekrarlayan));
        }

        [TestMethod]
        public void AllBanks_RegisteredServicesImplementIVirtualPOSService()
        {
            List<string> hatali = BankService.AllBanks
                .Where(s => s.BankService != null && !typeof(IVirtualPOSService).IsAssignableFrom(s.BankService))
                .Select(s => s.BankCode + " " + s.BankName)
                .ToList();

            Assert.AreEqual(0, hatali.Count, "IVirtualPOSService uygulamayan servis: " + string.Join(", ", hatali));
        }

        [TestMethod]
        public void AllBanks_RegisteredServicesHaveParameterlessConstructor()
        {
            List<string> hatali = new List<string>();

            foreach (Bank bank in BankService.AllBanks.Where(s => s.BankService != null))
            {
                try
                {
                    Activator.CreateInstance(bank.BankService);
                }
                catch (Exception ex)
                {
                    hatali.Add(bank.BankCode + " " + bank.BankName + ": " + ex.Message);
                }
            }

            Assert.AreEqual(0, hatali.Count, string.Join(" | ", hatali));
        }

        #endregion
    }
}
