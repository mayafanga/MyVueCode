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
    public class MailServiceTest
    {
        IMailService mailService = new MailService();
        [TestMethod()]
        public void FeedbackMailTest()
        {
            for (int i = 6; i <= 10; i++)
            {
                var data = mailService.FeedbackMail(i, 1, $"Title{i}", $"这是Title{i}的正文，是由{i}发给管理员的反馈信");
                Assert.AreEqual(200, data.status);
            }
            for (int i = 6; i <= 10; i++)
            {
                var data = mailService.FeedbackMail(1, i, $"Title{i + 5}", $"这是Title{i + 5}的正文，是由管理员发给Title{i}的解决信");
                Assert.AreEqual(200, data.status);
            }
        }

        [TestMethod()]
        public void FeedbackMailTest1()
        {
            var data = mailService.FeedbackMail(12, 1, $"Title12", $"这是Title12的正文，是由用户12发给管理员的反馈信");
            Assert.AreEqual(406, data.status);
        }
    }
}