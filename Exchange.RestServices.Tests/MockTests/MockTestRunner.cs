namespace Exchange.RestServices.Tests.MockTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Exchange;

    /// <summary>
    /// Mock test runner.
    /// </summary>
    internal static class MockTestRunner
    {
        /// <summary>
        /// Run mock test case.
        /// </summary>
        /// <param name="testCase">Test case.</param>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="desiredReturnStatusCode">Desired return http status code.</param>
        /// <param name="httpReturnContent">Return content.</param>
        /// <param name="inlineAssertation">Inline assertation.</param>
        internal static void RunTestCase(
            Action<ExchangeService> testCase, 
            ExchangeService exchangeService, 
            HttpStatusCode desiredReturnStatusCode,
            string httpReturnContent,
            Action<HttpRequestMessage> inlineAssertation)
        {
            ArgumentValidator.ThrowIfNull(testCase, nameof(testCase));
            HttpWebRequestClientProvider.Instance.EnterLock();
            MockHttpClient mock = new MockHttpClient(desiredReturnStatusCode, httpReturnContent);
            mock.InlineAssertation = inlineAssertation;
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            testCase(exchangeService);

            HttpWebRequestClientProvider.Instance.Reset();
            HttpWebRequestClientProvider.Instance.ExitLock();
        }

        /// <summary>
        /// Run test case and return HTTP 200 (OK).
        /// </summary>
        /// <param name="testCase">Test case.</param>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="httpReturnContent">Return content.</param>
        /// <param name="inlineAssertation">Inline assertation.</param>
        internal static void RunHttp200TestCase(
            Action<ExchangeService> testCase,
            ExchangeService exchangeService,
            string httpReturnContent,
            Action<HttpRequestMessage> inlineAssertation)
        {
            MockTestRunner.RunTestCase(
                testCase,
                exchangeService,
                HttpStatusCode.OK,
                httpReturnContent,
                inlineAssertation);
        }

        /// <summary>
        /// Run simple test case with Exchange service.
        /// </summary>
        /// <param name="testCase">Test case.</param>
        /// <param name="exchangeService">Exchange service.</param>
        internal static void RunTestCase(Action<ExchangeService> testCase, ExchangeService exchangeService)
        {
            ArgumentValidator.ThrowIfNull(testCase, nameof(testCase));
            testCase(exchangeService);
        }
    }
}
