namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Expand query.
    /// </summary>
    public class ExpandQuery : IExpandQuery
    {
        /// <summary>
        /// Expand prefix.
        /// </summary>
        private const string ExpandPrefix = "$expand=";

        /// <summary>
        /// Create new instance of <see cref="IExpandQuery"/>
        /// </summary>
        /// <param name="expandObject">Object to expand.</param>
        public ExpandQuery(string expandObject)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(
                expandObject, 
                nameof(expandObject));

            this.ExpandObject = expandObject;
        }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get { return $"{ExpandQuery.ExpandPrefix}{this.ExpandObject}"; }
        }

        /// <inheritdoc cref="IExpandQuery.ExpandObject"/>
        public string ExpandObject { get; }
    }
}