using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Movie.IService;
using Movie.Service;
using WebAPP.Attributes;
using WebAPP.Models.RequerModel;
using Movie.DTO;

namespace WebAPP.Controllers
{
    public class MovieController : ApiController
    {
        IMovieService movieService = new MovieService();
       

        // GET api/<controller>/5
        public ResponseMsg Get(int id)
        {
            var movie = movieService.GetMovieById(id);
            if (movie!=null)
            {
                return new ResponseMsg() { status = 200, msg = "获取成功", data = movie };
            }
            return new ResponseMsg() { status = 406, msg = "获取失败,找不到指定数据" };
        }

       
        [HttpGet]
        public ResponseMsg movieRecommend()
        {
            int pageIndex = 9, pageSize = 1;
            return this.movieRecommend(pageIndex, pageSize);
        }
        [HttpGet]
        public ResponseMsg movieRecommend(int pageIndex, int pageSize)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            pageIndex = pageIndex == 0 ? 9 : pageIndex;
            pageSize = pageSize == 0 ? 1 : pageSize;

            var movieList = movieService.GetMovieRecommed(pageIndex, pageSize);
            if (movieList.Count>0)
            {
                responseMsg.status = 200;
                responseMsg.msg = "获取成功";
                responseMsg.data = movieList;
            }
            else
            {
                responseMsg.status = 406;
                responseMsg.msg = "获取失败，数据库中不存在所需数据";
                responseMsg.data = movieList;
            }
            return responseMsg;
        }
        [HttpGet]
        public ResponseMsg movieRank()
        {
            ResponseMsg responseMsg = new ResponseMsg();
            var movieList = movieService.GetMovieRank();
            if (movieList.Count > 0)
            {
                responseMsg.status = 200;
                responseMsg.msg = "获取成功";
                responseMsg.data = movieList;
            }
            else
            {
                responseMsg.status = 406;
                responseMsg.msg = "获取失败，数据库中不存在所需数据";
                responseMsg.data = movieList;
            }
            return responseMsg;
        }
        [HttpGet]
        public ResponseMsg getAll(int pageIndex,int pageSize)
        {
            var datas = movieService.GetAll().Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
            return new ResponseMsg{ status = 200, msg = "获取成功", data = datas };
        }
        [HttpGet,ApiAuthorize]
        public ResponseMsg movieSuppose(int id)
        {
            return movieService.movieSuppose(id);
        }
        [HttpGet, ApiAuthorize]
        public ResponseMsg movieDownLoad(int id)
        {
            return movieService.movieDownLoad(id);
        }
    }
}