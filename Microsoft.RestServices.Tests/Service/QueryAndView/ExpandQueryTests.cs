namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using Graph;
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

        [TestMethod]
        public void TestExpandQueryArray()
        {
            ExpandQuery expandQueryAttach = new ExpandQuery("attachments");
            ExpandQuery expandQuerySubject = new ExpandQuery("subject");

            ExpandQuery expandQueryArray = new ExpandQuery(
                expandQuerySubject, 
                expandQueryAttach);

            Assert.AreEqual(
                "$expand=subject,attachments",
                expandQueryArray.Query);
        }

        [TestMethod]
        public void TestExpandQueryArrayWithExtendedProperties()
        {
            ExtendedPropertyDefinition extendedProperty1 = new ExtendedPropertyDefinition(
                MapiPropertyType.Boolean, 
                3092);

            ExtendedPropertyDefinition extendedProperty2 = new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                1153);

            SearchFilter.SearchFilterCollection filterCollection = new SearchFilter.SearchFilterCollection(FilterOperator.and);
            filterCollection.AddFilter(
                new SearchFilter.IsEqualTo(
                    SingleValueLegacyExtendedPropertyObjectSchema.Id, 
                    extendedProperty1.Definition));

            filterCollection.AddFilter(
                new SearchFilter.IsEqualTo(
                    SingleValueLegacyExtendedPropertyObjectSchema.Id,
                    extendedProperty2.Definition));

            ExpandQuery expandQueryAttach = new ExpandQuery("attachments");
            Assert.AreEqual(
                "$expand=attachments", 
                expandQueryAttach.Query);

            ExpandExtendedPropertyQuery extendedPropertyExpandQuery = new ExpandExtendedPropertyQuery(
                filterCollection, 
                PropertyValueType.SingleValueExtendedProperties);

            Assert.AreEqual(
                "$expand=SingleValueExtendedProperties($filter=Id eq 'Boolean 0x0C14' and Id eq 'String 0x0481')",
                extendedPropertyExpandQuery.Query);

            ExpandQuery arrayExpandQuery = new ExpandQuery(
                expandQueryAttach, 
                extendedPropertyExpandQuery);

            Assert.AreEqual(
                "$expand=attachments,SingleValueExtendedProperties($filter=Id eq 'Boolean 0x0C14' and Id eq 'String 0x0481')",
                arrayExpandQuery.Query);
        }
    }
}
