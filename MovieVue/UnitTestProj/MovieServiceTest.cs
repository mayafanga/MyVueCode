using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.IService;
using Movie.DTO;

namespace Movie.Service.Tests
{
    [TestClass()]
    public class MovieServiceTest
    {
        IService.IMovieService movieService = new MovieService();
        [TestMethod()]
        public void GetMovieRecommedTest()
        {
            var data = movieService.GetMovieRecommed(2, 2);
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void AddMovieTest()
        {
            string name = "电影", introduec = "电影简介";
            for (int i = 0; i < 10; i++)
            {
                MovieDTO movieDTO = new MovieDTO();
                movieDTO.mName = $"{name}:{i}";
                movieDTO.mImg = "http://wx4.sinaimg.cn/large/006CBSf2ly1g0qzaasxyfj31hc0ymk0a.jpg";
                movieDTO.mPath = "http://baidu.com";
                movieDTO.mTime = DateTime.Now;
                movieDTO.mIntroduec = $"{name}：{i}的{introduec}";
                var data = movieService.AddMovie(movieDTO.mName, movieDTO.mImg, movieDTO.mPath, movieDTO.mTime, movieDTO.mIntroduec);
                Assert.AreNotEqual(0, data.Id); 
            }
        }

        [TestMethod()]
        public void GetMovieRankTest()
        {
            var data = movieService.GetMovieRank();
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetMovieByIdTest()
        {
            var data = movieService.GetMovieById(1);
            Assert.AreNotEqual(0, data.Id);
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void DeleteMovieTest()
        {
            var data = movieService.DeleteMovie(1);
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var data = movieService.GetAll();
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void EditMovieTest()
        {
            var data = movieService.EditMovie(1, "天降正义", true, "这是一部胡写的电影");
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void movieDownLoadTest()
        {
            var data = movieService.movieDownLoad(1);
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void movieSupposeTest()
        {
            var data = movieService.movieSuppose(1);
            Assert.AreEqual(200, data.status);
        }
    }
}