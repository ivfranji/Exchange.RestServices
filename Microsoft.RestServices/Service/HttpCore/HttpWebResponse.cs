namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Headers;

    /// <summary>
    /// Defines HTTP web response.
    /// </summary>
    internal class HttpWebResponse : IHttpWebResponse
    {
        /// <summary>
        /// Create new instance of <see cref="HttpWebResponse"/>.
        /// </summary>
        /// <param name="content">Response content.</param>
        /// <param name="success">Response was successful.</param>
        /// <param name="error">Error if response failed.</param>
        /// <param name="httpResponseHeaders">Http response headers.</param>
        /// <param name="statusCode">Http status code.</param>
        internal HttpWebResponse(string content, bool success, string error, HttpHeaders httpResponseHeaders, HttpStatusCode statusCode)
        {
            this.Content = content;
            this.Success = success;
            this.Error = error;

            this.HttpResponseHeaders = new Dictionary<string, string>();
            foreach (KeyValuePair<string, IEnumerable<string>> header in httpResponseHeaders)
            {
                this.HttpResponseHeaders.Add(header.Key, String.Join(", ", header.Value));
            }

            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Response successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Error in case this wasn't success.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Response content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Http response headers.
        /// </summary>
        public IDictionary<string, string> HttpResponseHeaders { get; }

        /// <summary>
        /// Throws if wasn't successful.
        /// </summary>
        public void ThrowIfNeeded(Action<string, HttpStatusCode> errorHandlerAction = null)
        {
            if (!this.Success)
            {
                errorHandlerAction?.Invoke(this.Error, this.StatusCode);
            }
        }

        /// <summary>
        /// Http status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
    }
}