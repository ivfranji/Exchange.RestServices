namespace Microsoft.RestServices.Tests.Service.FilterFormatter
{
    using Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FormatterProviderTests
    {
        [TestMethod]
        public void TestFormatterProviderTypes()
        {
            FormatterProvider formatterProvider = new FormatterProvider();
            
            // For unknown type always provide string formatter
            Assert.IsInstanceOfType(
                formatterProvider["UnknownType"],
                typeof(StringFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["System.String"],
                typeof(StringFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["System.Boolean"],
                typeof(BoolFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["System.DateTime"],
                typeof(DateTimeFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["System.DateTimeOffset"],
                typeof(DateTimeOffsetFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["System.Int32"],
                typeof(IntFilterFormatter));

            Assert.IsInstanceOfType(
                formatterProvider["Microsoft.OutlookServices.Recipient"],
                typeof(RecipientFilterFormatter));
        }
    }
}
