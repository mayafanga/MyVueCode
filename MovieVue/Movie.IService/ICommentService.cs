using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public interface ICommentService:IServiceSupport
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="movieId">电影Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="context">评论正文</param>
        /// <returns></returns>
        ResponseMsg AddComment(int movieId, int userId, string context);
        /// <summary>
        /// 获取指定电影的已检测评论
        /// </summary>
        /// <param name="movieId">电影Id</param>
        /// <returns></returns>
        List<CommentDTO> GetComment(int movieId);
        /// <summary>
        /// 修改评论状态
        /// </summary>
        /// <param name="commentId">评论</param>
        /// <param name="check">状态</param>
        /// <returns>修改状态及修改后的评论信息</returns>
        ResponseMsg EditComment(int commentId, bool check);
    }
}
