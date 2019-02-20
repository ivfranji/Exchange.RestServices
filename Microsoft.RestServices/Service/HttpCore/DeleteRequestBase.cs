namespace Microsoft.RestServices.Exchange
{
    using System;

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
