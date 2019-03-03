namespace Microsoft.RestServices.Exchange
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
        /// <param name="responseCollection"></param>
        public SyncMailFolderHierarchyResponse(SyncMailFolderResponseCollection responseCollection, ExchangeService exchangeService, MailboxId mailboxId) 
            : base(responseCollection)
        {
            responseCollection.RegisterServiceAndResetChangeTracking(exchangeService, mailboxId);
            byte[] syncStateBytes = Encoding.UTF8.GetBytes(responseCollection.ODataDeltaLink);
            this.SyncState = Convert.ToBase64String(syncStateBytes);
        }

        /// <summary>
        /// Sync state.
        /// </summary>
        public string SyncState { get; private set; }
    }
}
