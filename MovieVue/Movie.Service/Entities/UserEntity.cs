using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movie.Service.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserEntity:BaseEntity
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public string UserMail { get; set; }
        public string UserPhone { get; set; }
        public bool UserAdmin { get; set; } = false;
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}
