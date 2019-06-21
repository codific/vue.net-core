# Front-End Implementation & Setup Back-End Communication
<br/>
### VueJS
#### Project Structure
```
/App/
|-- /components
|	|-- /authentication
|	|	|-- Login.vue
|	|	|-- Register.vue
|	|-- /layout
|	|	|-- Footer.vue
|	|	|-- Navigation.vue
|	|-- /profile
|	|	|-- Profile.vue
|	|-- /users
|	|	|-- Users.vue
|	|-- App.vue
|	|-- Home.vue
|	|-- PageNotFound.vue
|-- css
|	|-- site.css
|-- app.js
|-- boot-app.js
|-- icons.js
|-- router.js
|-- routes.js
|-- store.js
```
<br/>
#### app.js
```
import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from './components/App'
import BootstrapVue from 'bootstrap-vue'
import VueCookies from 'vue-cookies'

Vue.prototype.$http = axios

Vue.use(BootstrapVue)
Vue.use(VueCookies)

Vue.prototype.$cookies = VueCookies

sync(store, router)

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

```
<br/>
#### boot-app.js
```
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'bootstrap/dist/css/bootstrap.css'
import './css/site.css'
import 'core-js/es6/promise'
import 'core-js/es6/array'

import { app } from './app'

app.$mount('#app')
```
<br/>
#### router.js
```
import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from './routes'

Vue.use(VueRouter)

let router = new VueRouter({
    mode: 'history',
    routes
})

export default router
```
<br/>
#### routes.js
```
import Home from './components/Home'

export const routes = [
    { name: 'home', path: '/', component: Home },
]
```
<br/>
#### store.js
```
import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// STATE
const state = {

}

// GETTERS
const getters = {

}

// MUTATIONS
const mutations = {

}

// ACTIONS
const actions = ({

})

export default new Vuex.Store({
    state,
    getters,
    mutations,
    actions
})

```
<br/>
#### icons.js
```
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import { faBars, faLaptopCode, faArrowCircleLeft } from '@fortawesome/free-solid-svg-icons'

library.add(
    faBars,
    faLaptopCode,
    faArrowCircleLeft
)

export {
    FontAwesomeIcon
}

```
<br/>
#### App.vue
```
<template>
    <div id="app">
        <div class="d-flex flex-column h-100">
            <div id="page-content">
                <navigation></navigation>
                <router-view></router-view>
            </div>
            <app-footer></app-footer>
        </div>
    </div>
</template>
<script>
    import Navigation from './layout/Navigation'
    import Footer from './layout/Footer'

    export default {
      name: 'App',
        components: {
            'navigation': Navigation,
            'app-footer': Footer
      },

      data () {
        return {}
      }
    }</script>
<style>
    #page-content {
        flex: 1 0 auto;
    }
</style>

```
<br/>
#### Home.vue
```
<template>

</template>
<script>
    export default {
        name: 'App',
        data () {
            return {

            }
        }
    }
</script>
<style>

</style>
```
<br/>
#### Vue Lifecycle
![Alt text](/lectures/assets/images/lifecycle.png "Lifecycle")
(https://vuejs.org/)
<br/>
#### Make HTTP Request
```
let self = this;
this.$http.get("api/users")
    .then(response => {
        self.users = response.data;
    })
    .catch(e => {
        this.errors.push(e)
    })
```