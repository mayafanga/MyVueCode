using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Movie.DTO;
using Movie.IService;
using Movie.Service;
using Movie.Common;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using WebAPP.Models.ReponseModel;
using WebAPP.Models.RequerModel;
using WebAPP.Attributes;
using System.Drawing;
using System.IO;
using System.Web;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using Newtonsoft.Json;

namespace WebAPP.Controllers
{

    public class HomeController : ApiController
    {
        IUserService userService = new UserService();
        ISettingService settingService = new SettingService();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [ApiAuthorize]
        public string GetAuthInfo()
        {
            AuthInfo info = RequestContext.RouteData.Values["auth"] as AuthInfo;
            if (info == null)
            {
                return "获取不到，失败";
            }
            else
                return $"获取到了，Auth的id是 {info.id},Auth{(info.IsAdmin == true ? "是" : "不是")}管理员";
        }


        [HttpPost]
        public ResponseMsg register([FromBody] registerModel data)
        {
            if (!data.uPhone.IsPhone())
            {
                return new ResponseMsg() { status = 406, msg = "注册失败，手机号不合法" };
            }
            return userService.AddUser(data.uPhone, data.uPwd, data.uMail, data.uName);
        }
        [HttpPost]
        public ResponseMsg login([FromBody] loginModel data)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            var user = userService.CheckLogin(data.uPhone, data.uPwd);
            if (user != null)
            {
                AuthInfo authInfo = new AuthInfo() { id = user.Id, IsAdmin = user.uAdmin };
                try
                {
                    const string secret = "To Live is to change the world";
                    //secret需要加密
                    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                    IJsonSerializer serializer = new JsonNetSerializer();
                    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                    var token = encoder.Encode(authInfo, secret);
                    responseMsg.status = 200; responseMsg.msg = "登录成功"; responseMsg.data = token;
                }
                catch (Exception)
                {
                    responseMsg.status = 500; responseMsg.msg = "登录失败，服务器处理出错";
                    //throw;
                }

                return responseMsg;
            }
            return new ResponseMsg() { status = 406, msg = "登录失败，请检查手机号和密码是否正确" };

        }

        [HttpPost]
        public ResponseMsg retrievePwd([FromBody] retrievePwdModel data)
        {
            if (data.uPhone.IsPhone())
            {
                return userService.RetrievePwd(data.uName, data.uEmail, data.uPhone, data.uNewPwd);
            }
            return new ResponseMsg() { status = 406, msg = "手机号不合法" };
        }
        [HttpGet]
        [ApiAuthorize]
        public ResponseMsg header()
        {
            var authInfo = RequestContext.RouteData.Values["auth"] as AuthInfo;
            var head = settingService.GetHomeHeader(authInfo.IsAdmin);
            if (head != null)
            {
                return new ResponseMsg() { status = 200, msg = "获取成功", data = head.SettingValue };
            }
            return new ResponseMsg() { status = 501, msg = "数据库未查询到指定数据" };
        }
        [HttpGet]
        public ResponseMsg slider()
        {
            var slider = settingService.GetSliderImg();
            if (slider != null)
            {
                return new ResponseMsg() { status = 200, msg = "获取成功", data = slider.SettingValue };
            }
            return new ResponseMsg() { status = 501, msg = "数据库未查询到指定数据" };
        }
        [HttpPost]
        public ResponseMsg ImgUpload([FromBody] imgs imgs)
        {
            ResponseMsg msg = new ResponseMsg();
            string text = imgs.img;
            byte[] arr2 = Convert.FromBase64String(text.Substring(text.IndexOf("base64,") + 7).Trim('\0'));
            QiniuHelper qiniuHelper = new QiniuHelper();
            string result = qiniuHelper.PictureUpLoad(arr2, "MovieVue");
            if (!string.IsNullOrEmpty(result))
            {
                msg.data = $"http://superforest.cn/{ result}";
                msg.status = 200;
                msg.msg = "上传成功";
            }
            else
            {
                msg.status = 500;
                msg.msg = "上传失败";
            }
            return msg;
        }
        [HttpPost]
        public ResponseMsg PictureUpLoad()
        {
            ResponseMsg msg = new ResponseMsg();
            var file = HttpContext.Current.Request.Files[0];
            //检查文件是否被预览选中,判断方式：通过判断文件名是否为空或者空字符串
            if (!string.IsNullOrWhiteSpace(file.FileName))
            {
                //限定上传图片的格式类型
                string[] LimitPictureType = { ".JPG", ".JPEG", ".GIF", ".PNG", ".BMP" };
                //当图片上被选中时，拿到文件的扩展名
                string currentPictureExtension = Path.GetExtension(file.FileName).ToUpper();
                //此处对图片上传的类型进行限定操作
                if (LimitPictureType.Contains(currentPictureExtension))
                {
                    QiniuHelper qiniuHelper = new QiniuHelper();
                    string result = qiniuHelper.PictureUpLoad(file.InputStream, "MovieVue", currentPictureExtension);
                    if (!string.IsNullOrEmpty(result))
                    {
                        msg.data = $"http://superforest.cn/{ result}";
                        msg.status = 200;
                        msg.msg = "上传成功";
                    }
                    else
                    {
                        msg.status = 500;
                        msg.msg = "上传失败";
                    }
                }
                else
                {
                    msg.status = 406;
                    msg.msg = "上传失败，图片上传操作失败，请选择扩展名为：.JPG, .JPEG, .GIF, .PNG, .BMP 等类型图片。";
                }
            }
            else
            {
                msg.status = 406;
                msg.msg = "上传失败，未检测到图片。";
            }
            return msg;
        }
    }
}