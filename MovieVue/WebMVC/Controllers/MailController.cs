using Movie.DTO;
using Movie.Service;
using Movie.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Movie.IService;

namespace WebMVC.Controllers
{
    public class MailController : Controller
    {
        IUserService user = new UserService();
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }
        #region   获取IsDeleted为false的反馈
        //获取反馈
        public ActionResult GetAllMail(int pageSize, int pageIndex)
        {
            var datas = this.GetMail().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            int totalCount = this.GetMail().ToList().Count(); //总共多少条
            var strResult = "{\"totalCount\":" + totalCount + ",\"list\":" + JsonConvert.SerializeObject(datas) + "}";
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }
        //获取IsDeleted为false的反馈
        public List<MailEntity> GetMail()
        {
            using (MovieDbContext db = new MovieDbContext())
            {
                var datas = db.Mails.Where(m => m.IsDeleted == false).ToList();
                return ToMailDTOList(datas);
            }
            return null;
        }
        private MailEntity ToMailDTO(MailEntity entity)
        {
            MovieDbContext db = new MovieDbContext();
            BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
            MailEntity dTO = new MailEntity();
            dTO.Id = entity.Id;
            dTO.MailContext = entity.MailContext;
            string id = entity.MailFromUser;
            var data = baseService.GetById(int.Parse(id));
            if (data != null)
            {
                if (!String.IsNullOrEmpty(data.UserName))
                {
                    dTO.MailFromUser = data.UserName;
                }
            }
            else
            {
                dTO.MailFromUser = "匿名提交";
            }
            
            dTO.MailTitle = entity.MailTitle;
            return dTO;
        }
        private List<MailEntity> ToMailDTOList(List<MailEntity> list)
        {
            List<MailEntity> dTOs = new List<MailEntity>();
            foreach (var item in list)
            {
                var data = this.ToMailDTO(item);
                dTOs.Add(data);
            }
            return dTOs;
        }
        #endregion
    }
}