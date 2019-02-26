namespace Microsoft.RestServices.Tests.Service.ExtendedProperties
{
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SingleValueExtendedPropertiesTest
    {
        [TestMethod]
        public void TestExpandSingleValueProperties()
        {
            SingleValueLegacyExtendedProperty prop = new SingleValueLegacyExtendedProperty();
            prop.Id = "String 0x4001001E";

            SearchFilter filter = new SearchFilter.IsEqualTo(
                SingleValueLegacyExtendedPropertyObjectSchema.Id, 
                $"{{{prop.Id}}}");
            ExpandQuery expand = new ExpandQuery($"singleValueExtendedProperties({filter.Query})");

            Assert.AreEqual(
                "$expand=singleValueExtendedProperties($filter=Id eq '{String 0x4001001E}')",
                expand.Query);
        }
    }
}
