namespace Microsoft.RestServices.Exchange.Service.QueryAndView
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Outlook task property set.
    /// </summary>
    public class OutlookTaskPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="OutlookTaskPropertySet"/>
        /// </summary>
        public OutlookTaskPropertySet()
            : base(typeof(Task))
        {
            this.FirstClassProperties.Add(nameof(Task.CreatedDateTime));
            this.FirstClassProperties.Add(nameof(Task.Owner));
            this.FirstClassProperties.Add(nameof(Task.Subject));
        }
    }
}
