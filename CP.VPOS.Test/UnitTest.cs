using System;
using CP.VPOS;
using CP.VPOS.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CP.VPOS.Test
{
    [TestClass]
    public class UnitTest
    {
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


        [TestMethod]
        public void GarantiBankasiSaleTest()
        {
            VirtualPOSAuth garantiTest = GetGarantiAuth();
            SaleRequest saleRequest = GetGarantiSaleRequest();

            SaleResponse resp = VPOSClient.Sale(saleRequest, garantiTest);

            Console.WriteLine($"Garanti statu: {resp.statu.ToString()}");
            Console.WriteLine($"Garanti message: {resp.message}");
            Console.WriteLine($"Garanti transactionId: {resp.transactionId}");

            Assert.IsTrue(resp.statu == CP.VPOS.Enums.SaleResponseStatu.Success);
        }

        // [TestMethod]
        // public void GarantiBankasiCancelTest()
        // {
        //     VirtualPOSAuth garantiTest = GetGarantiAuth();
        //     SaleResponse saleResponse = VPOSClient.Sale(GetGarantiSaleRequest(), garantiTest);

        //     Assert.IsTrue(saleResponse.statu == CP.VPOS.Enums.SaleResponseStatu.Success, $"Sale failed: {saleResponse.message}");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(saleResponse.transactionId), "transactionId boş geldi");

        //     CancelRequest cancelRequest = new CancelRequest
        //     {
        //         customerIPAddress = "1.1.1.1",
        //         orderNumber = saleResponse.orderNumber,
        //         amount = (decimal)100.50,
        //         transactionId = saleResponse.transactionId,
        //         currency = CP.VPOS.Enums.Currency.TRY
        //     };

        //     CancelResponse cancelResponse = VPOSClient.Cancel(cancelRequest, garantiTest);

        //     Console.WriteLine($"Garanti cancel statu: {cancelResponse.statu.ToString()}");
        //     Console.WriteLine($"Garanti cancel message: {cancelResponse.message}");

        //     Assert.IsTrue(cancelResponse.statu == CP.VPOS.Enums.ResponseStatu.Success);
        // }

        // [TestMethod]
        // public void GarantiBankasiRefundTest()
        // {
        //     VirtualPOSAuth garantiTest = GetGarantiAuth();
        //     SaleResponse saleResponse = VPOSClient.Sale(GetGarantiSaleRequest(), garantiTest);

        //     Assert.IsTrue(saleResponse.statu == CP.VPOS.Enums.SaleResponseStatu.Success, $"Sale failed: {saleResponse.message}");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(saleResponse.transactionId), "transactionId boş geldi");

        //     RefundRequest refundRequest = new RefundRequest
        //     {
        //         customerIPAddress = "1.1.1.1",
        //         orderNumber = saleResponse.orderNumber,
        //         transactionId = saleResponse.transactionId,
        //         refundAmount = (decimal)100.50,
        //         currency = CP.VPOS.Enums.Currency.TRY
        //     };

        //     RefundResponse refundResponse = VPOSClient.Refund(refundRequest, garantiTest);

        //     Console.WriteLine($"Garanti refund statu: {refundResponse.statu.ToString()}");
        //     Console.WriteLine($"Garanti refund message: {refundResponse.message}");
        //     Console.WriteLine($"Garanti refund amount: {refundResponse.refundAmount}");

        //     Assert.IsTrue(refundResponse.statu == CP.VPOS.Enums.ResponseStatu.Success);
        // }


        //[TestMethod]
        public void IsBankasiBankasiSaleTest()
        {
            VirtualPOSAuth _isbankasiTest = new VirtualPOSAuth
            {
                bankCode = CP.VPOS.Services.BankService.IsBankasi,
                merchantID = "700655000200",
                merchantUser = "ISBANKAPI",
                merchantPassword = "ISBANK07",
                merchantStorekey = "TRPS0200",
                testPlatform = true
            };

            SaleRequest saleRequest = new SaleRequest
            {
                invoiceInfo = customerInfo,
                shippingInfo = customerInfo,
                saleInfo = new SaleInfo
                {
                    cardNameSurname = "Test Kart",
                    cardNumber = "4508034508034509",
                    cardExpiryDateMonth = 12,
                    cardExpiryDateYear = 2026,
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

            SaleResponse resp = VPOSClient.Sale(saleRequest, _isbankasiTest);

            Console.WriteLine($"Is Bankasi statu: {resp.statu.ToString()}");
            Console.WriteLine($"Is Bankasi message: {resp.message}");
            Console.WriteLine($"Is Bankasi transactionId: {resp.transactionId}");

            Assert.IsTrue(resp.statu == CP.VPOS.Enums.SaleResponseStatu.Success);
        }

        [TestMethod]
        public void QNBPaySaleTest()
        {
            VirtualPOSAuth _qnbPayTest = new VirtualPOSAuth
            {
                bankCode = CP.VPOS.Services.BankService.QNBpay,
                merchantID = "20158",
                merchantUser = "07fb70f9d8de575f32baa6518e38c5d6",
                merchantPassword = "61d97b2cac247069495be4b16f8604db",
                merchantStorekey = "$2y$10$N9IJkgazXMUwCzpn7NJrZePy3v.dIFOQUyW4yGfT3eWry6m.KxanK",
                testPlatform = true
            };

            SaleRequest saleRequest = new SaleRequest
            {
                invoiceInfo = customerInfo,
                shippingInfo = customerInfo,
                saleInfo = new SaleInfo
                {
                    cardNameSurname = "Test Kart",
                    cardNumber = "4022780520669303",
                    cardExpiryDateMonth = 1,
                    cardExpiryDateYear = 2050,
                    amount = (decimal)100.50,
                    cardCVV = "988",
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

            SaleResponse resp = VPOSClient.Sale(saleRequest, _qnbPayTest);

            Console.WriteLine($"QNBPay statu: {resp.statu.ToString()}");
            Console.WriteLine($"QNBPay message: {resp.message}");
            Console.WriteLine($"QNBPay transactionId: {resp.transactionId}");

            Assert.IsTrue(resp.statu == CP.VPOS.Enums.SaleResponseStatu.Success);
        }

        private VirtualPOSAuth GetGarantiAuth()
        {
            return new VirtualPOSAuth
            {
                bankCode = CP.VPOS.Services.BankService.GarantiBBVA,
                merchantID = "7000679",
                merchantUser = "30691297",
                merchantPassword = "123qweASD/",
                merchantStorekey = "12345678",
                testPlatform = true
            };
        }

        private SaleRequest GetGarantiSaleRequest()
        {
            return new SaleRequest
            {
                invoiceInfo = customerInfo,
                shippingInfo = customerInfo,
                saleInfo = new SaleInfo
                {
                    cardNameSurname = "Test Kart",
                    cardNumber = "5289394722895016",
                    cardExpiryDateMonth = 1,
                    cardExpiryDateYear = 2025,
                    amount = (decimal)100.50,
                    cardCVV = "030",
                    currency = CP.VPOS.Enums.Currency.TRY,
                    installment = 1,
                },
                payment3D = new Payment3D
                {
                    confirm = false
                },
                customerIPAddress = "1.1.1.1",
                orderNumber = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()
            };
        }
    }
}
