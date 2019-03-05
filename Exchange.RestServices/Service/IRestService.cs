namespace Exchange.RestServices
{
    /// <summary>
    /// Rest service.
    /// </summary>
    public interface IRestService
    {
        /// <summary>
        /// User agent associated with this service.
        /// </summary>
        string UserAgent { get; set; }
    }
}