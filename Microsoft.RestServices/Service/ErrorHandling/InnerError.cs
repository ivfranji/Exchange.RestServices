namespace Microsoft.RestServices.Exchange
{
    using Newtonsoft.Json;

    /// <summary>
    /// Inner error.
    /// </summary>
    public class InnerError
    {
        /// <summary>
        /// Create new instance of <see cref="InnerError"/>
        /// </summary>
        internal InnerError(string date, string requestId)
        {
            this.Date = date;
            this.RequestId = requestId;
        }

        /// <summary>
        /// Create new instance of <see cref="InnerError"/>
        /// </summary>
        internal InnerError()
        {
        }

        /// <summary>
        /// Date.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; internal set; }

        /// <summary>
        /// Request id.
        /// </summary>
        [JsonProperty("request-id")]
        public string RequestId { get; internal set; }
    }
}