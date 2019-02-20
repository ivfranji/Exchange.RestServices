namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Select query. https://docs.microsoft.com/en-us/previous-versions/office/office-365-api/api/beta/complex-types-for-mail-contacts-calendar-beta#Select
    /// </summary>
    public interface ISelectQuery : IQuery
    {
        /// <summary>
        /// Properties to fetch.
        /// </summary>
        string[] Properties { get; }
    }
}