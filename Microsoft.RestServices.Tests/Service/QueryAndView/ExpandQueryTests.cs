namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpandQueryTests
    {
        [TestMethod]
        public void TestSyncQueryProperties()
        {
            ExpandQuery expandQuery = new ExpandQuery("attachments");
            Assert.AreEqual(
                "$expand=attachments", 
                expandQuery.Query);
        }
    }
}
