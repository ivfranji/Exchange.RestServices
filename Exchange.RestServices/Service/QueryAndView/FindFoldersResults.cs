namespace Exchange.RestServices
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
        /// <param name="entityResponseCollection"></param>
        internal FindFoldersResults(EntityResponseCollection<MailFolder> entityResponseCollection)
            : base(entityResponseCollection)
        {
        }
    }
}
