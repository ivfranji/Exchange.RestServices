namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SearchFilterTests
    {
        [TestMethod]
        public void IsEqualToTest()
        {
            SearchFilter filter = new SearchFilter.IsEqualTo(
                "IsRead", 
                "True");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.eq);

            Assert.AreEqual(
                "$filter=IsRead eq 'True'"
                ,filter.Query);
        }
    }
}
