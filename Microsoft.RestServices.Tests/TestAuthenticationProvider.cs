namespace Microsoft.RestServices.Tests
{
    using System;
    using System.Net.Http.Headers;
    using System.Security.Cryptography.X509Certificates;
    using Exchange;
    using IdentityModel.Clients.ActiveDirectory;

    /// <summary>
    /// Test authentication provider. 
    /// </summary>
    internal class TestAuthenticationProvider : IAuthorizationTokenProvider
    {
        /// <summary>
        /// Retrieve token.
        /// </summary>
        /// <returns></returns>
        private string GetToken()
        {
            string authority = $"https://login.microsoftonline.com/{AppConfig.TenantId}";
            AuthenticationContext context = new AuthenticationContext(authority);
            

            X509Certificate2 certFromStore = null;
            using (X509Store store = new X509Store(StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection collection = store.Certificates.Find(
                    X509FindType.FindByThumbprint, 
                    AppConfig.CertThumbprint, 
                    false);

                if (collection.Count == 1)
                {
                    certFromStore = collection[0];
                }
            }

            if (certFromStore == null)
            {
                throw new ArgumentNullException("Certificate");
            }

            ClientAssertionCertificate cert = new ClientAssertionCertificate(
                AppConfig.ApplicationId.ToString(),
                certFromStore);

            AuthenticationResult token = context.AcquireTokenAsync(
                AppConfig.ResourceUri,
                cert).Result;

            return token.AccessToken;
        }

        /// <summary>
        /// Scheme.
        /// </summary>
        public string Scheme
        {
            get { return "Bearer"; }
        }

        /// <inheritdoc cref="IAuthorizationTokenProvider.GetAuthenticationHeader"/>
        public AuthenticationHeaderValue GetAuthenticationHeader()
        {
            string token = this.GetToken();

            return new AuthenticationHeaderValue(
                this.Scheme, 
                token);
        }
    }
}