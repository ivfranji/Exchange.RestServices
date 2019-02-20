namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SyncQueryTests
    {
        [TestMethod]
        public void TestSyncQueryProperties()
        {
            SyncQuery syncQuery = new SyncQuery(
                10, 
                null);

            Assert.IsTrue(syncQuery.InitialQuery);
            Assert.AreEqual(
                string.Empty, 
                syncQuery.Query);

            syncQuery.SelectedProperties = new SelectQuery("prop1");
            Assert.AreEqual(
                "$select=prop1", 
                syncQuery.Query);

            SyncToken syncToken = new SyncToken(
                "abcdefg", 
                SyncTokenType.DeltaToken);

            syncQuery = new SyncQuery(
                10, 
                syncToken);

            Assert.AreEqual(
                "$deltatoken=abcdefg",
                syncQuery.Query);

            syncQuery.SelectedProperties = new SelectQuery("prop1");
            Assert.AreEqual(
                "$select=prop1&$deltatoken=abcdefg",
                syncQuery.Query);
        }
    }
}
