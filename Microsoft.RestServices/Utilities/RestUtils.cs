namespace Microsoft.RestServices.Exchange.Utilities
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Common utils.
    /// </summary>
    internal static class RestUtils
    {
        /// <summary>
        /// Lazy buildNumber member.
        /// </summary>
        private static Lazy<string> buildNumber = new Lazy<string>(() =>
        {
            try
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                return fileVersionInfo.FileVersion;
            }
            catch
            {
                return "0.0";
            }
        });

        /// <summary>
        /// Assembly build number.
        /// </summary>
        internal static string BuildNumber
        {
            get { return RestUtils.buildNumber.Value; }
        }
    }
}
