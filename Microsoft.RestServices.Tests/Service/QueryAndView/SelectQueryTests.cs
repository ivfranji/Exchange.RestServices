namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SelectQueryTests
    {
        [TestMethod]
        public void TestSelectQueryProperties()
        {
            SelectQuery selectQuery = new SelectQuery("HasAttachment");

            Assert.AreEqual(
                "HasAttachment", 
                selectQuery.Properties[0]);

            Assert.AreEqual(
                "$select=HasAttachment", 
                selectQuery.Query);

            selectQuery = new SelectQuery(new []{"IsRead", "Body"});

            Assert.AreEqual(
                "IsRead", 
                selectQuery.Properties[0]);

            Assert.AreEqual(
                "Body",
                selectQuery.Properties[1]);

            Assert.AreEqual(
                "$select=IsRead,Body", 
                selectQuery.Query);
        }
    }
}
