using System.Threading;
using System.Threading.Tasks;
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleResponse> SaleAsync(SaleRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        ///  3D Çekimlerin sonuçları için kullanılır
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleResponse> Sale3DResponseAsync(Sale3DResponseRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// BIN ile Taksit listesi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BINInstallmentQueryResponse> BINInstallmentQueryAsync(BINInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// Tutar ile Taksit listesi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AllInstallmentQueryResponse> AllInstallmentQueryAsync(AllInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// Ek taksit kampanyası sorgulama
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AdditionalInstallmentQueryResponse> AdditionalInstallmentQueryAsync(AdditionalInstallmentQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// Ödeme iptal etme
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CancelResponse> CancelAsync(CancelRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// Ödeme iade etme
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RefundResponse> RefundAsync(RefundRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);

        /// <summary>
        /// İşlem sorgulama
        /// </summary>
        /// <param name="request"></param>
        /// <param name="auth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SaleQueryResponse> SaleQueryAsync(SaleQueryRequest request, VirtualPOSAuth auth, CancellationToken cancellationToken);
    }
}
