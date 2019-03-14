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
        /// <param name="entityResponseCollection">Response collection.</param>
        internal FindItemsResults(EntityResponseCollection<TItem> entityResponseCollection, ExchangeService exchangeService, MailboxId mailboxId)
            : base(entityResponseCollection)
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
