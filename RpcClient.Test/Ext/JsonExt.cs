using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RpcClient.Test.Ext
{
    internal static class JsonExt
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)where T:class,new()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer jsonSerialize = new DataContractJsonSerializer(obj.GetType());
                jsonSerialize.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string jsonStr) where T:class,new()
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                DataContractJsonSerializer jsonSerialize = new DataContractJsonSerializer(typeof(T));
                return jsonSerialize.ReadObject(stream) as T;
            }
        }
    }
}
