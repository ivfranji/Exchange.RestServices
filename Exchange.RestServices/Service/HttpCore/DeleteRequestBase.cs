namespace Exchange.RestServices
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Delete request base.
    /// </summary>
    internal class DeleteRequestBase : RequestBase
    {
        /// <summary>
        /// Create new instance of <see cref="DeleteRequestBase"/>.
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="httpRestUrlPreProcess">Http rest url preprocess.</param>
        internal DeleteRequestBase(ExchangeService exchangeService, Action<HttpRestUrl> httpRestUrlPreProcess) 
            : base(exchangeService, httpRestUrlPreProcess)
        {
        }

        /// <summary>
        /// Execute request - ASYNC.
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Delete(this.RestUrl))
            {
                IHttpWebResponse httpWebResponse = await this.ExecuteRequestAsync(httpWebRequest);
            }
        }

        /// <summary>
        /// Executes request.
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {
            using (IHttpWebRequest httpWebRequest = HttpWebRequest.Delete(this.RestUrl))
            {
                IHttpWebResponse httpWebResponse = this.ExecuteRequest(httpWebRequest);
            }
        }
    }
}
