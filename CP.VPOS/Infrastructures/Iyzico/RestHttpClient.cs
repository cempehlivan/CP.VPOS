using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CP.VPOS.Infrastructures.Iyzico
{
    internal class RestHttpClient
    {
        private static readonly HttpClient HttpClient;
        static RestHttpClient()
        {
            ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            System.Net.ServicePointManager.Expect100Continue = false;

            HttpClient = new HttpClient();
        }

        public static RestHttpClient Create()
        {
            return new RestHttpClient();
        }

        public async Task<T> GetAsync<T>(String url, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public Task<T> GetAsync<T>(String url, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            return SendAsync<T>(requestMessage, headers, cancellationToken);
        }

        public Task<T> PostAsync<T>(String url, Dictionary<string, string> headers, BaseRequest request, CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = JsonBuilder.ToJsonString(request)
            };

            return SendAsync<T>(requestMessage, headers, cancellationToken);
        }

        public Task<T> DeleteAsync<T>(String url, Dictionary<string, string> headers, BaseRequest request, CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = JsonBuilder.ToJsonString(request)
            };

            return SendAsync<T>(requestMessage, headers, cancellationToken);
        }

        public Task<T> PutAsync<T>(String url, Dictionary<string, string> headers, BaseRequest request, CancellationToken cancellationToken)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(url),
                Content = JsonBuilder.ToJsonString(request)
            };

            return SendAsync<T>(requestMessage, headers, cancellationToken);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage requestMessage, Dictionary<string, string> headers, CancellationToken cancellationToken)
        {
            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            using (HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false))
            {
                return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
        }
    }
}
