using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie.Service;
using Movie.DTO;
using Movie.Service.Entities;
using Movie.IService;
using System.Text;
using Movie.Common;

namespace WebMVC.Controllers
{
    public class HomePageController : Controller
    {
        ISettingService setting = new SettingService();
        public ActionResult Index()
        {
            return View();
        }
        //获取首页轮播
       public ActionResult GetSlider()
        {
            var datas = setting.GetSliderImg();
            if (datas != null)
            {
                ResponseMsg res = new ResponseMsg { status = 200, msg = "获取数据成功", data = datas.SettingValue };
                // return Json(datas, JsonRequestBehavior.AllowGet);
                //ResponseMsg reserror = new ResponseMsg { status = 500, msg = "获取数据失败" };
                return Content(res.data.ToString());
            }
            ResponseMsg reserror1 = new ResponseMsg { status = 500, msg = "获取数据失败" };
            return Content(reserror1.ToString());
        }
        //编辑首页轮播
        public ResponseMsg UpdateSliderImg(string ImgString)
        {
            return setting.SetSliderImg(ImgString);
        }

        //修改轮播图
        public ActionResult UpdateSlider(string sliderImg)
        {
            string strResult = "";
            string status = "";
            string msg = "";
            if (sliderImg != null)
            {
                string ImgString = ImgUpload(sliderImg);
                ResponseMsg res = setting.SetSliderImg(ImgString);
                strResult = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";

            }
            else
            {
                string ImgString = ImgUpload(sliderImg);
                ResponseMsg res = setting.SetSliderImg(ImgString);
                strResult = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            }
            return Content(strResult);
        }
        private string ImgUpload(string sliderImg)
        {
            StringBuilder jsonTxt = new StringBuilder();
            string[] webSliderBase = sliderImg.Split(new char[] { '#' });
            List<string> webAddressList = new List<string>();
            foreach (string text in webSliderBase)
            {
                byte[] arr2 = Convert.FromBase64String(text.Substring(text.IndexOf("base64,") + 7).Trim('\0'));
                QiniuHelper qiniuHelper = new QiniuHelper();
                string result = qiniuHelper.PictureUpLoad(arr2, "MovieVue");
                if (!string.IsNullOrEmpty(result))
                {
                    string savePath = $"http://superforest.cn/{ result}";
                    webAddressList.Add(savePath);
                }
                else
                {
                    string savePath = "";
                    webAddressList.Add(savePath);
                }
            }
            jsonTxt.Append("{\"img\":[");
            foreach (var str in webAddressList)
            {
                jsonTxt.Append("\"" + str + "\",");
            }
            jsonTxt.Remove(jsonTxt.ToString().Length - 1, 1);
            jsonTxt.Append("]}");
            return jsonTxt.ToString();
        }




    }
}