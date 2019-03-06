namespace Exchange.RestServices
{
    using System;

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
        protected RestException(string message, Uri requestUri, string requestMethod)
            : base(message)
        {
            this.RequestUri = requestUri;
            this.RequestMethod = requestMethod;
        }

        /// <summary>
        /// Request uri.
        /// </summary>
        public Uri RequestUri { get; }

        /// <summary>
        /// Request method.
        /// </summary>
        public string RequestMethod { get; }
    }
}
