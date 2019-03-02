namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Attachment id.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttachmentId : EntityId
    {
        /// <summary>
        /// Attachments container.
        /// </summary>
        private const string AttachmentContainer = "attachments";

        /// <summary>
        /// Create new instance of <see cref="AttachmentId{T}"/>
        /// </summary>
        /// <param name="id">Attachment id.</param>
        /// <param name="itemId">Item id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        public AttachmentId(string id, ItemId itemId, string mailboxId) 
            : base(id, mailboxId, typeof(Attachment))
        {
            this.ItemId = itemId;
            this.RootContainer = $"{this.ItemId.IdPath}/{AttachmentId.AttachmentContainer}";
        }

        /// <summary>
        /// Item id this attachment belongs to.
        /// </summary>
        public ItemId ItemId { get; }
    }
}