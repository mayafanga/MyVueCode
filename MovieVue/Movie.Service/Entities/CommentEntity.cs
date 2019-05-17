using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Entities
{
    /// <summary>
    /// 评论
    /// </summary>
    public class CommentEntity:BaseEntity
    {
        public string Context { get; set; }
        public virtual UserEntity Users { get; set; } 
        public virtual MovieEntity Movies { get; set; }
        public bool Check { get; set; } = false;
    }
}
