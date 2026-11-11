using System.Threading;
using System.Threading.Tasks;
using CP.VPOS.Infrastructures.Iyzico.Request;
using System.Collections.Generic;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class InstallmentInfo : IyzipayResource
    {
        public List<InstallmentDetail> InstallmentDetails { get; set; }

        public static Task<InstallmentInfo> RetrieveAsync(RetrieveInstallmentInfoRequest request, Options options, CancellationToken cancellationToken)
        {
            return RestHttpClient.Create().PostAsync<InstallmentInfo>(options.BaseUrl + "/payment/iyzipos/installment", GetHttpHeaders(request, options), request, cancellationToken);
        }
    }
}
