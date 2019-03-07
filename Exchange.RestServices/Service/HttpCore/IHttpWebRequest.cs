namespace Exchange.RestServices
{
    using System;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Http web request contract.
    /// </summary>
    internal interface IHttpWebRequest : IDisposable, IPreferenceHeaderSetter
    {
        /// <summary>
        /// Rest url.
        /// </summary>
        HttpRestUrl RestUrl { get; }

        /// <summary>
        /// Request url.
        /// </summary>
        Uri RequestUrl { get; }
        
        /// <summary>
        /// User agent to be used for request.
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Request headers.
        /// </summary>
        HttpRequestHeaders Headers { get; }

        /// <summary>
        /// Get response async.
        /// </summary>
        /// <returns></returns>
        Task<IHttpWebResponse> GetResponseAsync();

        /// <summary>
        /// Invokes request and retrieve response.
        /// </summary>
        /// <returns></returns>
        IHttpWebResponse GetResponse();

        /// <summary>
        /// Set particular header.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <param name="headerValue">Header value.</param>
        void SetRequestHeader(string headerName, string headerValue);

        /// <summary>
        /// Set request context.
        /// </summary>
        /// <param name="exchangeService"></param>
        void SetRequestContext(ExchangeService exchangeService);
    }
}
