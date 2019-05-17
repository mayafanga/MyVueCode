<template>
    <div>
        <!-- 轮播图 -->
        <div class="slider">
          <mt-swipe :auto="4000">
             <mt-swipe-item v-for="item in ImgSlider" :key="item">
                 <img :src="item" alt="" :isfull="true">
             </mt-swipe-item>
          </mt-swipe>
        </div>
        <!-- 好影推荐 -->
        <div class="movie-rec movierecommend">好影推荐</div>
        <div>
            <ul class="mui-table-view mui-grid-view mui-grid-9">
		        <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3" v-for="item in movieList" :key="item.Id">
                    <a @click="goMovieDetail(item.Id)">
		               <img :src="item.mImg" alt="" style="width:100px;height:100px;">
		               <div class="mui-media-body">{{item.mName}}</div>
                       <span class="mui-media-contentp">{{item.mIntroduec}}</span>
                    </a>
                </li>
		   </ul>
           <div class="getmore">
              <input type="button" value="换一批" class="getanother" @click="changeMovie">
           </div> 
        </div>
        <!-- 好影上榜 -->
        <div class="movie-rec movieonlist">好影上榜</div>
        <div>
            <ul class="mui-table-view mui-grid-view mui-grid-9">
                 <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3" v-for="item in movieRankList" :key="item.Id">
                    <a @click="goMovieDetail(item.Id)">
		               <img :src="item.mImg" alt="" style="width:100px;height:100px;">
		               <div class="mui-media-body">{{item.mName}}</div>
                       <span class="mui-media-contentp">{{item.mIntroduec}}</span>
                    </a>
                </li>
		   </ul>
        </div>
        <!-- 热门新闻 -->
        <div class="movie-rec hotnews">热门新闻</div>
        <div>
            <ul class="mui-table-view">
				<li class="mui-table-view-cell mui-media" v-for="item in newsList" :key="item.Id">
					<a @click="goNewsDetail(item.Id)">
						<img class="mui-media-object mui-pull-left" :src="item.aImg" style="width:42px;height:42px;">
						<div class="mui-media-body">
							<h1>{{item.aTitle}}</h1>
							<p class='mui-ellipsis'>
                              {{item.aContext}}
                            </p>
						</div>
					</a>
				</li>
		    </ul>
            <div class="getmore">
              <input type="button" value="换一批" class="getanother" @click="changeNews">
           </div> 
        </div>
    </div>
</template>
<script>
import {Toast} from "mint-ui";
export default {
    data(){
        return{
            ImgSlider:{},
            movieList:[],
            movieRankList:[],
            newsList:[],
            pageIndex:5, //每页显示多少条
            pageSize:1,    //当前页数
            moviestartnum:0,
            movieendnum:3
        }
    },
    created(){
     this.getSlider();
     this.getMovieRecommend();
     this.getMovieRank();
     this.getNewsRecommend();
    },
    methods:{
        getSlider(){  //获取轮播图
           this.$http.get("home/slider").then(result=>{  //获取数据失败
               if(result.body.status===200){
                  this.ImgSlider=JSON.parse(result.body.data).img;
               }else{
                   Toast('获取数据失败');
               } 
            })
        },
        getMovieRecommend(){  //获取好影推荐
            this.$http.get("movie/movieRecommend").then(result=>{
               if(result.body.status===200){
                   for(var i=this.moviestartnum;i<this.movieendnum;i++){
                       this.movieList.push(result.body.data[i]);
                   }
               }else{
                   Toast('获取数据失败');
               } 
            })
        },
        getMovieRank(){  //获取影上榜
           this.$http.get("movie/movieRank").then(result=>{
               if(result.body.status===200){
                   for(var i=0;i<3;i++){
                       this.movieRankList.push(result.body.data[i]);
                   }
               }else{
                   Toast('获取数据失败');
               }
           })
        },
        getNewsRecommend(){  //获取新闻推荐
            this.$http.get("news/newsRecommend?pageIndex="+this.pageIndex+"&pageSize="+this.pageSize).then(result=>{
                if(result.body.status===200){
                  this.newsList=result.body.data;
                }else{
                    Toast('获取数据失败');
                }
            })
        },
        goMovieDetail(id){  //转跳到详细电影页
            this.$router.push({name:'movieinfo',params:{id}});
        },
        goNewsDetail(id){  //转跳到详细新闻页
            this.$router.push({name:'newsinfo',params:{id}});
        },
        changeMovie(){  //电影的换一批功能
           this.moviestartnum=this.moviestartnum+3;
           this.movieendnum=this.movieendnum+3;
           this.movieList=[];
           this.getMovieRecommend();
        },
        changeNews(){  //新闻的换一批功能
            this.pageSize++;
            this.getNewsRecommend();
        }
    }
}
</script>
<style>
    .mint-swipe{
        height:200px;
    }
    .mint-swipe .mint-swipe-item{
        text-align: center;
    }
    .mint-swipe .mint-swipe-item:nth-child(1){
        background-color: red;
    }
    .mint-swipe .mint-swipe-item:nth-child(2){
        background-color: skyblue;
    }
    .mint-swipe .mint-swipe-item:nth-child(3){
        background-color: aqua;
    }
    .mui-grid-view .mui-grid-9{
       background-color: #fff;
       border:none;
       height:150px;
   }
   .mui-grid-view > .mui-grid-9 > .mui-media > img{
       width: 50%;
       height:10%; 
   }
   .mui-grid-view .mui-grid-9 .mui-media-body{
        font-size: 13px;
    }
    .mui-table-view .mui-table-view-cell a .mui-media-contentp{
        display: inline-block;
        font-size:13px;
        padding:5px;
    }
   .mui-grid-view .mui-grid-9 .mui-table-view-cell{
       height:150px;
       border: 0;
   }
    .mui-table-view .mui-table-view-cell{
        border:none;
    }
    .mui-table-view li h1{
        font-size:14px;
    }
    .mui-table-view li .mui-ellipsis{
        font-size: 13px;
        color:#797979;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
    }
    .movie-rec{
        width:100%;
        font-size:16px;
        font-weight:bolder;
        padding: 0 0 0 10px;
    }
    .movierecommend{
        margin-top:5px;
    }
    .getmore{
        width:50%;
        height:30px; 
        margin: 5px auto;
    }
    .getmore .getanother{
        width:100%;
        height:30px;
        font-size:14px;
    }
</style>