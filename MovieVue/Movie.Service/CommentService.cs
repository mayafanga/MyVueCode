using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;
using Movie.IService;
using Movie.Service.Entities;

namespace Movie.Service
{
    public class CommentService : ICommentService
    {
        public ResponseMsg AddComment(int movieId, int userId, string context)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
                    var movie = db.Movies.Where(m => m.Id == movieId).SingleOrDefault();
                    if (user != null && movie != null)
                    {
                        CommentEntity comment = new CommentEntity();
                        comment.Users = user;
                        comment.Movies = movie;
                        comment.Context = context;
                        db.Comments.Add(comment);
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "评论成功";
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "评论失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "评论失败,未找到电影或用户";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 200;
                responseMsg.msg = "评论失败，服务器出错";
                //throw;
            }
            return responseMsg;
        }

        public ResponseMsg EditComment(int commentId, bool check)
        {
            ResponseMsg msg = new ResponseMsg();
            using(MovieDbContext db=new MovieDbContext())
            {
                BaseService<CommentEntity> baseService = new BaseService<CommentEntity>(db);
                var comment=baseService.GetById(commentId);
                if (comment!=null)
                {
                    comment.Check = check;
                    if (db.SaveChanges()>0)
                    {
                        msg.status = 200;
                        msg.msg = "修改成功";
                        msg.data = this.ToDTO(comment);
                    }
                    else
                    {
                        msg.status = 501;
                        msg.msg = "修改失败";
                    }
                }
                else
                {
                    msg.status = 406;
                    msg.msg = $"找不到Id为{commentId}的评论";
                }
            }
            return msg;
        }

        public List<CommentDTO> GetComment(int movieId)
        {
            using (MovieDbContext db=new MovieDbContext())
            {
                var data=db.Comments.Where(c => c.Movies.Id == movieId && c.Check == true &&c.IsDeleted==false).ToList();
                //var data=db.Comments.Include("Movies").Where(c => c.Movies.Id == movieId && c.Check == true && c.IsDeleted == false).ToList();
                
                return this.ToDTOList(data);
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
    }
}
