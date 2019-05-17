import VueRouter from 'vue-router'

import HomeContainer from './components/tabbar/HomeContainer.vue'
import PersonalContainer from './components/tabbar/PersonalContainer.vue'
import JumpContainer from './components/tabbar/JumpContainer.vue'
import Login from './components/RegLog/Login.vue'
import Register from './components/RegLog/Register.vue'
import RecPwd from './components/RegLog/Retrievepwd.vue'
import NewsInfo from './components/news/newsInfo.vue'
import MovieInfo from './components/movie/movieInfo.vue'

var router = new VueRouter({
    routes: [
        { path: '/', redirect: '/home' },
        { path: '/home', component: HomeContainer },
        { path: '/personal', component: PersonalContainer },
        {
            path: '/jump',
            component: JumpContainer,
            redirect: '/jump/login',
            children: [
                { path: 'login', component: Login },
                { path: 'register', component: Register }
            ]
        },
        { path: '/jump/retrieve', component: RecPwd },
        { path: '/news/newsinfo/:id', component: NewsInfo, name: 'newsinfo' },
        { path: '/movies/movieinfo/:id', component: MovieInfo, name: 'movieinfo' }
    ],
    linkActiveClass: 'router-link-exact-active' //覆盖默认的路由高亮的类，默认的类叫做 router-link-active
});

export default router //向外暴露一个路由对象