namespace Exchange.RestServices
{
    using System;
    using System.Net;

    /// <summary>
    /// Base for all REST exceptions.
    /// </summary>
    public abstract class RestException : Exception
    {
        /// <summary>
        /// Create new instance of <see cref="RestException"/>
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="requestUri">Request uri.</param>
        /// <param name="requestMethod">Request method.</param>
        protected RestException(string message, Uri requestUri, string requestMethod, HttpStatusCode lastHttpStatusCode)
            : base(message)
        {
            this.RequestUri = requestUri;
            this.RequestMethod = requestMethod;
            this.LastHttpStatusCode = lastHttpStatusCode;
        }

        /// <summary>
        /// Request uri.
        /// </summary>
        public Uri RequestUri { get; }

        /// <summary>
        /// Request method.
        /// </summary>
        public string RequestMethod { get; }
        
        /// <summary>
        /// Last http status code.
        /// </summary>
        public HttpStatusCode LastHttpStatusCode
        {
            get; internal set;
        }
    }
}
