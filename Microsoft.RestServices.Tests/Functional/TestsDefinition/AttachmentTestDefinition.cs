namespace Microsoft.RestServices.Tests.Functional.TestsDefinition
{
    using System;
    using System.Collections.Generic;
    using Exchange;
    using OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

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
            msg.Save(WellKnownFolderName.Inbox);

            AttachmentId attachId = new AttachmentId(
                msg.Attachments[0].Id, 
                msg.ItemId, 
                AppConfig.MailboxA);

            Attachment attachment = exchangeService.GetAttachment(attachId);

            Assert.IsInstanceOfType(
                attachment, 
                typeof(ItemAttachment));

            ItemAttachment itemAttachment = (ItemAttachment) attachment;

            // TODO: Expand items.
            //Assert.IsInstanceOfType(
            //    itemAttachment.Item,
            //    typeof(Event));

            //Event attachedEvent = (Event) itemAttachment.Item;
            //Assert.AreEqual(
            //    startTime,
            //    attachedEvent.Start.DateTime);

            //Assert.AreEqual(
            //    endTime,
            //    attachedEvent.End.DateTime);
        }
    }
}
