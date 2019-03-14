namespace Exchange.RestServices.Service.HttpCore.Extensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

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
                HttpRequestContext requestContext = this.GetRequestContext(httpRequestMessage);
                if (null == requestContext)
                {
                    throw new ArgumentNullException(
                        nameof(requestContext), 
                        "Request context isn't set by service.");
                }

                if (null == requestContext.AuthorizationProvider)
                {
                    throw new ArgumentNullException(nameof(requestContext.AuthorizationProvider), "Authorization provider not available.");
                }

                httpRequestMessage.Headers.Authorization = requestContext.AuthorizationProvider.GetAuthenticationHeader();
            });
        }

        /// <inheritdoc cref="RetryHttpHandler.RetryExceeded"/>
        protected override void RetryExceeded(int retryCount, int totalDelayApplied, Uri requestUri, string httpMethod, HttpStatusCode lastHttpStatusCode)
        {
            // base will throw.
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

        /// <summary>
        /// Get http request context from request message.
        /// </summary>
        /// <param name="httpRequestMessage">Http request message.</param>
        /// <returns></returns>
        private HttpRequestContext GetRequestContext(HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage.Properties.ContainsKey(nameof(HttpRequestContext)))
            {
                return (HttpRequestContext) httpRequestMessage.Properties[nameof(HttpRequestContext)];
            }

            return null;
        }
    }
}
