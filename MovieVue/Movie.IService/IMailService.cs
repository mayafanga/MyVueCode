using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public interface IMailService:IServiceSupport
    {
        ResponseMsg FeedbackMail(int fromId,int toId,string title,string context);
    }
}
