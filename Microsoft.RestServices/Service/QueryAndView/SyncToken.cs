namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Text;

    /// <summary>
    /// Sync token - for sync operations.
    /// </summary>
    public class SyncToken : ISyncToken
    {
        /// <summary>
        /// Create new instance of <see cref="SyncToken"/>
        /// </summary>
        /// <param name="rawToken"></param>
        /// <param name="tokenType"></param>
        public SyncToken(string rawToken, SyncTokenType tokenType)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(rawToken, nameof(rawToken));
            this.Value = rawToken;
            this.Type = tokenType;
        }

        /// <inheritdoc cref="ISyncToken.Type"/>
        public SyncTokenType Type { get; }

        ///<inheritdoc cref="ISyncToken.Value"/>
        public string Value { get; }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get { return $"${SyncToken.GetTokenPrefix(this.Type)}{this.Value}"; }
        }

        /// <summary>
        /// Equals override.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is SyncToken token))
            {
                return false;
            }

            return this.Type == token.Type &&
                   this.Value == token.Value;
        }

        /// <summary>
        /// ToString impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Type}{{{this.Value}}}";
        }

        /// <summary>
        /// Serialize token into the string.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            string rawValue = $"{this.Type}|{this.Value}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(rawValue));
        }

        /// <summary>
        /// Deserialize token from string. Returns null if serialization cannot occur.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static SyncToken Deserialize(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            string rawValue = string.Empty;
            try
            {
                rawValue = Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch (FormatException e)
            {
                // In case non-base64 string is provided.
                return null;
            }

            string[] parts = rawValue.Split('|');
            if (parts.Length != 2)
            {
                return null;
            }

            SyncTokenType tokenType;
            if (!Enum.TryParse(parts[0], out tokenType))
            {
                return null;
            }

            return new SyncToken(parts[1], tokenType);
        }

        /// <summary>
        /// Tries to parse value of Url query into <see cref="ISyncToken"/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tokenType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool TryParseFromUrl(Uri url, SyncTokenType tokenType, out ISyncToken token)
        {
            return SyncToken.TryParseFromUrl(url.Query, tokenType, out token);
        }

        /// <summary>
        /// Tries to parse value into sync token.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="tokenType">Token type.</param>
        /// <param name="token">out SyncToken.</param>
        /// <returns></returns>
        public static bool TryParseFromUrl(string value, SyncTokenType tokenType, out ISyncToken token)
        {
            token = null;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            int startIndex = value.IndexOf(SyncToken.GetTokenPrefix(tokenType), StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
            {
                return false;
            }

            int endIndex = startIndex + SyncToken.GetTokenPrefix(tokenType).Length;
            string tokenValue = value.Substring(endIndex);
            if (tokenValue.IndexOf("&", StringComparison.Ordinal) != -1)
            {
                tokenValue = tokenValue.Split('&')[0];
            }

            token = new SyncToken(tokenValue, tokenType);
            return true;
        }

        /// <summary>
        /// Get token prefix.
        /// </summary>
        /// <param name="tokenType">Type of token to retrieve prefix for.</param>
        /// <returns></returns>
        private static string GetTokenPrefix(SyncTokenType tokenType)
        {
            return tokenType == SyncTokenType.DeltaToken
                ? "deltatoken="
                : "skiptoken=";
        }
    }
}