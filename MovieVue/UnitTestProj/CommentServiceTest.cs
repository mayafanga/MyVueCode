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
    public class CommentServiceTest
    {
        ICommentService commentService = new CommentService();
        [TestMethod()]
        public void AddCommentTest()
        {
            var data = commentService.AddComment(1, 2, "这电影太励志了！");
            Assert.AreEqual(200, data.status);
        }

        [TestMethod()]
        public void GetCommentTest()
        {
            var data = commentService.GetComment(1);
            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void EditCommentTest()
        {
            var data = commentService.EditComment(2, true);
            Assert.AreEqual(200, data.status);
        }
    }
}