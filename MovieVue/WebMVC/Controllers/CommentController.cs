using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie.DTO;
using Movie.Service;
using Movie.IService;
using Movie.Service.Entities;
using Newtonsoft.Json;

namespace WebMVC.Controllers
{
    public class CommentController : Controller
    {
        ICommentService comment = new CommentService();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        #region   获取Check为false的评论
        //获取评论
        public ActionResult GetAllComment(int pageSize,int pageIndex)
        {
            var datas = this.GetCommentByCheck().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            int totalCount = this.GetCommentByCheck().ToList().Count(); //总共多少条
            var strResult = "{\"totalCount\":" + totalCount + ",\"list\":" + JsonConvert.SerializeObject(datas) + "}";
            return Json(strResult, JsonRequestBehavior.AllowGet); 
        }
        //获取Check为false 的评论
        public List<CommentDTO> GetCommentByCheck()
        {
            using (MovieDbContext db = new MovieDbContext())
            {
                // var data=db.Comments.Where(c => c.Movies.Id == movieId && c.Check == true &&c.IsDeleted==false).ToList();
                var datas = db.Comments.Where(c => c.Check == false && c.IsDeleted == false).ToList();
                return ToDTOList(datas);
            }
            return null;
        }
        private CommentDTO ToDTO(CommentEntity entity)
        {
            CommentDTO dTO = new CommentDTO();
            dTO.context = entity.Context;
            dTO.id = entity.Id;
            dTO.usersName = entity.Users.UserName;
            dTO.dateTime = entity.CreateDateTime.ToString("yyyy.MM.dd");
            return dTO;
        }
        private List<CommentDTO> ToDTOList(List<CommentEntity> list)
        {
            List<CommentDTO> dTOs = new List<CommentDTO>();
            foreach (var item in list)
            {
                var data = this.ToDTO(item);
                dTOs.Add(data);
            }
            return dTOs;
        }
        #endregion
       
        //把check为0且符合的评论置为1
        public ActionResult EditComment(int commentId, bool check)
        {
            string jsonTxt = "";
            ResponseMsg res = comment.EditComment(commentId, check);
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + res.status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + res.msg + "\"" + "}";
            return Content(jsonTxt);
        }
        //删除评论
        public ActionResult DelComment(int Id)
        {
            string jsonTxt = "";
            string status = "";
            string msg = "";
            //isDel的值为true 说明此评论被删除
            using (MovieDbContext db = new MovieDbContext())
            {
                var data = db.Comments.Where(c => c.Id == Id).FirstOrDefault();
                data.IsDeleted = true;
                if (db.SaveChanges() > 0)
                {
                    status = "200";
                    msg = "删除成功";
                }
                else
                {
                    status = "500";
                    msg = "删除失败";
                }
            }
            jsonTxt = "{" + "\"" + "status" + "\"" + ":" + "\"" + status + "\"" + "," + "\"" + "msg" + "\"" + ":" + "\"" + msg + "\"" + "}";
            return Content(jsonTxt);
        }

    }
}