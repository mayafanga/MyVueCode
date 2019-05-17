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
    public class UserController : ApiController
    {
        IUserService userService = new UserService();
        [HttpGet,ApiAuthorize]
        public ResponseMsg GetUser()
        {
            AuthInfo authInfo = RequestContext.RouteData.Values["auth"] as AuthInfo;
            var user = userService.GetUserByID(authInfo.id);
            return user;
        }
        [HttpPost,ApiAuthorize]
        public ResponseMsg auditSelf(dynamic values)
        {
            AuthInfo authInfo = RequestContext.RouteData.Values["auth"] as AuthInfo;
            string uPassWord = values.uPassWord;
            string uName = values.uName;
            string uPhoto = values.uPhoto;
            string uMail = values.uMail;
            var data = userService.EditUser(authInfo.id, uPassWord, uName, uPhoto, uMail);
            return data;
        }
    }
}