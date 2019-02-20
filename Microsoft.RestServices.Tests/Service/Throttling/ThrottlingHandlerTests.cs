namespace Microsoft.RestServices.Tests.Service.Throttling
{
    using System;
    using System.Net;
    using Exchange;
    using VisualStudio.TestTools.UnitTesting;
    using HttpWebRequest = Exchange.HttpWebRequest;

    [TestClass]
    public class ThrottlingHandlerTests
    {
        [TestMethod]
        public void TestThrottlingHandlerBehavior()
        {
            MockHttpClient mock = new MockHttpClient((HttpStatusCode)429, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
            };

            IHttpWebRequest request = HttpWebRequest.Get(new HttpRestUrl(new Uri("https://graph.microsoft.com/beta")));

            IHttpWebResponse httpWebResponse = ThrottlingHandler.ExecuteRequestUnderThrottlingGuard(
                request.GetResponse,
                1,
                null);

            Assert.IsTrue(httpWebResponse.StatusCode == (HttpStatusCode)429);
            Assert.AreEqual(
                1, 
                mock.RetryCount);

            mock.ResetRetryCount();

            httpWebResponse = ThrottlingHandler.ExecuteRequestUnderThrottlingGuard(
                request.GetResponse,
                6,
                null);
            
            Assert.AreEqual(
                6,
                mock.RetryCount);

            mock = new MockHttpClient(
                (HttpStatusCode)429, 
                HttpStatusCode.OK, 
                3, 
                "{}");

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);
            
            httpWebResponse = ThrottlingHandler.ExecuteRequestUnderThrottlingGuard(
                request.GetResponse,
                15,
                null);

            Assert.AreEqual(
            3, 
            mock.RetryCount);

            Assert.AreEqual(
                HttpStatusCode.OK, 
                httpWebResponse.StatusCode);

            mock = new MockHttpClient(HttpStatusCode.OK, "{}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            httpWebResponse = ThrottlingHandler.ExecuteRequestUnderThrottlingGuard(
                request.GetResponse,
                6,
                null);

            Assert.AreEqual(
                1,
                mock.RetryCount);

            HttpWebRequestClientProvider.Instance.Reset();
        }
    }
}
