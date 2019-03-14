namespace Exchange.RestServices
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This handler allows caller to specify their own handlers outside of the code.
    /// </summary>
    internal class ExternalHttpHandler : DelegatingHandler
    {
        /// <summary>
        /// External handler header name.
        /// </summary>
        private const string ExternalHandlerHeaderName = "X-ExternalHttpHandler";

        /// <summary>
        /// Send request async. If extension hasn't requested shortcirtuit, it will send it to
        /// inner handler.
        /// </summary>
        /// <param name="httpRequest">Http request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        protected sealed async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            HttpRequestContext requestContext = this.GetRequestContext(httpRequest);
            if (requestContext?.HttpExtension != null)
            {
                await requestContext.HttpExtension.ProcessHttpRequest(httpRequest);
                if (httpRequest.Headers.Contains(ExternalHttpHandler.ExternalHandlerHeaderName))
                {
                    httpRequest.Headers.Remove(ExternalHttpHandler.ExternalHandlerHeaderName);
                }

                httpRequest.Headers.Add(
                    ExternalHttpHandler.ExternalHandlerHeaderName, 
                    requestContext.HttpExtension.GetType().FullName);

                if (requestContext.HttpExtension.ShortCircuit)
                {
                    return await requestContext.HttpExtension.SendAsync(
                        httpRequest, 
                        cancellationToken);
                }
            }

            return await base.SendAsync(
                httpRequest, 
                cancellationToken);
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
