namespace Exchange.RestServices.Tests.MockTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Mocks;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class OutlookMockTestsBase
    {
        /// <summary>
        /// Run test case with custom http 400 status.
        /// </summary>
        protected void RunTestCaseWith400Status(Action<ExchangeService> testCase, Func<string> errorData,
            HttpStatusCode errorStatusCode)
        {
            if (errorStatusCode < (HttpStatusCode) 400 && errorStatusCode > (HttpStatusCode) 499)
            {
                throw new ArgumentException("Test case needs to be run with status code between 400-499");
            }

            MockTestRunner.RunTestCase(
                testCase,
                this.GetExchangeServiceAetAProd(),
                errorStatusCode,
                errorData(),
                null);
        }

        /// <summary>
        /// Run simple HTTP200 test case with empty entityResponse.
        /// </summary>
        /// <param name="testCase"></param>
        /// <param name="inlineAssertation"></param>
        protected void RunHttp200WithEmptyResponseBetaEndpoint(Action<ExchangeService> testCase,
            Action<HttpRequestMessage> inlineAssertation)
        {
            MockTestRunner.RunHttp200TestCase(
                testCase,
                this.GetExchangeServiceAetABeta(),
                "{}",
                inlineAssertation);
        }

        /// <summary>
        /// Run simple HTTP200 test case with empty entityResponse.
        /// </summary>
        /// <param name="testCase"></param>
        /// <param name="inlineAssertation"></param>
        protected void RunHttp200WithEmptyResponseProdEndpoint(Action<ExchangeService> testCase,
            Action<HttpRequestMessage> inlineAssertation)
        {
            MockTestRunner.RunHttp200TestCase(
                testCase,
                this.GetExchangeServiceAetAProd(),
                "{}",
                inlineAssertation);
        }

        /// <summary>
        /// Run simple HTTP200 test case with custom content.
        /// </summary>
        /// <param name="testcase"></param>
        /// <param name="customResponse"></param>
        /// <param name="inlineAssertation"></param>
        protected void RunHttp200WithCustomResponseProdEndpoint(Action<ExchangeService> testcase,
            Func<string> customResponse, Action<HttpRequestMessage> inlineAssertation)
        {
            MockTestRunner.RunHttp200TestCase(
                testcase,
                this.GetExchangeServiceAetAProd(),
                customResponse(),
                inlineAssertation);
        }

        /// <summary>
        /// Run simple test case with Exchange service and mailbox id 'a@a.com'.
        /// </summary>
        /// <param name="testCase">Test case to run.</param>
        protected void RunSimpleExchangeServiceTestCase(Action<ExchangeService> testCase)
        {
            MockTestRunner.RunTestCase(
                testCase,
                this.GetExchangeServiceAetABeta());
        }

        /// <summary>
        /// Get exchange service with identity 'a@a.com'.
        /// </summary>
        /// <returns></returns>
        protected ExchangeService GetExchangeServiceAetABeta()
        {
            return this.GetExchangeServiceBeta("a@a.com");
        }

        /// <summary>
        /// Get exchange service with identity 'a@a.com'.
        /// </summary>
        /// <returns></returns>
        protected ExchangeService GetExchangeServiceAetAProd()
        {
            return this.GetExchangeServiceProd("a@a.com");
        }

        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        private ExchangeService GetExchangeServiceBeta(string mailboxId)
        {
            return new ExchangeService(
                "<bearer>",
                mailboxId,
                RestEnvironment.OutlookBeta);
        }

        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        private ExchangeService GetExchangeServiceProd(string mailboxId)
        {
            return new ExchangeService(
                "<bearer>",
                mailboxId,
                RestEnvironment.OutlookProd);
        }
    }

    /// <summary>
    /// Outlook mock tests.
    /// </summary>
    [TestClass]
    public class OutlookMockTests : OutlookMockTestsBase
    {
        #region UserAgent tests

        [TestMethod]
        public void Test_UserAgentBehavior()
        {
            this.RunSimpleExchangeServiceTestCase((exchangeService) =>
            {
                Assert.AreEqual(
                    "ExchangeRestClient",
                    exchangeService.UserAgent);

                exchangeService.UserAgent = null;
                Assert.AreEqual(
                    "ExchangeRestClient",
                    exchangeService.UserAgent);

                exchangeService.UserAgent = "abc";
                Assert.AreEqual(
                    "abc-ExchangeRestClient",
                    exchangeService.UserAgent);

                exchangeService.UserAgent = "";
                Assert.AreEqual(
                    "abc-ExchangeRestClient",
                    exchangeService.UserAgent);
            });
        }

        [TestMethod]
        public void Test_UserAgentSentOnRequests()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    exchangeService.UserAgent = "Tst";
                    exchangeService.GetInboxRules();
                },
                (httpRequestMessage) =>
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
                });
        }

        #endregion

        #region Tasks tests

        /// <summary>
        /// Find task item in well known folder name - Tasks.
        /// </summary>
        [TestMethod]
        public void Test_FindTaskItemsWithWellKnownFolderName()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) => { exchangeService.FindItems(WellKnownFolderName.Tasks, new TaskView(10)); },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        "https://outlook.office365.com/api/beta/users/a@a.com/tasks?$top=10&$skip=0",
                        httpRequestMessage.RequestUri.ToString());
                });
        }

        /// <summary>
        /// Find item in custom Tasks folder id.
        /// </summary>
        [TestMethod]
        public void Test_FindTaskItemsWithCustomFolderId()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    exchangeService.FindItems(new TaskFolderId("tasks==", "me"), new TaskView(10));
                },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        "https://outlook.office365.com/api/beta/users/a@a.com/taskfolders/tasks==/tasks?$top=10&$skip=0",
                        httpRequestMessage.RequestUri.ToString());
                });
        }

        #endregion

        #region Contacts tests

        [TestMethod]
        public void Test_FindContactItemsWithWellknownFolderName()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) => { exchangeService.FindItems(WellKnownFolderName.Contacts, new ContactView(10)); },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        "https://outlook.office365.com/api/beta/users/a@a.com/contacts?$top=10&$skip=0",
                        httpRequestMessage.RequestUri.ToString());
                });
        }

        [TestMethod]
        public void Test_FindContactItemsWithCustomFolderId()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    exchangeService.FindItems(new ContactFolderId("id==", "me"), new ContactView(10));
                },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        "https://outlook.office365.com/api/beta/users/a@a.com/contactfolders/id==/contacts?$top=10&$skip=0",
                        httpRequestMessage.RequestUri.ToString());
                });
        }

        #endregion

        #region MailFolder tests

        [TestMethod]
        public void Test_UpdateMailFolder()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    // not new folder is created, instead we are mocking updating 'existing' folder.
                    MailFolder mailFolder = new MailFolder();
                    mailFolder.MailboxId = exchangeService.MailboxId;
                    mailFolder.Service = exchangeService;
                    mailFolder.Id = "BBCC==";
                    mailFolder.DisplayName = "AbcFolder";
                    mailFolder.ResetChangeTracking();

                    Assert.IsNotNull(mailFolder.FolderId);
                    mailFolder.DisplayName = "FolderAbc";

                    mailFolder.Update();
                },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        Exchange.RestServices.HttpWebRequest.PATCH,
                        httpRequestMessage.Method);

                    Uri requestUri = new Uri("https://outlook.office365.com/api/beta/users/a@a.com/mailfolders/BBCC==");
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
                });
        }

        [TestMethod]
        public void Test_FindMailFolders()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    exchangeService.FindFolders(
                        new FolderId("MyTestFolder"),
                        new SearchFilter.IsEqualTo(
                            MailFolderObjectSchema.DisplayName,
                            "subFolder"),
                        new FolderView(10));
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri = new Uri(
                        "https://outlook.office365.com/api/beta/users/a@a.com/mailfolders/MyTestFolder/childfolders?$filter=DisplayName%20eq%20'subFolder'&$top=10&$skip=0");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
                });
        }

        [TestMethod]
        public void Test_DeleteMailFolders()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    FolderId folderId = new FolderId("gggg");
                    exchangeService.DeleteFolder(folderId);
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri = new Uri("https://outlook.office365.com/api/beta/users/a@a.com/mailfolders/gggg");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Assert.IsTrue(
                        httpRequestMessage.Method == HttpMethod.Delete);

                    Helper.ValidateXAnchorMailbox(
                        httpRequestMessage,
                        "a@a.com");
                });
        }

        #endregion

        #region Message tests

        [TestMethod]
        public void Test_ForwardMailFromMessageInstance()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    Message msg = new Message(exchangeService);
                    msg.Subject = "testsubject";
                    msg.Service = exchangeService;

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

                    msg.Forward(toRecipients, "forwardEmail");
                },
                (httpRequestMessage) =>
                {
                    Dictionary<string, object> forward =
                        Deserializer.Instance.Deserialize<Dictionary<string, object>>(
                            httpRequestMessage.Content.ReadAsStringAsync().Result, null);
                    Assert.AreEqual(
                        "forwardEmail",
                        forward["Comment"]);

                    Uri requestUri =
                        new Uri("https://outlook.office365.com/api/v2.0/users/a@a.com/messages/message-id/forward");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    List<Recipient> recipients =
                        Deserializer.Instance.Deserialize<List<Recipient>>(forward["ToRecipients"].ToString(), null);

                    Assert.AreEqual(
                        "rec1@a.com",
                        recipients[0].EmailAddress.Address);

                    Assert.AreEqual(
                        "rec2@a.com",
                        recipients[1].EmailAddress.Address);
                });
        }

        [TestMethod]
        public void Test_ForwardMailFromMessageInstanceThrowsOnNullRecipient()
        {
            ExchangeService exchangeService = this.GetExchangeServiceAetABeta();

            Message msg = new Message(exchangeService);
            msg.Subject = "testsubject";
            msg.Service = exchangeService;

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

            Assert.ThrowsException<ArgumentException>(() => { exchangeService.ForwardEmail(msg, parameters); });
        }

        [TestMethod]
        public void Test_ReplyMailFromMessageInstance()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    Message msg = new Message(exchangeService);
                    msg.Subject = "testsubject";
                    msg.Service = exchangeService;

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
                    parameters.Add("comment", "replyToEmail");
                    exchangeService.ReplyEmail(msg, parameters);
                },
                (httpRequestMessage) =>
                {
                    Dictionary<string, object> reply =
                        Deserializer.Instance.Deserialize<Dictionary<string, object>>(
                            httpRequestMessage.Content.ReadAsStringAsync().Result, null);

                    Assert.AreEqual(
                        "replyToEmail",
                        reply["Comment"]);

                    Uri requestUri =
                        new Uri("https://outlook.office365.com/api/v2.0/users/a@a.com/messages/message-id/reply");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
                });
        }

        [TestMethod]
        public void Test_SendMailFromMessageInstance()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    Message msg = new Message(exchangeService);
                    msg.Subject = "testsubject";
                    msg.Service = exchangeService;
                    msg.Send();
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri = new Uri("https://outlook.office365.com/api/beta/users/a@a.com/sendMail");
                    Assert.AreEqual(requestUri, httpRequestMessage.RequestUri);
                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
                });
        }

        [TestMethod]
        public void Test_MoveMailFromMessageInstance()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    Message msg = new Message(exchangeService);
                    msg.Subject = "testsubject";
                    msg.Service = exchangeService;

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
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri =
                        new Uri("https://outlook.office365.com/api/beta/users/a@a.com/messages/message-id/move");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Assert.AreEqual(
                        "{\"DestinationId\":\"destinationFolder\"}",
                        httpRequestMessage.Content.ReadAsStringAsync().Result);
                });
        }

        [TestMethod]
        public void Test_CopyMailFromMessageInstance()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    Message msg = new Message(exchangeService);
                    msg.Subject = "testsubject";
                    msg.Service = exchangeService;

                    msg.From = new Recipient()
                    {
                        EmailAddress = new EmailAddress()
                        {
                            Address = "from@d.com"
                        }
                    };

                    msg.Id = "message-id";

                    msg.ResetChangeTracking();
                    msg.Copy("destinationFolder");
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri =
                        new Uri("https://outlook.office365.com/api/beta/users/a@a.com/messages/message-id/copy");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Assert.AreEqual(
                        "{\"DestinationId\":\"destinationFolder\"}",
                        httpRequestMessage.Content.ReadAsStringAsync().Result);
                });
        }

        [TestMethod]
        public void Test_DeleteMessageFromServiceInstance()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    ItemId messageId = new MessageId("abcdef", "a@a.com");
                    exchangeService.DeleteItem(messageId);
                },
                (httpRequestMessage) =>
                {
                    Uri requestUri = new Uri("https://outlook.office365.com/api/beta/users/a@a.com/messages/abcdef");
                    Assert.AreEqual(
                        requestUri,
                        httpRequestMessage.RequestUri);

                    Assert.IsTrue(
                        httpRequestMessage.Method == HttpMethod.Delete);

                    Helper.ValidateXAnchorMailbox(
                        httpRequestMessage,
                        "a@a.com");
                });
        }

        [TestMethod]
        public void Test_FindMessageFromServiceInstance_EmptyResponse()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    FindItemsResults<Item> results = exchangeService.FindItems(
                        WellKnownFolderName.Inbox,
                        new MessageView(10, false));

                    Assert.IsFalse(results.MoreAvailable);
                    Assert.AreEqual(0, results.TotalCount);
                },
                (httpRequestMessage) =>
                {
                    Uri expectedUri =
                        new Uri(
                            "https://outlook.office365.com/api/v2.0/users/a@a.com/mailfolders/Inbox/messages?$top=10&$skip=0");

                    Assert.IsTrue(httpRequestMessage.Method == HttpMethod.Get);
                    Assert.AreEqual(expectedUri, httpRequestMessage.RequestUri);
                });
        }

        [TestMethod]
        public void Test_FindMessageFromServiceInstance_EmptyArray()
        {
            this.RunHttp200WithCustomResponseProdEndpoint(
                (exchangeService) =>
                {
                    FindItemsResults<Item> results = exchangeService.FindItems(
                        WellKnownFolderName.Inbox,
                        new MessageView(10, false));

                    Assert.IsTrue(results.Items.Count == 0);
                },
                () => { return "{\"value\": []}"; },
                (httpRequestMessage) =>
                {
                    Uri expectedUri =
                        new Uri(
                            "https://outlook.office365.com/api/v2.0/users/a@a.com/mailfolders/Inbox/messages?$top=10&$skip=0");

                    Assert.IsTrue(httpRequestMessage.Method == HttpMethod.Get);
                    Assert.AreEqual(expectedUri, httpRequestMessage.RequestUri);
                });
        }

        [TestMethod]
        public void Test_FindMessageFromServiceInstance_NullFields()
        {
            this.RunHttp200WithCustomResponseProdEndpoint(
                (exchangeService) =>
                {
                    FindItemsResults<Item> results = exchangeService.FindItems(
                        WellKnownFolderName.Inbox,
                        new MessageView(10, false));

                    Assert.IsTrue(results.Items.Count == 1);
                },
                () => { return "{\"value\": [{\"isDeliveryReceiptRequested\": null}]}"; },
                (httpRequestMessage) => { });
        }

        [TestMethod]
        public void Test_FindMessageFromServiceInstance_RoundTrip()
        {
            this.RunHttp200WithCustomResponseProdEndpoint(
                (exchangeService) =>
                {
                    FindItemsResults<Item> results = exchangeService.FindItems(
                        WellKnownFolderName.Inbox,
                        new MessageView(10, false));

                    Assert.IsTrue(results.Items.Count == 10);
                },
                () =>
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

                    EntityResponseCollection<Message> entityResponseCollection = new EntityResponseCollection<Message>();
                    entityResponseCollection.ODataContext =
                        "https://outlook.office365.com/api/v2.0/$metadata#users(user)/mailfolders('Inbox')/messages";
                    entityResponseCollection.Value = messages;
                    return Serializer.Instance.Serialize(entityResponseCollection);
                },
                (httpRequestMessage) => { });
        }

        [TestMethod]
        public void Test_GetItemInDelegateMailboxCorrectEndpoint()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    ExchangeService meService = new ExchangeService(
                        "abc",
                        "a@a.com",
                        RestEnvironment.OutlookProd);

                    Assert.IsTrue(meService.MailboxId.IdInEmailAddressForm);

                    Message msg = new Message();
                    msg.MailboxId = new MailboxId("b@b.com");
                    msg.Id = "id-123";
                    msg.ResetChangeTracking();

                    exchangeService.GetItem(msg.ItemId);
                },
                (httpRequestMessage) =>
                {
                    Uri uri = new Uri("https://outlook.office365.com/api/v2.0/users/b@b.com/messages/id-123");
                    Assert.AreEqual(
                        uri,
                        httpRequestMessage.RequestUri);

                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "b@b.com");
                });
        }

        [TestMethod]
        public void Test_GetItemInMailboxCorrectEndpoint()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    ExchangeService meService = new ExchangeService(
                        "abc",
                        "a@a.com",
                        RestEnvironment.OutlookProd);

                    Assert.IsTrue(meService.MailboxId.IdInEmailAddressForm);

                    Message msg = new Message();
                    msg.MailboxId = new MailboxId("a@a.com");
                    msg.Id = "id-123";
                    msg.ResetChangeTracking();

                    exchangeService.GetItem(msg.ItemId);
                },
                (httpRequestMessage) =>
                {
                    Uri uri = new Uri("https://outlook.office365.com/api/v2.0/users/a@a.com/messages/id-123");
                    Assert.AreEqual(
                        uri,
                        httpRequestMessage.RequestUri);

                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
                });
        }

        [TestMethod]
        public void Test_GetItemMeMailboxReplacedBySessionWideEndpoint()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    ExchangeService meService = new ExchangeService(
                        "abc",
                        "a@a.com",
                        RestEnvironment.OutlookProd);

                    Assert.IsTrue(meService.MailboxId.IdInEmailAddressForm);

                    Message msg = new Message();
                    msg.MailboxId = new MailboxId("me");
                    msg.Id = "id-123";
                    msg.ResetChangeTracking();

                    exchangeService.GetItem(msg.ItemId);
                },
                (httpRequestMessage) =>
                {
                    Uri uri = new Uri("https://outlook.office365.com/api/v2.0/users/a@a.com/messages/id-123");
                    Assert.AreEqual(
                        uri,
                        httpRequestMessage.RequestUri);

                    Helper.ValidateXAnchorMailbox(httpRequestMessage, "a@a.com");
                });
        }

        [TestMethod]
        public void Test_GetItemMeMailboxEndpoint()
        {
            this.RunHttp200WithEmptyResponseProdEndpoint(
                (exchangeService) =>
                {
                    exchangeService = new ExchangeService(
                        "abc",
                        string.Empty,
                        RestEnvironment.OutlookProd);

                    Assert.IsTrue(exchangeService.MailboxId.IdInMeForm);

                    exchangeService.GetItem(new MessageId(
                        "id-123",
                        "me"));
                },
                (httpRequestMessage) =>
                {
                    Uri uri = new Uri("https://outlook.office365.com/api/v2.0/me/messages/id-123");
                    Assert.AreEqual(
                        uri,
                        httpRequestMessage.RequestUri);

                    // For /me request we don't provide X-AnchorMailbox
                    // TODO: but we should...
                    Assert.IsFalse(
                        httpRequestMessage.Headers.Contains("X-AnchorMailbox"));
                });
        }

        #endregion

        #region ExchangeService tests

        [TestMethod]
        public void Test_ExchangeServiceThrowsOnEmptyTokenAndNullTokenProvider()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                ExchangeService exchangeService = new ExchangeService(
                    string.Empty,
                    "a@b.com");
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                IAuthorizationTokenProvider provider = null;
                ExchangeService exchangeService = new ExchangeService(
                    provider,
                    "a@b.com");
            });
        }

        [TestMethod]
        public void Test_ExchangeServiceMailboxIdChange()
        {
            ExchangeService exchangeService = this.GetExchangeServiceAetABeta();
            Assert.AreEqual(
                "a@a.com",
                exchangeService.MailboxId.Id);

            exchangeService.MailboxId = null;
            Assert.AreEqual(
                "me",
                exchangeService.MailboxId.Id);

            exchangeService.MailboxId = new MailboxId("b@b.com");
            Assert.AreEqual(
                "b@b.com",
                exchangeService.MailboxId.Id);
        }

        #endregion

        #region Tracing tests

        [TestMethod]
        public void Test_EnablingTrace()
        {
            ExchangeService exchangeService = this.GetExchangeServiceAetAProd();

            Assert.IsFalse(exchangeService.TraceEnabled);
            Assert.IsTrue(
                exchangeService.TraceListener != null);

            Assert.IsTrue(exchangeService.TraceListener.GetType() == typeof(DefaultTraceListener));

            exchangeService.TraceListener = null;
            Assert.IsNull(exchangeService.TraceListener);

            exchangeService.TraceEnabled = true;
            Assert.IsTrue(exchangeService.TraceEnabled);
            Assert.IsNotNull(exchangeService.TraceListener);

            exchangeService.TraceListener = null;
            Assert.IsFalse(exchangeService.TraceEnabled);
        }

        #endregion

        #region Event tests

        [TestMethod]
        public void Test_UpdateEventFromEventInstance()
        {
            this.RunHttp200WithEmptyResponseBetaEndpoint(
                (exchangeService) =>
                {
                    Event calendarEvent = new Event();
                    calendarEvent.Service = exchangeService;
                    calendarEvent.IsReminderOn = false;
                    calendarEvent.Subject = "Event subject";
                    calendarEvent.Id = "AABB==";
                    calendarEvent.ResetChangeTracking();

                    Assert.IsNotNull(calendarEvent.ItemId);

                    calendarEvent.IsReminderOn = true;
                    calendarEvent.Update();
                },
                (httpRequestMessage) =>
                {
                    Assert.AreEqual(
                        Exchange.RestServices.HttpWebRequest.PATCH,
                        httpRequestMessage.Method);

                    string content = httpRequestMessage.Content.ReadAsStringAsync().Result;
                    Event calEvent = Deserializer.Instance.Deserialize<Event>(
                        content,
                        typeof(Event));

                    Assert.IsTrue(calEvent.IsReminderOn);
                    Assert.IsNull(calEvent.Subject);
                });
        }

        #endregion

        #region Error handling test cases

        [TestMethod]
        public void Test_Handle401Error()
        {
            this.RunTestCaseWith400Status(
                (exchangeService) =>
                {
                    RestResponseException ex = null;
                    try
                    {
                        exchangeService.FindItems(WellKnownFolderName.Inbox, new MessageView(10));
                    }
                    catch (RestResponseException e)
                    {
                        ex = e;
                    }

                    Assert.IsNotNull(ex);
                    Assert.AreEqual(
                        (HttpStatusCode) 401,
                        ex.HttpStatusCode);
                },
                () => { return ""; },
                (HttpStatusCode) 401);
        }

        #endregion
    }
}