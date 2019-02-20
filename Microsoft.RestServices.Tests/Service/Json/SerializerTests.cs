namespace Microsoft.RestServices.Tests.Service
{
    using System.Collections.Generic;
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void TestSerializer()
        {
            Message msg = new Message(new ExchangeService("j", "a@b.com"));
            msg.Subject = "subj";
            msg.Body = new ItemBody();
            msg.Body.Content = "content";
            msg.Body.ContentType = BodyType.Html;

            Dictionary<string, object> additionalProperties = new Dictionary<string, object>();
            additionalProperties.Add("saveToSentItems", true);

            string s = Serializer.Instance.Serialize(msg, additionalProperties);
        }
    }
}
