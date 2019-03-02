namespace Microsoft.RestServices.Exchange
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Find folder search results.
    /// </summary>
    public class FindFoldersResults : FindResults<MailFolder>
    {
        /// <summary>
        /// Create new instance of <see cref="FindFoldersResults"/>
        /// </summary>
        /// <param name="responseCollection"></param>
        internal FindFoldersResults(ResponseCollection<MailFolder> responseCollection)
            : base(responseCollection)
        {
        }
    }
}
