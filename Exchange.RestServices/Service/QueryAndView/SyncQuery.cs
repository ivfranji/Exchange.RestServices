namespace Exchange.RestServices
{
    using System.Collections.Generic;

    /// <summary>
    /// Sync query implementation.
    /// </summary>
    internal class SyncQuery : ISyncQuery
    {
        /// <summary>
        /// Preferences.
        /// </summary>
        private List<string> preferences;

        /// <summary>
        /// Create new instance of <see cref="SyncQuery"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="syncToken">Sync token.</param>
        internal SyncQuery(int pageSize, ISyncToken syncToken)
        {
            this.preferences = new List<string>();
            this.preferences.Add($"odata.maxpagesize={pageSize}");
            this.preferences.Add("odata.track-changes");
            this.InitialQuery = syncToken == null;
            this.SyncToken = syncToken;
        }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get
            {
                if (this.InitialQuery)
                {
                    if (null != this.SelectedProperties)
                    {
                        return this.SelectedProperties.Query;
                    }

                    return string.Empty;
                }

                if (null != this.SelectedProperties)
                {
                    CompositeQuery compositeQuery = new CompositeQuery(new IQuery[]{ this.SelectedProperties, this.SyncToken });
                    return compositeQuery.Query;
                }

                return this.SyncToken.Query;
            }
        }

        /// <inheritdoc cref="ISyncQuery.Preferences"/>
        public IEnumerable<string> Preferences
        {
            get { return this.preferences; }
        }

        /// <inheritdoc cref="ISyncQuery.SyncToken"/>
        public ISyncToken SyncToken { get; }

        /// <inheritdoc cref="ISyncQuery.InitialQuery"/>
        public bool InitialQuery { get; }

        /// <inheritdoc cref="ISyncQuery.SelectedProperties"/>
        public ISelectQuery SelectedProperties { get; set; }

        /// <inheritdoc cref="ISyncQuery.ODataDeltaLink"/>
        public string ODataDeltaLink { get; set; }
    }
}