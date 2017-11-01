using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcClient
{
    public static class RpcHelper
    {
        #region get相关操作

        #region 返回string操作

        /// <summary>
        /// http get请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            return HttpHelper.Get(url, data,headers,userAgent,timeout,isGzip);
        }

        /// <summary>
        /// http 异步get请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static Task<string> GetAsync(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            return Task.Factory.StartNew(() => {
                return Get(url, data, headers, userAgent, timeout, isGzip);
            });
        }

        #endregion

        #region 返回TResult操作

        /// <summary>
        /// http get请求
        /// </summary>
        /// <typeparam name="TResult">结果Model</typeparam>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static TResult Get<TResult>(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string userAgent = null, int timeout = 10000, bool isGzip = false) 
            where TResult : class, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            string str = Get(url, data, headers, userAgent, timeout, isGzip);
            return str.JsonToObject<TResult>();
        }

        /// <summary>
        /// http get请求
        /// </summary>
        /// <typeparam name="TResult">结果Model</typeparam>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static Task<TResult> GetAsync<TResult>(string url, Dictionary<string, string> data = null, Dictionary<string, string> headers = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
            where TResult : class, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            return Task.Factory.StartNew(()=> {
                return Get<TResult>(url, data, headers, userAgent, timeout, isGzip);
            });
        }

        #endregion

        #endregion

        #region post相关操作

        #region 返回string操作

        /// <summary>
        /// http post请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static string Post<T>(string url, T data, Dictionary<string, string> headers = null, string contentType=null,string userAgent = null, int timeout = 10000, bool isGzip = false)
            where T:class,new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return HttpHelper.Post(url, data.ToJson(), headers, contentType,userAgent, timeout, isGzip);
        }

        /// <summary>
        /// http 异步post请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static Task<string> PostAsync<T>(string url, T data, Dictionary<string, string> headers = null, string contentType = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
            where T : class, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return Task.Factory.StartNew(()=> {
                return Post(url, data, headers, contentType, userAgent, timeout, isGzip);
            });
        }

        #endregion

        #region 返回TResult相关操作

        /// <summary>
        /// http post请求
        /// </summary>
        /// <typeparam name="T">请求参数类型</typeparam>
        /// <typeparam name="TResult">返回参数类型</typeparam>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static TResult Post<T,TResult>(string url, T data, Dictionary<string, string> headers = null, string contentType = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
            where T : class, new()
            where TResult:class,new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            string result = HttpHelper.Post(url, data.ToJson(), headers, contentType, userAgent, timeout, isGzip);
            return result.JsonToObject<TResult>();
        }

        /// <summary>
        /// http post请求
        /// </summary>
        /// <typeparam name="T">请求参数类型</typeparam>
        /// <typeparam name="TResult">返回参数类型</typeparam>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求参数</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="contentType">设置contentType</param>
        /// <param name="userAgent">用户代理</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static Task<TResult> PostAsync<T,TResult>(string url, T data, Dictionary<string, string> headers = null, string contentType = null, string userAgent = null, int timeout = 10000, bool isGzip = false)
            where T : class, new()
            where TResult : class, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            return Task.Factory.StartNew(() => {
                return Post<T,TResult>(url, data, headers, contentType, userAgent, timeout, isGzip);
            });
        }

        #endregion

        #endregion
    }
}
