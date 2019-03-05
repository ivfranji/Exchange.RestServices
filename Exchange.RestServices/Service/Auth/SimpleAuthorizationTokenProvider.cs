namespace Exchange.RestServices
{
    using System.Net.Http.Headers;

    /// <summary>
    /// Simple authorization token provider.
    /// </summary>
    public class SimpleAuthorizationTokenProvider : IAuthorizationTokenProvider
    {
        /// <summary>
        /// Bearer token.
        /// </summary>
        private string bearerToken;

        /// <summary>
        /// Create new instance of <see cref="SimpleAuthorizationTokenProvider"/>
        /// </summary>
        /// <param name="bearerToken"></param>
        internal SimpleAuthorizationTokenProvider(string bearerToken)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(
                bearerToken, 
                nameof(bearerToken));

            this.bearerToken = bearerToken;
            this.Scheme = "Bearer";
        }

        /// <summary>
        /// Scheme.
        /// </summary>
        internal string Scheme { get; }

        public AuthenticationHeaderValue GetAuthenticationHeader()
        {
            return new AuthenticationHeaderValue(
                this.Scheme, 
                this.bearerToken);
        }

        /// <summary>
        /// ToString Impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(SimpleAuthorizationTokenProvider);
        }
    }
}