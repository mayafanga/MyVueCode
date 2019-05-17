using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Service;
using Movie.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Common;

namespace Movie.Service.Tests
{
    [TestClass()]
    public class MovieDbContextTests
    {
        [TestMethod()]
        public void MovieDbContextTest()
        {
            MovieDbContext movieDbContext = new MovieDbContext();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MovieDbContext>());
            #region 初始化
            var user = movieDbContext.Users.Where(u => u.Id == 1).SingleOrDefault();
            if (user==null)
            {
                UserEntity userEntity = new UserEntity();
                userEntity.UserName = "admin";
                userEntity.Password = "admin".CalcMD5();
                userEntity.UserPhone = "admin";
                userEntity.UserAdmin = true;
                movieDbContext.Users.Add(userEntity);
            }
            var admin = movieDbContext.Settings.Where(s => s.Name == "AdminHeaderSrc").SingleOrDefault();
            if (admin==null)
            {
                SettingEntity AdminHeaderSrc = new SettingEntity();
                AdminHeaderSrc.Name = "AdminHeaderSrc";
                AdminHeaderSrc.Value = "管理员头部";
                movieDbContext.Settings.Add(AdminHeaderSrc);

            }
            var publicheader = movieDbContext.Settings.Where(s => s.Name == "PublicHeaderSrc").SingleOrDefault();
            if (publicheader ==null)
            {
                SettingEntity PublicHeaderSrc = new SettingEntity();
                PublicHeaderSrc.Name = "PublicHeaderSrc";
                PublicHeaderSrc.Value = "普通用户头部";
                movieDbContext.Settings.Add(PublicHeaderSrc);

            }
            var img= movieDbContext.Settings.Where(s => s.Name == "SliderImg").SingleOrDefault();
            if (img==null)
            {
                SettingEntity SliderImg = new SettingEntity();
                SliderImg.Name = "SliderImg";
                SliderImg.Value = "轮播图";
                movieDbContext.Settings.Add(SliderImg);
            }
            #endregion
            int a = movieDbContext.SaveChanges();
            Assert.AreNotEqual(0, a);
        }
    }
}