import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from './components/App'
import BootstrapVue from 'bootstrap-vue'
import VueCookies from 'vue-cookies'
import { FontAwesomeIcon } from './icons'
import VueShowdown from 'vue-showdown'

Vue.use(VueShowdown, {
    options: {
        emoji: true,
        vueTemplate: true,
        simpleLineBreaks: true,
        openLinksInNewWindow: true,
        splitAdjacentBlockquotes: true,
        ghCodeBlocks: true
    }
})
Vue.prototype.$http = axios

Vue.use(BootstrapVue)
Vue.use(VueCookies)

Vue.prototype.$cookies = VueCookies
Vue.component('icon', FontAwesomeIcon)

sync(store, router)

Vue.mixin({
    methods: {

    }
})

axios.interceptors.request.use(
    (config) => {
        let token = VueCookies.get('access_token') || null;

        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }

        return config;
    },

    (error) => {
        return Promise.reject(error);
    }
);

const app = new Vue({
    el: "#app",
    store,
    router,
    render: h => h(App),
})

export {
    app,
    router,
    store
}
