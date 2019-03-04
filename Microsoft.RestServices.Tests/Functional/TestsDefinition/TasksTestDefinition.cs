namespace Microsoft.RestServices.Tests.Functional.TestsDefinition
{
    using System;
    using Exchange;
    using OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tasks test definition.
    /// </summary>
    internal static class TasksTestDefinition
    {
        /// <summary>
        /// CRUD task operations.
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        public static void CreateUpdateReadDeleteTasks(ExchangeService exchangeService)
        {
            FolderId tasksFolderId = new TaskFolderId("me");
            string subject = Guid.NewGuid().ToString();

            Task task = new Task(exchangeService);
            task.Body = new ItemBody()
            {
                ContentType = BodyType.HTML,
                Content = "This is test task."
            };

            task.Subject = subject;
            task.AssignedTo = AppConfig.MailboxB;
            task.Importance = Importance.High;
            task.DueDateTime = new DateTimeTimeZone()
            {
                DateTime = DateTime.Now.AddDays(2),
                TimeZone = "UTC"
            };

            task.Save(tasksFolderId);
            Assert.IsNotNull(task.Id);

            task.Importance = Importance.Low;
            task.DueDateTime = new DateTimeTimeZone()
            {
                DateTime = DateTime.Now.AddDays(5),
                TimeZone = "UTC"

            };

            task.Update();

            Assert.AreEqual(
                Importance.Low,
                task.Importance);

            SearchFilter searchFilter = new SearchFilter.IsEqualTo(
                TaskObjectSchema.Subject,
                subject);
            FindItemsResults<Item> tasks = exchangeService.FindItems(
                tasksFolderId,
                searchFilter,
                new TaskView(10));

            Assert.AreEqual(
                1,
                tasks.TotalCount);

            task.Delete();
            Assert.IsNull(task.Id);
        }
    }
}
