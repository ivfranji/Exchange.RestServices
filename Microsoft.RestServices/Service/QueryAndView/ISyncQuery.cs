namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;

    /// <summary>
    /// Contract for sync.
    /// </summary>
    internal interface ISyncQuery : IQuery
    {
        /// <summary>
        /// Preferences.
        /// </summary>
        IEnumerable<string> Preferences { get; }

        /// <summary>
        /// Sync token associated with query.
        /// </summary>
        ISyncToken SyncToken { get; }

        /// <summary>
        /// Indicate this is initial query.
        /// </summary>
        bool InitialQuery { get; }

        /// <summary>
        /// Selected properties.
        /// </summary>
        ISelectQuery SelectedProperties { get; set; }
    }
}