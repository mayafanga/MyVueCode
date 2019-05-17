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
    public class MailService : IMailService
    {
        public ResponseMsg FeedbackMail(int fromId, int toId, string title, string context)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    var user = db.Users.Where(u => u.Id == fromId && u.IsDeleted == false).FirstOrDefault();
                    var admin= db.Users.Where(u => u.Id == toId && u.IsDeleted == false).FirstOrDefault();

                    if (user!=null&&admin!=null)
                    {
                        MailEntity mailEntity = new MailEntity();
                        mailEntity.MailFromUser = fromId.ToString();
                        mailEntity.MailToUser = toId.ToString();
                        mailEntity.MailTitle = title;
                        mailEntity.MailContext = context;
                        db.Mails.Add(mailEntity);
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "发送成功";
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "发送失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "发送失败,发送人或接收人不存在";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "发送失败，服务器处理出错";
                //throw;
            }
            return responseMsg;
        }
    }
}
