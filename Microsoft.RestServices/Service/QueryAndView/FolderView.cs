namespace Microsoft.RestServices.Exchange
{
    using Microsoft.Graph;

    /// <summary>
    /// Folder view.
    /// </summary>
    public class FolderView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="FolderView"/>.
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        public FolderView(int pageSize)
            : base(pageSize, typeof(MailFolder))
        {
        }

        /// <summary>
        /// Create new instance of <see cref="FolderView"/>.
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        public FolderView(int pageSize, int offset)
            : base(pageSize, offset, typeof(MailFolder))
        {
        }
    }
}