import Vue from 'vue'
import VueRouter from 'vue-router' //导入路由
import VueCookies from 'vue-cookies'
Vue.use(VueRouter); //安装路由
Vue.use(VueCookies);

//导入vue-resource
import VueResource from 'vue-resource'
//安装 vue-resource
Vue.use(VueResource);
//设置请求的根路径
Vue.http.options.root = 'http://www.superforest.cn:2000/';

//全局设置post时候表单数据格式组织形式
Vue.http.options.emulateHTTP = true;

Vue.http.interceptors.push((request, next) => {
    //请求发送前的处理逻辑
    request.headers.set('auth', VueCookies.get("auth"))
    next((response) => {
        //请求发送后的处理逻辑
        //根据请求的状态，response参数返回给successCallback或errorCallback
        return response
    })
})


import 'jquery'
//导入 MUI 的样式
import './lib/mui/css/mui.min.css';
import './lib/mui/css/icons-extra.css';
import 'mint-ui/lib/style.css' //引入 mint-UI 的样式

//导入bootstrap的样式
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'

import MintUI from 'mint-ui' //全局引入 MintUI
Vue.use(MintUI); //安装 MintUI
import { Upload } from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
Vue.use(Upload);


//导入格式化时间的插件
import moment from 'moment'
//定义全局的过滤器
Vue.filter('dateFormat', function(dataStr, pattern = "YYYY-MM-DD HH:mm:ss") {
    return moment(dataStr).format(pattern);
})
import app from './app.vue'
import router from './router.js' //导入 router.js 的根模块
var vm = new Vue({
    el: '#app',
    render: c => c(app),
    router //挂载路由
})