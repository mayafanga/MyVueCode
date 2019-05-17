using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public interface IArticleService: IServiceSupport
    {
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="title"></param>
        /// <param name="context"></param>
        /// <param name="imgSrc"></param>
        /// <returns></returns>
        ResponseMsg addArticle(string title, string context,string imgSrc);
        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        ResponseMsg updateArticle(int id, string title, string context);
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseMsg deleteArticle(int id);
        /// <summary>
        /// 获取推荐的新闻
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<NewsDTO> GetRecommendArticle(int pageIndex,int pageSize);
        /// <summary>
        /// 获取所有的新闻
        /// </summary>
        /// <returns></returns>
        List<NewsDTO> GetAllNews();
        /// <summary>
        /// 获取指定Id的新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        NewsDTO GetNewsById(int id);
    }
}
