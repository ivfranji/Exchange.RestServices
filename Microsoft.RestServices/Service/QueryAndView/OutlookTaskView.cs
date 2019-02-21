namespace Microsoft.RestServices.Exchange
{
    using Microsoft.Graph;
    using Service.QueryAndView;

    /// <summary>
    /// Outlook task view.
    /// </summary>
    public class OutlookTaskView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="OutlookTaskView"/>
        /// </summary>
        /// <param name="pageSize"></param>
        public OutlookTaskView(int pageSize)
            : this(pageSize, 0)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="OutlookTaskView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        public OutlookTaskView(int pageSize, int offset)
            : base(pageSize, offset, typeof(OutlookTask), new OutlookTaskPropertySet())
        {
        }
    }
}