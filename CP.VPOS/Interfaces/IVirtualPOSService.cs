using CP.VPOS.Models;

namespace CP.VPOS.Interfaces
{
    internal interface IVirtualPOSService
    {
        /// <summary>
        /// Kredi kartı çekim işlemi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        SaleResponse Sale(SaleRequest request, VirtualPOSAuth auth);

        /// <summary>
        ///  3D Çekimlerin sonuçları için kullanılır
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        SaleResponse Sale3DResponse(Sale3DResponseRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// BIN ile Taksit listesi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        BINInstallmentQueryResponse BINInstallmentQuery(BINInstallmentQueryRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// Tutar ile Taksit listesi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        AllInstallmentQueryResponse AllInstallmentQuery(AllInstallmentQueryRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// Ek taksit kampanyası sorgulama
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        AdditionalInstallmentQueryResponse AdditionalInstallmentQuery(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// Ödeme iptal etme
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        CancelResponse Cancel(CancelRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// Ödeme iade etme
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        RefundResponse Refund(RefundRequest request, VirtualPOSAuth auth);

        /// <summary>
        /// İşlem sorgulama
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        SaleQueryResponse SaleQuery(SaleQueryRequest request, VirtualPOSAuth auth);
    }
}
