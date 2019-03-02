namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Recipient filter formatter.
    /// </summary>
    internal sealed class RecipientFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.Type"/>
        public override string Type
        {
            get { return typeof(Microsoft.OutlookServices.Recipient).FullName; }
        }

        /// <inheritdoc cref="BaseFilterFormatter.QuoteRequired"/>
        protected override bool QuoteRequired
        {
            get { return true; }
        }

        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            Recipient recipient = (Recipient) obj;
            ArgumentValidator.ThrowIfNull(
                recipient.EmailAddress, 
                nameof(recipient.EmailAddress));

            if (string.IsNullOrEmpty(recipient.EmailAddress.Address))
            {
                ArgumentValidator.ThrowIfNullOrEmpty(
                    recipient.EmailAddress.Name, 
                    "Name");

                return this.Format(
                    recipient.EmailAddress.Name, 
                    filterOperator, 
                    propertyDefinition);
            }

            ArgumentValidator.ThrowIfNullOrEmpty(
                recipient.EmailAddress.Address, 
                "Address");

            return this.Format(
                recipient.EmailAddress.Address, 
                filterOperator, 
                propertyDefinition);
        }

        /// <inheritdoc cref="BaseFilterFormatter.FormatPropertyName"/>
        protected override string FormatPropertyName(string obj, PropertyDefinition propertyDefinition)
        {
            if (EmailAddressValidator.IsValid(obj))
            {
                return $"{propertyDefinition.Name}/EmailAddress/Address";
            }

            return $"{propertyDefinition.Name}/EmailAddress/Name";
        }
    }
}