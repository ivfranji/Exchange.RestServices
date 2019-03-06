namespace Exchange.RestServices.Tests.Service.TraceHandler
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    [TestClass]
    public class TraceListenerHttpTests
    {
        [TestMethod]
        public void Test_HttpRequestHeaderTracing()
        {
            this.RunTest(
                TraceFlags.HttpRequestHeaders,
                true,
                this.GetRequestMessage(), 
                new HttpResponseMessage(HttpStatusCode.OK),
                (traceType, traceMessage) =>
                {
                    Assert.AreEqual(
                        TraceFlags.HttpRequestHeaders.ToString(),
                        traceType);

                    Assert.AreEqual(
                        "TestHeader1: TestHeaderValue1\r\nTestHeader2: TestHeaderValue2",
                        traceMessage);
                });
        }

        [TestMethod]
        public void Test_HttpRequestTracing()
        {
            this.RunTest(
                TraceFlags.HttpRequest,
                true,
                this.GetRequestMessage(),
                this.GetResponseMessage(),
                (traceType, traceMessage) =>
                {
                    Assert.AreEqual(
                        TraceFlags.HttpRequest.ToString(),
                        traceType);

                    Assert.AreEqual(
                        "Test request content",
                        traceMessage);
                });
        }

        [TestMethod]
        public void Test_HttpResponseTracing()
        {
            this.RunTest(
                TraceFlags.HttpResponse,
                true,
                this.GetRequestMessage(),
                this.GetResponseMessage(),
                (traceType, traceMessage) =>
                {
                    Assert.AreEqual(
                        TraceFlags.HttpResponse.ToString(),
                        traceType);

                    Assert.AreEqual(
                        "Test response content",
                        traceMessage);
                });
        }

        [TestMethod]
        public void Test_HttpResponseHeadersTracing()
        {
            this.RunTest(
                TraceFlags.HttpResponseHeaders,
                true,
                this.GetRequestMessage(),
                this.GetResponseMessage(),
                (traceType, traceMessage) =>
                {
                    Assert.AreEqual(
                        TraceFlags.HttpResponseHeaders.ToString(),
                        traceType);

                    Assert.AreEqual(
                        "TestResponseHeader: TestValue1, TestValue2",
                        traceMessage);
                });
        }

        [TestMethod]
        public void Test_TraceListenerHttpHandlerNotThrowingOnNullRequestContext()
        {
            this.RunTest(
                TraceFlags.All,
                true,
                this.GetRequestMessage(),
                this.GetResponseMessage(),
                (traceType, traceMessage) =>
                {

                },
                true);
        }

        private void RunTest(TraceFlags traceFlags, bool traceEnabled, HttpRequestMessage httpRequestMessage, HttpResponseMessage responseMessage, Action<string, string> assertation, bool skipHttpRequestContext = false)
        {
            if (!skipHttpRequestContext)
            {
                ITraceListener traceListener = new MockTraceListener()
                {
                    InlineAssertation = assertation
                };

                HttpRequestContext requestContext = new HttpRequestContext(
                    new TraceContext(
                        traceEnabled,
                        traceFlags,
                        traceListener));

                httpRequestMessage.Properties.Add(
                    nameof(HttpRequestContext),
                    requestContext);
            }
            
            TraceListenerHttpHandler traceListenerHttpHandler = new TraceListenerHttpHandler();
            traceListenerHttpHandler.InnerHandler = new MockHttpMessageHandler(responseMessage);

            using (HttpClient httpClient = new HttpClient(traceListenerHttpHandler)) // not production code so disposing it as soon as test finish
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequestMessage).Result;
            }
        }

        private HttpRequestMessage GetRequestMessage()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                "https://localhost");

            requestMessage.Content = new StringContent("Test request content");

            requestMessage.Headers.Add("TestHeader1", "TestHeaderValue1");
            requestMessage.Headers.Add("TestHeader2", "TestHeaderValue2");

            return requestMessage;
        }

        private HttpResponseMessage GetResponseMessage()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Headers.Add(
                "TestResponseHeader", 
                new []{"TestValue1", "TestValue2"});

            responseMessage.Content = new StringContent("Test response content");
            return responseMessage;
        }

        private class MockTraceListener : ITraceListener
        {
            public Action<string, string> InlineAssertation
            {
                get; set;
            }

            public void Trace(string traceType, string traceMessage)
            {
                this.InlineAssertation(traceType, traceMessage);
            }
        }
    }
}
