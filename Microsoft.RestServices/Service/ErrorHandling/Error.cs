namespace Microsoft.RestServices.Exchange
{
    using Newtonsoft.Json;

    /// <summary>
    /// Error response from server.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Create new instance of <see cref="Error"/>
        /// </summary>
        [JsonConstructor]
        internal Error(string code, string message, InnerError innerError)
        {
            this.Code = code;
            this.Message = message;
            this.InnerError = innerError;
        }

        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; private set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; private set; }

        /// <summary>
        /// Inner error.
        /// </summary>
        [JsonProperty("innerError")]
        public InnerError InnerError { get; private set; }
    }
}
