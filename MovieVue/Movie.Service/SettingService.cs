using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;
using Movie.IService;

namespace Movie.Service
{
    public class SettingService : ISettingService
    {
        #region 获取HomeHeader
        public SettingDTO GetHomeHeader(bool idendity)
        {
            SettingDTO recommendDTO = new SettingDTO();
            using (MovieDbContext db = new MovieDbContext())
            {
                BaseService<Movie.Service.Entities.SettingEntity> baseService = new BaseService<Entities.SettingEntity>(db);
                if (idendity)
                {
                    var AdminHeaderSrc = baseService.GetAll().Where(r => r.Name == "AdminHeaderSrc").SingleOrDefault();
                    recommendDTO.SettingName = "AdminHeaderSrc";
                    recommendDTO.SettingValue = AdminHeaderSrc.Value;
                }
                else
                {
                    var PublicHeaderSrc = baseService.GetAll().Where(r => r.Name == "PublicHeaderSrc").SingleOrDefault();
                    recommendDTO.SettingName = "PublicHeaderSrc";
                    recommendDTO.SettingValue = PublicHeaderSrc.Value;
                }
            }
            return recommendDTO;
        }
        #endregion
                        
        #region 获取轮播图
        public SettingDTO GetSliderImg()
        {
            SettingDTO recommendDTO = new SettingDTO();
            using (MovieDbContext db = new MovieDbContext())
            {
                BaseService<Movie.Service.Entities.SettingEntity> baseService = new BaseService<Entities.SettingEntity>(db);
                var SliderImg = baseService.GetAll().Where(r => r.Name == "SliderImg").SingleOrDefault();
                recommendDTO.SettingName = "SliderImg";
                recommendDTO.SettingValue = SliderImg.Value;
            }
            return recommendDTO;
        }
        #endregion

        #region 设置轮播图
        public ResponseMsg SetSliderImg(string ImgString)
        {
            ResponseMsg responseMsg = new ResponseMsg();
            try
            {
                using (MovieDbContext db = new MovieDbContext())
                {
                    BaseService<Entities.SettingEntity> baseService = new BaseService<Entities.SettingEntity>(db);
                    var data = baseService.GetAll().Where(s => s.Name == "SliderImg").FirstOrDefault();
                    data.Value = ImgString;
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
            }
            catch (Exception)
            {
                responseMsg.status = 500;
                responseMsg.msg = "修改失败,服务器处理出错";
                //throw;
            }
            return responseMsg;
        } 
        #endregion
    }
}
