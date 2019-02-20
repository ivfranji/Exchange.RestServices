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
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

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
            folder1.DisplayName = "TestFolderSync1";
            folder1.Save(WellKnownFolderName.MsgFolderRoot);

            MailFolder folder2 = new MailFolder(exchangeService);
            folder2.DisplayName = "TestFolderSync2";
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
        public void GetMailMessageWithExtendedProperty()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxB);
            FindItemsResults<OutlookItem> findItemResults;
            MessageView messageView = new MessageView(10);
            messageView.SelectProperty(new ExtendedPropertyDefinition(MapiPropertyType.String, 0x0037));
            messageView.SelectProperty("HasAttachments");

            int counter = 0;
            do
            {
                findItemResults = exchangeService.FindItems(WellKnownFolderName.Inbox, messageView);
                foreach (OutlookItem item in findItemResults)
                {
                    Message msg = (Message) item;
                    Assert.IsNotNull(msg.SingleValueExtendedProperties);
                }

                messageView.Offset += messageView.PageSize;
                counter++;
            } while (findItemResults.MoreAvailable || counter == 3);
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
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo("Subject", messageSubject);

            FindItemsResults<OutlookItem> messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.AreEqual(1, messages.TotalCount);
            Message msg = (Message) messages.Items[0];
            msg.Reply("this is my reply");

            Thread.Sleep(5000); // allow some time for email to be delivered

            subjectFilter = new SearchFilter.IsEqualTo("Subject", $"Re: {messageSubject}");
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.IsTrue(messages.TotalCount == 1);

            messages.Items[0].Delete();
        }

        [TestMethod]
        public void SyncMessagesTest()
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);

            MailFolder folder = new MailFolder(exchangeService);
            folder.DisplayName = "TempSyncFolder";
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
            Assert.IsTrue(changes == 2);

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

            Event calendarEvent = new Event(exchangeService);
            calendarEvent.Body = new ItemBody()
            {
                Content = "test",
                ContentType = BodyType.Html
            };

            calendarEvent.Subject = "Test subject";
            calendarEvent.Start = new DateTimeTimeZone()
            {
                DateTime = this.GetFormattedDateTime(),
                TimeZone = "Central European Standard Time"
            };

            calendarEvent.End = new DateTimeTimeZone()
            {
                DateTime = this.GetFormattedDateTime(3),
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
            
            Thread.Sleep(5000); // allow item to be delivered to mailbox b

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            SearchFilter filter = new SearchFilter.IsEqualTo("Subject", "Test subject");
            FindItemsResults<OutlookItem> items = exchangeService.FindItems(calendarFolderId, filter, new EventView(10));

            Assert.IsTrue(items.TotalCount == 1);

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
            DateTime dateTime = DateTime.Now.AddHours(hoursToAdd);
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
        }
    }
}
