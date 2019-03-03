namespace Microsoft.RestServices.Tests.Service.PropertyChangeTracking
{
    using System;
    using System.Collections.Generic;
    using Functional;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PropertyChangeTests
    {
        [TestMethod]
        public void TestCollectionProperties()
        {
            Message msg = new Message();

            // collections are always instantiated in background.
            Assert.IsNotNull(msg.ToRecipients);
            Assert.AreEqual(
                0,
                msg.GetChangedPropertyNames().Count);

            msg.ToRecipients = new List<Recipient>();
            Assert.IsTrue(msg.GetChangedPropertyNames().Contains("ToRecipients"));

            msg.ResetChangeTracking();
            Assert.IsFalse(msg.GetChangedPropertyNames().Contains("ToRecipients"));

            msg.ToRecipients.Add(new Recipient());
            Assert.IsTrue(msg.GetChangedPropertyNames().Contains("ToRecipients"));
            Assert.AreEqual(
                msg.GetChangedPropertyNames().Count, 
                1);
        }

        [TestMethod]
        public void TestProperties()
        {
            Message msg = new Message();
            msg.ToRecipients = new List<Recipient>();
            msg.Subject = "Test subject";
            Assert.IsTrue(msg.GetChangedPropertyNames().Contains("ToRecipients"));
            Assert.IsTrue(msg.GetChangedPropertyNames().Contains("Subject"));

            msg.ResetChangeTracking();
            Assert.IsFalse(msg.GetChangedPropertyNames().Contains("ToRecipients"));
            Assert.IsFalse(msg.GetChangedPropertyNames().Contains("Subject"));

            msg.ToRecipients.Add(new Recipient());
            msg.Subject = "Updated subject";
            Assert.IsTrue(msg.GetChangedPropertyNames().Contains("ToRecipients"));
            Assert.AreEqual(
                msg.GetChangedPropertyNames().Count,
                2);

            Assert.AreEqual(
                "Updated subject", 
                msg.Subject);
        }

        [TestMethod]
        public void TestPropertyTrackingWithInnerProperties()
        {
            ItemAttachment itemAttachment = new ItemAttachment()
            {
                Name = "Item attach",
                Item = new Event()
                {
                     Subject = "Event subject",
                     Attendees = new List<Attendee>()
                     {
                         new Attendee()
                         {
                             EmailAddress = new EmailAddress() { Address = "a@a.com" }
                         }
                     },

                     Body = new ItemBody()

                }
            };

            Assert.AreEqual(
                2,
                itemAttachment.GetChangedProperies().Count);

            Assert.AreEqual(
                3,
                itemAttachment.Item.GetChangedProperies().Count);

            Assert.IsTrue(
                ItemAttachmentObjectSchema.Item.ChangeTrackable);

            itemAttachment.ResetChangeTracking();

            Assert.AreEqual(
                0,
                itemAttachment.GetChangedProperies().Count);

            Assert.AreEqual(
                0,
                itemAttachment.Item.GetChangedProperies().Count);
        }

        [TestMethod]
        public void TestPropertyChangeTrackingWithInnerListProperties()
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

            Assert.AreEqual(
                2,
                msg.GetChangedProperies().Count);

            Assert.AreEqual(
                2,
                msg.Attachments[0].GetChangedProperies().Count);

            Assert.AreEqual(
                4,
                ((ItemAttachment)msg.Attachments[0]).Item.GetChangedProperies().Count);

            msg.ResetChangeTracking();

            Assert.AreEqual(
                0,
                msg.GetChangedProperies().Count);

            Assert.AreEqual(
                0,
                msg.Attachments[0].GetChangedProperies().Count);

            Assert.AreEqual(
                0,
                ((ItemAttachment) msg.Attachments[0]).Item.GetChangedProperies().Count);
        }
    }
}
