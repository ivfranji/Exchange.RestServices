namespace Microsoft.RestServices.Tests.Service.ExtendedProperties
{
    using Microsoft.RestServices.Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SingleValueExtendedPropertiesTest
    {
        [TestMethod]
        public void TestExpandSingleValueProperties()
        {
            SingleValueLegacyExtendedProperty prop = new SingleValueLegacyExtendedProperty();
            prop.PropertyId = "String 0x4001001E";

            SearchFilter filter = new SearchFilter.IsEqualTo(
                SingleValueLegacyExtendedPropertyObjectSchema.PropertyId, 
                $"{{{prop.PropertyId}}}");
            ExpandQuery expand = new ExpandQuery($"singleValueExtendedProperties({filter.Query})");

            Assert.AreEqual(
                "$expand=singleValueExtendedProperties($filter=PropertyId eq '{String 0x4001001E}')",
                expand.Query);
        }
    }
}
