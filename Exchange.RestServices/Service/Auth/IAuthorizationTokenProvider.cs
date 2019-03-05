namespace Exchange.RestServices
{
    using System.Net.Http.Headers;

    /// <summary>
    /// Authorization token provider.
    /// </summary>
    public interface IAuthorizationTokenProvider
    {
        /// <summary>
        /// Construct authentication header.
        /// </summary>
        /// <returns></returns>
        AuthenticationHeaderValue GetAuthenticationHeader();
    }
}