namespace Microsoft.RestServices.Exchange.Service.QueryAndView
{
    using Microsoft.Graph;

    /// <summary>
    /// Outlook task property set.
    /// </summary>
    public class OutlookTaskPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="OutlookTaskPropertySet"/>
        /// </summary>
        public OutlookTaskPropertySet()
            : base(typeof(OutlookTask))
        {
            this.FirstClassProperties.Add(nameof(OutlookTask.CreatedDateTime));
            this.FirstClassProperties.Add(nameof(OutlookTask.Owner));
            this.FirstClassProperties.Add(nameof(OutlookTask.Subject));
        }
    }
}
