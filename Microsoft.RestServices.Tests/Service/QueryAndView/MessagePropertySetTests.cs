namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MessagePropertySetTests
    {
        [TestMethod]
        public void MessagePropertySetProperties()
        {
            MessagePropertySet messagePropertySet = new MessagePropertySet();
            Assert.IsNull(messagePropertySet.SelectProperties);

            Assert.IsTrue(
                messagePropertySet.FirstClassProperties.Contains(nameof(Entity.Id)));

            Assert.IsTrue(
                messagePropertySet.FirstClassProperties.Contains(nameof(Message.IsRead)));

            Assert.IsTrue(
                messagePropertySet.FirstClassProperties.Contains(nameof(Message.Subject)));

            Assert.IsTrue(
                messagePropertySet.FirstClassProperties.Contains(nameof(Message.ParentFolderId)));

            messagePropertySet.AddProperty(nameof(Message.Body));

            // First class properties + one added
            Assert.IsTrue(messagePropertySet.SelectProperties.Properties.Length == 5);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                messagePropertySet.AddProperty("NonExistingProp");
            });

            messagePropertySet.AddProperties(new []{nameof(Message.BodyPreview), nameof(Message.CcRecipients)});

            Assert.IsTrue(messagePropertySet.SelectProperties.Properties.Length == 7);
        }
    }
}
