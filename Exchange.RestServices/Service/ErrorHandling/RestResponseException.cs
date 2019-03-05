namespace Exchange.RestServices
{
    using System;
    using System.Net;

    /// <summary>
    /// Rest exception.
    /// </summary>
    public class RestResponseException : Exception
    {
        /// <summary>
        /// Create new instance of <see cref="RestResponseException"/>
        /// </summary>
        /// <param name="error">Error.</param>
        internal RestResponseException(Error error, HttpStatusCode httpStatusCode)
            : this(error.ToString(), httpStatusCode)
        {
            this.Error = error;
        }

        /// <summary>
        /// Create new instance of <see cref="RestResponseException"/>
        /// </summary>
        /// <param name="error">Error.</param>
        internal RestResponseException(string error, HttpStatusCode httpStatusCode)
            : base(error)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Error.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Http status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }
    }
}
