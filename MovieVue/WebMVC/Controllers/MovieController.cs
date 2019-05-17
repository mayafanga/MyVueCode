using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie.DTO;
using Movie.IService;
using Movie.Service;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class MovieController : Controller
    {
        IMovieService movieService = new MovieService();
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }
        //获取所有电影
        public ActionResult GetAllMovie(int pageSize,int pageIndex)
        {
            var datas = movieService.GetAll().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            int totalCount = movieService.GetAll().ToList().Count(); //总共多少条
            var strResult = "{\"totalCount\":" + totalCount + ",\"list\":" + JsonConvert.SerializeObject(datas) + "}";
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }

        //添加电影
        public ActionResult AddMovie(string Name, string Img, string Path, DateTime? mTime, string Introduce)
        {
            string jsonTxt = "";
            string status = "";
            string msg = "";
            var data = movieService.AddMovie(Name, Img, Path, mTime, Introduce);
            if (data != null)
            {
                status = "200";
                msg = "添加成功";
            }
            else
            {
                status = "500";
                msg = "添加失败";
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + msg + "\"" + "}";
            return Content(jsonTxt);
        }
        //根据Id获取电影
        public ActionResult GetMovieById (int id)
        {
            string jsonTxt = "";
            string status = "";
            string msg = "";
            var movie = movieService.GetMovieById(id);
            if (movie != null)
            {
               ResponseMsg res= new ResponseMsg() { status = 200, msg = "获取成功", data = movie };
                jsonTxt= "{\"status\":\"" + res.status + "\",\"msg\":\"" + res.msg + "\",\"list\":" + JsonConvert.SerializeObject(res.data) + "}";
            }
            else
            {
                ResponseMsg res = new ResponseMsg() { status = 406, msg = "获取失败,找不到指定数据"};
                jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            }
            return Json(jsonTxt, JsonRequestBehavior.AllowGet);
        }
        //删除电影
        public ActionResult DeleteMovieById(int id)
        {
            string jsonTxt = "";
            ResponseMsg res = movieService.DeleteMovie(id);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
        //修改电影
        public ActionResult UpdateMovie(int id, string mName, bool mMainPage, string mIntroduce)
        {
            string jsonTxt = "";
            ResponseMsg res = movieService.EditMovie(id, mName, mMainPage, mIntroduce);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
    }
}