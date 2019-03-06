namespace Exchange.RestServices
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
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
            HttpWebRequestClient.httpClient = this.CreateHttpClient(null);
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

                HttpWebRequestClient.httpClient = this.CreateHttpClient(httpClientHandler);
            }
            else
            {
                HttpWebRequestClient.httpClient = this.CreateHttpClient(null);
            }
        }

        /// <summary>
        /// Create http client.
        /// </summary>
        /// <param name="httpClientHandler"></param>
        /// <returns></returns>
        private HttpClient CreateHttpClient(HttpClientHandler httpClientHandler)
        {
            DelegatingHandler[] delegatingHandlers = new DelegatingHandler[]
            {
                new RetryDelegatingHandler(), 
            };

            HttpClient httpClient = new HttpClient(
                this.CreateHttpPipeline(delegatingHandlers, httpClientHandler));

            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };

            return httpClient;
        }

        /// <summary>
        /// Create http pipeline.
        /// </summary>
        /// <param name="delegatingHandlers">Delegating handlers.</param>
        /// <param name="innerHandler">Inner handler.</param>
        /// <returns></returns>
        private HttpMessageHandler CreateHttpPipeline(DelegatingHandler[] delegatingHandlers, HttpMessageHandler innerHandler = null)
        {
            if (innerHandler == null)
            {
                innerHandler = new HttpClientHandler();
            }

            if (delegatingHandlers == null)
            {
                return innerHandler;
            }

            HttpMessageHandler httpPipeline = innerHandler;

            for (int i = delegatingHandlers.Length - 1; i >= 0; i--)
            {
                if (delegatingHandlers[i] == null)
                {
                    throw new ArgumentNullException(nameof(delegatingHandlers));
                }
                if (delegatingHandlers[i].InnerHandler != null)
                {
                    throw new InvalidOperationException("Delegating handler already has inner handler.");
                }

                delegatingHandlers[i].InnerHandler = httpPipeline;
                httpPipeline = delegatingHandlers[i];
            }

            return httpPipeline;
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