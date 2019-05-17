using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Movie.Service.Entities
{
    /// <summary>
    /// 电影
    /// </summary>
    public class MovieEntity:BaseEntity
    {
        public string MovieName { get; set; }
        public string MovieImg { get; set; }
        public string MoviePath { get; set; }
        public int? MovieNumSuppose { get; set; } = 0;
        public int? MovieNumDowndload { get; set; } = 0;
        public bool MovieMainPage { get; set; } = false;
        public string MovieIntroduct { get; set; }        
        public DateTime? MovieTime { get; set; }
        public virtual ICollection<CommentEntity> Comment { get; set; } 
    
    }
}
