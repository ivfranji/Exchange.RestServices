namespace Exchange.RestServices.Tests.Service.Extensions
{
    using System;
    using Exchange.RestServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtendedPropertyTests
    {
        [TestMethod]
        public void TestExtendedPropertyDefinitionFormat()
        {
            string guidString = "27575576-54cd-4064-b58a-94f4bc8b05e4";
            Guid propertySet = new Guid(guidString);
            ExtendedPropertyDefinition extendedPropertyDefinition = new ExtendedPropertyDefinition(
                MapiPropertyType.String, 
                0x4541);

            Assert.AreEqual(
                "String 0x4541", 
                extendedPropertyDefinition.Definition);

            extendedPropertyDefinition = new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                9029);

            Assert.AreEqual(
                "String 0x2345",
                extendedPropertyDefinition.Definition);

            extendedPropertyDefinition = new ExtendedPropertyDefinition(
                MapiPropertyType.BinaryArray,
                9029,
                propertySet);

            Assert.AreEqual(
                $"BinaryArray {{{guidString}}} Id 0x2345", 
                extendedPropertyDefinition.Definition);

            Assert.IsTrue(
                extendedPropertyDefinition.PropertyValueType == PropertyValueType.MultiValueExtendedProperties);

            extendedPropertyDefinition = new ExtendedPropertyDefinition(
                MapiPropertyType.Boolean,
                "PropName",
                propertySet);

            Assert.AreEqual(
                $"Boolean {{{guidString}}} Name PropName",
                extendedPropertyDefinition.Definition);

            Assert.IsTrue(
                extendedPropertyDefinition.PropertyValueType == PropertyValueType.SingleValueExtendedProperties);
        }
    }
}
