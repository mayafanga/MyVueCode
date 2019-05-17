using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie.Service;
using Movie.IService;
using Movie.DTO;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class NewsController : Controller
    {
        IArticleService news = new ArticleService();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        //获取所有新闻
        public ActionResult GetAllNews(int pageIndex, int pageSize)
        {
            var datas = news.GetAllNews().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            int totalCount = news.GetAllNews().ToList().Count(); //总共多少条
            var strResult = "{\"totalCount\":" + totalCount + ",\"list\":" + JsonConvert.SerializeObject(datas) + "}";
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }
        //根据Id获取新闻
        public ActionResult GetNewsById(int id)
        {
            string jsonTxt = "";
            string status = "";
            string msg = "";
            var datas = news.GetNewsById(id);
            if (datas != null)
            {
                status = "200"; msg = "查询成功";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + msg + "\"" + "," + "\"" + "data" + "\"" + ":" + "\"" + JsonConvert.SerializeObject(datas) + "\"" + "}";
            }
            else
            {
                status = "406"; msg = "获取失败,找不到指定数据";
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + msg + "\"" + "}";
            }
            return Json(jsonTxt, JsonRequestBehavior.AllowGet);
        }
        //添加新闻
        public ActionResult AddNews(string title, string context, string imgSrc)
        {
            string jsonTxt = "";
            ResponseMsg res = news.addArticle(title,context,imgSrc);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
        //根据Id删除新闻
        public ActionResult DeleteMews(int id)
        {
            string jsonTxt = "";
            ResponseMsg res = news.deleteArticle(id);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
        public ActionResult UpdateNews(int id, string title, string context)
        {
            string jsonTxt = "";
            ResponseMsg res = news.updateArticle(id, title, context);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);

        }
    }
}