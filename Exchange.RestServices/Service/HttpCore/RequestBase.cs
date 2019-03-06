namespace Exchange.RestServices
{
    using System;
    using System.Net;

    /// <summary>
    /// Base class for all requests.
    /// </summary>
    internal abstract class RequestBase
    {
        /// <summary>
        /// Exchange rest service.
        /// </summary>
        private ExchangeService exchangeService;

        /// <summary>
        /// Create new instance of <see cref="RequestBase"/>
        /// </summary>
        /// <param name="exchangeService"></param>
        protected RequestBase(ExchangeService exchangeService)
        {
            this.exchangeService = exchangeService;
            this.RestUrl = new HttpRestUrl(
                this.exchangeService.Url);
        }

        /// <summary>
        /// Create new instance of <see cref="RequestBase"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="httpRestUrlPreProcess">Callback for configuring http rest url.</param>
        protected RequestBase(ExchangeService exchangeService, Action<HttpRestUrl> httpRestUrlPreProcess)
            : this(exchangeService)
        {
            ArgumentValidator.ThrowIfNull(
                httpRestUrlPreProcess,
                nameof(httpRestUrlPreProcess));

            httpRestUrlPreProcess(this.RestUrl);
        }

        /// <summary>
        /// Deserialization type.
        /// </summary>
        internal Type DeserializationType { get; set; }

        /// <summary>
        /// Allow client to 
        /// </summary>
        internal Action<IPreferenceHeaderSetter> PreferHeaderSetter { get; set; }

        /// <summary>
        /// Http rest url.
        /// </summary>
        protected HttpRestUrl RestUrl { get; }

        /// <summary>
        /// Pre-process web request before getting response.
        /// </summary>
        /// <param name="httpWebRequest"></param>
        protected virtual void PreProcessHttpWebRequest(IHttpWebRequest httpWebRequest)
        {
        }

        /// <summary>
        /// Process web response.
        /// </summary>
        /// <param name="httpWebResponse"></param>
        protected virtual void ProcessHttpWebResponse(IHttpWebResponse httpWebResponse)
        {
            httpWebResponse.ThrowIfNeeded(this.ThrowRestException);
        }

        /// <summary>
        /// Execute request and returns response.
        /// </summary>
        /// <param name="httpWebRequest">Http web request.</param>
        /// <returns></returns>
        protected IHttpWebResponse ExecuteRequest(IHttpWebRequest httpWebRequest)
        {
            this.PreProcessHttpWebRequestInternal(httpWebRequest);
            this.PreferHeaderSetter?.Invoke(httpWebRequest);

            IHttpWebResponse httpWebResponse = httpWebRequest.GetResponse();
            this.ProcessHttpWebResponseInternal(httpWebResponse);
            return httpWebResponse;
        }

        /// <summary>
        /// Deserialize http web response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpWebResponse"></param>
        /// <returns></returns>
        protected T Deserialize<T>(IHttpWebResponse httpWebResponse, Type type = null)
        {
            return Deserializer.Instance.Deserialize<T>(
                httpWebResponse, 
                type);
        }

        /// <summary>
        /// Deserialize string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected T Deserialize<T>(string content)
        {
            return Deserializer.Instance.Deserialize<T>(
                content, 
                null);
        }

        /// <summary>
        /// Throws rest exception if server returns error.
        /// </summary>
        /// <param name="error">Error string.</param>
        protected void ThrowRestException(string error, HttpStatusCode httpStatusCode)
        {
            ErrorWrapper deserializedError = null;
            try
            {
                deserializedError = this.Deserialize<ErrorWrapper>(error);
            }
            catch (Exception)
            {
            }

            if (deserializedError?.Error != null)
            {
                throw new RestResponseException(
                    deserializedError.Error, 
                    httpStatusCode);
            }

            // Ideally we shouldn't be reaching this one.
            throw new RestResponseException(
                error, 
                httpStatusCode);
        }

        /// <summary>
        /// Process http web response internal.
        /// </summary>
        /// <param name="httpWebResponse">Http web response.</param>
        private void ProcessHttpWebResponseInternal(IHttpWebResponse httpWebResponse)
        {
            // since ProcessHttpWebResponse may throw in any child class
            // we process this at service level first to get logs etc...
            this.exchangeService.ProcessHttpWebResponse(httpWebResponse);
            this.ProcessHttpWebResponse(httpWebResponse);
        }

        /// <summary>
        /// Process http web request internal.
        /// </summary>
        /// <param name="httpWebRequest">Http web request.</param>
        private void PreProcessHttpWebRequestInternal(IHttpWebRequest httpWebRequest)
        {
            this.PreProcessHttpWebRequest(httpWebRequest);
            this.exchangeService.PrepareHttpWebRequest(httpWebRequest);
            this.exchangeService.TraceHttpWebRequest(httpWebRequest);
            this.exchangeService.TraceHttpRequestHeaders(httpWebRequest.Headers);
        }
    }
}
