namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Filter query.
    /// </summary>
    public interface IFilterQuery : IQuery
    {
        /// <summary>
        /// Filter operator.
        /// </summary>
        FilterOperator FilterOperator { get; }
    }
}