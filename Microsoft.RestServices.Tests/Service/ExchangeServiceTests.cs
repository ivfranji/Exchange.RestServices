namespace Microsoft.RestServices.Tests.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using Mocks;
    using VisualStudio.TestTools.UnitTesting;
    using FolderView = Microsoft.RestServices.Exchange.FolderView;
    using HttpWebRequest = Microsoft.RestServices.Exchange.HttpWebRequest;

    [TestClass]
    public class ExchangeServiceTests
    {
        [TestMethod]
        public void TestExchangeServiceCannotBeCreatedWithoutAuthorization()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                ExchangeService service = new ExchangeService(
                    string.Empty, 
                    "a@b.com");
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                IAuthorizationTokenProvider provider = null;
                ExchangeService service = new ExchangeService(
                    provider, 
                    "a@b.com");
            });
        }

        [TestMethod]
        public void FindItemsWithEmptyResponse()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK, 
                "");

            mock.InlineAssertation = (message) =>
            {
                Uri expectedUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/mailfolders/Inbox/messages?$top=10&$skip=0");

                Assert.IsTrue(message.Method == HttpMethod.Get);
                Assert.AreEqual(expectedUri, message.RequestUri);

            };

            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService service = new ExchangeService(
                "abc", 
                "a@a.com");

            FindItemsResults<OutlookItem> results = service.FindItems(
                WellKnownFolderName.Inbox, 
                new MessageView(10, false));

        }

        [TestMethod]
        public void FindItemsWithEmptyCollection()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "{\"value\": []}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            FindItemsResults<OutlookItem> results = service.FindItems(
                WellKnownFolderName.Inbox,
                new MessageView(10, false));

            Assert.IsTrue(results.Items.Count == 0);

        }

        [TestMethod]
        public void FindItemsTestWithNullFields()
        {
            // ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            string responseToReturn = "{\"value\": [{\"isDeliveryReceiptRequested\": null}]}";
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, responseToReturn);
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            FindItemsResults<OutlookItem> results = service.FindItems(
                WellKnownFolderName.Inbox,
                new MessageView(10, false));

            Assert.IsTrue(results.Items.Count == 1);

        }

        [TestMethod]
        public void FindItemsTest()
        {
            List<Message> messages = new List<Message>();

            for (int i = 0; i < 10; i++)
            {
                Message msg = new Message();
                msg.Subject = $"aa - {i}";
                msg.IsRead = false;

                msg.ResetChangeTracking();
                messages.Add(msg);
            }

            ResponseCollection<Message> responseCollection = new ResponseCollection<Message>();
            responseCollection.ODataContext =
                "https://graph.microsoft.com/beta/$metadata#users(user)/mailfolders('Inbox')/messages";
            responseCollection.Value = messages;
            string serialized = Serializer.Instance.Serialize(responseCollection);

            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, serialized);
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            FindItemsResults<OutlookItem> results = service.FindItems(
                WellKnownFolderName.Inbox,
                new MessageView(10, false));

            Assert.IsTrue(results.Items.Count == 10);

        }

        [TestMethod]
        public void SendMail()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (message) =>
            {
                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/sendMail");
                Assert.AreEqual(requestUri, message.RequestUri);

                Helper.ValidateXAnchorMailbox(message, "a@a.com");
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;


            msg.Send();
        }

        [TestMethod]
        public void ReplyTestMail()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                MockMessageCommentModel messageCommentModel = Deserializer.Instance.Deserialize<MockMessageCommentModel>(httpRequestMessage.Content.ReadAsStringAsync().Result, null);
                Assert.AreEqual(
                    "replyToEmail", 
                    messageCommentModel.Comment);

                // From needs to become To in reply.
                Assert.AreEqual(
                    "from@d.com", 
                    messageCommentModel.Message.ToRecipients[0].EmailAddress.Address);

                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/messages/message-id/reply");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new List<Recipient>(1);
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";
            msg.ResetChangeTracking();

            Dictionary<string, object> parameters =  new Dictionary<string, object>();
            parameters.Add("comment", "replyToEmail");
            service.ReplyEmail(msg, parameters);
        }

        [TestMethod]
        public void ForwardTestMail()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                MockMessageCommentModel messageCommentModel = Deserializer.Instance.Deserialize<MockMessageCommentModel>(httpRequestMessage.Content.ReadAsStringAsync().Result, null);
                Assert.AreEqual(
                    "forwardEmail",
                    messageCommentModel.Comment);

                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/messages/message-id/forward");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Assert.AreEqual(
                    "rec1@a.com",
                    messageCommentModel.Message.ToRecipients[0].EmailAddress.Address);

                Assert.AreEqual(
                    "rec2@a.com",
                    messageCommentModel.Message.ToRecipients[1].EmailAddress.Address);
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new List<Recipient>(1);
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";
            msg.ResetChangeTracking();

            List<Recipient> toRecipients = new List<Recipient>();
            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec1@a.com"
                }
            });

            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec2@a.com"
                }
            });

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("comment", "forwardEmail");
            parameters.Add(nameof(toRecipients), toRecipients);
            service.ForwardEmail(msg, parameters);
        }

        [TestMethod]
        public void ForwardMailThrowArgumentExceptionOnNullRecipients()
        {
            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new List<Recipient>(1);
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";
            msg.ResetChangeTracking();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("comment", "forwardEmail");

            Assert.ThrowsException<ArgumentException>(() =>
            {
                service.ForwardEmail(msg, parameters);
            });
        }

        [TestMethod]
        public void ForwardTestMailFromMessageEntity()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                MockMessageCommentModel messageCommentModel = Deserializer.Instance.Deserialize<MockMessageCommentModel>(httpRequestMessage.Content.ReadAsStringAsync().Result, null);
                Assert.AreEqual(
                    "forwardEmail",
                    messageCommentModel.Comment);

                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/messages/message-id/forward");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Assert.AreEqual(
                    "rec1@a.com",
                    messageCommentModel.Message.ToRecipients[0].EmailAddress.Address);

                Assert.AreEqual(
                    "rec2@a.com",
                    messageCommentModel.Message.ToRecipients[1].EmailAddress.Address);
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new List<Recipient>(1);
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";
            msg.ResetChangeTracking();

            List<Recipient> toRecipients = new List<Recipient>();
            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec1@a.com"
                }
            });

            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec2@a.com"
                }
            });

            msg.Forward("forwardEmail", toRecipients);
        }

        [TestMethod]
        public void MoveMessageTest()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/messages/message-id/move");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Assert.AreEqual(
                    "{\"destinationId\":\"destinationFolder\"}",
                    httpRequestMessage.Content.ReadAsStringAsync().Result);
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new List<Recipient>(1);
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";

            List<Recipient> toRecipients = new List<Recipient>();
            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec1@a.com"
                }
            });

            msg.ResetChangeTracking();
            msg.Move("destinationFolder");
        }

        [TestMethod]
        public void CopyMessageTest()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/messages/message-id/copy");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Assert.AreEqual(
                    "{\"destinationId\":\"destinationFolder\"}",
                    httpRequestMessage.Content.ReadAsStringAsync().Result);
            };

            ExchangeService service = new ExchangeService(
                "abc",
                "a@a.com");

            Message msg = new Message(service);
            msg.Subject = "testsubject";
            msg.Service = service;

            msg.From = new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "from@d.com"
                }
            };

            msg.ToRecipients = new ObservableCollection<Recipient>();
            msg.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "a@a.com"
                }
            });

            msg.Id = "message-id";

            ObservableCollection<Recipient> toRecipients = new ObservableCollection<Recipient>();
            toRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = "rec1@a.com"
                }
            });

            msg.ResetChangeTracking();
            msg.Copy("destinationFolder");
        }

        [TestMethod]
        public void UserAgentTests()
        {
            ExchangeService service = Helper.Service;
            Assert.AreEqual(
                "ExchangeRestClient", 
                service.UserAgent);

            service.UserAgent = null;
            Assert.AreEqual(
                "ExchangeRestClient", 
                service.UserAgent);

            service.UserAgent = "abc";
            Assert.AreEqual(
                "abc-ExchangeRestClient", 
                service.UserAgent);

            service.UserAgent = "";
            Assert.AreEqual(
                "abc-ExchangeRestClient", 
                service.UserAgent);
        }

        [TestMethod]
        public void TraceEnablementTests()
        {
            ExchangeService service = Helper.Service;
            Assert.IsFalse(service.TraceEnabled);
            Assert.IsTrue(service.TraceListener != null);
            Assert.IsTrue(service.TraceListener.GetType() == typeof(DefaultTraceListener));

            service.TraceListener = null;
            Assert.IsNull(service.TraceListener);

            service.TraceEnabled = true;
            Assert.IsTrue(service.TraceEnabled);
            Assert.IsNotNull(service.TraceListener);
        }

        [TestMethod]
        public void MailboxIdChange()
        {
            ExchangeService service = Helper.Service;
            Assert.AreEqual(
                "a@a.com", 
                service.MailboxId.Id);

            service.MailboxId = null;
            Assert.AreEqual(
                "me", 
                service.MailboxId.Id);
        }

        [TestMethod]
        public void TestDeleteItem()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/b@b.com/messages/abcdef");
                Assert.AreEqual(
                    requestUri, 
                    httpRequestMessage.RequestUri);

                Assert.IsTrue(
                    httpRequestMessage.Method == HttpMethod.Delete);

                Helper.ValidateXAnchorMailbox(
                    httpRequestMessage, 
                    "b@b.com");
                
            };

            ExchangeService service = Helper.Service;
            ItemId messageId = new MessageId("abcdef", "b@b.com");

            service.DeleteItem(messageId);
        }

        [TestMethod]
        public void TestDeleteFolder()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                //Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/mailfolders/gggg");
                //Assert.AreEqual(
                //    requestUri,
                //    httpRequestMessage.RequestUri);

                Assert.IsTrue(
                    httpRequestMessage.Method == HttpMethod.Delete);

                Helper.ValidateXAnchorMailbox(
                    httpRequestMessage,
                    "a@a.com");

            };

            ExchangeService service = Helper.Service;
            FolderId folderId = new FolderId("gggg");

            service.DeleteFolder(folderId);
            HttpWebRequestClientProvider.Instance.Reset();
        }

        [TestMethod]
        public void TestFindFolders()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Uri requestUri = new Uri(
                    "https://graph.microsoft.com/beta/users/a@a.com/mailfolders/MyTestFolder/childfolders?$filter=DisplayName%20eq%20'subFolder'&$top=10&$skip=0");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");

            };

            ExchangeService service = Helper.Service;

            service.FindFolders(
                new FolderId("MyTestFolder"),
                new SearchFilter.IsEqualTo(
                    MailFolderObjectSchema.DisplayName, 
                    "subFolder"),
                new FolderView(10));

            HttpWebRequestClientProvider.Instance.Reset();
        }

        [TestMethod]
        public void TestUpdateOutlookItem()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "{}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Assert.AreEqual(
                    HttpWebRequest.PATCH,
                    httpRequestMessage.Method);

                string content = httpRequestMessage.Content.ReadAsStringAsync().Result;
                Event calEvent = Deserializer.Instance.Deserialize<Event>(
                    content, 
                    typeof(Event));

                Assert.IsTrue(calEvent.IsReminderOn);
                Assert.IsNull(calEvent.Subject);
            };

            ExchangeService service = Helper.Service;
            Event calendarEvent = new Event();
            calendarEvent.Service = service;
            calendarEvent.IsReminderOn = false;
            calendarEvent.Subject = "Event subject";
            calendarEvent.Id = "AABB==";
            calendarEvent.ResetChangeTracking();

            Assert.IsNotNull(calendarEvent.ItemId);

            calendarEvent.IsReminderOn = true;
            calendarEvent.Update();


            HttpWebRequestClientProvider.Instance.Reset();
        }

        [TestMethod]
        public void TestUpdateOutlookItemReturnNonNull()
        {
            MockHttpClient mock = new MockHttpClient(
                HttpStatusCode.OK, 
                "{\"id\":\"AACC==\",\"isRead\":\"false\", \"subject\":\"Event subject\"}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Assert.AreEqual(
                    HttpWebRequest.PATCH,
                    httpRequestMessage.Method);

                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/events/AABB==");
                Assert.AreEqual(
                    requestUri, 
                    httpRequestMessage.RequestUri);

                string content = httpRequestMessage.Content.ReadAsStringAsync().Result;
                Event calEvent = Deserializer.Instance.Deserialize<Event>(
                    content,
                    typeof(Event));

                Assert.IsTrue(calEvent.IsReminderOn);
                Assert.IsNull(calEvent.Subject);
            };

            ExchangeService service = Helper.Service;
            Event calendarEvent = new Event();
            calendarEvent.Service = service;
            calendarEvent.IsReminderOn = false;
            calendarEvent.Subject = "Event subject";
            calendarEvent.Id = "AABB==";
            calendarEvent.ResetChangeTracking();

            Assert.IsNotNull(calendarEvent.ItemId);

            calendarEvent.IsReminderOn = true;
            calendarEvent.Update();

            Assert.IsNotNull(calendarEvent);
            Assert.AreEqual(
                "AACC==", 
                calendarEvent.Id);

            Assert.AreEqual(
                "a@a.com", 
                calendarEvent.ItemId.MailboxId.Id);

            HttpWebRequestClientProvider.Instance.Reset();
        }

        [TestMethod]
        public void TestUpdateMailFolder()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "{}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Assert.AreEqual(
                    HttpWebRequest.PATCH, 
                    httpRequestMessage.Method);

                Uri requestUri = new Uri("https://graph.microsoft.com/beta/users/a@a.com/mailfolders/BBCC==");
                Assert.AreEqual(
                    requestUri,
                    httpRequestMessage.RequestUri);

                MailFolder deserializedFolder =
                    Deserializer.Instance.Deserialize<MailFolder>(
                        httpRequestMessage.Content.ReadAsStringAsync().Result,
                        null);

                Assert.AreEqual(
                    deserializedFolder.DisplayName, 
                    "FolderAbc");
            };

            ExchangeService service = Helper.Service;
            MailFolder mailFolder = new MailFolder();
            mailFolder.MailboxId = service.MailboxId;
            mailFolder.Service = service;
            mailFolder.Id = "BBCC==";
            mailFolder.DisplayName = "AbcFolder";
            mailFolder.ResetChangeTracking();

            Assert.IsNotNull(mailFolder.FolderId);
            mailFolder.DisplayName = "FolderAbc";

            mailFolder.Update();

            HttpWebRequestClientProvider.Instance.Reset();
        }

        [TestMethod]
        public void TestServiceIsAppendingUserAgent()
        {
            MockHttpClient mock = new MockHttpClient(HttpStatusCode.OK, "{}");
            HttpWebRequestClientProvider.Instance.RegisterHttpClientProvider(mock.MockClient);

            mock.InlineAssertation = (httpRequestMessage) =>
            {
                Assert.IsNotNull(httpRequestMessage.Headers.UserAgent);
                Assert.AreEqual(
                    1, 
                    httpRequestMessage.Headers.UserAgent.Count);

                foreach (ProductInfoHeaderValue value in httpRequestMessage.Headers.UserAgent)
                {
                    Assert.AreEqual(
                        "Tst-ExchangeRestClient", 
                        value.Product.Name);
                }
            };

            ExchangeService service = Helper.Service;
            service.UserAgent = "Tst";
            service.GetInboxRules();

            HttpWebRequestClientProvider.Instance.Reset();
        }

        internal class MockMessageCommentModel
        {
            public Message Message { get; set; }

            public string Comment { get; set; }
        }
    }
}
