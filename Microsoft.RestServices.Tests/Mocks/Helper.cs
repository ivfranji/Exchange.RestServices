namespace Microsoft.RestServices.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Net.Http;
    using VisualStudio.TestTools.UnitTesting;

    internal class Helper
    {
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
