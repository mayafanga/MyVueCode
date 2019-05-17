using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Service.Entities;
using Movie.IService;
using Movie.DTO;
using Movie.Common;

namespace Movie.Service
{
    public class UserService : IUserService
    {
        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="PhoneNum"></param>
        /// <param name="Password"></param>
        /// <param name="Email"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ResponseMsg AddUser(string PhoneNum, string Password, string Email, string UserName)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {

                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    bool exisits = baseService.GetAll().Any(u => u.UserPhone == PhoneNum);
                    if (exisits)
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "该号码已被注册";
                    }
                    else
                    {
                        UserEntity userEntity = new UserEntity();
                        userEntity.UserPhone = PhoneNum;
                        userEntity.UserName = UserName;
                        userEntity.Password = Password.CalcMD5();
                        userEntity.UserMail = Email;
                        db.Users.Add(userEntity);
                        int result = db.SaveChanges();
                        if (result>0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "注册成功";

                        }else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "注册失败";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseMsg.status = 500;
                responseMsg.msg = "注册失败，后台请求处理异常";
                //responseMsg.msg = ex.Data.ToString();
                throw;
            }
            return responseMsg;

        }
        #endregion

        #region 修改用户
        public ResponseMsg EditUser(int id,string password, string name, string photo, string mail)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var data = baseService.GetById(id);
                    if (data != null)
                    {
                        data.Password = password == null ? data.Password : password.CalcMD5();
                        data.UserName = name == null ? data.UserName : name;
                        data.UserPhoto = photo == null ? data.UserPhoto : photo;
                        data.UserMail = mail == null ? data.UserMail : mail;
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改成功";
                            responseMsg.data = this.ToDTO(data);
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "修改失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "修改失败，未找到指定用户";
                    }
                }
            }
            catch (Exception)
            {

                responseMsg.status = 500;
                responseMsg.msg = "修改失败，服务器处理出错";
                throw;
            }
            return responseMsg;
        } 
        #endregion

        #region 通过ID查找指定用户
        public ResponseMsg GetUserByID(int id)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var data = baseService.GetById(id);
                    if (data != null)
                    {
                        responseMsg.status = 200;
                        responseMsg.msg = "查询成功";
                        responseMsg.data = this.ToDTO(data);
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "未查询到指定ID的用户";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "查询失败，服务器处理出错";
                //throw;
            }
            return responseMsg;
        } 
        #endregion

        #region 通过手机号和密码检查登录状态
        public UserDTO CheckLogin(string PhoneNum, string Password)
        {
            using (MovieDbContext db = new MovieDbContext())
            {
                string password = Password.CalcMD5();
                BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                var user = baseService.GetAll().SingleOrDefault(u => u.UserPhone == PhoneNum && u.Password == password);
                return user == null ? null : ToDTO(user);
            }
        }

        #endregion

        #region 通过用户名、邮箱、手机号找回密码
        /// <summary>
        /// 通过用户名、邮箱、手机号找回密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ResponseMsg RetrievePwd(string userName, string email, string phone, string newPassword)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var user = baseService.GetAll().Where(u => u.UserName == userName && u.UserMail == email && u.UserPhone == phone).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = newPassword.CalcMD5();
                        int result = db.SaveChanges();
                        if (result > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改成功";
                        }else if (result == 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改失败，新密码与原密码相同";
                        }else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "修改失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "用户不存在";
                    }
                }
            }
            catch (Exception ex)
            {
                responseMsg.status = 500;
                responseMsg.msg = "修改失败";
                //throw;
            }
            return responseMsg;

        }
        #endregion

        #region 通过手机号和邮箱验证码找回密码
        /// <summary>
        /// 通过手机号和邮箱验证码找回密码
        /// </summary>
        /// <param name="verifyCode"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public ResponseMsg RetrievePwd(string phone, string newPassword)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var user = baseService.GetAll().Where(u => u.UserPhone == phone).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = newPassword.CalcMD5();
                        int result = db.SaveChanges();
                        if (result > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改成功";
                        }
                        else if (result == 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改失败，新密码与原密码相同";
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "修改失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "修改失败";
                    }
                }
            }
            catch (Exception ex)
            {
                responseMsg.status = 500;
                responseMsg.msg = "修改失败";
                //throw;
            }
            return responseMsg;
        }
        #endregion

        #region DTO序列化
        private UserDTO ToDTO(UserEntity userEntity)
        {
            UserDTO dto = new UserDTO();
            dto.Id = userEntity.Id;
            //dto.Password = userEntity.Password;
            dto.uAdmin = userEntity.UserAdmin;
            dto.uMail = userEntity.UserMail;
            dto.uPhone = userEntity.UserPhone;
            dto.uPhoto = userEntity.UserPhoto;
            dto.uName = userEntity.UserName;

            return dto;
        }
        private List<UserDTO> ToDTOList(List<UserEntity> entities)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var item in entities)
            {
                var data = this.ToDTO(item);
                userDTOs.Add(data);
            }
            return userDTOs;
        }


        #endregion

        #region 获取全部
        public List<UserDTO> GetAll()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var data = baseService.GetAll().ToList();
                    userDTOs = this.ToDTOList(data);
                }
            }
            catch (Exception)
            {
                userDTOs = null;
                //throw;
            }
            return userDTOs;
        }

        #endregion


        public ResponseMsg AdminEditUser(int id, string password, bool isdelete)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<UserEntity> baseService = new BaseService<UserEntity>(db);
                    var user = baseService.GetById(id);
                    if (user != null)
                    {
                        user.Password = password.CalcMD5();
                        user.IsDeleted = isdelete;
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改成功";
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "修改失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "修改出错,查无此人";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "修改出错，服务器出错";
                //throw;
            }
            return responseMsg;
        }

    }
}
