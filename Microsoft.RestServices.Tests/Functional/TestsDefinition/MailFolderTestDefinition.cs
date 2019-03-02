namespace Microsoft.RestServices.Tests.Functional
{
    using System.Threading;
    using Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;
    using FolderView = Exchange.FolderView;

    /// <summary>
    /// Test definition for mail folders.
    /// </summary>
    internal static class MailFolderTestDefinition
    {
        /// <summary>
        /// Test mail folder sync.
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void SyncMailFolders(ExchangeService exchangeService)
        {
            string folder1Name = "TempSyncFolder1";
            string folder2Name = "TempSyncFolder2";

            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();
            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);

            FindFoldersResults findFolders = exchangeService.FindFolders(
                WellKnownFolderName.MsgFolderRoot,
                new FolderView(30));

            foreach (MailFolder mailFolder in findFolders)
            {
                if (mailFolder.DisplayName == folder1Name ||
                    mailFolder.DisplayName == folder2Name)
                {
                    mailFolder.Delete();
                }
            }

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
            folder1.DisplayName = folder1Name;
            folder1.Save(WellKnownFolderName.MsgFolderRoot);

            MailFolder folder2 = new MailFolder(exchangeService);
            folder2.DisplayName = folder2Name;
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

        /// <summary>
        /// Get mail folders request.
        /// </summary>
        public static void GetMailFolders(ExchangeService exchangeService)
        {
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);
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

        /// <summary>
        /// Basic CRUD operations test.
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteMailFolder(ExchangeService exchangeService)
        {
            HttpWebRequestClientProvider.Instance.EnterLock();
            HttpWebRequestClientProvider.Instance.Reset();

            exchangeService.MailboxId = new MailboxId(AppConfig.MailboxA);

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
    }
}
