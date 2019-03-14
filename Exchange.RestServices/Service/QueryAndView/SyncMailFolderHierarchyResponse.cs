namespace Exchange.RestServices
{
    using System;
    using System.Text;
    using Microsoft.OutlookServices;

    /// <summary>
    /// Sync mail folder hierarchy 
    /// </summary>
    public sealed class SyncMailFolderHierarchyResponse : ChangeResults<MailFolder>
    {
        /// <summary>
        /// Create new instance of <see cref="SyncMailFolderHierarchyResponse"/>
        /// </summary>
        /// <param name="entityResponseCollection"></param>
        public SyncMailFolderHierarchyResponse(SyncMailFolderEntityResponseCollection entityResponseCollection, ExchangeService exchangeService, MailboxId mailboxId) 
            : base(entityResponseCollection)
        {
            entityResponseCollection.RegisterServiceAndResetChangeTracking(exchangeService, mailboxId);
            byte[] syncStateBytes = Encoding.UTF8.GetBytes(entityResponseCollection.ODataDeltaLink);
            this.SyncState = Convert.ToBase64String(syncStateBytes);
        }

        /// <summary>
        /// Sync state.
        /// </summary>
        public string SyncState { get; private set; }
    }
}
