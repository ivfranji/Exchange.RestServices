namespace Exchange.RestServices.Tests.MockTests
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Mock test runner.
    /// </summary>
    internal static class MockTestRunner
    {
        /// <summary>
        /// Run mock test case.
        /// </summary>
        /// <param name="testCase">Test case to run.</param>
        /// <param name="mailboxId">Mailbox Id.</param>
        /// <param name="mockHttpResponseMessage">Mock response message.</param>
        /// <param name="inlineAssertation">Inline assertation.</param>
        /// <param name="restEnvironment">Rest environment - Default BETA.</param>
        internal static void RunTestCase(
            Action<ExchangeService> testCase,
            string mailboxId,
            HttpResponseMessage mockHttpResponseMessage,
            Action<HttpRequestMessage> inlineAssertation,
            Environment restEnvironment = Environment.Beta)
        {
            ExchangeService exchangeService = restEnvironment == Environment.Beta
                ? MockTestRunner.GetExchangeServiceBeta(mailboxId)
                : MockTestRunner.GetExchangeServiceProd(mailboxId);

            SimpleUnitTestHttpExtension simpleUnitTestHttpExtension = new SimpleUnitTestHttpExtension(mockHttpResponseMessage);
            simpleUnitTestHttpExtension.InlineAssertation = inlineAssertation;
            exchangeService.HttpExtension = simpleUnitTestHttpExtension;

            testCase(exchangeService);
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
            string mailboxId,
            HttpResponseMessage httpResponseMessage,
            Action<HttpRequestMessage> inlineAssertation,
            Environment restEnvironment = Environment.Beta)
        {
            MockTestRunner.RunTestCase(
                testCase,
                mailboxId,
                httpResponseMessage, 
                inlineAssertation,
                restEnvironment);
        }

        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        private static ExchangeService GetExchangeServiceBeta(string mailboxId)
        {
            return new ExchangeService(
                "<bearer>",
                mailboxId,
                RestEnvironment.OutlookBeta);
        }

        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        private static ExchangeService GetExchangeServiceProd(string mailboxId)
        {
            return new ExchangeService(
                "<bearer>",
                mailboxId,
                RestEnvironment.OutlookProd);
        }
    }

    /// <summary>
    /// Mock environment.
    /// </summary>
    internal enum Environment
    {
        Beta,
        Prod
    }
}
