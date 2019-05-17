<template>
    <div>
         <div class="mui-content">
			<form class="mui-input-group">
				<div class="mui-input-row">
					<label>昵称</label>
					<input id='account' type="text" class="mui-input-clear mui-input" placeholder="请输入昵称" v-model="name">
				</div>
				<div class="mui-input-row">
					<label>邮箱</label>
					<input id='email' type="email" class="mui-input-clear mui-input" placeholder="请输入邮箱" v-model="email">
				</div>
                <div class="mui-input-row">
					<label>手机号</label>
					<input id='phone' type="phone" class="mui-input-clear mui-input" placeholder="请输邮手机号" v-model="phone">
				</div>
				<div class="mui-input-row">
					<label>输入密码</label>
					<input id='phone' type="password" class="mui-input-clear mui-input" placeholder="请输入新密码" v-model="pwd">
				</div>
			</form>
			<div class="mui-content-padded">
				<button id='sub' class="mui-btn mui-btn-block mui-btn-primary" @click="retrievePwd">提交</button>
			</div>
       </div>
    </div>
</template>
<script>
import {Toast} from "mint-ui";
export default {
    data(){
		return{
		   name:'',
		   email:'',
		   phone:'',
		   pwd:''
		}
	},
	methods:{
    retrievePwd(){   //重置密码
		  if(this.name==""){
			  Toast('昵称不能为空');
		  }else if(this.email==""){
			  Toast('邮箱不能为空');
		  }else if(this.phone==""){
			   Toast('手机号不能为空');
		  }else if(this.pwd==""){
              Toast('密码不能为空');
		  }else{  
			  this.$http.post('home/retrievePwd',{"uName":this.name,"uEmail":this.email,"uPhone":this.phone,"uNewPwd":this.pwd}).then(result=>{
				  if(result.body.status===200){
					  this.$router.push({path:'/jump/login',replace:true});
				  }else{
						Toast(result.body.msg);
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