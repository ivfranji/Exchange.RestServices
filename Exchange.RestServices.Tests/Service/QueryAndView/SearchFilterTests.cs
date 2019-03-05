namespace Exchange.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.OutlookServices;
    using Exchange.RestServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SearchFilterTests
    {
        [TestMethod]
        public void IsEqualToTest()
        {
            SearchFilter filter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.IsRead, 
                "True");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.eq);

            Assert.AreEqual(
                "$filter=IsRead eq True"
                ,filter.Query);
        }

        [TestMethod]
        public void NotEqualToTest()
        {
            SearchFilter filter = new SearchFilter.NotEqualTo(
                MessageObjectSchema.Body,
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
                MessageObjectSchema.CreatedDateTime,
                "2019-02-19");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.gt);

            Assert.AreEqual(
                "$filter=CreatedDateTime gt 2019-02-19"
                , filter.Query);
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo()
        {
            SearchFilter filter = new SearchFilter.IsGreaterThanOrEqualTo(
                MessageObjectSchema.CreatedDateTime,
                "20-02-19");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.ge);

            Assert.AreEqual(
                "$filter=CreatedDateTime ge 20-02-19"
                , filter.Query);
        }

        [TestMethod]
        public void IsLessThan()
        {
            SearchFilter filter = new SearchFilter.IsLessThan(
                MessageObjectSchema.ReceivedDateTime,
                "19-02-2019");

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.lt);

            Assert.AreEqual(
                "$filter=ReceivedDateTime lt 19-02-2019"
                , filter.Query);
        }

        [TestMethod]
        public void IsLessThanOrEqualTo()
        {
            SearchFilter filter = new SearchFilter.IsLessThanOrEqualTo(
                MailFolderObjectSchema.TotalItemCount,
                5);

            Assert.AreEqual(
                filter.FilterOperator,
                FilterOperator.le);

            Assert.AreEqual(
                "$filter=TotalItemCount le 5"
                , filter.Query);
        }

        [TestMethod]
        public void TestSearchFilterCollection()
        {
            SearchFilter lessThanOrEqualTo = new SearchFilter.IsLessThanOrEqualTo(
                MailFolderObjectSchema.TotalItemCount,
                5);

            SearchFilter greaterThan = new SearchFilter.IsGreaterThan(
                MessageObjectSchema.CreatedDateTime,
                new DateTimeOffset(new DateTime(2019, 2, 1)));

            SearchFilter notEqualTo = new SearchFilter.NotEqualTo(
                MessageObjectSchema.Body,
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
                "$filter=TotalItemCount le 5 and CreatedDateTime gt 2019-02-01T12:00:00Z and Body ne 'test body'",
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
                "$filter=TotalItemCount le 5 or CreatedDateTime gt 2019-02-01T12:00:00Z or Body ne 'test body'",
                searchFilterCollection.Query);
        }

        [TestMethod]
        public void TestRecipientFilter()
        {
            SearchFilter recipientFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.From,
                "a@b.com");

            Assert.AreEqual(
                "$filter=From/EmailAddress/Address eq 'a@b.com'",
                recipientFilter.Query);

            recipientFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.From,
                "A B");

            Assert.AreEqual(
                "$filter=From/EmailAddress/Name eq 'A B'",
                recipientFilter.Query);

            recipientFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.ToRecipients,
                "A B");
        }

        [TestMethod]
        public void Test_ExtendedPropertyFilter()
        {
            SearchFilter.ExtendedPropertyFilter extendedPropertyFilter = new SearchFilter.ExtendedPropertyFilter(
                FilterOperator.eq,
                new ExtendedPropertyDefinition(MapiPropertyType.String, 0x001A), 
                "IPM.Note");

            Assert.AreEqual(
                "$filter=SingleValueExtendedProperties/Any(ep: ep/PropertyId eq 'String 0x001A' and ep/Value eq 'IPM.Note')",
                extendedPropertyFilter.Query);

            extendedPropertyFilter = new SearchFilter.ExtendedPropertyFilter(
                FilterOperator.eq,
                new ExtendedPropertyDefinition(MapiPropertyType.Boolean, "CustomProp", new Guid("12f8e471-c941-4b2a-8216-8ad844533aa3")),
                "true");

            Assert.AreEqual(
                "$filter=SingleValueExtendedProperties/Any(ep: ep/PropertyId eq 'Boolean {12f8e471-c941-4b2a-8216-8ad844533aa3} Name CustomProp' and ep/Value eq 'true')",
                extendedPropertyFilter.Query);

            SearchFilter fromFilter = new SearchFilter.IsEqualTo(MessageObjectSchema.From, new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            SearchFilter.SearchFilterCollection filterCollection1 = new SearchFilter.SearchFilterCollection(
                FilterOperator.and, 
                extendedPropertyFilter, 
                fromFilter);

            Assert.AreEqual(
                "$filter=SingleValueExtendedProperties/Any(ep: ep/PropertyId eq 'Boolean {12f8e471-c941-4b2a-8216-8ad844533aa3} Name CustomProp' and ep/Value eq 'true') and From/EmailAddress/Address eq 'a@a.com'",
                filterCollection1.Query);

            SearchFilter.SearchFilterCollection filterCollection2 = new SearchFilter.SearchFilterCollection(
                FilterOperator.or, 
                filterCollection1,
                new SearchFilter.NotEqualTo(
                    MessageObjectSchema.IsRead, 
                    "false"));

            Assert.AreEqual(
                "$filter=SingleValueExtendedProperties/Any(ep: ep/PropertyId eq 'Boolean {12f8e471-c941-4b2a-8216-8ad844533aa3} Name CustomProp' and ep/Value eq 'true') and From/EmailAddress/Address eq 'a@a.com' or IsRead ne false",
                filterCollection2.Query);
        }
    }
}
