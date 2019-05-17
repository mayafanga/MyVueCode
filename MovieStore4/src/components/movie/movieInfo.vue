<template>
    <div>
        <!-- 电影视频 -->
        <div class="movie-video">
            <video controls autoplay loop>
                <source src="https://www.bilibili.com/video/av23470615/?p=3" type="video/mp4">
            </video>
        </div>
       <!-- 电影信息 -->
       <div class="movie-content">
          <h3>{{movieInfo.mName}}</h3>
          <div class="liketime">
             <p> <a href="#" class="icon-a" @click="addSuppose"><span class="glyphicon glyphicon-thumbs-up"></span></a>
              <span>点赞数:{{movieInfo.mSuppose}}</span></p>
              <span>上传时间：{{movieInfo.mTime|dateFormat}}</span>
          </div>
          <div class="movie-context">
              <p>{{movieInfo.mIntroduce}}</p>
          </div>
       </div>
       <!-- 电影评论 -->
       <div class="cmt-container">
          <h3>发表评论</h3>
          <hr>
          <textarea placeholder="请输入你的评论(最多吐槽120字)" maxlength="120" v-model="msg"></textarea>
          <mt-button type="primary" size="large" @click="postComment">发表评论</mt-button>

          <div class="cmt-list">
             <div class="cmt-item" v-for="(item,i) in commentList" :key="item.id">
                 <div class="cmt-title">
                     第{{i+1}}楼&nbsp;&nbsp;用户：{{item.usersName}}&nbsp;&nbsp;发表时间：{{item.dateTime|dateFormat}}
                 </div>
                 <div class="cmt-body">
                    {{item.context==='undefined'?'没有任何评论':item.context}}
                 </div>
             </div>    
          </div> 
       </div>
    </div>
</template>
<script>
import { Toast } from "mint-ui";
export default {
    data(){
        return{
            id:this.$route.params.id,
            movieInfo:{},
            commentList:[],
            msg:'',
        }
    },
    created(){
      this.getMovieInfo();
      this.getComment();
    },
    methods:{
      getMovieInfo(){  //获取电影详细信息
         this.$http.get('movie/get?id='+this.id).then(result=>{
             if(result.body.status===200){
                 this.movieInfo=result.body.data;
             }else{
                 Toast('获取数据失败');
             }
         })
      },
      addSuppose(){  //点赞功能
          this.$http.get('movie/movieSuppose?id='+this.id).then(result=>{
              console.log(result);
              if(result.body.status===200){
                  Toast('感谢你的点赞');
                  this.getMovieInfo();
              }else{
                  Toast('点赞前请先登录呦');
              }
          })
      },
      getComment(){   //获取评论
        this.$http.get('comment/GetMovieComment?id='+this.id).then(result=>{
            if(result.body.status===200){
                this.commentList=this.commentList.concat(result.body.data);
            }else{
                Toast('获取评论失败');
            }
        })
      },
      postComment(){  //发送评论 
         if(this.msg.trim().length===0){
             Toast('评论不能为空');
         }
          this.$http.post('comment/movieComment',{"mId":this.id,"context":this.msg.trim()}).then(result=>{
              if(result.body.status===200){
                  var cmt={
                     "id": 0,
                     "context": this.msg.trim(),
                     "usersName": "匿名用户",
                     "dateTime": Date.now()
                  };
                  this.commentList.unshift(cmt);
                  this.msg="";
              }else{
                  Toast('评论失败');
              }
          })
      }
    }
}
</script>
<style>
    .cmt-container h3{
        font-size: 18px;
    }
     .cmt-container textarea{
         font-size: 14px;
         height:85px;
         margin:0;
     }
    .cmt-container .cmt-list{
          margin: 5px 0;
    }
    .cmt-container .cmt-list .cmt-item{
        font-size: 13px;
    }
    .cmt-container .cmt-list .cmt-item .cmt-title{
        line-height: 30px;
        background-color: #ccc;
    }
    .cmt-container .cmt-list .cmt-item .cmt-body{
        line-height: 35px;
        text-indent: 2em;
    }
    .movie-video{
        width:100%;
        background: #000;
        position: relative;
        padding-bottom: 56.25%;
        height:0;
    }
    .movie-video video{
        position:absolute;
        top:0;
        left:0;
        width:100%;
        height: 100%;
    }
    .movie-content{
        width:100%;
        padding:10px 0 0 0;
    }
    .movie-content h3{
        padding-left: 20px;
        font-size: 14px;
    }
     .movie-content .liketime p{
         display: inline-block;
     }
     .movie-content .liketime{
         padding: 0 20px;
         display: flex;
         justify-content:space-between;
     }
    .movie-content .liketime .icon-a{
        text-decoration: none;
        display: inline-block;
        color:rgb(143, 141, 141);
        width:20px;
        height:20px;
        text-align: center;
        line-height: 20px;
    }
      .movie-content .liketime .icon-a:hover{
        color:red;
    }
</style>