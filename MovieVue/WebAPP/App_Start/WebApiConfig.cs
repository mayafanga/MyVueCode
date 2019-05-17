using Movie.Common;
using Movie.Service;
using Movie.Service.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPP
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            #region 跨域设置
            var allowedMethods = ConfigurationManager.AppSettings["cors_allowedMethods"];
            var allowedOrigin = ConfigurationManager.AppSettings["cors_allowedOrigin"];
            var allowedHeaders = ConfigurationManager.AppSettings["cors_allowedHeaders"];
            var geduCors = new EnableCorsAttribute(allowedOrigin, allowedHeaders, allowedMethods)
            {
                SupportsCredentials = true
            };
            config.EnableCors(geduCors);
            #endregion

            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "VueApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        public static void FirstStar()
        {
            MovieDbContext movieDbContext = new MovieDbContext();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MovieDbContext>());
            #region 初始化
            var user = movieDbContext.Users.Where(u => u.Id == 1).SingleOrDefault();
            if (user == null)
            {
                UserEntity userEntity = new UserEntity();
                userEntity.UserName = "admin";
                userEntity.Password = "admin".CalcMD5();
                userEntity.UserPhone = "18337281836";
                userEntity.UserAdmin = true;
                movieDbContext.Users.Add(userEntity);
            }
            else
            {
                user.UserName = "admin";
                user.Password = "admin".CalcMD5();
                user.UserPhone = "18337281836";
                user.UserAdmin = true;
            }
            var admin = movieDbContext.Settings.Where(s => s.Name == "AdminHeaderSrc").SingleOrDefault();
            if (admin == null)
            {
                SettingEntity AdminHeaderSrc = new SettingEntity();
                AdminHeaderSrc.Name = "AdminHeaderSrc";
                AdminHeaderSrc.Value = "管理员头部";
                movieDbContext.Settings.Add(AdminHeaderSrc);

            }
            var publicheader = movieDbContext.Settings.Where(s => s.Name == "PublicHeaderSrc").SingleOrDefault();
            if (publicheader == null)
            {
                SettingEntity PublicHeaderSrc = new SettingEntity();
                PublicHeaderSrc.Name = "PublicHeaderSrc";
                PublicHeaderSrc.Value = "普通用户头部";
                movieDbContext.Settings.Add(PublicHeaderSrc);

            }
            var img = movieDbContext.Settings.Where(s => s.Name == "SliderImg").SingleOrDefault();
            if (img == null)
            {
                SettingEntity SliderImg = new SettingEntity();
                SliderImg.Name = "SliderImg";
                SliderImg.Value = "轮播图";
                movieDbContext.Settings.Add(SliderImg);
            }
            #endregion
            int a = movieDbContext.SaveChanges();
        }
    }
}
