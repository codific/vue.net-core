import Home from './components/Home'
import Game from './components/game/Game'
import Contact from './components/contact/Contact'
import Login from './components/authentication/Login'
import Register from './components/authentication/Register'
import Profile from './components/profile/Profile'
import Users from './components/users/Users'
import DayBase from './components/lectures/DayBase'
import PageNotFound from './components/PageNotFound'

import store from './store'

const guard = async (to, from, next) => {
    store.dispatch("validateAuthentication").then(response => {
        if (store.getters.loggedIn) {
            next();
        } else {
            next('/login');
        }
    });
};

const authGuard = async (to, from, next) => {
    store.dispatch("validateAuthentication").then(response => {
        if (!store.getters.loggedIn) {
            next();
        } else {
            next('/');
        }
    });
};

const adminGuard = async (to, from, next) => {
    store.dispatch("validateAuthentication").then(response => {
        if (store.getters.loggedIn && store.getters.hasAdminRights) {
            next();
        } else {
            next('/');
        }
    });
};

export const routes = [
    { name: 'home', path: '/', component: Home },
    { name: 'game', path: '/game', component: Game },
    { name: 'contact', path: '/contact', component: Contact },
    { name: 'login', path: '/login', component: Login, beforeEnter: authGuard },
    { name: 'register', path: '/register', component: Register, beforeEnter: authGuard },
    { name: 'profile', path: '/profile', component: Profile, beforeEnter: guard },
    { name: 'users', path: '/users', component: Users, beforeEnter: adminGuard },
    { name: 'lecture', path: '/lectures/day/:day', component: DayBase },

    { name: 'page-not-found',  path: '/404', component: PageNotFound },
    { name: 'redirect', path: '*', redirect: '/404' },  
]
