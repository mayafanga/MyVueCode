using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Service.Entities;

namespace Movie.Service
{
    public class BaseService<T>where T:BaseEntity
    {
        private MovieDbContext dbContext;
        public BaseService(MovieDbContext movieDb)
        {
            this.dbContext = movieDb;
        }
        #region 获取所有数据
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().Where(e => e.IsDeleted == false);
        }
        #endregion

        #region 根据ID获取实体
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return this.GetAll().Where(e => e.Id == id).SingleOrDefault();
        }
        #endregion

        #region 根据ID删除数据
        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="id">需删除数据的ID</param>
        /// <returns></returns>
        public bool MarkDelete(int id)
        {
            var data = GetById(id);
            if (data!=null)
            {
                data.IsDeleted = true;
                return dbContext.SaveChanges() > 0;
            }
            return false;
            
        } 
        #endregion
    }
}
