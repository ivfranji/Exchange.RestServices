namespace Exchange.RestServices
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Retry http handler.
    /// </summary>
    internal abstract class RetryHttpHandler : DelegatingHandler
    {
        /// <summary>
        /// Retry attempt.
        /// </summary>
        private const string RetryAttempt = "X-RetryAttempt";

        /// <summary>
        /// Total delay applied.
        /// </summary>
        private const string TotalDelayApplied = "X-TotalDelayApplied";

        /// <summary>
        /// Create new instance of <see cref="RetryHttpHandler"/>
        /// </summary>
        /// <param name="retryOptions">Retry options.</param>
        protected RetryHttpHandler(RetryOptions retryOptions)
        {
            this.RetryOptions = retryOptions ?? RetryOptions.DefaultRetryOptions;

            string moduleName = this.GetType().Name;
            this.TotalDelayAppliedHttpHeaderName = $"{RetryHttpHandler.TotalDelayApplied}-{moduleName}";
            this.RetryAttemptHttpHeaderName = $"{RetryHttpHandler.RetryAttempt}-{moduleName}";
        }

        /// <summary>
        /// Total delay http header name.
        /// </summary>
        internal string TotalDelayAppliedHttpHeaderName { get; }

        /// <summary>
        /// Total delay http header name.
        /// </summary>
        internal string RetryAttemptHttpHeaderName { get; }

        /// <summary>
        /// Retry options.
        /// </summary>
        internal RetryOptions RetryOptions { get; }

        protected sealed async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            int retryCount = 0;
            int totalDelayApplied = 0;
            do
            {
                await this.PreProcessHttpRequest(httpRequest);
                HttpResponseMessage responseMessage = await base.SendAsync(httpRequest, cancellationToken);
                if (!this.ShouldRetry(responseMessage))
                {
                    return responseMessage;
                }
                else
                {
                    if (retryCount >= this.RetryOptions.RetryCount)
                    {
                        this.RetryExceeded(
                            retryCount,
                            totalDelayApplied,
                            httpRequest.RequestUri,
                            httpRequest.Method.Method);

                        // In case RetryFailed don't throw.
                        throw new RetryCountException(
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
                        this.TotalDelayAppliedHttpHeaderName,
                        totalDelayApplied.ToString());
                }

            } while (true);
        }

        /// <summary>
        /// Give chance to child classes to prepare request
        /// before sending.
        /// </summary>
        protected virtual async Task PreProcessHttpRequest(HttpRequestMessage httpRequestMessage)
        {
            await Task.Run((() => { }));
        }

        /// <summary>
        /// Handle retry exceeded attempt.
        /// </summary>
        /// <param name="retryCount">Retry count.</param>
        /// <param name="totalDelayApplied">Total delay applied.</param>
        /// <param name="requestUri">Request uri.</param>
        /// <param name="httpMethod">Http method.</param>
        protected abstract void RetryExceeded(int retryCount, int totalDelayApplied, Uri requestUri, string httpMethod);

        /// <summary>
        /// Indicate if it should retry based on response message.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        /// <returns></returns>
        protected abstract bool ShouldRetry(HttpResponseMessage responseMessage);
        
        /// <summary>
        /// Apply delay to throttled call.
        /// </summary>
        protected abstract Task<int> ApplyDelay(HttpResponseMessage responseMessage, CancellationToken cancellationToken);

        /// <summary>
        /// Update retry count.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        /// <param name="retryCount">Retry count.</param>
        private void UpdateRetryCount(HttpResponseMessage responseMessage, ref int retryCount)
        {
            this.SetHttpHeader(
                responseMessage,
                this.RetryAttemptHttpHeaderName,
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
    }
}
