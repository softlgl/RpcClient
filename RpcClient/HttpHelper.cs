﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace RpcClient
{
    public class HttpHelper
    {
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> headers, int timeout = 10000, bool isGzip = false)
        {
            return PostDataToServer(url, null, "GET", headers, timeout, isGzip);
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> data, Dictionary<string, string> headers, int timeout = 10000, bool isGzip = false)
        {
            if (data != null && data.Count > 0)
            {
                StringBuilder builder = new StringBuilder($"{url}?");
                foreach (var item in data)
                {
                    builder.Append($"{item.Key}={item.Value}&");
                }
                url = builder.ToString().TrimEnd('&');
            }
            return Get(url, headers, timeout, isGzip);
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="headers">请求头集合</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip</param>
        /// <returns></returns>
        public static string Post(string url, string data,Dictionary<string, string> headers, int timeout = 10000, bool isGzip = false)
        {
            return PostDataToServer(url, data, "POST", headers, timeout, isGzip);
        }

        /// <summary>
        /// 请求http方法
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <param name="data">请求的参数</param>
        /// <param name="method">请求方式(post/get)</param>
        /// <param name="headers">请求头</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isGzip">是否为gzip请求</param>
        /// <returns>请求内容</returns>
        private static string PostDataToServer(string url, string data, string method, Dictionary<string, string> headers,int timeout , bool isGzip)
        {
            HttpWebRequest request = null;
            string result = "";
            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = timeout;
                request.KeepAlive = true;
                ServicePointManager.Expect100Continue = false;

                //设置头信息
                if (headers != null && headers.Count > 0)
                {
                    foreach (var item in headers)
                    {
                        request.Headers.Add(item.Key,item.Value);
                    }
                }
                switch (method.ToUpper())
                {
                    case "GET":
                        request.Method = "GET";
                        break;
                    case "POST":
                        {
                            request.Method = "POST";
                            if (isGzip)
                            {
                                request.Headers.Add("Accept-Encoding", "gzip");
                                request.AutomaticDecompression = DecompressionMethods.GZip;
                            }

                            byte[] bdata = Encoding.UTF8.GetBytes(data);
                            request.ContentType = "application/json;charset=utf-8";
                            request.ContentLength = bdata.Length;

                            Stream streamOut = request.GetRequestStream();
                            streamOut.Write(bdata, 0, bdata.Length);
                            streamOut.Close();
                        }
                        break;
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream streamIn = response.GetResponseStream())
                    {
                        if (isGzip)
                        {
                            var cmpTypes = response.Headers.GetValues("Content-Encoding");
                            using (GZipStream steam = new GZipStream(streamIn, CompressionMode.Decompress))
                            {
                                using (StreamReader reader = new StreamReader(steam))
                                {
                                    result = reader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            using (StreamReader reader = new StreamReader(streamIn))
                            {
                                result = reader.ReadToEnd();
                            }
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
    }
}