namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
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

        [TestMethod]
        public void NotEqualToTest()
        {
            SearchFilter filter = new SearchFilter.NotEqualTo(
                "Body",
                "test body");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.ne);

            Assert.AreEqual(
                "$filter=Body ne 'test body'"
                , filter.Query);
        }

        [TestMethod]
        public void IsGreaterThanTest()
        {
            SearchFilter filter = new SearchFilter.IsGreaterThan(
                "DateReceived",
                "19-02-01");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.gt);

            Assert.AreEqual(
                "$filter=DateReceived gt '19-02-01'"
                , filter.Query);
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo()
        {
            SearchFilter filter = new SearchFilter.IsGreaterThanOrEqualTo(
                "DateReceived",
                "19-02-01");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.ge);

            Assert.AreEqual(
                "$filter=DateReceived ge '19-02-01'"
                , filter.Query);
        }

        [TestMethod]
        public void IsLessThan()
        {
            SearchFilter filter = new SearchFilter.IsLessThan(
                "Size",
                "5");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.lt);

            Assert.AreEqual(
                "$filter=Size lt '5'"
                , filter.Query);
        }

        [TestMethod]
        public void IsLessThanOrEqualTo()
        {
            SearchFilter filter = new SearchFilter.IsLessThanOrEqualTo(
                "Size",
                "5");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.le);

            Assert.AreEqual(
                "$filter=Size le '5'"
                , filter.Query);
        }

        [TestMethod]
        public void TestSearchFilterCollection()
        {
            SearchFilter lessThanOrEqualTo = new SearchFilter.IsLessThanOrEqualTo(
                "Size",
                "5");

            SearchFilter greaterThan = new SearchFilter.IsGreaterThan(
                "DateReceived",
                "19-02-01");

            SearchFilter notEqualTo = new SearchFilter.NotEqualTo(
                "Body",
                "test body");

            SearchFilter.SearchFilterCollection searchFilterCollection = new SearchFilter.SearchFilterCollection(
                FilterOperator.and,
                lessThanOrEqualTo,
                greaterThan,
                notEqualTo);

            Assert.AreEqual(
                FilterOperator.and,
                searchFilterCollection.FilterOperator);

            Assert.AreEqual(
                "$filter=Size le '5' and DateReceived gt '19-02-01' and Body ne 'test body'",
                searchFilterCollection.Query);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                searchFilterCollection = new SearchFilter.SearchFilterCollection(FilterOperator.ge);
            });

            searchFilterCollection = new SearchFilter.SearchFilterCollection(
                FilterOperator.or,
                lessThanOrEqualTo,
                greaterThan,
                notEqualTo);

            Assert.AreEqual(
                FilterOperator.or,
                searchFilterCollection.FilterOperator);

            Assert.AreEqual(
                "$filter=Size le '5' or DateReceived gt '19-02-01' or Body ne 'test body'",
                searchFilterCollection.Query);
        }
    }
}
