namespace Exchange.RestServices.Tests.Service.Entity
{
    using Exchange;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskFolderIdTests
    {
        [TestMethod]
        public void TestTaskFolderIdProperties()
        {
            FolderId taskFolderId = new TaskFolderId("a@b.com");
            Assert.AreEqual(
                "tasks",
                taskFolderId.MessagesContainer);
        }
    }
}
