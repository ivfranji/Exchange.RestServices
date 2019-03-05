namespace Exchange.RestServices.Tests.Functional.TestsDefinition
{
    using System;
    using System.Collections.Generic;
    using Exchange;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Attachment operation test cases definition.
    /// </summary>
    internal static class AttachmentTestDefinition
    {
        /// <summary>
        /// CRUD operation for file attachments.
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteFileAttachment(ExchangeService exchangeService)
        {
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

        /// <summary>
        /// CRUD operation for item attachments
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteItemAttachment(ExchangeService exchangeService)
        {
            DateTime startTime = TestHelpers.GetFormattedDateTime();
            DateTime endTime = TestHelpers.GetFormattedDateTime(2);
            string timeZone = "Central European Standard Time";

            Message msg = new Message(exchangeService);
            msg.Subject = "Test item attachment";

            ItemAttachment attach = new ItemAttachment()
            {
                Name = "Attached message",
                Item = new Message()
                {
                    Body = new ItemBody()
                    {
                        ContentType = BodyType.HTML,
                        Content = "Lets meet up"
                    },
                    ToRecipients = new List<Recipient>()
                    {
                        new Recipient()
                        {
                            EmailAddress = new EmailAddress()
                            {
                                Address = "a@a.com"
                            }
                        }
                    }
                }
            };

            msg.Attachments.Add(attach);
            msg.Save(WellKnownFolderName.Inbox);

            AttachmentId attachId = new AttachmentId(
                msg.Attachments[0].Id, 
                msg.ItemId, 
                AppConfig.MailboxA);

            Attachment attachment = exchangeService.GetAttachment(attachId);

            Assert.IsInstanceOfType(
                attachment, 
                typeof(ItemAttachment));

            attachment = exchangeService.GetAttachment(attachId,
                new ExpandQuery("Microsoft.OutlookServices.ItemAttachment/Item"));

            ItemAttachment itemAttachment = (ItemAttachment) attachment;
            Assert.IsNotNull(itemAttachment.Item);
            Assert.IsInstanceOfType(itemAttachment.Item, typeof(Message));
        }
    }
}
