namespace Microsoft.RestServices.Exchange
{
    using System;

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
