namespace Exchange.RestServices
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Service.HttpCore.Extensions;

    /// <summary>
    /// Http web request client.
    /// </summary>
    internal class HttpWebClient : IHttpWebClient
    {
        /// <summary>
        /// Underlying client.
        /// </summary>
        private static HttpClient httpClient;

        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static readonly HttpWebClient httpWebClient = new HttpWebClient();

        /// <summary>
        /// Create new instance of <see cref="HttpWebClient"/>
        /// </summary>
        private HttpWebClient()
        {
            HttpWebClient.httpClient = this.CreateHttpClient(null);
        }

        /// <summary>
        /// Singleton.
        /// </summary>
        public static IHttpWebClient HttpClient
        {
            get { return HttpWebClient.httpWebClient; }
        }

        /// <summary>
        /// Sends call async.
        /// </summary>
        /// <param name="reqeustMessage">Request message.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            return HttpWebClient.httpClient.SendAsync(requestMessage);
        }

        /// <inheritdoc cref="IHttpWebClient.SetProxyServer"/>
        public void SetProxyServer(IWebProxy proxyServer)
        {
            if (null != HttpWebClient.httpClient)
            {
                HttpWebClient.httpClient.Dispose();
            }

            if (null != proxyServer)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxyServer
                };

                HttpWebClient.httpClient = this.CreateHttpClient(httpClientHandler);
            }
            else
            {
                HttpWebClient.httpClient = this.CreateHttpClient(null);
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
                new ThrottlingHttpHandler(),
                new AuthZHttpHandler(), 
                new TraceListenerHttpHandler(),
                new ExternalHttpHandler(), 
            };

            HttpClient httpClient = new HttpClient(
                this.CreateHttpPipeline(delegatingHandlers, httpClientHandler));

            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };

            httpClient.Timeout = TimeSpan.FromSeconds(30);
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