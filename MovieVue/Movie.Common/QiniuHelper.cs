using Newtonsoft.Json;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Common
{
    public class QiniuHelper
    {
        private string AccessKey = "OtUptgv0qUHDGCRvMaIy1GghyS4Yws8pQ0qu7y7o";
        private string SecretKey = "pTUScz_4XYr7wFvvGNdDySTooiCCGCVSxBU2amM2";
        /// <summary>
        /// 七牛云图片上传
        /// </summary>
        /// <param name="imgs">文件byte[]</param>
        /// <param name="space">上传文件夹</param>
        /// <returns></returns>
        public string PictureUpLoad(byte[] imgs,string space)
        {
            string guidName = Guid.NewGuid().ToString();
            string suffix = ".jpg";
            string filename = $"{space}/{guidName}{suffix}";
            Mac mac = new Mac(AccessKey, SecretKey);
            string Bucket = "imgs";
            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 设置要上传的目标空间
            putPolicy.Scope = Bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(100);
            // 文件上传完毕后，在多少天后自动被删除
            putPolicy.DeleteAfterDays = 100;
            // 生成上传token
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_East;
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadData(imgs, filename, token, null);
            var data = JsonConvert.DeserializeObject<Response>(result.Text);
            return data.key;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imgs">文件流</param>
        /// <param name="space">上传文件夹</param>
        /// <param name="fileType">文件后缀名</param>
        /// <returns></returns>
        public string PictureUpLoad(Stream imgs,string space, string fileType)
        {
            string guidName = Guid.NewGuid().ToString();
            string suffix = fileType;
            // 上传文件名
            string filename = $"{space}/{guidName}{suffix}";
            Mac mac = new Mac(AccessKey, SecretKey);
            string Bucket = "imgs";
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = Bucket;
            putPolicy.SetExpires(100);
            putPolicy.DeleteAfterDays = 100;
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            config.Zone = Zone.ZONE_CN_East;
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U512K;
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadStream(imgs, filename, token, null);

            var data = JsonConvert.DeserializeObject<Response>(result.Text);
            return data.key;
        }
        private string PictureUpLoadBase()
        {
            return null;
        }
    }
    class Response
    {
        public string hash { get; set; }
        public string key { get; set; }
    }
}
