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
    public class MailController : ApiController
    {
        IMailService mailService = new MailService();
      
        [HttpPost,ApiAuthorize]
        public ResponseMsg send(dynamic obj)
        {
            string mtitle = obj.mTitle;
            string context = obj.mContext;
            var auth = RequestContext.RouteData.Values["auth"] as AuthInfo;
            var mail = mailService.FeedbackMail(auth.id, 1, mtitle, context);
            return mail;
        }
        
    }
}