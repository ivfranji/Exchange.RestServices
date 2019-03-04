namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Outlook task view.
    /// </summary>
    public class TaskView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="TaskView"/>
        /// </summary>
        /// <param name="pageSize"></param>
        public TaskView(int pageSize)
            : this(pageSize, 0)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="TaskView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        public TaskView(int pageSize, int offset)
            : base(pageSize, offset, typeof(Task), new TaskPropertySet())
        {
        }
    }
}