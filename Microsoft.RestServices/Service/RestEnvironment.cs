namespace Microsoft.RestServices.Exchange
{
    using System;
    using Service;

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

        /// <summary>
        /// Graph prod.
        /// </summary>
        private static RestEnvironment outlookProd = new RestEnvironment(
            new Uri("https://outlook.office365.com/api/v2.0"),
            "Outlook Prod",
            false,
            FeatureSet.None);

        public RestEnvironment(Uri baseUri, string name, bool isBeta, FeatureSet featureSet = FeatureSet.All)
        {
            this.BaseUri = baseUri;
            this.Name = name;
            this.IsBeta = isBeta;
            this.FeatureSet = featureSet;
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
        /// Feature set.
        /// </summary>
        public FeatureSet FeatureSet { get; }

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

        /// <summary>
        /// Outlook Prod environment.
        /// </summary>
        public static RestEnvironment OutlookProd
        {
            get
            {
                return RestEnvironment.outlookProd;
            }
        }
    }
}