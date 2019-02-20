namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using Mocks;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SyncFolderItemsCollectionTests
    {
        [TestMethod]
        public void TestSyncFolderItemsCollectionWithNullResponseCollection()
        {
            SyncFolderItemsCollection<Message> collection = new SyncFolderItemsCollection<Message>(
                null, 
                Helper.Service,
                null);

            Assert.IsTrue(collection.TotalCount == 0);
            Assert.IsTrue(collection.SyncState == string.Empty);
        }
    }
}
