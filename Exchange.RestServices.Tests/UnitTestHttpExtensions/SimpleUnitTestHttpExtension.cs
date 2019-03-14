namespace Exchange.RestServices
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Simple http extension registered with Exchange service in mocks.
    /// </summary>
    internal class SimpleUnitTestHttpExtension : IHttpExtension
    {
        /// <summary>
        /// Http response message.
        /// </summary>
        private HttpResponseMessage httpResponseMessage;

        /// <summary>
        /// Create new instance of <see cref="SimpleUnitTestHttpExtension"/>
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        public SimpleUnitTestHttpExtension(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Process http request.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public Task ProcessHttpRequest(HttpRequestMessage httpRequest)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Short cirtuit - don't send request to internet, instead
        /// use <see cref="SendAsync"/> to return mock response.
        /// </summary>
        public bool ShortCircuit
        {
            get { return true; }
        }

        /// <summary>
        /// Performs inline assertation.
        /// </summary>
        public Action<HttpRequestMessage> InlineAssertation { get; set; }

        /// <summary>
        /// Short circuit request sent to internet, instead it handle
        /// return of the response.
        /// </summary>
        /// <param name="httpRequest">Http request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            InlineAssertation?.Invoke(httpRequest);
            return Task<HttpResponseMessage>.FromResult(this.httpResponseMessage);
        }
    }
}