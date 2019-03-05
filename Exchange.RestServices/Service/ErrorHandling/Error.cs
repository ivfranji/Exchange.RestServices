namespace Exchange.RestServices
{
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Error response from server.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Inner error.
        /// </summary>
        [JsonProperty("innerError")]
        public Error InnerError { get; set; }

        /// <summary>
        /// Custom error data.
        /// </summary>
        [JsonExtensionData(ReadData = true)]
        public Dictionary<string, object> CustomErrorData { get; set; }

        /// <summary>
        /// Create string representation of the error.
        /// </summary>
        /// <param name="sb"></param>
        protected internal void ToString(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(this.Code))
            {
                sb.AppendLine($"Code: {this.Code}");
            }

            if (!string.IsNullOrEmpty(this.Message))
            {
                sb.AppendLine($"Message: {this.Message}");
            }

            if (null != this.CustomErrorData)
            {
                foreach (KeyValuePair<string, object> pair in this.CustomErrorData)
                {
                    sb.AppendLine($"{pair.Key}: {pair.Value}");
                }
            }

            if (this.InnerError != null)
            {
                sb.AppendLine("=== InnerError ===");
                this.InnerError.ToString(sb);
            }
        }

        /// <summary>
        /// ToString impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            this.ToString(sb);
            return sb.ToString();
        }
    }
}
