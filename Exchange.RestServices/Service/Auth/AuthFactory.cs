namespace Exchange.RestServices.Service.Auth
{
    /// <summary>
    /// Auth provider factory.
    /// </summary>
    static class AuthFactory
    {
        /// <summary>
        /// Lock instance.
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// Token provider.
        /// </summary>
        private static IAuthorizationTokenProvider tokenProvider;

        /// <summary>
        /// Get token provider.
        /// </summary>
        /// <returns></returns>
        internal static IAuthorizationTokenProvider GetTokenProvider()
        {
            return AuthFactory.tokenProvider;
        }

        /// <summary>
        /// Set token provider.
        /// </summary>
        /// <param name="tokenProvider"></param>
        internal static void SetTokenProvider(IAuthorizationTokenProvider tokenProvider)
        {
            ArgumentValidator.ThrowIfNull(tokenProvider, nameof(tokenProvider));
            AuthFactory.tokenProvider = tokenProvider;
        }
    }
}
