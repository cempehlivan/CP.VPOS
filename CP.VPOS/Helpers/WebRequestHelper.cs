using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CP.VPOS.Helpers
{
    /// <summary>
    /// HttpWebRequest tabanlı banka uçları için asenkron POST işlemlerini yürütür.
    /// </summary>
    internal static class WebRequestHelper
    {
        /// <summary>
        /// Verilen adrese asenkron POST yapar ve yanıt gövdesini döner.
        /// İstek, timeout süresi dolduğunda veya cancellationToken iptal edildiğinde sonlandırılır.
        /// </summary>
        /// <param name="link">İstek adresi</param>
        /// <param name="body">Gönderilecek gövde</param>
        /// <param name="contentType">İçerik tipi</param>
        /// <param name="responseEncoding">Yanıtın okunacağı karakter kümesi</param>
        /// <param name="timeoutMilliseconds">Zaman aşımı süresi (ms)</param>
        /// <param name="cancellationToken">İşlemi iptal etmek için kullanılır</param>
        internal static async Task<string> PostAsync(string link, byte[] body, string contentType, Encoding responseEncoding, int timeoutMilliseconds, CancellationToken cancellationToken)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.Expect100Continue = false;

#pragma warning disable SYSLIB0014
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
#pragma warning restore SYSLIB0014

            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = body.Length;
            request.Timeout = timeoutMilliseconds;

            // HttpWebRequest.Timeout yalnızca senkron çağrılarda geçerli olduğu için
            // asenkron yolda zaman aşımı ayrı bir token ile uygulanır.
            using (CancellationTokenSource timeoutSource = new CancellationTokenSource(timeoutMilliseconds))
            using (CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutSource.Token))
            using (linkedSource.Token.Register(request.Abort))
            {
                try
                {
                    using (Stream requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
                    {
                        await requestStream.WriteAsync(body, 0, body.Length, linkedSource.Token).ConfigureAwait(false);
                    }

                    using (WebResponse response = await request.GetResponseAsync().ConfigureAwait(false))
                    using (Stream responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream, responseEncoding))
                    {
                        return await reader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
                catch (WebException) when (cancellationToken.IsCancellationRequested)
                {
                    // İsteği çağıran taraf iptal etti; WebException yerine beklenen tipi fırlat.
                    throw new OperationCanceledException(cancellationToken);
                }
            }
        }
    }
}
