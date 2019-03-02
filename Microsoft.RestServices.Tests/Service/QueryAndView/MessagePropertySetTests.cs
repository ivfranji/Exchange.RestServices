namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MessagePropertySetTests
    {
        [TestMethod]
        public void MessagePropertySetProperties()
        {
            MessagePropertySet messagePropertySet = new MessagePropertySet();
            Assert.IsNull(messagePropertySet.Properties);

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
            Assert.IsTrue(messagePropertySet.Properties.Properties.Length == 5);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                messagePropertySet.AddProperty("NonExistingProp");
            });

            messagePropertySet.AddProperties(new []{nameof(Message.BodyPreview), nameof(Message.CcRecipients)});

            Assert.IsTrue(messagePropertySet.Properties.Properties.Length == 7);

            messagePropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                3421));

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'String 0x0D5D')",
                messagePropertySet.ExpandQuery.Query);

            messagePropertySet = new MessagePropertySet();
            Assert.IsNull(messagePropertySet.Properties);
            Assert.IsNull(messagePropertySet.ExpandQuery);

            messagePropertySet.Add(new ExtendedPropertyDefinition(MapiPropertyType.StringArray, 3421));
            Assert.AreEqual(
                "$expand=MultiValueExtendedProperties($filter=Id eq 'StringArray 0x0D5D')",
                messagePropertySet.ExpandQuery.Query);

            messagePropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                3421));

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'String 0x0D5D'),MultiValueExtendedProperties($filter=Id eq 'StringArray 0x0D5D')",
                messagePropertySet.ExpandQuery.Query);

            messagePropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.Boolean,
                0x0E1F));

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'String 0x0D5D' or Id eq 'Boolean 0x0E1F'),MultiValueExtendedProperties($filter=Id eq 'StringArray 0x0D5D')",
                messagePropertySet.ExpandQuery.Query);
        }
    }
}
