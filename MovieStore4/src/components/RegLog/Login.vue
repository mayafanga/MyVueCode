<template>
    <div>
		<div class="mui-content">
			<form id='login-form' class="mui-input-group">
				<div class="mui-input-row">
					<label>账号</label>
					<input id='account' type="text" v-model="account" class="mui-input-clear mui-input" placeholder="请输入账号">
				</div>
				<div class="mui-input-row">
					<label>密码</label>
					<input id='password' type="password" v-model="password" class="mui-input-clear mui-input" placeholder="请输入密码">
				</div>
			</form>
			<div class="mui-content-padded">
				<button id='login' type="button" class="mui-btn mui-btn-block mui-btn-primary" @click="Login">登录</button>
				<div class="link-area"><router-link to="/jump/register" id='reg' tag="span">注册账号</router-link> <span class="spliter">|</span> <router-link to="/jump/retrieve" id='forgetPassword' tag="span">重置密码</router-link>
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
				 account:'',
				 password:''
			 }
		 },
		 methods:{
        Login(){   //登录
           this.$http.post('home/login',{"uPwd":this.password,"uPhone":this.account}).then(result=>{
						if(result.body.status===200){
							this.$cookies.set("auth",result.body.data);
							let redirect=decodeURIComponent(this.$route.query.redirect||"/home");
							this.$router.push({path:redirect});
						}else{
              Toast('输入密码或账号错误，请重新输入');
						}
					 }).catch(function(){
              console.log("服务器异常");
					 })
				}
		 }

}
</script>
<style>
    .area {
			margin: 20px auto 0px auto;
		}
			
	.mui-input-group {
		margin-top: 10px;
	}
	
	.mui-input-group:first-child {
		margin-top: 15px;
	}
	
	.mui-input-group label {
		width: 22%;
	}
	
	.mui-input-row label~input,
	.mui-input-row label~select,
	.mui-input-row label~textarea {
		width: 78%;
	}
	
	.mui-checkbox input[type=checkbox],
	.mui-radio input[type=radio] {
		top: 6px;
	}
	
	.mui-content-padded {
		margin-top: 25px;
	}
	
	.mui-btn {
		padding: 10px;
	}
	
	.link-area {
		display: block;
		margin-top: 25px;
		text-align: center;
	}
	.link-area span{
		color:#26a2ff;
	}
	.spliter {
		color: #bbb;
		padding: 0px 8px;
	}
</style>