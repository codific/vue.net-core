import Vue from 'vue'
import Vuex from 'vuex'
import VueCookies from 'vue-cookies'
import axios from 'axios'
import jwtDecode from 'jwt-decode'

Vue.use(Vuex)
Vue.use(VueCookies)

const roleClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

// STATE
const state = {
    accessToken: VueCookies.get('access_token') || null,
    userEmail: (VueCookies.get('access_token') != null) ? jwtDecode(VueCookies.get('access_token')).sub : '',
    userRoles: (VueCookies.get('access_token') != null) ? jwtDecode(VueCookies.get('access_token'))[roleClaim] : null,
}

// GETTERS
const getters = {
    loggedIn(state) {
        return state.accessToken !== null;
    },
    userEmail(state) {
        return state.userEmail;
    },
    userRoles(state) {
        return state.userRoles;
    },
    hasAdminRights(state) {
        return state.userRoles != null ? state.userRoles.includes('Admin') : false
    }
}

// MUTATIONS
const mutations = {
    SET_TOKEN(state, token) {
        state.accessToken = token;
    },
    SET_USER_EMAIL(state, email) {
        state.userEmail = email;
    },
    SET_USER_ROLES(state, roles) {
        state.userRoles = roles;
    }
}

// ACTIONS
const actions = ({
    validateAuthentication(context) {
        context.commit("SET_TOKEN", (VueCookies.get('access_token') || null));
    },
    retrieveToken(context, credentials) {
        return new Promise((resolve, reject) => {
            axios.post("api/auth/login", {
                'email': credentials.email,
                'password': credentials.password
            })
                .then(response => {
                    if (response.data.success == true) {
                        context.commit("SET_TOKEN", response.data.jwt);
                        VueCookies.set('access_token', response.data.jwt);
                        context.commit("SET_USER_EMAIL", jwtDecode(response.data.jwt).sub);
                        context.commit("SET_USER_ROLES", jwtDecode(response.data.jwt)[roleClaim]);
                    }

                    resolve(response);
                })
                .catch(e => {
                    reject(e);
                })
        });
    },
    destroyToken(context) {
        context.commit("SET_TOKEN", null);
        VueCookies.remove('access_token');
        context.commit("SET_USER_EMAIL", '');
        context.commit("SET_USER_ROLES", null);
    },
    registerUser(context, credentials) {
        return new Promise((resolve, reject) => {
            axios.post("api/auth/register", {
                'email': credentials.email,
                'password': credentials.password,
                'confirmedPassword': credentials.confirmedPassword,
                'firstName': credentials.firstName,
                'lastName': credentials.lastName
            })
                .then(response => {
                    resolve(response);
                })
                .catch(e => {
                    reject(e);
                })
        });
    }
})

export default new Vuex.Store({
    state,
    getters,
    mutations,
    actions
})
