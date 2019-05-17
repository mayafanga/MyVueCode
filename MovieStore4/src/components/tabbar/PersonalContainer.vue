<template>
    <div>
       <div class="personalinfo">
         <ul>
             <li>
                <el-upload
                   class="avatar-uploader"
                   :action="actionUrl"
                   :show-file-list="false"
                   :on-change="getFile">
                   <img v-if="imageUrl" ref="phoUrl" :src="imageUrl" class="avatar">
                   <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                </el-upload>
             </li>
             <li><span>昵称</span><input type="text" v-model="userInfo.uName" disabled="true"></li>
             <li><span>电话号</span><input type="text" v-model="userInfo.uPhone" disabled="true"></li>
             <li><span>邮箱</span><input type="text" v-model="userInfo.uMail" disabled="true"></li>
         </ul>
       </div>
       <div class="cmt-container">
          <h3>回馈信息</h3>
          <hr>
          <p><span>输入标题：</span><input type="text" v-model="mailTitle"></p>
          <textarea placeholder="请输入回馈信息" maxlength="250" v-model="mailMsg"></textarea>
          <mt-button type="primary" size="large" @click="sendMail">点击回馈</mt-button>
       </div>
    </div>
</template>
<script>
import {Toast} from "mint-ui";
export default {
   data() {
      return {
       actionUrl:'',
       imageUrl:'', //上传图片
       userInfo:{},
       mailTitle:'',
       mailMsg:''
      };
    },
    created(){
      this.getUserInfo();
    },
    methods: {
     getUserInfo(){ //获取个人信息
        this.$http.get('user/getuser').then(result=>{

            if(result.body.status===200){
                this.userInfo=result.body.data;
                this.imageUrl=this.userInfo.uPhoto;
                console.log(this.userInfo);
            }else{
              Toast('登录失败');
            }
        })
     },
     sendMail(){ //发送站内信息
       this.$http.post('mail/send',{"mTitle":this.mailTitle,"mContext":this.mailMsg}).then(result=>{
           if(result.body.status===200){
               Toast('感谢你的反馈，我们会继续更好');
           }else{
               Toast('反馈失败');
           }
       })
     },
     getBase64(file){  //把图片转成base64编码
         return new Promise(function(resolve,reject){
             let reader=new FileReader();
             let imgResult="";
             reader.readAsDataURL(file);
             reader.onload=function(){
                 imgResult=reader.result;
             };
             reader.onerror=function(error){
                 reject(error);
             };
             reader.onloadend=function(){
                 resolve(imgResult);
             }
         })
     },
     getFile(file,fileList){  //上传头像
       this.getBase64(file.raw).then(res=>{
           this.$http.post('home/ImgUpload',{"img":res}).then(result=>{
               if(result.body.status===200){
                   this.imageUrl=result.body.data;
               }else{
                   Toast('图片上传失败');
               }
           })
       })
     }
    
    }
}
</script>
<style>
    .personalinfo ul{
       list-style: none;
       width:100%;
       margin:0 0 10px 0;
       padding:0;
    }
    .personalinfo ul li{
        border-bottom: 1px solid #ccc;
        padding:0;
        margin:0;
    }
    .personalinfo ul li:nth-child(1){
        margin-bottom: 10px;
    }
    .personalinfo ul li input{
        width:85%;
        display: inline;
        border:1px solid #fff;
        outline: none;
    }

    .cmt-container h3{
        font-size: 18px;
        font-weight: bolder;
        margin: 0;
    }
    .cmt-container p{
        padding:10px 10px;
    }
    .cmt-container p input{
        width:75%;
        display: inline-block;
        border:1px solid #ccc;
    }
    .cmt-container textarea{
         font-size: 14px;
         height:85px;
         margin:0;
    }
    .avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    width:101px;
    height:101px;
  }
  .avatar-uploader .el-upload:hover {
    border-color: #409EFF;
  }
  .avatar-uploader .el-upload .el-upload__input{
      display: none;
  }
  .avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 100px;
    height: 100px;
    line-height: 100px;
    text-align: center;
  }
  .avatar {
    width: 100px;
    height: 100px;
    display: block;
  }
</style>