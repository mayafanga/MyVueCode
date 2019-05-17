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
    public class UserController : Controller
    {
        IUserService user = new UserService();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //获取所有用户 
        public ActionResult GetAllUser(int pageSize, int pageIndex)
        {
            var datas = user.GetAll().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            int totalCount = user.GetAll().ToList().Count(); //总共多少条
            var strResult = "{\"totalCount\":" + totalCount + ",\"list\":" + JsonConvert.SerializeObject(datas) + "}";
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }
        //根据Id获取用户
        public ActionResult GetUserById(int id)
        {
            var res =  user.GetUserByID(id);
            var jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "," + "\"" + "data" + "\"" + ":" + "\"" + JsonConvert.SerializeObject(res.data) + "\"" + "}";
            return Content(jsonTxt);
        }
        //管理员修改用户密码
        public ActionResult EditUserPwd(int id, string password, bool isdelete)
        {
            ResponseMsg res = user.AdminEditUser(id, password, isdelete);
            var jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
    }
}