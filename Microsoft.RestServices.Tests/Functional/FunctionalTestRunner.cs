namespace Microsoft.RestServices.Tests.Functional
{
    using System;
    using Exchange;

    /// <summary>
    /// Functional test runner.
    /// </summary>
    internal static class FunctionalTestRunner
    {
        /// <summary>
        /// Runs test case.
        /// </summary>
        /// <param name="testCase">Test case.</param>
        /// <param name="exchangeService">Exchange service.</param>
        internal static void RunTestCase(
            Action<ExchangeService> testCase, 
            ExchangeService exchangeService)
        {
            ArgumentValidator.ThrowIfNull(
                testCase, 
                nameof(testCase));
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            testCase(exchangeService);

            HttpWebRequestClientProvider.Instance.ExitLock();
        }
    }
}
