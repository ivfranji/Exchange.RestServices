namespace Microsoft.RestServices.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using Microsoft.RestServices.Exchange;
    using Microsoft.Graph;
    using VisualStudio.TestTools.UnitTesting;
    using FolderView = Microsoft.RestServices.Exchange.FolderView;

    [TestClass]
    public class FunctionalTestsBase
    {
        /// <summary>
        /// Extended property constant.
        /// </summary>
        protected const string extendedPropertyGuid = "4d557659-9e3f-405e-8f6d-86d2d9d5c630";

        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        protected ExchangeService GetExchangeService(string mailboxId)
        {
            return new ExchangeService(
                new TestAuthenticationProvider(), 
                mailboxId);
        }
    }

    [TestClass]
    public class MailFolderFunctionalTests : FunctionalTestsBase
    {
        [TestMethod]
        public void GetMailFolders()
        {
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
            FindFoldersResults findFoldersResults = null;
            FolderView folderView = new FolderView(10);

            FolderId sharedFolderId = new FolderId(WellKnownFolderName.MsgFolderRoot);

            do
            {
                findFoldersResults = exchangeService.FindFolders(sharedFolderId, folderView);
                folderView.Offset += folderView.PageSize;

                foreach (MailFolder folder in findFoldersResults)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxA, 
                        folder.FolderId.MailboxId.Id);
                }

            } while (findFoldersResults.MoreAvailable);

            HttpWebRequestClientProvider.Instance.ExitLock();
        }

        [TestMethod]
        public void CRUDMailFolder()
        {
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

            foreach (MailFolder folder in exchangeService.FindFolders(WellKnownFolderName.Inbox, new FolderView(10)))
            {
                folder.Delete();
            }

            MailFolder folder1 = new MailFolder(exchangeService)
            {
                DisplayName = "MyTestFolder1"
            };

            Assert.IsNull(folder1.Id);
            folder1.Save(WellKnownFolderName.Inbox);
            Assert.IsNotNull(folder1.Id);

            MailFolder folder2 = new MailFolder(exchangeService);
            folder2.DisplayName = "MyTestFolder2";

            Assert.IsNull(folder2.Id);
            folder2.Save(WellKnownFolderName.Inbox);
            Assert.IsNotNull(folder2.Id);

            Thread.Sleep(5000);

            folder2 = folder2.Move(folder1.Id);

            folder1.DisplayName = "NewDisplayName";
            folder1.Update();

            Assert.AreEqual(
                "NewDisplayName", 
                folder1.DisplayName);

            Assert.AreEqual(
                folder1.Id, 
                folder2.ParentFolderId);

            folder2.Delete();
            Assert.IsNull(folder2.DisplayName);
            Assert.IsNull(folder2.Id);

            folder1.Delete();
            Assert.IsNull(folder1.DisplayName);
            Assert.IsNull(folder1.Id);

            HttpWebRequestClientProvider.Instance.ExitLock();
        }

        [TestMethod]
        public void SyncMailFoldersTest()
        {
            string folder1Name = "TempSyncFolder1";
            string folder2Name = "TempSyncFolder2";

            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

            FindFoldersResults findFolders = exchangeService.FindFolders(
                WellKnownFolderName.MsgFolderRoot,
                new FolderView(30));

            foreach (MailFolder mailFolder in findFolders)
            {
                if (mailFolder.DisplayName == folder1Name || 
                    mailFolder.DisplayName == folder2Name)
                {
                    mailFolder.Delete();
                }
            }

            string syncState = null;
            int counter = 0;
            SyncFolderItemsCollection<MailFolder> sync;
            do
            {
                sync = exchangeService.SyncFolderHierarchy(syncState);
                syncState = sync.SyncState;

                counter++;

            } while (sync.MoreAvailable || counter == 4);

            Assert.IsFalse(sync.MoreAvailable);

            MailFolder folder1 = new MailFolder(exchangeService);
            folder1.DisplayName = folder1Name;
            folder1.Save(WellKnownFolderName.MsgFolderRoot);

            MailFolder folder2 = new MailFolder(exchangeService);
            folder2.DisplayName = folder2Name;
            folder2.Save(WellKnownFolderName.MsgFolderRoot);

            sync = exchangeService.SyncFolderHierarchy(syncState);
            syncState = sync.SyncState;

            Assert.AreEqual(
                2, 
                sync.TotalCount);

            foreach (ItemChange<MailFolder> change in sync)
            {
                Assert.IsTrue(change.ChangeType == SyncChangeType.Created);
            }

            folder1.Delete();
            folder2.Delete();

            sync = exchangeService.SyncFolderHierarchy(syncState);

            Assert.IsTrue(sync.TotalCount == 2);
            foreach (ItemChange<MailFolder> change in sync)
            {
                Assert.IsTrue(change.ChangeType == SyncChangeType.Deleted);
            }

            HttpWebRequestClientProvider.Instance.ExitLock();
        }
    }

    [TestClass]
    public class MailMessageFunctionalTests : FunctionalTestsBase
    {
        [TestMethod]
        public void GetMailMessageWithSingleExtendedProperties()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxB);
            
            MessageView messageView = new MessageView(1);
            messageView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String, 
                0x0037));

            messageView.PropertySet.Add("HasAttachments");

            {
                FindItemsResults<OutlookItem> findItemResults = exchangeService.FindItems(
                    WellKnownFolderName.Inbox, 
                    messageView);
                foreach (OutlookItem item in findItemResults)
                {
                    Message msg = (Message) item;
                    Assert.AreEqual(
                        1,
                        msg.SingleValueExtendedProperties.Count);
                }

                messageView.Offset += messageView.PageSize;
                messageView.PropertySet.Add(
                    new ExtendedPropertyDefinition(MapiPropertyType.Boolean, 
                        0x0E1F));

                findItemResults = exchangeService.FindItems(
                    WellKnownFolderName.Inbox,
                    messageView);
                foreach (OutlookItem item in findItemResults)
                {
                    Message msg = (Message)item;
                    Assert.AreEqual(
                        2,
                        msg.SingleValueExtendedProperties.Count);
                }
            }
        }

        [TestMethod]
        public void CRUDExtendedProperties()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
            string subject = Guid.NewGuid().ToString();

            Message msg = new Message(exchangeService);
            msg.Subject = subject;
            msg.SingleValueExtendedProperties.Add(new SingleValueLegacyExtendedProperty()
            {
                Id = $"String {FunctionalTestsBase.extendedPropertyGuid} Name Blah",
                Value = "BlahValue"
            });

            msg.MultiValueExtendedProperties.Add(new MultiValueLegacyExtendedProperty()
            {
                Id = $"StringArray {FunctionalTestsBase.extendedPropertyGuid} Name BlahArray",
                Value = new List<string>()
                {
                    "A",
                    "B",
                    "C"
                }
            });

            msg.Save(WellKnownFolderName.Inbox);

            MessageView msgView = new MessageView(1);
            msgView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String, 
                "Blah", 
                new Guid(FunctionalTestsBase.extendedPropertyGuid)));

            msgView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.StringArray,
                "BlahArray",
                new Guid(FunctionalTestsBase.extendedPropertyGuid)));

            SearchFilter filter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject, 
                subject);

            FindItemsResults<OutlookItem> findItemsResults = exchangeService.FindItems(
                WellKnownFolderName.Inbox, 
                filter, 
                msgView);

            foreach (OutlookItem item in findItemsResults)
            {
                msg = (Message) item;
                Assert.AreEqual(
                    1, 
                    msg.SingleValueExtendedProperties.Count);

                Assert.AreEqual(
                    1,
                    msg.MultiValueExtendedProperties.Count);

                msg.Delete();
            }
        }

        [TestMethod]
        public void SendMessageFromMailboxAToMailboxB()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
            string messageSubject = Guid.NewGuid().ToString();

            Message message = new Message(exchangeService);
            message.Subject = messageSubject;

            message.ToRecipients = new List<Recipient>();
            message.ToRecipients.Add(new Recipient()
            {
                EmailAddress = new EmailAddress()
                {
                    Address = AppConfig.MailboxB
                }
            });

            message.Body = new ItemBody()
            {
                Content = "Test message",
                ContentType = BodyType.Html
            };

            message.Send();

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxB);

            Thread.Sleep(5000); // allow some time for email to be delivered
            MessageView messageView = new MessageView(10);
            FolderId inbox = new FolderId(WellKnownFolderName.Inbox);
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject, 
                messageSubject);

            FindItemsResults<OutlookItem> messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.AreEqual(1, messages.TotalCount);
            Message msg = (Message) messages.Items[0];
            msg.Reply("this is my reply");

            Thread.Sleep(5000); // allow some time for email to be delivered

            subjectFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject, 
                $"Re: {messageSubject}");
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.IsTrue(messages.TotalCount == 1);

            messages.Items[0].Delete();
        }

        [TestMethod]
        public void SyncMessagesTest()
        {
            string folderName = "TempSyncFolder";
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

            FindFoldersResults findFolders = exchangeService.FindFolders(
                WellKnownFolderName.MsgFolderRoot,
                new FolderView(30));

            foreach (MailFolder mailFolder in findFolders)
            {
                if (mailFolder.DisplayName == folderName)
                {
                    mailFolder.Delete();
                }
            }

            MailFolder folder = new MailFolder(exchangeService);
            folder.DisplayName = folderName;
            folder.Save(WellKnownFolderName.MsgFolderRoot);

            for (int i = 0; i < 10; i++)
            {
                this.CreateMessage(
                    i, 
                    folder.FolderId, 
                    exchangeService);
            }

            string syncState = null;
            MessagePropertySet propertySet = new MessagePropertySet();
            propertySet.AddProperty("ToRecipients");
            SyncFolderItemsCollection<OutlookItem> syncCollection;
            int counter = 0;
            int numberOfMessages = 0;
            do
            {
                syncCollection = exchangeService.SyncFolderItems(
                    folder.FolderId, 
                    propertySet, 
                    4, 
                    syncState);

                syncState = syncCollection.SyncState;
                numberOfMessages += syncCollection.TotalCount;
                counter++;

                foreach (ItemChange<OutlookItem> itemChange in syncCollection)
                {
                    Assert.IsTrue(
                        itemChange.ChangeType == SyncChangeType.Created);
                }

            } while (syncCollection.MoreAvailable || counter == 4);

            Assert.IsFalse(syncCollection.MoreAvailable);
            Assert.IsTrue(numberOfMessages == 10);

            FindItemsResults<OutlookItem> items = exchangeService.FindItems(folder.FolderId, new MessageView(4));

            for (int i = 0; i < items.TotalCount; i++)
            {
                Message msg = (Message) items.Items[i];
                if (i < 2)
                {
                    msg.IsRead = false;
                    msg.Update();
                }
                else
                {
                    msg.Delete();
                }
            }

            syncCollection = exchangeService.SyncFolderItems(
                folder.FolderId, 
                propertySet, 
                10, 
                syncState);

            Assert.IsFalse(syncCollection.MoreAvailable);
            Assert.IsTrue(
                syncCollection.TotalCount == 4);

            int changes = syncCollection.Items.Count(i => i.ChangeType == SyncChangeType.Deleted);
            Assert.AreEqual(
                2, 
                changes);

            changes = syncCollection.Items.Count(i => i.ChangeType == SyncChangeType.ReadFlagChanged);
            Assert.IsTrue(changes == 2);

            folder.Delete();
        }

        private void CreateMessage(int messageId, FolderId parentFolderId, ExchangeService exchangeService)
        {
            Message message = new Message(exchangeService);
            message.Subject = $"Test msg {messageId}";
            message.Body = new ItemBody()
            {
                ContentType = BodyType.Html,
                Content = $"This is test message for sync {messageId}"
            };

            message.ToRecipients = new List<Recipient>()
            {
                new Recipient()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Address = $"abc{messageId}@def.com"
                    }
                }
            };

            message.Save(parentFolderId);
        }
    }

    [TestClass]
    public class MessageRuleFunctionalTests : FunctionalTestsBase
    {
        [TestMethod]
        public void CRUDMessageRulesTest()
        {
            ExchangeService service = this.GetExchangeService(AppConfig.MailboxA);

            MessageRule messageRule = new MessageRule(service);
            messageRule.IsEnabled = true;
            messageRule.Sequence = 1;
            messageRule.Actions = new MessageRuleActions()
            {
                Delete = true,
                StopProcessingRules = true
            };

            messageRule.Conditions = new MessageRulePredicates()
            {
                FromAddresses = new List<Recipient>()
                {
                    new Recipient()
                    {
                        EmailAddress = new EmailAddress()
                        {
                            Address = "a@b.com"
                        }
                    }
                }
            };
            IList<string> s = new ObservableCollection<string>();
            messageRule.DisplayName = "Test rule";

            Assert.IsNull(messageRule.Id);
            messageRule.Save();
            Assert.IsNotNull(messageRule.Id);

            Assert.IsNotNull(messageRule.Id);

            MessageRule getMessageRule = service.GetInboxRule(messageRule.Id);
            Assert.IsNotNull(getMessageRule);

            getMessageRule.IsEnabled = false;
            getMessageRule.Update();
            Assert.IsFalse(getMessageRule.IsEnabled);


            List<MessageRule> rules = service.GetInboxRules();
            Assert.IsTrue(rules.Count == 1);

            getMessageRule.Delete();
            rules = service.GetInboxRules();
            Assert.IsTrue(rules.Count == 0);
        }
    }

    [TestClass]
    public class EventsFunctionalTests : FunctionalTestsBase
    {
        [TestMethod]
        public void CRUDEvents()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxB);
            FolderId calendarFolderId = new CalendarFolderId("me");
            string subject = Guid.NewGuid().ToString();

            Event calendarEvent = new Event(exchangeService);
            calendarEvent.Body = new ItemBody()
            {
                Content = "test",
                ContentType = BodyType.Html
            };

            calendarEvent.Subject = subject;
            calendarEvent.Start = new DateTimeTimeZone()
            {
                DateTime = this.GetFormattedDateTime(),
                TimeZone = "Central European Standard Time"
            };

            calendarEvent.End = new DateTimeTimeZone()
            {
                DateTime = this.GetFormattedDateTime(5),
                TimeZone = "Central European Standard Time"
            };
            
            calendarEvent.Attendees = new List<Attendee>()
            {
                new Attendee()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Address = AppConfig.MailboxA
                    }
                }
            };

            calendarEvent.Save(calendarFolderId);
            DateTime created = DateTime.Now;

            Thread.Sleep(5000); // allow item to be delivered to mailbox b

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo(
                EventObjectSchema.Subject, 
                subject);

            FindItemsResults<OutlookItem> items = exchangeService.FindItems(
                calendarFolderId, 
                subjectFilter, 
                new EventView(10));

            Assert.AreEqual(
                1,
                items.TotalCount);

            Event meeting = (Event) items.Items[0];
            meeting.Decline(
                "no comment", 
                true);

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxB);
            calendarEvent.Delete();
        }

        /// <summary>
        /// Get formatted date/time.
        /// </summary>
        /// <param name="hoursToAdd"></param>
        /// <returns></returns>
        private string GetFormattedDateTime(int hoursToAdd = 2)
        {
            DateTime dateTime = DateTime.UtcNow.AddHours(hoursToAdd);
            DateTime roundDateTime = new DateTime(
                dateTime.Year, 
                dateTime.Month, 
                dateTime.Day, 
                dateTime.Hour,
                dateTime.Minute - (dateTime.Minute % 15), 
                0);

            return roundDateTime.ToString("yyyy-MM-ddThh:mm:ss");
        }
    }

    [TestClass]
    public class TasksFunctionalTests : FunctionalTestsBase
    {
        //[TestMethod]
        //public void CRUDTasks()
        //{
        //    ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
        //    FolderId taskFolderId = new OutlookTaskFolderId(AppConfig.MailboxA);
        //    OutlookTask task = new OutlookTask(exchangeService);
        //    task.Subject = "sub";
        //    task.Body = new ItemBody()
        //    {
        //        Content = "Content",
        //        ContentType = BodyType.Html
        //    };

        //    task.Save(taskFolderId);
        //}
    }

    [TestClass]
    public class InferenceClassificationTests : FunctionalTestsBase
    {
        [TestMethod]
        public void CRUDInferenceClassificationOverride()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

            InferenceClassificationOverride inferenceClassification = new InferenceClassificationOverride(exchangeService);
            inferenceClassification.ClassifyAs = InferenceClassificationType.Focused;
            inferenceClassification.SenderEmailAddress = new EmailAddress()
            {
                Address = "a@b.hr"
            };

            Assert.IsNull(inferenceClassification.Id);
            inferenceClassification.Save();
            Assert.IsNotNull(inferenceClassification.Id);

            List<InferenceClassificationOverride> inferenceClassificationOverrides =
                exchangeService.GetInferenceClassificationOverrides();

            Assert.IsTrue(inferenceClassificationOverrides.Count == 1);
            Assert.AreEqual(
                inferenceClassificationOverrides[0].ClassifyAs, 
                inferenceClassification.ClassifyAs);

            Assert.AreEqual(
                inferenceClassificationOverrides[0].SenderEmailAddress.Address,
                inferenceClassification.SenderEmailAddress.Address);

            inferenceClassification.ClassifyAs = InferenceClassificationType.Other;
            inferenceClassification.Update();
            
            inferenceClassificationOverrides =
                exchangeService.GetInferenceClassificationOverrides();

            Assert.IsTrue(inferenceClassificationOverrides.Count == 1);
            Assert.AreEqual(
                inferenceClassificationOverrides[0].ClassifyAs,
                InferenceClassificationType.Other);

            inferenceClassification.Delete();
            Assert.IsNull(inferenceClassification.Id);
            Assert.IsNull(inferenceClassification.SenderEmailAddress);
        }
    }

    [TestClass]
    public class AttachmentTests : FunctionalTestsBase
    {
        [TestMethod]
        public void CRUDAttachments()
        {
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
            exchangeService.Preferences.Add(new Preference("IdType=\"ImmutableId\""));
            Message msg = new Message(exchangeService);
            msg.Subject = "Test attachment";

            FileAttachment attach = new FileAttachment()
            {
                ContentBytes = "VGhpcyBpcyB0ZXN0IGF0dGFjaG1lbnQ=",
                IsInline = false,
                Name = "Test.txt"
            };

            msg.Attachments.Add(attach);
            msg.Save(WellKnownFolderName.Inbox);
            AttachmentId attachmentId = new AttachmentId(msg.Attachments[0].Id, msg.ItemId, AppConfig.MailboxA);
            Attachment attachment = exchangeService.GetAttachment(attachmentId);
            Assert.IsNotNull(attachment);
            Assert.AreEqual("Test.txt", attachment.Name);

            HttpWebRequestClientProvider.Instance.ExitLock();
        }

        public void T()
        {

            ExchangeService exchangeService = new ExchangeService(
                "<bearerToken>", 
                "testmbx@test.com");

            Message message = new Message(exchangeService);
            message.Subject = "test subject";
            message.Body = new ItemBody()
            {
                ContentType = BodyType.Html,
                Content = "Body of the message"
            };

            message.Save(WellKnownFolderName.Inbox);
        }
    }


    
}
