namespace Exchange.RestServices
{
    /// <summary>
    /// Page query. https://docs.microsoft.com/en-us/previous-versions/office/office-365-api/api/beta/complex-types-for-mail-contacts-calendar-beta#TopSkip
    /// </summary>
    public interface IPageQuery : IQuery
    {
        /// <summary>
        /// Offset.
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        /// Page size.
        /// </summary>
        int PageSize { get; set; }
    }
}