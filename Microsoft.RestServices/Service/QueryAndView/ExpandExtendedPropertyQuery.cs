namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Extended property expand query.
    /// </summary>
    internal class ExpandExtendedPropertyQuery : ExpandQuery
    {
        /// <summary>
        /// Create new instance of <see cref="ExpandExtendedPropertyQuery"/>
        /// </summary>
        /// <param name="extendedPropertySearchFilterCollection"></param>
        /// <param name="propertyTypeValue"></param>
        internal ExpandExtendedPropertyQuery(SearchFilter extendedPropertySearchFilterCollection, PropertyValueType propertyTypeValue)
        {
            this.ExpandObject = $"{propertyTypeValue}({extendedPropertySearchFilterCollection.Query})";
        }
    }
}