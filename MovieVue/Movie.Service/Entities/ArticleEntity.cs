using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Entities
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleEntity:BaseEntity
    {
        public string ArticleTitle { get; set; }
        public string ArticleContext { get; set; }
        public string ArticleImg { get; set; }
    }
}
