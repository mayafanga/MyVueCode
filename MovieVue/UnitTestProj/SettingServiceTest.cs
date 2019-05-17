using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.IService;
using Movie.DTO;

namespace Movie.Service.Tests
{
    [TestClass()]
    public class SettingServiceTest
    {
        ISettingService settingService = new SettingService();
        [TestMethod()]
        public void GetSliderImgTest()
        {
            var data = settingService.GetSliderImg();
            Assert.AreEqual("SliderImg", data.SettingName);
        }

        [TestMethod()]
        public void GetHomeHeaderTest()
        {
            var admin = settingService.GetHomeHeader(true);
            Assert.AreEqual("AdminHeaderSrc", admin.SettingName);
            var publicer = settingService.GetHomeHeader(false);
            Assert.AreEqual("PublicHeaderSrc", publicer.SettingName);

        }

        [TestMethod()]
        public void SetSliderImgTest()
        {
            var data = settingService.SetSliderImg("{\"这是新的轮播图\"}");
            Assert.AreEqual(200, data.status);
        }
    }
}