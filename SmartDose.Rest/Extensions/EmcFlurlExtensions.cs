using Flurl;
using Flurl.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SmartDose.Rest.Extensions
{
    #region Helpers

    public enum EmcHttpStatusCode
    {
        Undefined = -1,
        FlurlTimeOut = -2,
        FlurlException = -3,
        Exception = -4,

    }
    public class EmcFlurHttpResponse
    {
        public bool IsTimeout { get; set; }
        public HttpStatusCode StatusCode { get; set; } = (HttpStatusCode)(EmcHttpStatusCode.Undefined);
        public Exception Exception { get; set; } = null;
        public string ResponseMessage { get; set; } = "";
    }

    public class EmcFlurHttpResponse<T> : EmcFlurHttpResponse
    {
        public T Self { get; set; }
    }
    #endregion

    public static class EmcFlurlExtensions
    {
        public static bool IsHttpStatusCode(this HttpStatusCode thisValue, HttpStatusCode httpStatusCode)
            => thisValue.Equals(httpStatusCode);
        public static bool IsHttpStatusCodeOK(this EmcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode(HttpStatusCode.OK);
        public static bool IsHttpStatusCodeUndefined(this EmcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)EmcHttpStatusCode.Undefined);
        public static bool IsHttpStatusCodeRequestTimeout(this EmcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)EmcHttpStatusCode.FlurlTimeOut);
        public static bool IsHttpStatusCodeFlurException(this EmcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)EmcHttpStatusCode.FlurlException);
        public static bool IsHttpStatusCodeException(this EmcFlurHttpResponse thisValue)
            => thisValue.StatusCode.IsHttpStatusCode((HttpStatusCode)EmcHttpStatusCode.Exception);
        public static bool HasException(this EmcFlurHttpResponse thisValue)
            => thisValue.Exception != null;

        #region THECALL
        private async static Task<EmcFlurHttpResponse<T>> EmcHttpCall<T>(Func<Task<T>> httpCallAsync)
        {
            var emcResponse = new EmcFlurHttpResponse<T>();
            try
            {
                emcResponse.Self = await httpCallAsync().ConfigureAwait(false);
                emcResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (FlurlHttpTimeoutException ex)
            {
                emcResponse.StatusCode = (HttpStatusCode)EmcHttpStatusCode.FlurlTimeOut;
                emcResponse.Exception = ex;
            }
            catch (FlurlHttpException ex1)
            {
                emcResponse.StatusCode = ex1.Call.Response?.StatusCode ?? (HttpStatusCode)(EmcHttpStatusCode.FlurlException);
                emcResponse.ResponseMessage = await ex1.Call.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                emcResponse.Exception = ex1;
            }
            catch (Exception ex3)
            {
                emcResponse.StatusCode = (HttpStatusCode)(EmcHttpStatusCode.Exception);
                emcResponse.Exception = ex3;
            }
            return emcResponse;
        }
        #endregion

        public async static Task<EmcFlurHttpResponse<T>> EmcGetJsonAsync<T>(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<T>(() => url.GetJsonAsync<T>(cancellationToken, completionOption));
        public async static Task<EmcFlurHttpResponse<T>> EmcGetJsonAsync<T>(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<T>(() => url.GetJsonAsync<T>(cancellationToken, completionOption));

        public async static Task<EmcFlurHttpResponse> EmcPutJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.PutJsonAsync(data));
        public async static Task<EmcFlurHttpResponse> EmcPutJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.PutJsonAsync(data));

        public async static Task<EmcFlurHttpResponse> EmcPostJsonAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.PostJsonAsync(data, cancellationToken, completionOption));
        public async static Task<EmcFlurHttpResponse> EmcPostJsonAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.PostJsonAsync(data, cancellationToken, completionOption));

        public async static Task<EmcFlurHttpResponse<HttpResponseMessage>> EmcDeleteAsync(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.DeleteAsync(cancellationToken, completionOption));
        public async static Task<EmcFlurHttpResponse<HttpResponseMessage>> EmcDeleteAsync(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
            => await EmcHttpCall<HttpResponseMessage>(() => url.DeleteAsync(cancellationToken, completionOption));
    }
}
