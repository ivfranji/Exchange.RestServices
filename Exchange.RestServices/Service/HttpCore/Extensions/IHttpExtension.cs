namespace Exchange.RestServices
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension handler.
    /// </summary>
    public interface IHttpExtension
    {
        /// <summary>
        /// Process http request.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        Task ProcessHttpRequest(HttpRequestMessage httpRequest);

        /// <summary>
        /// If true, it should all <see cref="SendAsync"/>, otherwise not.
        /// This should be used by test cases to mock response.
        /// </summary>
        bool ShortCircuit { get; }

        /// <summary>
        /// Perform Send async.
        /// </summary>
        /// <param name="httpRequest">Http request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken);
    }
}
