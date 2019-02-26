namespace Microsoft.RestServices.Tests.Service.PropertyChangeTracking
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Exchange;
    using Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertyBagTests
    {
        [TestMethod]
        public void TestPropertyBagBehavior()
        {
            PropertyBag bag = new PropertyBag(new MessageObjectSchema());
            Assert.IsFalse(bag.IsNew);
            Assert.IsNotNull(bag["ToRecipients"]);
            Assert.IsNotNull(bag["CcRecipients"]);
            bag.MarkAsNew();
            Assert.IsTrue(bag.IsNew);

            bag["Subject"] = "Test";
            Assert.IsTrue(bag.GetChangedProperties().Count == 1);
            Assert.AreEqual("Test", bag["Subject"]);

            bag.Clear();
            Assert.IsNotNull(bag["ToRecipients"]);
            Assert.IsNotNull(bag["CcRecipients"]);
            Assert.IsNull(bag["Subject"]);

            Assert.ThrowsException<KeyNotFoundException>(() => { bag["SomeNonExistingKey"] = "value"; });
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                object obj = bag["SomeNonExistingKey"];
            });
            Assert.ThrowsException<InvalidOperationException>(() => { bag["Subject"] = 546; });

            bag["Body"] = new ItemBody()
            {
                Content = "a"
            };

            Assert.IsTrue(bag.IsPropertyChanged("Body"));

            bag["Body"] = null;
            Assert.IsNull(bag["Body"]);

            // Value types are set to default values upon
            // assigning null.
            bag["IsRead"] = true;
            Assert.IsTrue((bool) bag["IsRead"]);
            bag["IsRead"] = null;
            Assert.IsFalse((bool) bag["IsRead"]);

            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                // Message doesn't have that property and should throw
                // key not found exception.
                object totalItemCount = bag["TotalItemCount"];
            });

            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                // Message doesn't have that property and should throw
                // key not found exception.
                object totalItemCount = bag[MailFolderObjectSchema.TotalItemCount];
            });

            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                // Message doesn't have that property and should throw
                // key not found exception.
                bag[MailFolderObjectSchema.TotalItemCount] = 8;
            });

            Assert.ThrowsException<ArgumentNullException>(() => { bag = new PropertyBag(null); });
        }

        [TestMethod]
        public void TestPropertyBagCollectionObjects()
        {
            PropertyBag bag = new PropertyBag(new MessageObjectSchema());

            IList<string> props = bag.GetChangedProperties();
            Assert.AreEqual(
                0,
                props.Count);

            ((IList<SingleValueLegacyExtendedProperty>)
                (bag[MessageObjectSchema.SingleValueExtendedProperties])).Add(new SingleValueLegacyExtendedProperty()
            {
                Id = "A",
                Value = "Abc"
            });

            props = bag.GetChangedProperties();
            Assert.AreEqual(
                1,
                props.Count);
        }

        [TestMethod]
        public void TestPropertyBagDefaultBehavior()
        {
            PropertyBag bag = new PropertyBag(new MessageObjectSchema());
            IList<string> props = bag.GetChangedProperties();
            Assert.AreEqual(
                0,
                props.Count);

            foreach ( FieldInfo fieldInfo in typeof(MessageObjectSchema).GetFields(
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.FlattenHierarchy) )
            {
                PropertyDefinition propertyDefinition = fieldInfo.GetValue(null) as PropertyDefinition;
                if ( null != propertyDefinition )
                {
                    if ( !propertyDefinition.IsList )
                    {
                        Assert.AreEqual(
                            propertyDefinition.DefaultValue,
                            bag[propertyDefinition]);
                    }
                    else
                    {
                        Assert.IsNotNull(bag[propertyDefinition]);
                        Assert.IsTrue(
                            PropertyDefinition.IsGenericList(bag[propertyDefinition].GetType()));
                    }
                }
                else
                {
                    throw new ArgumentNullException("Schema shouldn't contain anything but static prop definition.");
                }
            }
        }
    }
}