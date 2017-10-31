using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RpcClient.Test.Model;

namespace RpcClient.Test
{
    [TestClass]
    public class JsonExtTest
    {
        /// <summary>
        /// 类序列化反序列化测试
        /// </summary>
        [TestMethod]
        public void TestObjectJson()
        {
            School school = new School();
            school.Name = "实验中学";
            school.Classes = new List<Class>
            {
                new Class{Name="一班",Count=25},
                new Class{Name="二班",Count=30}
            };
            string json = school.ToJson();
            School school2 =json.JsonToObject<School>();
        }

        /// <summary>
        /// 集合序列化反序列化测试
        /// </summary>
        [TestMethod]
        public void TestListJson()
        {
            School school = new School();
            school.Name = "实验中学";
            school.Classes = new List<Class>
            {
                new Class{Name="一班",Count=25},
                new Class{Name="二班",Count=30}
            };

            School school2 = new School();
            school2.Name = "第一中学";
            school2.Classes = new List<Class>
            {
                new Class{Name="三班",Count=26},
                new Class{Name="四班",Count=28}
            };

            List<School> schools = new List<School> { school,school2 };

            string json = schools.ToJson();
            List<School> schools2 = json.JsonToObject<List<School>>();
        }
    }
}
