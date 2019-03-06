namespace Exchange.RestServices
{
    using System;
    using System.Net;

    /// <summary>
    /// Exception thrown by retry handler in case call has been throttled more
    /// time than retry option configured.
    /// </summary>
    public class CallThrottledException : RestException
    {
        /// <summary>
        /// Creates new instance of <see cref="CallThrottledException"/>
        /// </summary>
        /// <param name="retryCount"></param>
        /// <param name="totalDelayApplied"></param>
        /// <param name="requestUri"></param>
        /// <param name="requestMethod"></param>
        public CallThrottledException(int retryCount, int totalDelayApplied, Uri requestUri, string requestMethod) 
            : base("Call throttled.", requestUri, requestMethod)
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

        /// <summary>
        /// Last status code.
        /// </summary>
        public HttpStatusCode LastStatusCode { get; }
    }
}
