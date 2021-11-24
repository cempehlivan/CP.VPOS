using System;
using CP.VPOS;
using CP.VPOS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CP.VPOS.Test
{
    [TestClass]
    public class SaleTest
    {
        [TestMethod]
        public void NestpayAkbankSaleTest()
        {
            VirtualPOSAuth nestpayAkbank = new VirtualPOSAuth
            {
                bankCode = CP.VPOS.Services.BankService.Akbank,
                merchantID = "100100000",
                merchantUser = "AKTESTAPI",
                merchantPassword = "AKBANK01",
                merchantStorekey = "123456",
                testPlatform = true
            };

            CustomerInfo customerInfo = new CustomerInfo
            {
                taxNumber = "1111111111",
                emailAddress = "test@test.com",
                name = "cem",
                surname = "pehlivan",
                phoneNumber = "1111111111",
                addressDesc = "adres",
                cityName = "istanbul",
                country = CP.VPOS.Enums.Country.TUR,
                postCode = "34000",
                taxOffice = "maltepe",
                townName = "maltepe"
            };

            SaleRequest saleRequest = new SaleRequest
            {
                invoiceInfo = customerInfo,
                shippingInfo = customerInfo,
                saleInfo = new SaleInfo
                {
                    cardNameSurname = "cem test",
                    cardNumber = "4355084355084358",
                    cardExpiryDateMonth = 12,
                    cardExpiryDateYear = 2030,
                    amount = (decimal)100.50,
                    cardCVV = "000",
                    currency = CP.VPOS.Enums.Currency.TRY,
                    installment = 1,
                },
                payment3D = new Payment3D
                {
                    confirm = false
                },
                customerIPAddress = "1.1.1.1",
                orderNumber = Convert.ToInt32((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString("X")
            };


            var resp = VPOSClient.Sale(saleRequest, nestpayAkbank);

            Console.WriteLine($"statu: {resp.statu.ToString()}");
            Console.WriteLine($"message: {resp.message}");
            Console.WriteLine($"transactionId: {resp.transactionId}");

            Assert.IsTrue(resp.statu == CP.VPOS.Enums.SaleResponseStatu.Success);
        }
    }
}