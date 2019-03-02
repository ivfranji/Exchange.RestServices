namespace Microsoft.RestServices.Tests.Service
{
    using System;
    using System.Net;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// == Several routing scenarios ==
    ///
    /// 1. Exchange service mailboxId = me, folder/item ids null / me - it should result in /me/...
    /// 2. Exchange service mailboxId = me, folder/item ids != null / should result in whatever on folder item is.
    /// 3. Exchange service mailboxId != me, folder/item ids null / should result in mailboxId from service
    /// 4. Exchange service mailboxId != me, folder/item ids != null / should result in mailboxId from item/folder
    /// </summary>
    [TestClass]
    public class ExchangeServiceRequestRoutingTests
    {
        [TestMethod]
        public void TestMeGetItemScenario()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK,
                "");

            mock.InlineAssertation = (message) =>
            {
                Uri uri = new Uri("https://Microsoft.OutlookServices.microsoft.com/beta/me/messages/id-123");
                Assert.AreEqual(
                    uri, 
                    message.RequestUri);

                // For /me request we don't provide X-AnchorMailbox
                // TODO: but we should...
                Assert.IsFalse(
                    message.Headers.Contains("X-AnchorMailbox"));
            };

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService meService = new ExchangeService(
                "abc", 
                string.Empty);

            Assert.IsTrue(meService.MailboxId.IdInMeForm);

            meService.GetItem(new MessageId(
                "id-123", 
                "me"));
        }

        [TestMethod]
        public void TestEmailGetItemScenarioWithMeMessageId()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK,
                "");

            mock.InlineAssertation = (message) =>
            {
                Uri uri = new Uri("https://Microsoft.OutlookServices.microsoft.com/beta/users/a@a.com/messages/id-123");
                Assert.AreEqual(
                    uri,
                    message.RequestUri);

                Helper.ValidateXAnchorMailbox(message, "a@a.com");
            };

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService meService = new ExchangeService(
                "abc",
                "a@a.com");

            Assert.IsTrue(meService.MailboxId.IdInEmailAddressForm);

            meService.GetItem(new MessageId(
                "id-123",
                "me"));
        }

        [TestMethod]
        public void TestEmailGetItemScenarioWithDelegateMessageId()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK,
                "");

            mock.InlineAssertation = (message) =>
            {
                Uri uri = new Uri("https://Microsoft.OutlookServices.microsoft.com/beta/users/b@b.com/messages/id-123");
                Assert.AreEqual(
                    uri,
                    message.RequestUri);

                Helper.ValidateXAnchorMailbox(message, "b@b.com");

            };

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService meService = new ExchangeService(
                "abc",
                "a@a.com");

            Assert.IsTrue(meService.MailboxId.IdInEmailAddressForm);

            meService.GetItem(new MessageId(
                "id-123",
                "b@b.com"));
        }
    }
}
