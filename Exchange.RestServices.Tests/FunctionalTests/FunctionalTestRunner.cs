namespace Exchange.RestServices.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            //HttpWebRequestClientProvider.Instance.EnterLock();
            //HttpWebRequestClientProvider.Instance.Reset();

            testCase(exchangeService);

            //HttpWebRequestClientProvider.Instance.ExitLock();
        }

        /// <summary>
        /// Run multi threaded test case.
        /// </summary>
        /// <param name="exchangeService"></param>
        /// <param name="actions"></param>
        internal static void RunMultiThreadedTestCase(params TestCaseToExchangeServiceMapper[] testCaseToExchangeServiceMapper)
        {
            // don't lock here to tests multi thread access
            // to single instance of httpclient.
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < testCaseToExchangeServiceMapper.Length; i++)
            {
                ThreadStart threadStart = () =>
                {
                    testCaseToExchangeServiceMapper[i].TestCase(testCaseToExchangeServiceMapper[i].ExchangeService);
                };

                Thread thread = new Thread(threadStart);
                thread.Start();
                threads.Add(thread);
            }

            Thread.Sleep(30000);
            foreach (Thread thread in threads)
            {
                Assert.AreEqual(
                    ThreadState.Stopped,
                    thread.ThreadState);
            }
        }
    }

    /// <summary>
    /// Action to service mapper.
    /// </summary>
    internal class TestCaseToExchangeServiceMapper
    {
        /// <summary>
        /// Exchange service.
        /// </summary>
        public ExchangeService ExchangeService { get; set; }

        /// <summary>
        /// Test case.
        /// </summary>
        public Action<ExchangeService> TestCase { get; set; }
    }
}
