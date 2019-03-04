namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Represents Contact Id.
    /// </summary>
    public class ContactId : ItemId
    {
        /// <summary>
        /// Create new instance of <see cref="ContactId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox Id.</param>
        public ContactId(string entityId, string mailboxId) 
            : this(entityId, new MailboxId(mailboxId))
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ContactId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox Id.</param>
        public ContactId(string entityId, MailboxId mailboxId) 
            : base(entityId, mailboxId, typeof(Contact))
        {
        }
    }
}
