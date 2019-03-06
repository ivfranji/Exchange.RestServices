namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// Throws when retry-count exceeded.
    /// </summary>
    public class RetryCountException : RestException
    {
        /// <summary>
        /// Create new instance of <see cref="RetryCountException"/>
        /// </summary>
        /// <param name="retryCount">Retry count.</param>
        /// <param name="totalDelayApplied">Total delay applied.</param>
        /// <param name="requestUri"></param>
        /// <param name="requestMethod"></param>
        public RetryCountException(int retryCount, int totalDelayApplied, Uri requestUri, string requestMethod) 
            : base("Retry handler exceeded retry count.", requestUri, requestMethod)
        {
            this.RetryCount = retryCount;
            this.TotalDelayApplied = totalDelayApplied;
        }

        /// <summary>
        /// Retry count before error thrown.
        /// </summary>
        public int RetryCount { get; }

        /// <summary>
        /// Total delay applied.
        /// </summary>
        public int TotalDelayApplied { get; }
    }
}
