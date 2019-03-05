namespace Exchange.RestServices.Tests.Service.QueryAndView
{
    using Exchange.RestServices;
    using Microsoft.OutlookServices;
    using Mocks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SyncFolderItemsCollectionTests
    {
        [TestMethod]
        public void TestSyncFolderItemsCollectionWithNullResponseCollection()
        {
            SyncFolderItemsCollection<Message> collection = new SyncFolderItemsCollection<Message>(
                null,
                new ExchangeService(
                    "abc",
                    "a@a.com",
                    RestEnvironment.OutlookBeta),
                null);

            Assert.IsTrue(collection.TotalCount == 0);
            Assert.IsTrue(collection.SyncState == string.Empty);
        }
    }
}
