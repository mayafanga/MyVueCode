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
    public class NewsController : ApiController
    {
        IArticleService newsService = new ArticleService();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public ResponseMsg Get(int id)
        {
            var news = newsService.GetNewsById(id);
            if (news!=null)
            {
                return new ResponseMsg()
                {
                    status = 200,
                    msg = "获取成功",
                    data = news
                };
            }
            return new ResponseMsg()
            {
                status = 406,
                msg = "获取失败，找不到指定数据",
                data = news
            };
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public ResponseMsg newsRecommend(int pageIndex,int pageSize)
        {
            int index = pageIndex == 0 ? 5 : pageIndex;
            int size = pageSize == 0 ? 1 : pageSize;
            var newsList = newsService.GetRecommendArticle(index, size);
            if (newsList.Count>0)
            {
                return new ResponseMsg() { status = 200, msg = "获取成功", data = newsList };
            }
            return new ResponseMsg() { status = 406, msg = "获取失败，未查到相应数据" };
        }
    }
}