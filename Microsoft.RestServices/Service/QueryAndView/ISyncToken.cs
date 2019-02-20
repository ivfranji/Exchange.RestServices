namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Sync token contract.
    /// </summary>
    public interface ISyncToken : IQuery
    {
        /// <summary>
        /// Token value.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Token type.
        /// </summary>
        SyncTokenType Type { get; }

        /// <summary>
        /// Serialize token.
        /// </summary>
        /// <returns></returns>
        string Serialize();
    }
}