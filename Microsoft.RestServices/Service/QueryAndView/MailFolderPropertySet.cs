namespace Microsoft.RestServices.Exchange
{
    using Graph;

    /// <summary>
    /// Folder property set,
    /// </summary>
    public class MailFolderPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="MailFolderPropertySet"/>
        /// </summary>
        public MailFolderPropertySet()
            : base(typeof(MailFolder))
        {
            this.FirstClassProperties.Add(nameof(MailFolder.ChildFolderCount));
            this.FirstClassProperties.Add(nameof(MailFolder.DisplayName));
            this.FirstClassProperties.Add(nameof(MailFolder.TotalItemCount));
        }
    }
}