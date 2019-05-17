using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Entities
{
    /// <summary>
    /// 站内信
    /// </summary>
    public class MailEntity:BaseEntity
    {
        public string MailToUser { get; set; }
        public string MailFromUser { get; set; }
        public string MailTitle { get; set; }
        public string MailContext { get; set; }
    }
}
