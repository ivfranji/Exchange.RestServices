namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Sync item collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncFolderItemsCollection<T> : ChangeResults<T> where  T : Entity
    {
        /// <summary>
        /// Sync token.
        /// </summary>
        private ISyncToken syncToken;

        /// <summary>
        /// Create new instance of <see cref="SyncItemCollection{T}"/>
        /// </summary>
        /// <param name="responseCollection">Response collection.</param>
        internal SyncFolderItemsCollection(SyncResponseCollection<T> responseCollection, ExchangeService exchangeService, MailboxId mailboxId) 
            : base(responseCollection)
        {
            if (responseCollection != null)
            {
                // if token isn't delta, then it is skip.
                if (!SyncToken.TryParseFromUrl(responseCollection.ODataDeltaLink, SyncTokenType.DeltaToken, out this.syncToken))
                {
                    SyncToken.TryParseFromUrl(responseCollection.ODataNextLink, SyncTokenType.SkipToken, out this.syncToken);
                }

                // Since property bag will be 'dirty' after loading
                // properties from JSON, reset change tracking since
                // latest version is already in the collection.
                responseCollection.RegisterServiceAndResetChangeTracking(
                    exchangeService, 
                    mailboxId);
            }
        }

        /// <summary>
        /// Sync state.
        /// </summary>
        public string SyncState
        {
            get
            {
                if (null == this.syncToken)
                {
                    return string.Empty;
                }

                return this.syncToken.Serialize();
            }
        }
    }
}
