using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;
using Movie.IService;
using Movie.Service.Entities;

namespace Movie.Service
{
    public class ArticleService : IArticleService
    {
        #region 添加新闻
        public ResponseMsg addArticle(string title, string context, string imgSrc)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    ArticleEntity articleEntity = new ArticleEntity();
                    articleEntity.ArticleTitle = title;
                    articleEntity.ArticleContext = context;
                    articleEntity.ArticleImg = imgSrc;
                    db.Articles.Add(articleEntity);
                    if (db.SaveChanges() > 0)
                    {
                        responseMsg.status = 200;
                        responseMsg.msg = "添加成功";
                    }
                    else
                    {
                        responseMsg.status = 501;
                        responseMsg.msg = "添加失败，请检查输入内容";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "服务器处理请求出错";
                //throw;
            }
            return responseMsg;
        } 
        #endregion

        //未测试
        public ResponseMsg deleteArticle(int id)
        {
            using(MovieDbContext db=new MovieDbContext())
            {
                BaseService<ArticleEntity> baseService = new BaseService<ArticleEntity>(db);

                try
                {
                    if (baseService.MarkDelete(id))
                    {
                        return new ResponseMsg() { status = 200, msg = "删除成功" };
                    }
                    else
                    {
                        return new ResponseMsg() { status = 406, msg = "删除失败" };

                    }
                }
                catch (Exception)
                {

                    return new ResponseMsg() { status = 406, msg = "删除失败,查无此新闻" };
                }
            }
        }

        #region 通过ID获取新闻
        public NewsDTO GetNewsById(int id)
        {
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<ArticleEntity> baseService = new BaseService<ArticleEntity>(db);
                    var data = baseService.GetAll().Where(a => a.Id == id).FirstOrDefault();
                    return this.ToNewsDTO(data);
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        } 
        #endregion

        #region 获取所有新闻列表
        public List<NewsDTO> GetAllNews()
        {
            List<NewsDTO> newsDTOs = new List<NewsDTO>();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<ArticleEntity> baseService = new BaseService<ArticleEntity>(db);
                    var data = baseService.GetAll().ToList();
                    newsDTOs = this.ToNewsDTOList(data);
                }
            }
            catch (Exception)
            {
                newsDTOs = null;
                //throw;
            }
            return newsDTOs;
        }
        #endregion

        #region 获取推荐的新闻列表
        public List<NewsDTO> GetRecommendArticle(int pageIndex, int pageSize)
        {
            List<NewsDTO> newsRecommendDTOs = new List<NewsDTO>();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<ArticleEntity> baseService = new BaseService<ArticleEntity>(db);
                    var data = baseService.GetAll().OrderBy(m => m.Id).Skip((pageSize - 1) * pageIndex).Take(pageIndex).ToList();
                    newsRecommendDTOs = this.ToNewsDTOList(data);
                }
            }
            catch (Exception)
            {
                newsRecommendDTOs = null;
                //throw;
            }
            return newsRecommendDTOs;
        } 
        #endregion
        ///未测试
        public ResponseMsg updateArticle(int id, string title, string context)
        {
            ResponseMsg msg = new ResponseMsg();
            using(MovieDbContext db=new MovieDbContext())
            {
                BaseService<ArticleEntity> baseService = new BaseService<ArticleEntity>(db);
                var news = baseService.GetById(id);
                if (news != null)
                {
                    news.ArticleTitle = title;
                    news.ArticleContext = context;
                    if (db.SaveChanges() > 0)
                    {
                        msg.status = 200;
                        msg.msg = "修改成功";
                        msg.data = news;
                    }
                    else
                    {
                        msg.status = 501;
                        msg.msg = "修改失败";
                    }
                }else
                {
                    msg.status = 406;
                    msg.msg = "修改失败，查无此对象";
                }
            }
            return msg;
        }

        #region DTO序列化
        private List<NewsDTO> ToNewsDTOList(List<ArticleEntity> entities)
        {
            List<NewsDTO> newsRecommendDTOs = new List<NewsDTO>();
            foreach (var item in entities)
            {
                var newsRecommendDTO = this.ToNewsDTO(item);
                newsRecommendDTOs.Add(newsRecommendDTO);
            }
            return newsRecommendDTOs;
        } 
        private NewsDTO ToNewsDTO(ArticleEntity articleEntities)
        {
            NewsDTO newsRecommendDTO = new NewsDTO();
            newsRecommendDTO.Id = articleEntities.Id;
            newsRecommendDTO.aTitle = articleEntities.ArticleTitle;
            newsRecommendDTO.aImg = articleEntities.ArticleImg;
            newsRecommendDTO.aContext = articleEntities.ArticleContext;
            newsRecommendDTO.aTime = articleEntities.CreateDateTime.ToString("yyyy-MM-dd");
            return newsRecommendDTO;
        }
        
        #endregion
    }
}
