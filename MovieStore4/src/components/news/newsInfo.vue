<template>
    <div>
        <div class="newsinfo-container">
        <h3 class="title">{{newsInfo.aTitle}}</h3>
        <p class="subtitle">
            <span>发表时间：{{newsInfo.aTime|dateFormat}}</span>
        </p>
        <hr>
        <div class="content">
            <p>
                {{newsInfo.aContext}}
            </p>
        </div>
    </div>
    </div>
</template>
<script>
export default {
    data(){
        return{
            id:this.$route.params.id,
            newsInfo:{}
        }
    },
    created(){
       this.getNewsInfo();
    },
    methods:{
       getNewsInfo(){  // 获取新闻详细信息
           this.$http.get('news/get?id='+this.id).then(result=>{
               if(result.body.status===200){
                   this.newsInfo=result.body.data;
               }
           })  
       }
    }
}
</script>
<style>
    .newsinfo-container{
        padding:0 4px;
    }
    .newsinfo-container .title{
        font-size: 16px;
        text-align: center;
        margin: 15px 0;
        color: red;
    }
    .newsinfo-container .subtitle{
        font-size: 13px;
        color:#226aff;
    }
    .newsinfo-container .content img{
        width:100%;
    }
</style>