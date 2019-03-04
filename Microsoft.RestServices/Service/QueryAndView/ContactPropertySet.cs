namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Contact property set.
    /// </summary>
    public class ContactPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="ContactPropertySet"/>
        /// </summary>
        public ContactPropertySet() 
            : base(typeof(Contact))
        {
            this.FirstClassProperties.Add(nameof(Contact.DisplayName));
            this.FirstClassProperties.Add(nameof(Contact.EmailAddresses));
            this.FirstClassProperties.Add(nameof(Contact.CreatedDateTime));
        }
    }
}
