using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DTO
{
    public class CommentDTO
    {
        public int id { get; set; }
        public string context { get; set; }
        public string usersName { get; set; }
        public string dateTime { get; set; } 
    }
}
