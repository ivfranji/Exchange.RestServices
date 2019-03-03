namespace Microsoft.RestServices.Tests.Service
{
    using System;
    using System.Collections.Generic;
    using Functional;
    using Microsoft.RestServices.Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void TestRoundTripSerializerDeserializer()
        {
            Message msg = new Message(new ExchangeService("j", "a@b.com"));
            msg.Subject = "subj";
            msg.Body = new ItemBody();
            msg.Body.Content = "content";
            msg.Body.ContentType = BodyType.HTML;

            Dictionary<string, object> additionalProperties = new Dictionary<string, object>();
            additionalProperties.Add("saveToSentItems", true);

            string s = Serializer.Instance.Serialize(msg, additionalProperties);

            CustomModel customModel = Deserializer.Instance.Deserialize<CustomModel>(
                s, 
                null);

            Assert.IsTrue(customModel.SaveToSentItems);
            Assert.AreEqual(
                "subj", 
                customModel.Message.Subject);

            Assert.AreEqual(
                BodyType.HTML,
                customModel.Message.Body.ContentType);

            Assert.AreEqual(
                "content",
                customModel.Message.Body.Content);
        }

        [TestMethod]
        public void TestInnerPropertySerialization()
        {
            DateTime startTime = TestHelpers.GetFormattedDateTime();
            DateTime endTime = TestHelpers.GetFormattedDateTime(2);
            string timeZone = "Central European Standard Time";

            Message msg = new Message();
            msg.Subject = "Test item attachment";
            ItemAttachment attach = new ItemAttachment()
            {
                Name = "Event Item",
                Item = new Event()
                {
                    Attendees = new List<Attendee>()
                    {
                        new Attendee()
                        {
                            EmailAddress = new EmailAddress() { Address = "attendee1@t.com" }
                        }
                    },
                    Body = new ItemBody()
                    {
                        ContentType = BodyType.HTML,
                        Content = "Lets meet up"
                    },

                    Start = new DateTimeTimeZone()
                    {
                        DateTime = startTime,
                        TimeZone = timeZone
                    },

                    End = new DateTimeTimeZone()
                    {
                        DateTime = endTime,
                        TimeZone = timeZone
                    }
                }
            };

            msg.Attachments.Add(attach);

            string serializedMessage = Serializer.Instance.Serialize(
                msg, 
                null, 
                false);

            MockMessage mockMessage = Deserializer.Instance.Deserialize<MockMessage>(
                serializedMessage, 
                null);

            Assert.IsNull(
                mockMessage.Attachments[0].Item.IsAllDay);

            Assert.IsNull(
                mockMessage.Attachments[0].Item.ShowAs);

            Assert.IsNull(
                mockMessage.Attachments[0].Item.Locations);
        }

        private class CustomModel
        {
            public Message Message { get; set; }

            public bool SaveToSentItems { get; set; }
        }

        private class MockMessage
        {
            public string Subject { get; set; }


            public List<MockAttach> Attachments { get; set; }
        }

        private class MockAttach
        {
            public string Name { get; set; }

            public MockItem Item { get; set; }
        }

        private  class MockItem
        {
            public DateTimeTimeZone Start { get; set; }

            public DateTimeTimeZone End { get; set; }

            public ItemBody Body { get; set; }

            public List<Attendee> Attendees { get; set; }

            public string IsAllDay { get; set; }

            public string ShowAs { get; set; }

            public List<string> Locations { get; set; }
        }
    }
}
