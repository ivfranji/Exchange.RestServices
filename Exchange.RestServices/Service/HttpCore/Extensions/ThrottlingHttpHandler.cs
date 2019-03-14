namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Retry delegating handler.
    /// </summary>
    internal class ThrottlingHttpHandler : RetryHttpHandler
    {
        #region Headers

        /// <summary>
        /// Retry after header.
        /// </summary>
        private const string RetryAfterHttpHeaderName = "Retry-After";

        #endregion

        /// <summary>
        /// Create instance of <see cref="ThrottlingHttpHandler"/>
        /// </summary>
        /// <param name="retryOptions">Retry options.</param>
        public ThrottlingHttpHandler(RetryOptions retryOptions = null)
            : base(retryOptions)
        {
        }

        /// <inheritdoc cref="RetryHttpHandler.RetryExceeded"/>
        protected override void RetryExceeded(int retryCount, int totalDelayApplied, Uri requestUri, string httpMethod, HttpStatusCode lastHttpStatusCode)
        {
            throw new CallThrottledException(
                retryCount,
                totalDelayApplied,
                requestUri,
                httpMethod,
                lastHttpStatusCode);
        }

        /// <summary>
        /// Indicate if request should retry.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        protected override bool ShouldRetry(HttpResponseMessage responseMessage)
        {
            return this.RequestThrottled(responseMessage);
        }

        /// <summary>
        /// Apply delay to throttled call.
        /// </summary>
        protected override async Task<int> ApplyDelay(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            int delay = this.RetryOptions.DelaySeconds;
            if (responseMessage.Headers.TryGetValues(ThrottlingHttpHandler.RetryAfterHttpHeaderName, out IEnumerable<string> retryAfterValue))
            {
                string retryAfter = retryAfterValue.FirstOrDefault();
                if (int.TryParse(retryAfter, out int retryAfterDelay))
                {
                    delay = retryAfterDelay;
                }
            }

            await Task.Delay(
                TimeSpan.FromSeconds(delay),
                cancellationToken);

            return delay;
        }

        /// <summary>
        /// Indicate if request was throttled based on status code within HttpResponseMessage.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        /// <returns></returns>
        private bool RequestThrottled(HttpResponseMessage responseMessage)
        {
            return responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable ||
                   responseMessage.StatusCode == (HttpStatusCode) 429;
        }
    }
}
