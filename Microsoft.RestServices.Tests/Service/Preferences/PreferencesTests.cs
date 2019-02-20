namespace Microsoft.RestServices.Tests.Service.Preferences
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PreferencesTests
    {
        [TestMethod]
        public void TestPreferenceProperties()
        {
            Preference preference1 = new Preference("outlook.timezone=\"Pacific Standard Time\"");
            Assert.AreEqual(
                "outlook.timezone=\"Pacific Standard Time\"", 
                preference1.Prefer);

            Preference preference2 = new Preference("outlook.timezone=\"Pacific Standard Time\"");
            Assert.IsTrue(preference1.Equals(preference2));
        }

        [TestMethod]
        public void TestPreferenceAppliedOnHttpWebRequest()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK,
                "");

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (message) =>
            {
                Assert.IsTrue(message.Headers.Contains("Prefer"));

                IEnumerable<string> value;
                Assert.IsTrue(message.Headers.TryGetValues("Prefer", out value));

                foreach (string s in value)
                {
                    Assert.AreEqual("IdType=\"ImmutableId\"", s);
                }
            };

            
            ExchangeService exchangeService = new ExchangeService(
                "bearer", 
                "a@b.com");

            exchangeService.Preferences.Add(new Preference("IdType=\"ImmutableId\""));
            exchangeService.GetItem(new MessageId("abc", exchangeService.MailboxId));

            HttpWebRequestClientProvider.Instance.Reset();
        }
    }
}
