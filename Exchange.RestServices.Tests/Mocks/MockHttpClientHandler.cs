namespace Exchange.RestServices.Tests.Mocks
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Mock handler for testing throttling delegate handler.
    /// </summary>
    internal class MockHttpMessageHandler : HttpMessageHandler
    {
        /// <summary>
        /// Response message to return.
        /// </summary>
        private HttpResponseMessage responseMessageToReturn;

        public MockHttpMessageHandler(HttpResponseMessage responseMessageToReturn)
        {
            this.responseMessageToReturn = responseMessageToReturn;
        }

        /// <summary>
        /// Sends request async.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.Invoke();
        }

        /// <summary>
        /// Override method in base class.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> Invoke()
        {
            return this.responseMessageToReturn;
        }
    }
}
