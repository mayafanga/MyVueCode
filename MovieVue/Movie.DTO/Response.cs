using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DTO
{
    public class ResponseMsg
    {
        public int status { get; set; }
        public string msg { get; set; }
        public Object data { get; set; }
    }
}
