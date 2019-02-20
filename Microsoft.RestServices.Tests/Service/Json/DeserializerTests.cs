namespace Microsoft.RestServices.Tests.Service.Json
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exchange;
    using Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeserializerTests
    {
        [TestMethod]
        public void TestIListDeserialization()
        {
            string toRecipients = @"
                {
                  ""toRecipients"" : [
                    {
                      ""emailAddress"": {
                        ""address"": ""a@a.com"",
                        ""name"": ""A A""
                      }
                    },
                    {
                      ""emailAddress"": {
                        ""address"": ""b@b.com"",
                        ""name"": ""B B""
                      }
                    }
                  ]
                }";

            Message msg = Deserializer.Instance.Deserialize<Message>(
                toRecipients,
                null);

            msg.ResetChangeTracking();

            Assert.IsNotNull(msg.ToRecipients);
            Assert.IsInstanceOfType(msg.ToRecipients, typeof(ObservableCollection<Recipient>));
            Assert.IsTrue(msg.ToRecipients is IList<Recipient>);
        }
    }
}
