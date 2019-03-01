namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Net.Http.Headers;
    
    /// <summary>
    /// Http web request contract.
    /// </summary>
    internal interface IHttpWebRequest : IDisposable
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
        /// Authorization to be used in the request.
        /// </summary>
        AuthenticationHeaderValue Authorization { get; set; }

        /// <summary>
        /// User agent to be used for request.
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// Request headers.
        /// </summary>
        HttpRequestHeaders Headers { get; }

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
    }
}
