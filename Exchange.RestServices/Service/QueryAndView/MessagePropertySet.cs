namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Message property set.
    /// </summary>
    public class MessagePropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="MessagePropertySet"/>
        /// </summary>
        public MessagePropertySet()
            : base(typeof(Message))
        {
            this.FirstClassProperties.Add(nameof(Message.IsRead));
            this.FirstClassProperties.Add(nameof(Message.Subject));
            this.FirstClassProperties.Add(nameof(Message.ParentFolderId));
        }
    }
}