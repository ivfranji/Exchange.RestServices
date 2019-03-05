namespace Exchange.RestServices.Tests.Service.Entity
{
    using Exchange.RestServices;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
