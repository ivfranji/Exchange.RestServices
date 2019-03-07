namespace Exchange.RestServices
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for all GET requests.
    /// </summary>
    /// <typeparam name="T">Type to return.</typeparam>
    internal class GetRequestBase<T> : RequestBase
    {
        /// <summary>
        /// Create new instance of <see cref="GetRequestBase{T}"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        internal GetRequestBase(ExchangeService exchangeService)
            : base(exchangeService)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="GetRequestBase{T}"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="httpRestUrlPreProcess">Callback for configuring http rest url.</param>
        internal GetRequestBase(ExchangeService exchangeService, Action<HttpRestUrl> httpRestUrlPreProcess)
            : base(exchangeService, httpRestUrlPreProcess)
        {
        }

        /// <summary>
        /// Execute request - ASYNC.
        /// </summary>
        /// <returns></returns>
        public async Task<T> ExecuteAsync()
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Get(this.RestUrl))
            {
                IHttpWebResponse httpWebResponse = await this.ExecuteRequestAsync(httpWebRequest);
                return this.Deserialize<T>(
                    httpWebResponse,
                    this.DeserializationType);
            }
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        /// <returns></returns>
        public T Execute()
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Get(this.RestUrl))
            {
                IHttpWebResponse httpWebResponse = this.ExecuteRequest(httpWebRequest);
                return this.Deserialize<T>(
                    httpWebResponse, 
                    this.DeserializationType);
            }  
        }
    }
}
