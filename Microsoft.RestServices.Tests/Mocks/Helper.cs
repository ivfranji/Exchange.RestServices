namespace Microsoft.RestServices.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    internal class Helper
    {
        /// <summary>
        /// Bearer token: abc
        /// xAnchorMailbox a@a.com
        /// </summary>
        internal static ExchangeService Service
        {
            get
            {
                return new ExchangeService(
                    "abc",
                    "a@a.com");
            }
        }

        internal static void ValidateXAnchorMailbox(HttpRequestMessage message, string expectedValue)
        {
            Assert.IsTrue(message.Headers.Contains("X-AnchorMailbox"));

            IEnumerable<string> values;
            Assert.IsTrue(
                message.Headers.TryGetValues("X-AnchorMailbox", out values));

            foreach (string value in values)
            {
                Assert.AreEqual(
                    value,
                    expectedValue);
            }
        }
    }
}
