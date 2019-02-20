namespace Microsoft.RestServices.Tests
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FolderIdTests
    {
        [TestMethod]
        public void TestFolderIdProperties()
        {
            FolderId folderId = new FolderId(
                "abcdefgh", 
                "test@domain.com");

            Assert.AreEqual(
                folderId.Id,
                "abcdefgh");

            Assert.AreEqual(
                folderId.MessagesContainer,
                "mailfolders/abcdefgh/messages");

            Assert.AreEqual(
                folderId.RootContainer, 
                "mailfolders");

            Assert.AreEqual(
                folderId.MessagesDelta,
                "mailfolders/abcdefgh/messages/delta");

            Assert.AreEqual(
                folderId.ChildFoldersContainer,
                "mailfolders/abcdefgh/childfolders");

            Assert.AreEqual(
                folderId.ToString(),
                "abcdefgh");

            Assert.AreEqual(
                folderId.MailboxId,
                new MailboxId("test@domain.com"));

            folderId = new FolderId("abcdeef");
            Assert.AreEqual(
                folderId.MailboxId, 
                new MailboxId("me"));

            folderId = new FolderId(WellKnownFolderName.ArchiveInbox);
            Assert.AreEqual(
                folderId.MailboxId,
                new MailboxId("me"));
        }
    }
}
