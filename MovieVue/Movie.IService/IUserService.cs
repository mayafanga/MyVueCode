using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public  interface IUserService:IServiceSupport
    {
        UserDTO CheckLogin(string PhoneNum, string Password);
        ResponseMsg AddUser(string PhoneNum, string Password, string Email, string UserName);
        ResponseMsg RetrievePwd(string userName, string email, string phone,string newPassword);
        ResponseMsg RetrievePwd(string phone,string newPassword);
        ResponseMsg GetUserByID(int id);
        ResponseMsg EditUser(int id,string password, string name, string photo, string mail);
        List<UserDTO> GetAll();
        ResponseMsg AdminEditUser(int id, string password, bool isdelete);
    }
}
