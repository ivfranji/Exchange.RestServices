namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines contract for IHttpClient.
    /// </summary>
    internal interface IHttpWebRequestClient : IDisposable
    {
        /// <summary>
        /// Sends message and retrieves response.
        /// </summary>
        /// <param name="reqeustMessage"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxyServer"></param>
        void SetProxyServer(IWebProxy proxyServer);
    }

    /// <summary>
    /// Http web request client.
    /// </summary>
    internal class HttpWebRequesClient : IHttpWebRequestClient
    {
        private IWebProxy proxyServer;

        /// <summary>
        /// Underlying client.
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// Create new instance of <see cref="HttpWebRequesClient"/>
        /// </summary>
        internal HttpWebRequesClient()
        {
        }

        /// <summary>
        /// Sends call async.
        /// </summary>
        /// <param name="reqeustMessage">Request message.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            if (this.proxyServer != null)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    Proxy = this.proxyServer
                };

                this.httpClient = new HttpClient(
                    httpClientHandler, 
                    true);
            }
            else
            {
                this.httpClient = new HttpClient();
            }

            return this.httpClient.SendAsync(requestMessage);
        }

        /// <inheritdoc cref="IHttpWebRequestClient.SetProxyServer"/>
        public void SetProxyServer(IWebProxy proxyServer)
        {
            this.proxyServer = proxyServer;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }

    /// <summary>
    /// Http client provider.
    /// </summary>
    internal class HttpWebRequestClientProvider
    {
        /// <summary>
        /// Lock object.
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static HttpWebRequestClientProvider instance = new HttpWebRequestClientProvider();

        /// <summary>
        /// Create new instance with default provider.
        /// </summary>
        private HttpWebRequestClientProvider()
        {
            this.GetClient = this.DefaultClientProvider;
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        internal static HttpWebRequestClientProvider Instance
        {
            get { return HttpWebRequestClientProvider.instance; }
        }

        /// <summary>
        /// Client factory.
        /// </summary>
        public Func<IHttpWebRequestClient> GetClient
        {
            get;
            set;
        }

        /// <summary>
        /// Register http web request client provider.
        /// </summary>
        /// <param name="provider">Http web request client.</param>
        internal void RegisterHttpClientProvider(Func<IHttpWebRequestClient> provider)
        {
                this.GetClient = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Reset provider to default one.
        /// </summary>
        internal void Reset()
        {
            lock (HttpWebRequestClientProvider.lockObject)
            {
                this.GetClient = this.DefaultClientProvider;
            }
        }

        /// <summary>
        /// Default client provider.
        /// </summary>
        /// <returns></returns>
        private IHttpWebRequestClient DefaultClientProvider()
        {
            return new HttpWebRequesClient();
        }

        internal void EnterLock()
        {
            Monitor.Enter(HttpWebRequestClientProvider.lockObject);
        }

        internal void ExitLock()
        {
            Monitor.Exit(HttpWebRequestClientProvider.lockObject);
        }
    }
}