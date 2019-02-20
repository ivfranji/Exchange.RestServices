namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Represents rest environment to connect to.
    /// </summary>
    public class RestEnvironment
    {
        /// <summary>
        /// Graph beta.
        /// </summary>
        private static RestEnvironment graphBeta = new RestEnvironment(
            new Uri("https://graph.microsoft.com/beta"),
            "Graph Beta", 
            true);

        /// <summary>
        /// Graph prod.
        /// </summary>
        private static RestEnvironment graphProd = new RestEnvironment(
            new Uri("https://graph.microsoft.com/v1.0"),
            "Graph Prod",
            false);

        public RestEnvironment(Uri baseUri, string name, bool isBeta)
        {
            this.BaseUri = baseUri;
            this.Name = name;
            this.IsBeta = isBeta;
        }

        /// <summary>
        /// Base uri.
        /// </summary>
        public Uri BaseUri { get; }

        /// <summary>
        /// Environment name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Indicate if environment is beta.
        /// </summary>
        public bool IsBeta { get; }

        /// <summary>
        /// Graph beta environment.
        /// </summary>
        public static RestEnvironment GraphBeta
        {
            get { return RestEnvironment.graphBeta; }
        }

        /// <summary>
        /// Graph prod environment.
        /// </summary>
        public static RestEnvironment GraphProd
        {
            get { return RestEnvironment.graphProd; }
        }
    }
}