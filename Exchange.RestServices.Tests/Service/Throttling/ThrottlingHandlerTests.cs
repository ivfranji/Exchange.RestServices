namespace Exchange.RestServices.Tests.Service.Throttling
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

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

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestThrottlingWithRetryAfterHeader()
        {
            try
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                responseMessage.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(4));

                this.TestThrottlingBehaviorWithCustomStatusCode(
                    HttpStatusCode.ServiceUnavailable,
                    responseMessage);
            }
            catch (AggregateException e)
            {
                CallThrottledException throttledException = (CallThrottledException) e.InnerException;
                Assert.IsNotNull(throttledException);
                Assert.AreEqual(
                    3,
                    throttledException.RetryCount);

                // 3 * 4 seconds retrieved from Retry-After
                Assert.AreEqual(
                    12,
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
        private HttpResponseMessage TestThrottlingBehaviorWithCustomStatusCode(HttpStatusCode statusCode, HttpResponseMessage responseMessage = null)
        {
            if (null == responseMessage)
            {
                responseMessage = new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent("Response content")
                };
            }

            ThrottlingHttpHandler throttlingHttp = new ThrottlingHttpHandler(new RetryOptions(3, 2));
            throttlingHttp.InnerHandler = new MockHttpMessageHandler(responseMessage);
            HttpClient httpClient = new HttpClient(throttlingHttp);
            return httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:1234")).Result;
        }
    }
}
