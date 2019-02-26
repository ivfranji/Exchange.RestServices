namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Graph;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CompositeQueryTests
    {
        [TestMethod]
        public void TestCompositeQueryProperties()
        {
            SelectQuery selectQuery = new SelectQuery(new []{ "Body", "IsRead" });
            SearchFilter searchFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Body, 
                "test");

            CompositeQuery compositeQuery = new CompositeQuery(
                new IQuery[]
                {
                    selectQuery,
                    searchFilter
                });

            Assert.AreEqual(
                "$select=Body,IsRead&$filter=Body eq 'test'", 
                compositeQuery.Query);

            compositeQuery = new CompositeQuery(new []{ selectQuery });

            Assert.AreEqual(
                "$select=Body,IsRead",
                compositeQuery.Query);
        }

        [TestMethod]
        public void TestSelectAndPageQueryProperties()
        {
            SelectQuery selectQuery = new SelectQuery("IsRead");
            PageQuery pageQuery = new PageQuery(
                2, 
                17);

            SelectAndPageQuery query = new SelectAndPageQuery(
                selectQuery, 
                pageQuery);

            Assert.AreEqual(
                "$select=IsRead&$top=17&$skip=2", 
                query.Query);
        }

        [TestMethod]
        public void TestFilterAndPageQueryProperties()
        {
            SearchFilter filter = new SearchFilter.IsEqualTo(
                MailFolderObjectSchema.DisplayName, 
                "MhMh");

            PageQuery pageQuery = new PageQuery(
                14,
                11);

            FilterAndPageQuery query = new FilterAndPageQuery(
                filter, 
                pageQuery);

            Assert.AreEqual(
                "$filter=DisplayName eq 'MhMh'&$top=11&$skip=14",
                query.Query);
        }
    }
}
