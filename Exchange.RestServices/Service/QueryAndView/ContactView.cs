namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Contact view.
    /// </summary>
    public class ContactView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="ContactView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        public ContactView(int pageSize) 
            : this(pageSize, 0)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ContactView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Page offset.</param>
        public ContactView(int pageSize, int offset) 
            : base(pageSize, offset, typeof(Contact), new ContactPropertySet())
        {
        }
    }
}
