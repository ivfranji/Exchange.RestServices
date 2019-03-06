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
    internal class RetryDelegatingHandler : DelegatingHandler
    {
        #region Headers

        /// <summary>
        /// Retry after header.
        /// </summary>
        private const string RetryAfter = "Retry-After";

        /// <summary>
        /// Retry attempt.
        /// </summary>
        private const string RetryAttempt = "X-RetryAttempt";

        /// <summary>
        /// Total delay applied.
        /// </summary>
        private const string TotalDelayApplied = "X-TotalDelayApplied";

        #endregion

        /// <summary>
        /// Create instance of <see cref="RetryDelegatingHandler"/>
        /// </summary>
        /// <param name="retryOptions">Retry options.</param>
        public RetryDelegatingHandler(RetryOptions retryOptions = null)
        {
            this.RetryOptions = retryOptions ?? RetryOptions.DefaultRetryOptions;
        }

        /// <summary>
        /// Retry options.
        /// </summary>
        public RetryOptions RetryOptions { get; }

        /// <summary>
        /// Send request.
        /// </summary>
        /// <param name="httpRequest">Http request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            int retryCount = 0;
            int totalDelayApplied = 0;
            do
            {
                HttpResponseMessage responseMessage = await base.SendAsync(httpRequest, cancellationToken);
                if (!this.RequestThrottled(responseMessage))
                {
                    return responseMessage;
                }
                else
                {
                    if (retryCount >= this.RetryOptions.RetryCount)
                    {
                        throw new CallThrottledException(
                            retryCount,
                            totalDelayApplied,
                            httpRequest.RequestUri, 
                            httpRequest.Method.Method);
                    }

                    this.UpdateRetryCount(
                        responseMessage, 
                        ref retryCount);

                    totalDelayApplied += await this.ApplyDelay(
                        responseMessage, 
                        cancellationToken);

                    this.SetHttpHeader(
                        responseMessage, 
                        RetryDelegatingHandler.TotalDelayApplied, 
                        totalDelayApplied.ToString());
                }
                
            } while (true);
        }

        /// <summary>
        /// Apply delay to throttled call.
        /// </summary>
        private async Task<int> ApplyDelay(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            int delay = this.RetryOptions.DelaySeconds;
            if (responseMessage.Headers.TryGetValues(RetryDelegatingHandler.RetryAfter, out IEnumerable<string> retryAfterValue))
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
        /// Update retry count.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        /// <param name="retryCount">Retry count.</param>
        private void UpdateRetryCount(HttpResponseMessage responseMessage, ref int retryCount)
        {
            this.SetHttpHeader(
                responseMessage, 
                RetryDelegatingHandler.RetryAttempt,
                retryCount.ToString());

            retryCount++;
        }

        /// <summary>
        /// Sets http header to particular value.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        /// <param name="httpHeader">Http header name.</param>
        /// <param name="value">Http header value.</param>
        private void SetHttpHeader(HttpResponseMessage responseMessage, string httpHeader, string value)
        {
            if (responseMessage.Headers.Contains(httpHeader))
            {
                responseMessage.Headers.Remove(httpHeader);
            }

            responseMessage.Headers.Add(
                httpHeader, 
                value);
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

    /// <summary>
    /// Retry handler options.
    /// </summary>
    internal class RetryOptions
    {
        /// <summary>
        /// Default retry options.
        /// </summary>
        private static RetryOptions defaultRetryOptions = new RetryOptions(
            3, 
            RetryOptions.DefaultDelaySeconds);

        /// <summary>
        /// Default delay.
        /// </summary>
        private const int DefaultDelaySeconds = 10;

        /// <summary>
        /// Create new instance of <see cref="RetryOptions"/>
        /// </summary>
        /// <param name="retryCount"></param>
        public RetryOptions(int retryCount, int delaySeconds)
        {
            this.RetryCount = retryCount;
            this.DelaySeconds = delaySeconds;
        }

        /// <summary>
        /// Retry count
        /// </summary>
        public int RetryCount { get; }

        /// <summary>
        /// Default delay;
        /// </summary>
        public int DelaySeconds { get; }

        /// <summary>
        /// Default retry options.
        /// </summary>
        internal static RetryOptions DefaultRetryOptions
        {
            get { return RetryOptions.defaultRetryOptions; }
        }
    }
}
