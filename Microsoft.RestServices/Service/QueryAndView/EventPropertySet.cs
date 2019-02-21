namespace Microsoft.RestServices.Exchange.Service.QueryAndView
{
    using Microsoft.Graph;

    /// <summary>
    /// Event property set.
    /// </summary>
    public class EventPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="EventPropertySet"/>
        /// </summary>
        public EventPropertySet() 
            : base(typeof(Event))
        {
            this.FirstClassProperties.Add(nameof(Event.Start));
            this.FirstClassProperties.Add(nameof(Event.End));
            this.FirstClassProperties.Add(nameof(Event.Subject));
        }
    }
}
