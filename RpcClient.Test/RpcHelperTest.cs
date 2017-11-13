using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpcClient.Test.Model;
using RpcClient.Test.Ext;

namespace RpcClient.Test
{
    [TestClass]
    public class RpcHelperTest
    {
        /// <summary>
        /// rpc get测试
        /// </summary>
        [TestMethod]
        public void RpcGet()
        {
            string result = RpcHelper.GetAsync("https://www.baidu.com").Result;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// rpc post测试
        /// </summary>
        [TestMethod]
        public void RpcPost()
        {
            string result = RpcHelper.PostAsync("https://www.baidu.com", new LoginDto { LoginName = "hqmcq", LoginPwd = "123456" }).Result;
            Assert.IsNotNull(result);
        }


        public async void RpcGetTest()
        {
            string result=await RpcHelper.GetAsync("https://www.baidu.com");
            Console.WriteLine(result);
        }

        static async void RpcPostTest()
        {
            ApiResponseBase<User> response = await RpcHelper.PostAsync<LoginDto, ApiResponseBase<User>>("http://192.168.6.191:8086/api/Login/Login ", new LoginDto { LoginName = "hqmcq", LoginPwd = "123456" });
            Console.WriteLine(response.ToJson());
        }
    }
}
