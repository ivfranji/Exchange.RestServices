namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// Post request base.
    /// </summary>
    internal class PostRequestBase : RequestBase
    {
        internal PostRequestBase(ExchangeService exchangeService, string postContent) 
            : base(exchangeService)
        {
            this.PostContent = postContent;
        }

        /// <summary>
        /// Create new instance of <see cref="PostRequestBase"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="postContent">Post content.</param>
        /// <param name="httpRestUrlPreProcess">Pre url processor.</param>
        internal PostRequestBase(ExchangeService exchangeService, string postContent, Action<HttpRestUrl> httpRestUrlPreProcess)
            : base(exchangeService, httpRestUrlPreProcess)
        {
            this.PostContent = postContent;
        }

        /// <summary>
        /// Post content.
        /// </summary>
        protected string PostContent { get; }

        /// <summary>
        /// Execute request.
        /// </summary>
        internal void Execute()
        {
            IHttpWebResponse response = this.ExecuteRequest(this.PostContent);
        }

        /// <summary>
        /// Execute and returns result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T Execute<T>()
        {
            IHttpWebResponse response = this.ExecuteRequest(this.PostContent);
            return this.Deserialize<T>(
                response, 
                this.DeserializationType);
        }

        /// <summary>
        /// Execute POST request.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected IHttpWebResponse ExecuteRequest(string content)
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Post(this.RestUrl, content))
            {
                IHttpWebResponse httpWebResponse = this.ExecuteRequest(httpWebRequest);
                return httpWebResponse;
            }
        }
    }
}
