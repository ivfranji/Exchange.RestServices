namespace Exchange.RestServices.Service.HttpCore.Extensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;

    /// <summary>
    /// Authorization http handler.
    /// </summary>
    internal class AuthZHttpHandler : RetryHttpHandler
    {
        /// <summary>
        /// Create new instance of <see cref="AuthZHttpHandler"/>
        /// </summary>
        /// <param name="retryOptions"></param>
        public AuthZHttpHandler(RetryOptions retryOptions = null) 
            : base(retryOptions)
        {
        }

        /// <inheritdoc cref="RetryHttpHandler.PreProcessHttpRequest"/>
        protected override async Task PreProcessHttpRequest(HttpRequestMessage httpRequestMessage)
        {
            // Authenticate request before it is sent.
            await Task.Run(() =>
            {
                httpRequestMessage.Headers.Authorization = AuthFactory.GetTokenProvider().GetAuthenticationHeader();
            });
        }

        /// <inheritdoc cref="RetryHttpHandler.RetryExceeded"/>
        protected override void RetryExceeded(int retryCount, int totalDelayApplied, Uri requestUri, string httpMethod)
        {
            // base class will throw.
        }

        /// <summary>
        /// Retry on unauthorized.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        protected override bool ShouldRetry(HttpResponseMessage responseMessage)
        {
            return responseMessage.StatusCode == HttpStatusCode.Unauthorized;
        }

        /// <inheritdoc cref="RetryHttpHandler.ApplyDelay"/>
        protected override Task<int> ApplyDelay(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
