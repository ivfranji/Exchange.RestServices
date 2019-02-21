namespace Microsoft.RestServices.Exchange
{
    using Microsoft.Graph;

    /// <summary>
    /// Message view.
    /// </summary>
    public class MessageView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="MessageView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        public MessageView(int pageSize, bool expandAttachments = false)
            : this(pageSize, 0, expandAttachments)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="MessageView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        public MessageView(int pageSize, int offset, bool expandAttachments)
            : base(pageSize, offset, typeof(Message), new MessagePropertySet())
        {
            if (expandAttachments)
            {
                this.ExpandProperties.Add("attachments");
            }
        }
    }
}