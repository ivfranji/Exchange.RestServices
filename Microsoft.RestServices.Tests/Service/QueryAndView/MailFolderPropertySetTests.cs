namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MailFolderPropertySetTests
    {
        [TestMethod]
        public void MailFolderPropertySetProperties()
        {
            MailFolderPropertySet mailFolderPropertySet = new MailFolderPropertySet();
            Assert.IsNull(mailFolderPropertySet.Properties);

            Assert.IsTrue(
                mailFolderPropertySet.FirstClassProperties.Contains(nameof(Entity.Id)));

            Assert.IsTrue(
                mailFolderPropertySet.FirstClassProperties.Contains(nameof(MailFolder.ChildFolderCount)));

            Assert.IsTrue(
                mailFolderPropertySet.FirstClassProperties.Contains(nameof(MailFolder.DisplayName)));

            Assert.IsTrue(
                mailFolderPropertySet.FirstClassProperties.Contains(nameof(MailFolder.TotalItemCount)));

            mailFolderPropertySet.AddProperty("ChildFolderCount");

            // First class properties + one added
            Assert.IsTrue(mailFolderPropertySet.Properties.Properties.Length == 4);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailFolderPropertySet.AddProperty("NonExistingProp");
            });

            Assert.IsNull(mailFolderPropertySet.ExpandQuery);

            mailFolderPropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String, 
                3421));

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'String 0x0D5D')",
                mailFolderPropertySet.ExpandQuery.Query);

            mailFolderPropertySet = new MailFolderPropertySet();
            Assert.IsNull(mailFolderPropertySet.Properties);
            Assert.IsNull(mailFolderPropertySet.ExpandQuery);

            mailFolderPropertySet.Add(new ExtendedPropertyDefinition(MapiPropertyType.StringArray, 3421));
            Assert.AreEqual(
                "$expand=MultiValueExtendedProperties($filter=Id eq 'StringArray 0x0D5D')", 
                mailFolderPropertySet.ExpandQuery.Query);

            mailFolderPropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                3421));

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'String 0x0D5D'),MultiValueExtendedProperties($filter=Id eq 'StringArray 0x0D5D')",
                mailFolderPropertySet.ExpandQuery.Query);
        }
    }
}
