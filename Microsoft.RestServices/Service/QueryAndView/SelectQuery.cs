namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Select query.
    /// </summary>
    public class SelectQuery : ISelectQuery
    {
        /// <summary>
        /// Prefix.
        /// </summary>
        private const string SelectPrefix = "$select=";

        /// <summary>
        /// Create new instance of <see cref="ISelectQuery"/>
        /// </summary>
        /// <param name="property">Property to retrieve.</param>
        public SelectQuery(string property)
            : this(new[] {property})
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ISelectQuery"/>
        /// </summary>
        /// <param name="properties">Properties to fetch.</param>
        public SelectQuery(string[] properties)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(properties, nameof(properties));
            this.Properties = properties;
        }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get { return $"{SelectQuery.SelectPrefix}{string.Join(",", this.Properties)}"; }
        }

        /// <inheritdoc cref="ISelectQuery.Properties"/>
        public string[] Properties { get; }
    }
}