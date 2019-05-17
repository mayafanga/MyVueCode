using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;
using Movie.Service.Entities;
using Movie.IService;

namespace Movie.Service
{
    public class MovieService : IMovieService
    {
        #region 添加电影
        /// <summary>
        /// 添加电影
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Img"></param>
        /// <param name="Path"></param>
        /// <param name="mTime"></param>
        /// <param name="Introduce"></param>
        /// <returns></returns>
        public MovieDTO AddMovie(string Name, string Img, string Path, DateTime? mTime, string Introduce)
        {
            MovieDTO movieDTO = new MovieDTO();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    MovieEntity movieEntity = new MovieEntity();
                    movieEntity.MovieName = Name;
                    movieEntity.MovieImg = Img;
                    movieEntity.MoviePath = Path;
                    movieEntity.MovieTime = mTime;
                    movieEntity.MovieIntroduct = Introduce;
                    db.Movies.Add(movieEntity);
                    if (db.SaveChanges() > 0)
                    {
                        movieDTO = this.ToDTO(movieEntity);
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return movieDTO;
        }
        #endregion

        #region 获取电影排行榜
        /// <summary>
        /// 获取电影排行榜
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<MovieRecommendDTO> GetMovieRank()
        {
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetAll().OrderByDescending(m => m.MovieNumDowndload + m.MovieNumSuppose).Take(9).ToList();
                    return this.ToRecommendDTOList(data);
                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        } 
        #endregion

        #region 获取首页推荐的电影
        /// <summary>
        /// 获取首页推荐的电影
        /// </summary>
        /// <returns></returns>
        public List<MovieRecommendDTO> GetMovieRecommed(int pageIndex, int pageSize)
        {
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseEntity = new BaseService<MovieEntity>(db);
                    var movieList = baseEntity.GetAll().Where(m => m.MovieMainPage == true).OrderBy(m=>m.Id).Skip((pageSize-1)*pageIndex).Take(pageIndex).ToList();
                    return ToRecommendDTOList(movieList);
                }
            }
            catch (Exception ex)
            {
                //throw;
                return null;
                //throw;
            }
        }
        #endregion

        #region 通过ID获得电影
        public MovieDTO GetMovieById(int id)
        {
            MovieDTO movieDTO = new MovieDTO();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetById(id);
                    movieDTO = this.ToDTO(data);
                }
            }
            catch (Exception)
            {
                movieDTO = null;
                //throw;
            }
            return movieDTO;
        }
        #endregion

        #region DTO序列化
        private MovieDTO ToDTO(MovieEntity movieEntity)
        {
            MovieDTO movieDTO = new MovieDTO();
            movieDTO.Id = movieEntity.Id;
            movieDTO.mName = movieEntity.MovieName;
            movieDTO.mPath = movieEntity.MoviePath;
            movieDTO.mTime = movieEntity.MovieTime;
            movieDTO.mSuppose = movieEntity.MovieNumSuppose;
            movieDTO.mNumDownLoad = movieEntity.MovieNumDowndload;
            movieDTO.mMainPage = movieEntity.MovieMainPage;
            movieDTO.mIntroduec = movieEntity.MovieIntroduct;
            movieDTO.mImg = movieEntity.MovieImg;
            return movieDTO;
        }
        private List<MovieRecommendDTO> ToRecommendDTOList(List<MovieEntity> movieEntities)
        {
            List<MovieRecommendDTO> movieListDTOs = new List<MovieRecommendDTO>();
            foreach (var item in movieEntities)
            {
                MovieRecommendDTO movieListDTO = new MovieRecommendDTO();
                movieListDTO.Id = item.Id;
                movieListDTO.mName = item.MovieName;
                movieListDTO.mPath = item.MoviePath;
                movieListDTO.mTime = item.MovieTime;
                movieListDTO.mSuppose = item.MovieNumSuppose;
                movieListDTO.mNumDownLoad = item.MovieNumDowndload;
                movieListDTO.mIntroduec = item.MovieIntroduct;
                movieListDTO.mImg = item.MovieImg;
                movieListDTOs.Add(movieListDTO);
            }
            return movieListDTOs;
        }
        private List<MovieDTO> ToDTOList(List<MovieEntity> movieEntities)
        {
            List<MovieDTO> lists = new List<MovieDTO>();
            foreach (var item in movieEntities)
            {
                var data = this.ToDTO(item);
                lists.Add(data);
            }
            return lists;
        }
        #endregion


        public List<MovieDTO> GetAll()
        {
            List<MovieDTO> dtoList = new List<MovieDTO>();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetAll().ToList();
                    dtoList = this.ToDTOList(data);
                }
            }
            catch (Exception)
            {
                dtoList = null;
                //throw;
            }
            return dtoList;
        }

        #region 删除电影
        public ResponseMsg DeleteMovie(int id)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    if (baseService.MarkDelete(id))
                    {
                        responseMsg.status = 200;
                        responseMsg.msg = "删除成功";
                    }
                    else
                    {
                        responseMsg.status = 501;
                        responseMsg.msg = "删除失败";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "删除失败，服务器处理出错";
                //throw;
            }
            return responseMsg;
        }


        #endregion

        public ResponseMsg EditMovie(int id,string mName, bool mMainPage, string mIntroduce)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetById(id);
                    if (data != null)
                    {
                        data.MovieName = mName;
                        data.MovieMainPage = mMainPage;
                        data.MovieIntroduct = mIntroduce;
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "修改成功";
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "修改失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = $"参数有误，找不到Id为{id}的电影信息！";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "修改失败，服务器处理出错";
                throw;
            }
            return responseMsg;
        }

        public ResponseMsg movieDownLoad(int id)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetById(id);
                    if (data != null)
                    {
                        data.MovieNumDowndload++;
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = " 下载成功";
                            responseMsg.data = new { mpath = data.MoviePath };
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = " 下载失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = " 下载失败,查无此电影";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = " 下载失败,服务器处理失败";
                //throw;
            }
            return responseMsg;
        }

        public ResponseMsg movieSuppose(int id)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<MovieEntity> baseService = new BaseService<MovieEntity>(db);
                    var data = baseService.GetById(id);
                    if (data != null)
                    {
                        data.MovieNumSuppose++;
                        if (db.SaveChanges() > 0)
                        {
                            responseMsg.status = 200;
                            responseMsg.msg = "点赞成功";
                            responseMsg.data = new { mpath = data.MoviePath };
                        }
                        else
                        {
                            responseMsg.status = 501;
                            responseMsg.msg = "点赞失败";
                        }
                    }
                    else
                    {
                        responseMsg.status = 406;
                        responseMsg.msg = "点赞失败,查无此电影";
                    }
                }
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "点赞失败,服务器处理失败";
                //throw;
            }
            return responseMsg;
        }
    }
}
