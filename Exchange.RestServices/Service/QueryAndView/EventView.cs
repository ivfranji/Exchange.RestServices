namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;
    using Service.QueryAndView;

    /// <inheritdoc />
    /// <summary>
    /// Event view.
    /// </summary>
    public class EventView : ViewBase
    {
        /// <summary>
        /// Create new instance of <see cref="EventView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        public EventView(int pageSize) 
            : this(pageSize, 0)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="EventView"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        public EventView(int pageSize, int offset) 
            : base(pageSize, offset, typeof(Event), new EventPropertySet())
        {
        }
    }
}