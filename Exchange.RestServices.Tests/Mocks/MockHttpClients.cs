namespace Exchange.RestServices.Tests
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using Exchange.RestServices;


    internal class MockHttpClient : IHttpWebClient
    {
        private HttpStatusCode statusCodeOnStart;
        private HttpStatusCode statusCodeOnEnd;

        internal MockHttpClient(HttpStatusCode statusCode, string contentToReturn)
        {
            this.ReturnStatusCode = statusCode;
            this.ContentToReturn = contentToReturn;
            this.RetryCount = 0;
            this.statusCodeOnStart = statusCode;
        }

        internal MockHttpClient(HttpStatusCode statusCodeOnStart, HttpStatusCode statusCodeOnEnd, int changeStatusCodeAfter, string contentToReturn)
            : this(statusCodeOnStart, contentToReturn)
        {
            if (changeStatusCodeAfter < 1)
            {
                throw new ArgumentException("Change after must be at least 1");
            }

            this.statusCodeOnEnd = statusCodeOnEnd;
            this.ChangeStatusCodeAfterRetries = changeStatusCodeAfter;
        }

        /// <summary>
        /// Callback to execute assert statements against
        /// <see cref="HttpRequestMessage"/> before sending
        /// request.
        /// </summary>
        public Action<HttpRequestMessage> InlineAssertation
        {
            get;
            set;
        }

        // <summary>
        /// Mock client instance.
        /// </summary>
        public Func<IHttpWebClient> MockClient
        {
            get { return () => this; }
        }

        private bool HasContinuation
        {
            get { return this.ChangeStatusCodeAfterRetries > 0; }
        }

        internal int ChangeStatusCodeAfterRetries { get; }

        internal int RetryCount { get; private set; }

        internal HttpStatusCode ReturnStatusCode { get; private set; }

        internal string ContentToReturn { get; }

        public void Dispose()
        {
        }

        /// <summary>
        /// Mock.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            if (null != this.InlineAssertation)
            {
                this.InlineAssertation(requestMessage);
            }

            this.RetryCount++;
            if (this.HasContinuation && this.ChangeStatusCodeAfterRetries == this.RetryCount)
            {
                this.ReturnStatusCode = this.statusCodeOnEnd;
            }
            
            return this.MockInvoke(requestMessage);
        }

        /// <inheritdoc cref="IHttpWebClient.SetProxyServer"/>
        public void SetProxyServer(IWebProxy proxyServer)
        {
        }

        /// <summary>
        /// Override method in base class.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> MockInvoke(HttpRequestMessage requestMessage)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(this.ReturnStatusCode);
            responseMessage.Content = new StringContent(
                this.ContentToReturn,
                Encoding.UTF8,
                "application/json");

            return responseMessage;
        }

        internal void ResetRetryCount()
        {
            this.RetryCount = 0;
        }
    }
}
