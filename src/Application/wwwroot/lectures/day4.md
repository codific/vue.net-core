# Authentication & Authorization
<br/>
## Back-End Authentication and Authorization
### Authentication
* Claim Based Authentication
* [JSON Web Token(JWT)](https://jwt.io/)
* Authentication Scheme
```
private string BuildJwtToken(User user, bool rememberMe)
 {
     var claims = new List<Claim>()
     {
         new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
         new Claim(JwtRegisteredClaimNames.Sub, user.Email)
     };

     var userRoles = this.userManager.GetRolesAsync(user).Result;
     var userClaims = this.userManager.GetClaimsAsync(user).Result;

     for (int i = 0; i < userRoles.Count; i++)
     {
         claims.Add(new Claim(ClaimTypes.Role, userRoles[i]));
     }

     for (int i = 0; i < userClaims.Count; i++)
     {
         claims.Add(new Claim(userClaims[i].ValueType, userClaims[i].Value));
     }

     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

     DateTime expirationDate;
     if (rememberMe)
     {
         expirationDate = DateTime.Now.AddDays(14);
     }
     else
     {
         expirationDate = DateTime.Now.AddHours(3);
     }

     var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
       this.configuration["Jwt:Issuer"],
       claims,
       expires: expirationDate,
       signingCredentials: creds);

     return new JwtSecurityTokenHandler().WriteToken(token);
 }

```
<br/>
### Authorization
* Roles
* Policies
<br/>

#### Permissions Setup
```
public static ApplicationPermission AdminPermission = new ApplicationPermission("Admin Permission", "admin", "Permission for administrator");

...

public class ApplicationPermission
{
    public ApplicationPermission(string name, string value, string description = null)
    {
        Name = name;
        Value = value;
        Description = description;
    }

    public string Name { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
}
```

<br/>
#### Policy Setup

```
services.AddAuthorization(options =>
{
    options.AddPolicy("SomePolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("some-permission");
    });
});
```

<br/>
#### Roles Setup

```
public async Task<Tuple<bool, string[]>> CreateRoleAsync(Role role, IEnumerable<string> claims)
{
    if (claims == null)
        claims = new string[] { };

    string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
    if (invalidClaims.Any())
        return Tuple.Create(false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });


    var result = await this.roleManager.CreateAsync(role);
    if (!result.Succeeded)
        return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());


    role = await this.roleManager.FindByNameAsync(role.Name);

    foreach (string claim in claims.Distinct())
    {
        result = await this.roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, ApplicationPermissions.GetPermissionByValue(claim)));

        if (!result.Succeeded)
        {
            await DeleteRoleAsync(role);
            return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
        }
    }

    return Tuple.Create(true, new string[] { });
}
```
<br/>
## Front-End Authentication and Authorization
### State Managment
```
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

```
<br/>
### Interceptors
```
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
```
<br/>
### Route Managment
```
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
    { name: 'login', path: '/login', component: Login, beforeEnter: authGuard },
    { name: 'register', path: '/register', component: Register, beforeEnter: authGuard },
    { name: 'profile', path: '/profile', component: Profile, beforeEnter: guard },
    { name: 'users', path: '/users', component: Users, beforeEnter: adminGuard },
]
```