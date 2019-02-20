namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MessageViewTests
    {
        [TestMethod]
        public void TestMessageViewProperties()
        {
            MessageView messageView = new MessageView(
                10, 
                false);

            Assert.AreEqual(
                "$top=10&$skip=0", 
                messageView.ViewQuery.Query);

            messageView = new MessageView(
                10, 
                12, 
                true);

            Assert.AreEqual(
                "$top=10&$skip=12&$expand=attachments",
                messageView.ViewQuery.Query);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                messageView.SelectProperty("unknown-message-property");
            });

            messageView.SelectProperty(
                new []{"Body", "hasAttachments"});

            Assert.AreEqual(
                "$top=10&$skip=12&$select=Body,hasAttachments&$expand=attachments", 
                messageView.ViewQuery.Query);

            messageView.SelectProperty(new ExtendedPropertyDefinition(MapiPropertyType.String, 0x4001));

            Assert.AreEqual(
                "$top=10&$skip=12&$select=Body,hasAttachments&$expand=attachments,singleValueExtendedProperties($filter=Id eq 'String 0x4001')",
                messageView.ViewQuery.Query);

            messageView = new MessageView(
                10,
                12,
                false);

            messageView.SelectProperty(new ExtendedPropertyDefinition(MapiPropertyType.String, 0x4001));
            Assert.AreEqual(
                "$top=10&$skip=12&$expand=singleValueExtendedProperties($filter=Id eq 'String 0x4001')",
                messageView.ViewQuery.Query);
        }
    }
}
