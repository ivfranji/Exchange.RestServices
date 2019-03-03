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
                messageView.PropertySet.Add("unknown-message-property");
            });

            messageView.PropertySet.Add(new[] { "Body", "HasAttachments"});

            // First class properties will always be returned through this property set - Id, IsRead,Subject,ParentFolderId
            Assert.AreEqual(
                "$top=10&$skip=12&$select=Id,IsRead,Subject,ParentFolderId,Body,HasAttachments&$expand=attachments", 
                messageView.ViewQuery.Query);

            messageView.PropertySet.Add(new ExtendedPropertyDefinition(MapiPropertyType.String, 0x4001));
            
            Assert.AreEqual(
                "$top=10&$skip=12&$select=Id,IsRead,Subject,ParentFolderId,Body,HasAttachments&$expand=attachments,SingleValueExtendedProperties($filter=PropertyId eq 'String 0x4001')",
                messageView.ViewQuery.Query);

            messageView = new MessageView(
                10,
                12,
                false);

            messageView.PropertySet.Add(new ExtendedPropertyDefinition(MapiPropertyType.String, 0x4001));
            Assert.AreEqual(
                "$top=10&$skip=12&$expand=SingleValueExtendedProperties($filter=PropertyId eq 'String 0x4001')",
                messageView.ViewQuery.Query);
        }
    }
}
