namespace Microsoft.RestServices.Tests.Service.Entity
{
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CalendarIdTests
    {
        [TestMethod]
        public void TestCalendarIdProperties()
        {
            FolderId calendarFolderId = new CalendarFolderId(
                "AABB==", 
                "me");

            Assert.AreEqual(
                "calendars/AABB==", 
                calendarFolderId.IdPath);

            Assert.AreEqual(
                "calendars/AABB==/events", 
                calendarFolderId.MessagesContainer);

            Assert.AreEqual(
                "calendars/AABB==",
                calendarFolderId.ChildFoldersContainer);
        }

        [TestMethod]
        public void TestDefaultCalendarIdProperties()
        {
            FolderId calendarFolderId = new CalendarFolderId("me");

            Assert.AreEqual(
                "calendar",
                calendarFolderId.IdPath);

            Assert.AreEqual(
                "calendar/events",
                calendarFolderId.MessagesContainer);

            Assert.AreEqual(
                "calendar",
                calendarFolderId.ChildFoldersContainer);
        }
    }
}
