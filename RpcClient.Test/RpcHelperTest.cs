using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            RpcGetTest();
            Console.Read();
        }

        /// <summary>
        /// rpc post测试
        /// </summary>
        [TestMethod]
        public void RpcPost()
        {
        }


        public async void RpcGetTest()
        {
            string result=await RpcHelper.GetAsync("https://www.baidu.com");
            Console.WriteLine(result);
        }
    }
}
