namespace Exchange.RestServices.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using Exchange;
    using Microsoft.OutlookServices;

    /// <summary>
    /// Test helpers.
    /// </summary>
    internal static class TestHelpers
    {
        /// <summary>
        /// Delete folder if it exist.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="exchangeService"></param>
        /// <param name="folderRoot"></param>
        internal static void DeleteFolderIfExist(string folderName, ExchangeService exchangeService, WellKnownFolderName folderRoot)
        {
            FindFoldersResults findFolders = exchangeService.FindFolders(
                folderRoot,
                new FolderView(30));

            foreach (MailFolder mailFolder in findFolders)
            {
                if (mailFolder.DisplayName == folderName)
                {
                    mailFolder.Delete();
                }
            }
        }

        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="exchangeService"></param>
        /// <returns></returns>
        internal static MailFolder CreateFolder(string folderName, ExchangeService exchangeService, WellKnownFolderName folderRoot)
        {
            MailFolder folder = new MailFolder(exchangeService);
            folder.DisplayName = folderName;
            folder.Save(folderRoot);

            return folder;
        }

        /// <summary>
        /// Create message in the folder.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="parentFolderId"></param>
        /// <param name="exchangeService"></param>
        internal static void CreateMessage(int messageId, FolderId parentFolderId, ExchangeService exchangeService)
        {
            Message message = new Message(exchangeService);
            message.Subject = $"Test msg {messageId}";
            message.Body = new ItemBody()
            {
                ContentType = BodyType.HTML,
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

        /// <summary>
        /// Get formatted date/time.
        /// </summary>
        /// <param name="hoursToAdd"></param>
        /// <returns></returns>
        internal static DateTime GetFormattedDateTime(int hoursToAdd = 2)
        {
            DateTime dateTime = DateTime.UtcNow.AddHours(hoursToAdd);
            DateTime roundDateTime = new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute - (dateTime.Minute % 15),
                0);

            return roundDateTime;
        }
    }
}
