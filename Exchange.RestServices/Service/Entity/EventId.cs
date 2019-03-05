namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Event id.
    /// </summary>
    public class EventId : ItemId
    {
        /// <summary>
        /// Create new instance of <see cref="EventId"/>
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public EventId(string entityId, string mailboxId) 
            : this(entityId, new MailboxId(mailboxId))
        {
        }

        public EventId(string entityId, MailboxId mailboxId)
            : base(entityId, mailboxId, typeof(Event))
        {
        }
    }
}