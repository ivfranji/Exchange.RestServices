namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Outlook task id.
    /// </summary>
    public class TaskId : ItemId
    {
        /// <summary>
        /// Create new instance of <see cref="TaskId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public TaskId(string entityId, string mailboxId) 
            : base(entityId, mailboxId, typeof(Task))
        {
        }

        /// <summary>
        /// Create new instance of <see cref="TaskId"/>
        /// </summary>
        /// <param name="entityId">Entity Id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public TaskId(string entityId, MailboxId mailboxId) 
            : base(entityId, mailboxId, typeof(Task))
        {
        }
    }
}