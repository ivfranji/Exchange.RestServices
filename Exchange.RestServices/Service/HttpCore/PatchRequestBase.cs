namespace Exchange.RestServices
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Base request for HTTP PATCH.
    /// </summary>
    internal class PatchRequestBase : RequestBase
    {
        /// <summary>
        /// Create new instance of <see cref="PatchRequestBase"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="postContent">Post content.</param>
        /// <param name="httpRestUrlPreProcess">Pre url processor.</param>
        internal PatchRequestBase(ExchangeService exchangeService, string patchContent, Action<HttpRestUrl> httpRestUrlPreProcess)
            : base(exchangeService, httpRestUrlPreProcess)
        {
            this.PatchContent = patchContent;
        }

        /// <summary>
        /// Post content.
        /// </summary>
        protected string PatchContent { get; }

        /// <summary>
        /// Execute in async fashion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal async Task<T> ExecuteAsync<T>()
        {
            IHttpWebResponse response = await this.ExecuteRequestAsync(this.PatchContent);
            return this.Deserialize<T>(
                response,
                this.DeserializationType);
        }

        /// <summary>
        /// Execute and returns result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T Execute<T>()
        {
            IHttpWebResponse response = this.ExecuteRequest(this.PatchContent);
            return this.Deserialize<T>(
                response, 
                this.DeserializationType);
        }

        /// <summary>
        /// Execute POST request - Async
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected async Task<IHttpWebResponse> ExecuteRequestAsync(string content)
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Patch(this.RestUrl, content))
            {
                IHttpWebResponse httpWebResponse = await this.ExecuteRequestAsync(httpWebRequest);
                return httpWebResponse;
            }
        }

        /// <summary>
        /// Execute POST request.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected IHttpWebResponse ExecuteRequest(string content)
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Patch(this.RestUrl, content))
            {
                IHttpWebResponse httpWebResponse = this.ExecuteRequest(httpWebRequest);
                return httpWebResponse;
            }
        }
    }
}
