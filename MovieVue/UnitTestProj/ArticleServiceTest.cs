using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;
using Movie.IService;

namespace Movie.Service.Tests
{
    [TestClass()]
    public class ArticleServiceTest
    {
        IArticleService articleService = new ArticleService();
        [TestMethod()]
        public void addArticleTest()
        {
            for (int i = 0; i < 10; i++)
            {
                string title = $"{DateTime.Now}将降温{i}", context = $"{DateTime.Now}今天夜里到白白天，全国各地受寒潮影响，将有大范围的降温，请大家及时添加衣物，注意保暖", img = "http://wx4.sinaimg.cn/large/006CBSf2ly1g0qzaasxyfj31hc0ymk0a.jpg";
                var data = articleService.addArticle(title, context, img);
                Assert.AreEqual(200, data.status); 
            }
        }

        [TestMethod()]
        public void GetRecommendArticleTest()
        {
            var data = articleService.GetRecommendArticle(3, 3);
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetAllNewsTest()
        {
            var data = articleService.GetAllNews();
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetNewsByIdTest()
        {
            var data = articleService.GetNewsById(1);
            Assert.IsNotNull(data);
        }
    }
}