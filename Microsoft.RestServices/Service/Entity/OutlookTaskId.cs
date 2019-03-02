namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Outlook task id.
    /// </summary>
    public class OutlookTaskId : ItemId
    {
        /// <summary>
        /// Create new instance of <see cref="OutlookTaskId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public OutlookTaskId(string entityId, string mailboxId) 
            : base(entityId, mailboxId, typeof(Task))
        {
        }

        /// <summary>
        /// Create new instance of <see cref="OutlookTaskId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public OutlookTaskId(string entityId, MailboxId mailboxId) 
            : base(entityId, mailboxId, typeof(Task))
        {
        }
    }
}