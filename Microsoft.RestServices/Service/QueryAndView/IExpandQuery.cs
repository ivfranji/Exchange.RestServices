namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Expand query contract.
    /// </summary>
    public interface IExpandQuery : IQuery
    {
        /// <summary>
        /// Object to expand.
        /// </summary>
        string ExpandObject { get; }
    }
}