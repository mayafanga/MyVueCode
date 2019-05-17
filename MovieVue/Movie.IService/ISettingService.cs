using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.DTO;

namespace Movie.IService
{
    public interface ISettingService:IServiceSupport
    {
        /// <summary>
        /// 获取首页顶部导航
        /// </summary>
        /// <param name="idendity"></param>
        /// <returns></returns>
        SettingDTO GetHomeHeader(bool idendity);
        /// <summary>
        /// 获取首页轮播图
        /// </summary>
        /// <returns></returns>
        SettingDTO GetSliderImg();

        ResponseMsg SetSliderImg(string ImgString);
    }
}
