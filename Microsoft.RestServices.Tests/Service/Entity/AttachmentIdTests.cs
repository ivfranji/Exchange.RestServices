namespace Microsoft.RestServices.Tests.Service.Entity
{
    using Microsoft.RestServices.Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AttachmentIdTests
    {
        [TestMethod]
        public void TestAttachmentIdProperties()
        {
            AttachmentId attachmentId = new AttachmentId(
                "attachId", 
                new MessageId("itemId", "me"), 
                "me");

            Assert.AreEqual(
                "messages/itemId/attachments/attachId", 
                attachmentId.IdPath);

            Assert.AreEqual(
                "attachId",
                attachmentId.Id);
        }
    }
}
