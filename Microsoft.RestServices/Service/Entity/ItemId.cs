namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Represents item id (Message, Events, Tasks...)
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public abstract class ItemId : EntityId
    {
        protected ItemId(string entityId, string mailboxId, Type idType) 
            : this(entityId, new MailboxId(mailboxId), idType)
        {
        }

        protected ItemId(string entityId, MailboxId mailboxId, Type type)
            : base(entityId, mailboxId, type)
        {
        }
    }
}