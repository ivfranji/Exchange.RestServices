namespace Exchange.RestServices.Tests.Functional.TestsDefinition
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Calendar event test definition.
    /// </summary>
    internal static class EventTestDefinition
    {
        /// <summary>
        /// CRUD operation for calendar events.
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteEvents(ExchangeService exchangeService)
        {
            FolderId calendarFolderId = new CalendarFolderId("me");
            string subject = Guid.NewGuid().ToString();

            Event calendarEvent = new Event(exchangeService);
            calendarEvent.Body = new ItemBody()
            {
                Content = "test",
                ContentType = BodyType.HTML
            };

            calendarEvent.Subject = subject;
            calendarEvent.Start = new DateTimeTimeZone()
            {
                DateTime = TestHelpers.GetFormattedDateTime(),
                TimeZone = "Central European Standard Time"
            };

            calendarEvent.End = new DateTimeTimeZone()
            {
                DateTime = TestHelpers.GetFormattedDateTime(5),
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

            Thread.Sleep(8000); // allow item to be delivered to mailbox b

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
            SearchFilter subjectFilter = new SearchFilter.IsEqualTo(
                EventObjectSchema.Subject,
                subject);

            FindItemsResults<Item> items = exchangeService.FindItems(
                calendarFolderId,
                subjectFilter,
                new EventView(10));

            Assert.AreEqual(
                1,
                items.TotalCount);


            Event meeting = (Event) items.Items[0];
            meeting.Decline(
                true,
                "no comment");

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxB);
            calendarEvent.Delete();
        }

        public static void CreateReadUpdateDeleteCalendar(ExchangeService exchangeService)
        {
            //Calendar
        }
    }
}
