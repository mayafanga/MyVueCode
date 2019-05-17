<template>
    <div class="app-container">
        <!-- 返回按钮 -->
        <mt-header fixed title="马小马、马小亚、马大芳">
		   <span slot="left" @click="goBack" v-show="flag">
		      <mt-button icon="back">返回</mt-button>
		   </span>
	    </mt-header>
        <!-- 当登陆着是admin时 显示此导航栏 下面的底部导航设为空 -->
        <!-- 首页内容区 -->
           <transition mode="out-in">
             <router-view></router-view>
           </transition>
        <!-- 底部导航兰 -->
        <nav class="mui-bar mui-bar-tab">
			<router-link class="mui-tab-item1" to="/home">
				<span class="mui-icon mui-icon-home"></span>
				<span class="mui-tab-label">首页</span>
			</router-link>
			<router-link class="mui-tab-item1" to="/personal">
				<span class="mui-icon mui-icon-contact"></span>
				<span class="mui-tab-label">个人信息</span>
			</router-link>
            <router-link class="mui-tab-item1" to="/jump">
				<span class="mui-icon mui-icon-contact"></span>
				<span class="mui-tab-label">注册或登录</span>
			</router-link>
	    </nav>
    </div>
</template>
<script>
export default {
    data(){
        return {
            flag:false
        }
    },
    created(){
      this.flag=this.$route.path==="/home"?false:true;
    },
    methods:{
        goBack(){
            //点击后退
            this.$router.go(-1);
        }
    },
    watch:{
        '$route.path':function(newVal){
            if(newVal==="/home"){
                this.flag=false;
            }else{
                this.flag=true;
            }
        }
    }
}
</script>
<style>
.mint-header{
	z-index: 99;
}
  .app-container{
      padding-top:40px;
      padding-bottom:50px;
      overflow-x:hidden;
  }
  .v-enter{
      opacity:0;
      transform:translateX(-100%);
      position:absolute;
  }
  .v-enter-active,
  .v-leave-active{
      transition:all 0.5s ease;
  }
  /* 该类名用来解决 tabbar 无法点击切换的问题 */
  .mui-bar-tab .mui-tab-item1 .mui-active{
      color:#007aff;
  }
  .mui-bar-tab .mui-tab-item1{
      display:table-cell;
      overflow:hidden;
      width:100%;
      height:50px;
      text-align:center;
      vertical-align:middle;
      white-space:nowrap;
      text-overflow:ellipsis;
      color:#929292;
  }
  .mui-bar-tab .mui-tab-item1 .mui-icon{
      top:3px;
      width:24px;
      height:24px;
      padding-top:0;
      padding-bottom:0;
  }
  .mui-bar-tab .mui-tab-item1 .mui-icon~.mui-tab-label{
      font-size:11px;
      display:block;
      overflow:hidden;
      text-overflow:ellipsis;
  }
</style>

