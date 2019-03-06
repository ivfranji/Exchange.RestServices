namespace Exchange.RestServices.Tests.Service.Throttling
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Throttling handler test.
    /// </summary>
    [TestClass]
    public class ThrottlingHandlerTests
    {
        /// <summary>
        /// Test throttling behavior with status code 429.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestThrottling429Behavior()
        {
            try
            {
                this.TestThrottlingBehaviorWithCustomStatusCode((HttpStatusCode) 429);
                Assert.Fail("We shouldn't be here...");
            }
            catch (AggregateException e)
            {
                CallThrottledException throttledException = (CallThrottledException) e.InnerException;
                Assert.IsNotNull(throttledException);
                Assert.AreEqual(
                    3,
                    throttledException.RetryCount);

                Assert.AreEqual(
                    6,
                    throttledException.TotalDelayApplied);

                throw;
            }
        }

        /// <summary>
        /// Test throttling behavior with status code 503.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestThrottling503Behavior()
        {
            try
            {
                this.TestThrottlingBehaviorWithCustomStatusCode(HttpStatusCode.ServiceUnavailable);
                Assert.Fail("We shouldn't be here...");
            }
            catch (AggregateException e)
            {
                CallThrottledException throttledException = (CallThrottledException) e.InnerException;
                Assert.IsNotNull(throttledException);
                Assert.AreEqual(
                    3,
                    throttledException.RetryCount);

                Assert.AreEqual(
                    6,
                    throttledException.TotalDelayApplied);

                throw;
            }
        }

        /// <summary>
        /// No throttling should be applied on HTTP 200
        /// </summary>
        [TestMethod]
        public void TestThrottling200Behavior()
        {
            HttpResponseMessage responseMessage = this.TestThrottlingBehaviorWithCustomStatusCode(HttpStatusCode.OK);
            Assert.AreEqual(
                "Response content",
                responseMessage.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Test throttling with custom status code.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private HttpResponseMessage TestThrottlingBehaviorWithCustomStatusCode(HttpStatusCode statusCode)
        {
            RetryDelegatingHandler retryDelegating = new RetryDelegatingHandler(new RetryOptions(3, 2));
            retryDelegating.InnerHandler = new MockHttpMessageHandler(
                new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent("Response content")
                });

            HttpClient httpClient = new HttpClient(retryDelegating);
            return httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:1234")).Result;
        }

        /// <summary>
        /// Mock handler for testing throttling delegate handler.
        /// </summary>
        private class MockHttpMessageHandler : HttpMessageHandler
        {
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
}
