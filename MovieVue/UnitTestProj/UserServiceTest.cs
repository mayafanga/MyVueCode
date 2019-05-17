using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Service;
using Movie.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Tests
{
    [TestClass()]
    public class UserServiceTest
    {
        IUserService userService = new UserService();
        [TestMethod()]
        public void AddUserTest()
        {
            for (int i = 0; i < 10; i++)
            {
                var data = userService.AddUser($"1312378925{i}", "000", $"232201304{i}@qq.com", "李瑞森");
                Assert.AreEqual(200, data.status);
            }
        }

        [TestMethod()]
        public void RetrievePwdTest()
        {
            var data = userService.RetrievePwd("13123789256", "123");
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void CheckLoginTest()
        {
            var data = userService.CheckLogin("13123789256", "123");
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void RetrievePwdTest1()
        {
            var data = userService.RetrievePwd("李瑞森", "2322013048@qq.com", "13123789258", "000");
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void GetUserByIDTest()
        {
            var data = userService.GetUserByID(1);
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void EditUserTest()
        {
            var data = userService.EditUser(1, "000", "admin", "www.baidu.com", "liruisen0@163.com");
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var data = userService.GetAll();
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void AdminEditUserTest()
        {
            var data = userService.AdminEditUser(5, "123", true);
            Assert.AreEqual(200, data.status);
        }
    }
}