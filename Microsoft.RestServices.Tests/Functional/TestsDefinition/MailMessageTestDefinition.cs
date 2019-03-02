namespace Microsoft.RestServices.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

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
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxB);
            MessageView messageView = new MessageView(1);
            messageView.PropertySet.Add(new ExtendedPropertyDefinition(
                MapiPropertyType.String,
                0x0037));

            messageView.PropertySet.Add("HasAttachments");

            {
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
        }

        /// <summary>
        /// Send message from mailbox a to mailbox b
        /// </summary>
        public static void SendMessageFromMailboxAToMailboxB(ExchangeService exchangeService)
        {
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
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

            Thread.Sleep(8000); // allow some time for email to be delivered
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
    }
}
