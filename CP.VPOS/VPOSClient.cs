using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CP.VPOS.Helpers;
using CP.VPOS.Interfaces;
using CP.VPOS.Models;
using CP.VPOS.Services;


namespace CP.VPOS
{
    public class VPOSClient
    {
        /// <summary>
        /// Karttan çekim yapmak için kullanılır. 3D çekim yapmak için "SaleRequest.payment3D.confirm = true" olarak gönderilmelidir.
        /// </summary>
        /// <param name="request">Kart çekim işlemine ait işlem bilgileri</param>
        /// <param name="auth">Banka API bilgileri</param>
        /// <returns></returns>
        public static SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            request.saleInfo.Validate();
            request.invoiceInfo.Validate();
            request.shippingInfo.Validate();
            auth.Validate();

            if (!FoundationHelper.IsCardNumberValid(request.saleInfo.cardNumber))
                throw new ValidationException("Geçersiz kart numarası. Lütfen kart numaranızı kontrol ediniz.");

            #region Adres Max Length
            int city_town_length = 25;

            request.invoiceInfo.cityName = request.invoiceInfo.cityName.getMaxLength(city_town_length);
            request.invoiceInfo.townName = request.invoiceInfo.townName.getMaxLength(city_town_length);
            request.invoiceInfo.addressDesc = request.invoiceInfo.addressDesc.getMaxLength(200);
            request.invoiceInfo.postCode = request.invoiceInfo.postCode.getMaxLength(10);
            request.invoiceInfo.emailAddress = request.invoiceInfo.emailAddress.getMaxLength(100);
            request.invoiceInfo.phoneNumber = request.invoiceInfo.phoneNumber.getMaxLength(20);
            request.invoiceInfo.name = request.invoiceInfo.name.getMaxLength(50);
            request.invoiceInfo.surname = request.invoiceInfo.surname.getMaxLength(50);
            request.invoiceInfo.taxNumber = request.invoiceInfo.taxNumber.getMaxLength(20);
            request.invoiceInfo.taxOffice = request.invoiceInfo.taxOffice.getMaxLength(50);

            request.shippingInfo.cityName = request.shippingInfo.cityName.getMaxLength(city_town_length);
            request.shippingInfo.townName = request.shippingInfo.townName.getMaxLength(city_town_length);
            request.shippingInfo.addressDesc = request.shippingInfo.addressDesc.getMaxLength(200);
            request.shippingInfo.postCode = request.shippingInfo.postCode.getMaxLength(10);
            request.shippingInfo.emailAddress = request.shippingInfo.emailAddress.getMaxLength(100);
            request.shippingInfo.phoneNumber = request.shippingInfo.phoneNumber.getMaxLength(20);
            request.shippingInfo.name = request.shippingInfo.name.getMaxLength(50);
            request.shippingInfo.surname = request.shippingInfo.surname.getMaxLength(50);
            request.shippingInfo.taxNumber = request.shippingInfo.taxNumber.getMaxLength(20);
            request.shippingInfo.taxOffice = request.shippingInfo.taxOffice.getMaxLength(50);
            #endregion

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.Sale(request, auth);
        }

        /// <summary>
        /// 3D yapılan çekim işlemi sonucunu döner
        /// </summary>
        /// <param name="request"></param>
        public static SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            auth.Validate();

            if (auth.bankCode == "0067") // YapıKredi Bankası
            {
                if (request.currency == null)
                    throw new ValidationException("currency alanı Yapı Kredi bankası için zorunludur");
            }

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.Sale3DResponse(request, auth);
        }

        /// <summary>
        /// Karta yapılabilecek taksit sayısını döner
        /// </summary>
        /// <param name="request">Kart bilgisi</param>
        /// <param name="auth">Banka API bilgileri</param>
        /// <returns></returns>
        public static BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            auth.Validate();

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.BINInstallmentQuery(request, auth);
        }

        /// <summary>
        /// Tutar ile taksit sayısını döner
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth">Banka API bilgileri</param>
        /// <returns></returns>
        public static AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            auth.Validate();

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.AllInstallmentQuery(request, auth);
        }

        /// <summary>
        /// Satış yapılabilecek ek taksit kampanyalarını döner
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth">Banka API bilgileri</param>
        /// <returns></returns>
        public static AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            request.saleInfo.Validate();
            auth.Validate();

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.AdditionalInstallmentQuery(request, auth);
        }

        /// <summary>
        ///  Ödeme iptal etme. Aynı gün yapılan ödemeler için kullanılabilir. Çekilen tutarın tamamı iptal edilir ve müşteri ekstresine hiçbir işlem yansımaz.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth">Banka API bilgileri</param>
        /// <returns></returns>
        public static CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            auth.Validate();

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.Cancel(request, auth);
        }

        /// <summary>
        /// Ödeme iade etme. Belirtilen tutar kadar kısmi iade işlemi yapılır
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public static RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth)
        {
            request.Validate();
            auth.Validate();

            IVirtualPOSService vPOSService = GetVirtualPOSService(auth.bankCode);

            return vPOSService.Refund(request, auth);
        }

        /// <summary>
        /// Tüm banka listesi
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<Bank> AllBankList(Func<Bank, bool> where)
        {
            var banks = BankService.AllBanks
                .Where(where)
                .Select(s => new Bank
                {
                    BankCode = s.BankCode,
                    BankName = s.BankName,
                    CollectiveVPOS = s.CollectiveVPOS,
                    CommissionAutoAdd = s.CommissionAutoAdd,
                    InstallmentAPI = s.InstallmentAPI,
                }).ToList();

            return banks;
        }

        /// <summary>
        /// Kredi Kartı BIN sorgulama
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreditCardBinQueryResponse CreditCardBinQuery(CreditCardBinQueryRequest request)
        {
            request.Validate();

            List<CreditCardBinQueryResponse> binList = BinService.GetBinList();

            CreditCardBinQueryResponse _binDetail = binList.FirstOrDefault(s => s.binNumber == request.binNumber);

            CreditCardBinQueryResponse binDetail = null;

            if (_binDetail != null)
            {
                binDetail = _binDetail.DeepClone();

                if ((int)binDetail.cardProgram >= 0)
                    binDetail.banksWithInstallments = binList.Where(s => s.cardProgram == binDetail.cardProgram).GroupBy(s => s.bankCode).OrderByDescending(s=> s.Count()).Select(s => s.Key).ToList();

                binDetail.banksWithInstallments = binDetail.banksWithInstallments ?? new List<string>();

                if (!binDetail.banksWithInstallments.Any(s => s == binDetail.bankCode))
                    binDetail.banksWithInstallments.Add(binDetail.bankCode);


                if(binDetail.banksWithInstallments.IndexOf(binDetail.bankCode) != 0)
                {
                    binDetail.banksWithInstallments.Remove(binDetail.bankCode);
                    binDetail.banksWithInstallments.Insert(0, binDetail.bankCode);
                }
            }

            return binDetail;
        }

        private static IVirtualPOSService GetVirtualPOSService(string bankCode)
        {
            IVirtualPOSService virtualPOSService = null;

            var searchBank = Services.BankService.AllBanks.FirstOrDefault(s => s.BankCode == bankCode);

            if (searchBank?.BankService != null)
                virtualPOSService = (IVirtualPOSService)Activator.CreateInstance(searchBank.BankService);
            else
                throw new Exception("Banka entegrasyonu bulunamadı.");

            return virtualPOSService;
        }
    }
}
