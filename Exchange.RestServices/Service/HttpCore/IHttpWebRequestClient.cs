namespace Exchange.RestServices
{
    using System;
    using System.Net;
    using System.Net.Http;
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
}