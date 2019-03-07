namespace Exchange.RestServices.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Exchange;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task = System.Threading.Tasks.Task;

    /// <summary>
    /// Test definition for mail message.
    /// </summary>
    internal static class MailMessageTestDefinition
    {
        /// <summary>
        /// Get mail with single extended property.
        /// </summary>
        public static void GetMailMessageWithSingleExtendedProperties(ExchangeService exchangeService)
        {
            MessageView messageView = new MessageView(1);
            messageView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                0x0037));

            messageView.PropertySet.Add("HasAttachments");
            FindItemsResults<Item> findItemResults = exchangeService.FindItems(
                WellKnownFolderName.Inbox,
                messageView);
            foreach (Item item in findItemResults)
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
            foreach (Item item in findItemResults)
            {
                Message msg = (Message) item;
                Assert.AreEqual(
                    2,
                    msg.SingleValueExtendedProperties.Count);
            }
        }

        /// <summary>
        /// Send message from mailbox a to mailbox b
        /// </summary>
        public static void SendMessageFromMailboxAToMailboxB(ExchangeService exchangeService)
        {
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
                ContentType = BodyType.HTML
            };

            message.Send();

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxB);

            Thread.Sleep(10000); // allow some time for email to be delivered
            MessageView messageView = new MessageView(10);
            FolderId inbox = new FolderId(WellKnownFolderName.Inbox);
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject,
                messageSubject);

            FindItemsResults<Item> messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.AreEqual(1, messages.TotalCount);
            Message msg = (Message) messages.Items[0];
            msg.Reply("this is my reply");

            Thread.Sleep(8000); // allow some time for email to be delivered

            subjectFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject,
                $"Re: {messageSubject}");
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            messages = exchangeService.FindItems(inbox, subjectFilter, messageView);

            Assert.IsTrue(messages.TotalCount == 1);

            messages.Items[0].Delete();
        }

        /// <summary>
        /// CRUD operation against extended properties.
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        public static void CreateReadUpdateDeleteExtendedProperties(ExchangeService exchangeService)
        {
            const string extendedPropertyGuid = "4d557659-9e3f-405e-8f6d-86d2d9d5c630";
            string subject = Guid.NewGuid().ToString();

            Message msg = new Message(exchangeService);
            msg.Subject = subject;
            msg.SingleValueExtendedProperties.Add(new SingleValueLegacyExtendedProperty()
            {
                PropertyId = $"String {extendedPropertyGuid} Name Blah",
                Value = "BlahValue"
            });

            msg.MultiValueExtendedProperties.Add(new MultiValueLegacyExtendedProperty()
            {
                PropertyId = $"StringArray {extendedPropertyGuid} Name BlahArray",
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
                new Guid(extendedPropertyGuid)));

            msgView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.StringArray,
                "BlahArray",
                new Guid(extendedPropertyGuid)));

            SearchFilter filter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject,
                subject);

            FindItemsResults<Item> findItemsResults = exchangeService.FindItems(
                WellKnownFolderName.Inbox,
                filter,
                msgView);

            foreach (Item item in findItemsResults)
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

        /// <summary>
        /// Find message call.
        /// </summary>
        public static void FindMessage(ExchangeService exchangeService)
        {
            string folderName = "TestFindItemFolder";

            TestHelpers.DeleteFolderIfExist(
                folderName,
                exchangeService,
                WellKnownFolderName.MsgFolderRoot);

            MailFolder folder = TestHelpers.CreateFolder(
                folderName,
                exchangeService,
                WellKnownFolderName.MsgFolderRoot);

            for (int i = 0; i < 9; i++)
            {
                TestHelpers.CreateMessage(
                    1,
                    folder.FolderId,
                    exchangeService);
            }

            for (int i = 0; i < 10; i++)
            {
                TestHelpers.CreateMessage(
                    i,
                    folder.FolderId,
                    exchangeService);
            }

            // TODO: Verify further...
            // there are 10 "Test msg 1" and 9 others. Expecting to see
            // sync 5 times.
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo(
                MessageObjectSchema.Subject,
                "Test msg 1");

            MessageView mv = new MessageView(2);
            FindItemsResults<Item> items = null;
            int counter = 0;
            do
            {
                items = exchangeService.FindItems(
                    folder.FolderId,
                    subjectFilter,
                    mv);

                mv.Offset += mv.PageSize;
                counter++;

            } while (items.MoreAvailable);

            Assert.AreEqual(
                6,
                counter);
        }

        /// <summary>
        /// Sync messages
        /// </summary>
        public static void SyncMessages(ExchangeService exchangeService)
        {
            string folderName = "TempSyncFolder";

            TestHelpers.DeleteFolderIfExist(
                folderName,
                exchangeService,
                WellKnownFolderName.MsgFolderRoot);

            MailFolder folder = TestHelpers.CreateFolder(
                folderName,
                exchangeService,
                WellKnownFolderName.MsgFolderRoot);

            for (int i = 0; i < 10; i++)
            {
                TestHelpers.CreateMessage(
                    i,
                    folder.FolderId,
                    exchangeService);
            }

            string syncState = null;
            MessagePropertySet propertySet = new MessagePropertySet();
            propertySet.AddProperty("ToRecipients");
            SyncFolderItemsCollection<Item> syncCollection;
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

                foreach (ItemChange<Item> itemChange in syncCollection)
                {
                    Assert.IsTrue(
                        itemChange.ChangeType == SyncChangeType.Created);
                }

            } while (syncCollection.MoreAvailable || counter == 4);

            Assert.IsFalse(syncCollection.MoreAvailable);
            Assert.AreEqual(10, numberOfMessages);

            FindItemsResults<Item> items = exchangeService.FindItems(folder.FolderId, new MessageView(4));

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

        public static async Task CreateReadUpdateDeleteMessageAsync(ExchangeService exchangeService)
        {
            string folderName = "AsyncTestCrudItems";
            TestHelpers.DeleteFolderIfExist(
                folderName, 
                exchangeService, 
                WellKnownFolderName.MsgFolderRoot);

            MailFolder mailFolder = TestHelpers.CreateFolder(
                folderName,
                exchangeService,
                WellKnownFolderName.MsgFolderRoot);

            for (int i = 0; i < 10; i++)
            {
                Message msg = new Message(exchangeService);
                msg.Subject = Guid.NewGuid().ToString();
                msg.Body = new ItemBody()
                {
                    ContentType = BodyType.HTML,
                    Content = $"body {Guid.NewGuid().ToString()}"
                };

                await msg.SaveAsync(mailFolder.FolderId);
            }

            FindItemsResults<Item> items = await exchangeService.FindItemsAsync(mailFolder.FolderId, new MessageView(12));
            Assert.AreEqual(
                10,
                items.TotalCount);

            foreach (Item item in items)
            {
                if (item is Message msg)
                {
                    msg.Subject = $"Changed subject - {msg.Subject}";
                    await msg.UpdateAsync();
                }
            }

            items = await exchangeService.FindItemsAsync(mailFolder.FolderId, new MessageView(12));
            foreach (Item item in items)
            {
                if (item is Message msg)
                {
                    Assert.IsTrue(
                        msg.Subject.StartsWith("Changed subject -"));

                    await item.DeleteAsync();
                }
            }
        }
    }
}
