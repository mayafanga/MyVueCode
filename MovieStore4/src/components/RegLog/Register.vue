<template>
    <div>
       <div class="mui-content">
			<form class="mui-input-group">
				<div class="mui-input-row">
					<label>昵称</label>
					<input id='account' type="text" class="mui-input-clear mui-input" placeholder="请输入昵称" v-model="name">
				</div>
				<div class="mui-input-row">
					<label>密码</label>
					<input id='password' type="password" class="mui-input-clear mui-input" placeholder="请输入密码" v-model="pwd">
				</div>
				<div class="mui-input-row">
					<label>确认密码</label>
					<input id='password_confirm' type="password" class="mui-input-clear mui-input" placeholder="请确认密码" v-model="repwd">
				</div>
				<div class="mui-input-row">
					<label>邮箱</label>
					<input id='email' type="email" class="mui-input-clear mui-input" placeholder="请输入邮箱" v-model="email">
				</div>
                <div class="mui-input-row">
					<label>手机号</label>
					<input id='phone' type="phone" class="mui-input-clear mui-input" placeholder="请输邮手机号" v-model="phone">
				</div>
			</form>
			<div class="mui-content-padded">
				<button id='reg' class="mui-btn mui-btn-block mui-btn-primary" @click="register">注册</button>
			</div>
       </div>
    </div>
</template>
<script>
import {Toast} from 'mint-ui'
export default {
    data(){
		return{
			name:'',
			pwd:'',
			repwd:'',
			email:'',
			phone:'',
			info:''
		}
	},
	created(){
       this.register(); //注册功能
	},
	methods:{
       register(){
		  var regEmail=/^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$/;
		  if(this.name==""){
			  Toast('昵称不能为空');
		  }else if(this.pwd==""){
			  Toast('密码不能为空');
		  }else if(this.repwd==""){
			  Toast('请确认密码');
		  }else if(this.pwd!=this.repwd){
			  Toast('两次输入密不一致');
		  }else if(this.email==""){
			  Toast('邮箱不能为空');
		  }else if(regEmail.test(this.email)){
             Toast('邮箱格式错误');
		  }else if(this.phone==""){
			   Toast('手机号不能为空');
		  }else{
             this.$http.post('home/register',{"uName":this.name,"uPwd":this.pwd,"uMail":this.email,"uPhone":this.phone}).then(result=>{
				 if(result.body.status===200){
					  this.$router.push({path:'/jump/login',replace:true});
					  console.log('123456');
				 }else{
					 Toast('注册失败');
				 }
			 })
		  }
	   }
	}
}
</script>
<style>
    .area {
	   margin: 20px auto 0px auto;
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
</style>