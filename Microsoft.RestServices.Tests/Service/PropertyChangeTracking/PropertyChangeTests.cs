namespace Microsoft.RestServices.Tests.Service.PropertyChangeTracking
{
    using System.Collections.Generic;
    using Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertyChangeTests
    {
        [TestMethod]
        public void TestCollectionProperties()
        {
            Message msg = new Message();

            // collections are always instantiated in background.
            Assert.IsNotNull(msg.ToRecipients);
            Assert.AreEqual(
                0,
                msg.GetChangedProperties().Count);

            msg.ToRecipients = new List<Recipient>();
            Assert.IsTrue(msg.GetChangedProperties().Contains("ToRecipients"));

            msg.ResetChangeTracking();
            Assert.IsFalse(msg.GetChangedProperties().Contains("ToRecipients"));

            msg.ToRecipients.Add(new Recipient());
            Assert.IsTrue(msg.GetChangedProperties().Contains("ToRecipients"));
            Assert.AreEqual(
                msg.GetChangedProperties().Count, 
                1);
        }

        [TestMethod]
        public void TestProperties()
        {
            Message msg = new Message();
            msg.ToRecipients = new List<Recipient>();
            msg.Subject = "Test subject";
            Assert.IsTrue(msg.GetChangedProperties().Contains("ToRecipients"));
            Assert.IsTrue(msg.GetChangedProperties().Contains("Subject"));

            msg.ResetChangeTracking();
            Assert.IsFalse(msg.GetChangedProperties().Contains("ToRecipients"));
            Assert.IsFalse(msg.GetChangedProperties().Contains("Subject"));

            msg.ToRecipients.Add(new Recipient());
            msg.Subject = "Updated subject";
            Assert.IsTrue(msg.GetChangedProperties().Contains("ToRecipients"));
            Assert.AreEqual(
                msg.GetChangedProperties().Count,
                2);

            Assert.AreEqual(
                "Updated subject", 
                msg.Subject);
        }
    }
}
