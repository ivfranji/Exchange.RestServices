namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Net;
    using System.Threading;
    
    /// <summary>
    /// Http client provider.
    /// </summary>
    internal class HttpWebRequestClientProvider
    {
        /// <summary>
        /// Default web request client.
        /// </summary>
        private static IHttpWebRequestClient defaultWebRequestClient;

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
            HttpWebRequestClientProvider.defaultWebRequestClient = new HttpWebRequestClient();
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
        /// Web proxy changed. It will register new proxy with client.
        /// </summary>
        /// <param name="webProxy"></param>
        internal void ProxyChanged(IWebProxy webProxy)
        {
            HttpWebRequestClientProvider.defaultWebRequestClient.SetProxyServer(webProxy);
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
            return HttpWebRequestClientProvider.defaultWebRequestClient;
        }

        /// <summary>
        /// TEST ONLY!!!
        /// </summary>
        internal void EnterLock()
        {
            Monitor.Enter(HttpWebRequestClientProvider.lockObject);
        }

        /// <summary>
        /// TEST ONLY!!!
        /// </summary>
        internal void ExitLock()
        {
            Monitor.Exit(HttpWebRequestClientProvider.lockObject);
        }
    }
}