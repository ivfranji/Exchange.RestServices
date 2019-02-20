namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Rest exception.
    /// </summary>
    public class RestResponseException : Exception
    {
        /// <summary>
        /// Create new instance of <see cref="RestResponseException"/>
        /// </summary>
        /// <param name="error">Error.</param>
        internal RestResponseException(Error error)
            : this(error.Message)
        {
            this.Error = error;
        }

        /// <summary>
        /// Create new instance of <see cref="RestResponseException"/>
        /// </summary>
        /// <param name="error">Error.</param>
        internal RestResponseException(string error)
            : base(error)
        {
        }


        /// <summary>
        /// Error.
        /// </summary>
        public Error Error { get; }
    }
}
