<template>
    <div>
        <router-link to="/jump/login" class="jump-reclog" tag="span">登录</router-link>
        <router-link to="/jump/register" class="jump-reclog" tag="span">注册</router-link>
        <router-view></router-view>
    </div>
</template>
<script>
export default {
    
}
</script>
<style>
    .jump-reclog{
        display: inline-block;
        text-decoration: none;
        color: #26a2ff;
        text-align: center;
        line-height: 50px;
        width:48%;
        height:50px;
        background-color: #fff;
        margin-left:1%;
        font-weight:bolder;
    }
    .router-link-active{
        border:1px solid #26a2ff;
        font-weight: bolder;
    }
</style>