namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Web entityResponse contract.
    /// </summary>
    internal interface IHttpWebResponse
    {
        /// <summary>
        /// Throw exception if entityResponse invalid or error.
        /// </summary>
        void ThrowIfNeeded(Action<string, HttpStatusCode> errorHandlerAction = null);

        /// <summary>
        /// Status code.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Response content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Response headers.
        /// </summary>
        IDictionary<string, string> HttpResponseHeaders { get; }

        /// <summary>
        /// Indicate success status code.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Indicate error in case Success != true.
        /// </summary>
        string Error { get; }
    }
}
