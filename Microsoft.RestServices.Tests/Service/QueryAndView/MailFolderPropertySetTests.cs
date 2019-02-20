namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MailFolderPropertySetTests
    {
        [TestMethod]
        public void MailFolderPropertySetProperties()
        {
            MailFolderPropertySet mailFolderPropertySet = new MailFolderPropertySet();
            Assert.IsNull(mailFolderPropertySet.SelectProperties);

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
            Assert.IsTrue(mailFolderPropertySet.SelectProperties.Properties.Length == 4);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailFolderPropertySet.AddProperty("NonExistingProp");
            });
        }
    }
}
