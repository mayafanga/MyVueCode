using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        //public string Password { get; set; }
        public string uName { get; set; }
        public string uPhoto { get; set; }
        public string uMail { get; set; }
        public string uPhone { get; set; }
        public bool   uAdmin { get; set; }
    }
}
