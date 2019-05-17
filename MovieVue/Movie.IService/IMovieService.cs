using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public interface IMovieService:IServiceSupport
    {
        MovieDTO AddMovie(string Name,string Img,string Path,DateTime? mTime,string Introduce);
        /// <summary>
        /// 首页推荐
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<MovieRecommendDTO> GetMovieRecommed(int pageIndex, int pageSize);
        List<MovieDTO> GetAll();
        //List<MovieDTO> GetMovieByName();
        MovieDTO GetMovieById(int id);
        /// <summary>
        /// 首页排行榜
        /// </summary>
        /// <returns></returns>
        List<MovieRecommendDTO> GetMovieRank();

        ResponseMsg DeleteMovie(int id);

        ResponseMsg EditMovie(int id,string mName, bool mMainPage, string mIntroduce);
        ResponseMsg movieDownLoad(int id);
        ResponseMsg movieSuppose(int id);

    }
}
