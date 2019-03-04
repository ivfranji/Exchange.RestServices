namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Outlook task property set.
    /// </summary>
    public class TaskPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="TaskPropertySet"/>
        /// </summary>
        public TaskPropertySet()
            : base(typeof(Task))
        {
            this.FirstClassProperties.Add(nameof(Task.CreatedDateTime));
            this.FirstClassProperties.Add(nameof(Task.Owner));
            this.FirstClassProperties.Add(nameof(Task.Subject));
        }
    }
}
