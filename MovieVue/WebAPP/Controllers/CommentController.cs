using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Movie.DTO;
using Movie.IService;
using Movie.Service;
using WebAPP.Attributes;
using WebAPP.Models.ReponseModel;
using WebAPP.Models.RequerModel;

namespace WebAPP.Controllers
{
    public class CommentController : ApiController
    {
        ICommentService commentService = new CommentService();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost,ApiAuthorize]
        public ResponseMsg movieComment([FromBody]dynamic value)
        {
            int moiveId = value.mId;
            string context = value.context;
            AuthInfo authInfo = RequestContext.RouteData.Values["auth"] as AuthInfo;
            var data = commentService.AddComment(moiveId, authInfo.id, context);
            return data;
        }
        [HttpGet]
        public ResponseMsg GetMovieComment(int id)
        {
            ResponseMsg msg = new ResponseMsg();
            var comment = commentService.GetComment(id);
            if (comment.Count>0)
            {
                msg.status = 200;
                msg.msg = "获取成功";
                msg.data = comment;
            }
            else
            {
                msg.status = 406;
                msg.msg = "获取失败";
            }
            return msg;
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}