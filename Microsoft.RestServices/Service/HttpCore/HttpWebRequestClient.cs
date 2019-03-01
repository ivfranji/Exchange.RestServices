namespace Microsoft.RestServices.Exchange
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Http web request client.
    /// </summary>
    internal class HttpWebRequestClient : IHttpWebRequestClient
    {
        /// <summary>
        /// Underlying client.
        /// </summary>
        private static HttpClient httpClient;

        /// <summary>
        /// Create new instance of <see cref="HttpWebRequestClient"/>
        /// </summary>
        internal HttpWebRequestClient()
        {
            HttpWebRequestClient.httpClient = new HttpClient();
        }

        /// <summary>
        /// Sends call async.
        /// </summary>
        /// <param name="reqeustMessage">Request message.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            return HttpWebRequestClient.httpClient.SendAsync(requestMessage);
        }

        /// <inheritdoc cref="IHttpWebRequestClient.SetProxyServer"/>
        public void SetProxyServer(IWebProxy proxyServer)
        {
            if (null != HttpWebRequestClient.httpClient)
            {
                HttpWebRequestClient.httpClient.Dispose();
            }

            if (null != proxyServer)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxyServer
                };

                HttpWebRequestClient.httpClient = new HttpClient(
                    httpClientHandler, 
                    true);
            }
            else
            {
                HttpWebRequestClient.httpClient = new HttpClient();
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}