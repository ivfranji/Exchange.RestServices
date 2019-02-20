namespace Microsoft.RestServices.Tests.Service.PropertyChangeTracking
{
    using System;
    using System.Collections.Generic;
    using Exchange;
    using Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertyBagTests
    {
        [TestMethod]
        public void TestPropertyBagBehavior()
        {
            PropertyBag bag = new PropertyBag(typeof(Message));
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
            Assert.ThrowsException<KeyNotFoundException>(() => { object obj = bag["SomeNonExistingKey"]; });
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
            Assert.IsTrue((bool)bag["IsRead"]);
            bag["IsRead"] = null;
            Assert.IsFalse((bool)bag["IsRead"]);
        }
    }
}
