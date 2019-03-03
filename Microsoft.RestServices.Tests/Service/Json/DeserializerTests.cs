namespace Microsoft.RestServices.Tests.Service.Json
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Exchange;
    using Microsoft.OutlookServices;
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

        [TestMethod]
        public void TestMessageWithAttachmentDeserialization()
        {
            string serializedMessage = @"
                {
                      ""@odata.type"": ""#Microsoft.OutlookServices.message"",
                      ""subject"": ""message with attachs"",
                      ""isRead"": ""true"",
                      ""attachments"": [
                        {
                          ""@odata.type"": ""#Microsoft.OutlookServices.ReferenceAttachment"",
                          ""id"": ""referenceAttachment=="",
                          ""sourceUrl"": ""https://myweb.com""
                        },
                        {
                          ""contentBytes"": ""dGVzdCBjYXNlIHdvcmtz"",
                          ""@odata.type"": ""#Microsoft.OutlookServices.FileAttachment"",
                          ""contentType"": ""ct"",
                          ""size"": 0,
                          ""isInline"": false,
                          ""id"": ""fileAttachment=="",
                          ""name"": ""test.txt""
                        },
                        {
                          ""@odata.type"": ""#Microsoft.OutlookServices.ItemAttachment"",
                          ""id"": ""itemAttachment=="",
                          ""item"":{
                            ""id"": ""attachmentitemId"",
                            ""changeKey"": ""ck=="",
                            ""subject"": ""attachment item subject"",
                            ""isRead"": ""true"",
                            ""@odata.type"": ""#Microsoft.OutlookServices.message""
                          }
                        }
                    ]
                 }";

            Message msg = Deserializer.Instance.Deserialize<Message>(serializedMessage, null);
            Assert.AreEqual(
                3, 
                msg.Attachments.Count);

            foreach (Attachment attachment in msg.Attachments)
            {
                if (attachment is ReferenceAttachment refAttach)
                {
                    Assert.AreEqual(
                        "https://myweb.com",
                        refAttach.SourceUrl);

                    Assert.AreEqual(
                        "referenceAttachment==",
                        refAttach.Id);
                }
                else if (attachment is FileAttachment fileAttach)
                {
                    Assert.AreEqual(
                        "test.txt",
                        fileAttach.Name);

                    byte[] contentBytes = Convert.FromBase64String(fileAttach.ContentBytes);
                    string content = Encoding.UTF8.GetString(contentBytes);
                    Assert.AreEqual(
                        "test case works",
                        content);
                }
                else if (attachment is ItemAttachment itemAttach)
                {
                    Message attachMsg = (Message) itemAttach.Item;
                    Assert.AreEqual(
                        "attachmentitemId",
                        attachMsg.Id);

                    Assert.IsTrue(attachMsg.IsRead);
                    Assert.AreEqual(
                        "ck==",
                        attachMsg.ChangeKey);
                }
                else
                {
                    Assert.Fail("We shouldn't be here...");
                }
            }
        }

        [TestMethod]
        public void TestMessageWithEventAttachmentDeserialization()
        {
            string serializedMessage = @"{
              ""@odata.type"": ""#Microsoft.OutlookServices.Message"",
              ""subject"": ""message with attachs"",
              ""isRead"": ""true"",
              ""attachments"": [
                {
                  ""@odata.type"": ""#Microsoft.OutlookServices.ItemAttachment"",
                  ""id"": ""itemAttachment=="",
                  ""item"":{
                    ""id"": ""attachmentitemId"",
                    ""changeKey"": ""ck=="",
                    ""subject"": ""attachment item subject"",
                    ""@odata.type"": ""#Microsoft.OutlookServices.Event"",
                    ""attendees"": [
                      {
                        ""emailAddress"":{
                          ""address"": ""att1@a.com""
                        }
                      },
                      {
                        ""emailAddress"": {
                          ""address"": ""att2@a.com""
                        }
                      }
                    ],
                    ""body"": {
                      ""Content"": ""this is event"",
                      ""ContentType"": ""Html""
                    },
                    ""start"":{
                      ""dateTime"": ""2019-01-01T12:00:00"",
                      ""TimeZone"": ""Pacific Standard Time""
                    },
                    ""end"": {
                      ""dateTime"": ""2019-01-01T04:00:00"",
                      ""TimeZone"": ""Pacific Standard Time""
                    }
                  }
                }
              ]
            }";

            Message msg = Deserializer.Instance.Deserialize<Message>(
                serializedMessage, 
                null);

            Assert.AreEqual(
                1, 
                msg.Attachments.Count);
            ItemAttachment messageAttach = (ItemAttachment) msg.Attachments[0];
            Event messageAttachEvent = (Event) messageAttach.Item;
            Assert.AreEqual(
                2,
                messageAttachEvent.Attendees.Count);

            Assert.IsNotNull(messageAttachEvent.Start.DateTime);
            Assert.IsNotNull(messageAttachEvent.End.DateTime);
        }
    }
}
