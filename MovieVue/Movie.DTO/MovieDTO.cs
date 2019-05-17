using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public DateTime? mTime { get; set; }
        public string mName { get; set; }
        public string mImg { get; set; }
        public string mPath { get; set; }
        public int? mSuppose { get; set; }
        public int? mNumDownLoad { get; set; }
        public bool mMainPage { get; set; }
        public string mIntroduec { get; set; }
    }
}
