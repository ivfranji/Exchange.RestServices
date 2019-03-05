namespace Exchange.RestServices.Tests.Service.PropertyChangeTracking
{
    using System.Collections.Generic;
    using Exchange;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertyDefinitionTests
    {
        [TestMethod]
        public void TestPropertyDefinitionBehavior()
        {
            PropertyDefinition propDef = new PropertyDefinition(
                "Recipients", 
                typeof(IList<string>));

            Assert.AreEqual(
                "Recipients",
                propDef.Name);

            Assert.IsTrue(propDef.IsList);
            Assert.IsFalse(propDef.IsValueType);
            Assert.IsNull(propDef.DefaultValue);

            Assert.AreEqual(
                typeof(IList<string>),
                propDef.Type);

            Assert.AreEqual(
                typeof(string),
                propDef.GetListUnderlyingType());

            Assert.IsTrue(
                PropertyDefinition.IsGenericList(typeof(List<int>)));

            propDef = new PropertyDefinition(
                "ItemCount",
                typeof(int));

            Assert.IsTrue(propDef.IsValueType);
            Assert.IsFalse(propDef.IsList);

            Assert.AreEqual(
                0, 
                propDef.DefaultValue);

            Assert.AreEqual(
                typeof(int),
                propDef.Type);

            Assert.IsNull(
                propDef.GetListUnderlyingType());

            // Currently hash code is calculated from name of property
            // as it can be only one property with particular name in
            // the class and public constructor is not allowed.
            Assert.AreEqual(
                "ItemCount".GetHashCode(),
                propDef.GetHashCode());
        }
    }
}
