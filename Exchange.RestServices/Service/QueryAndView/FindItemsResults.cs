namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Find item results.
    /// </summary>
    /// <typeparam name="TItem">Type within collection.</typeparam>
    public class FindItemsResults<TItem> : FindResults<TItem> where TItem : Item
    {
        /// <summary>
        /// Create new instance of <see cref="FindItemsResults{TItem}"/>.
        /// </summary>
        /// <param name="responseCollection">Response collection.</param>
        internal FindItemsResults(ResponseCollection<TItem> responseCollection, ExchangeService exchangeService, MailboxId mailboxId)
            : base(responseCollection)
        {
            if (this.Items != null)
            {
                foreach (TItem item in this.Items)
                {
                    item.Service = exchangeService;
                    item.MailboxId = mailboxId;
                    item.ResetChangeTracking();
                }
            }
        }
    }
}
