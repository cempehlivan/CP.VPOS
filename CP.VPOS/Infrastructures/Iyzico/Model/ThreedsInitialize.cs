using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CP.VPOS.Infrastructures.Iyzico.Request;

namespace CP.VPOS.Infrastructures.Iyzico.Model
{
    internal class ThreedsInitialize : IyzipayResource
    {
        [JsonProperty(PropertyName = "threeDSHtmlContent")]
        public String HtmlContent { get; set; }

        public static async Task<ThreedsInitialize> CreateAsync(CreatePaymentRequest request, Options options, CancellationToken cancellationToken)
        {
            ThreedsInitialize response = await RestHttpClient.Create().PostAsync<ThreedsInitialize>(options.BaseUrl + "/payment/3dsecure/initialize", GetHttpHeaders(request, options), request, cancellationToken).ConfigureAwait(false);

            if (response != null)
            {
                response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
            }
            return response;
        }
    }
}
