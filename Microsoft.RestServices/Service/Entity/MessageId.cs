namespace Microsoft.RestServices.Exchange
{
    using Microsoft.Graph;

    /// <summary>
    /// Message id.
    /// </summary>
    public class MessageId : ItemId
    {
        public MessageId(string entityId, string mailboxId) 
            : this(entityId, new MailboxId(mailboxId))
        {
        }

        public MessageId(string entityId, MailboxId mailboxId)
            : base(entityId, mailboxId, typeof(Message))
        {
        }
    }
}