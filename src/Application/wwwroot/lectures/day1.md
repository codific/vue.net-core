# Setup Environment, Back-End Scaffolding & Data Layer
<br/>
SPA or MPA?
---
### Multi-Page Application
#### Pros
* Solid
* Very good and easy for proper SEO management
#### Cons
* Frontend and backend development are tightly coupled
* The development becomes quite complex
<br/>
### Single-Page Application
#### Pros
* SPA is fast
* The development is simplified and streamlined
* SPAs are easy to debug
* SPA can cache any local storage effectively
#### Cons
* SEO optimization
* It is slow to download
* It requires JavaScript to be present and enabled
* SPA is less secure
* Memory leak in JavaScript

***
#### Setup Development Environment
###### Visual Studio, IIS Express, MS SQL Server Managment Studio, .Net Core, NodeJS

<br/>
[Visual Studio Community](https://visualstudio.microsoft.com/)
![Alt text](/lectures/assets/images/visual-studio.jpg "Visual Studio Community")
<br/>	
[Internet Information Services (IIS) 10.0 Express](https://www.microsoft.com/en-us/download/details.aspx?id=48264)
![Alt text](/lectures/assets/images/iis-express.jpg "IIS Express")
<br/>
[MS SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)
![Alt text](/lectures/assets/images/ssms.jpg "SSMS")
<br/>
[.Net Core](https://dotnet.microsoft.com/download)
![Alt text](/lectures/assets/images/dotnet-core.jpg "DotNet Core")
<br/>
[NodeJS](https://nodejs.org/en/)
![Alt text](/lectures/assets/images/nodejs.jpg "NodeJS")
<br/>

***
#### Scaffolding, files we need and syntaxes we will use
<br/>
##### Project Organization
![Alt text](/lectures/assets/images/project-organization.jpg "Project Organization")
<br/>
##### .Net Core
* Entity Framework
* Data Access Layer
* Business Layer
* Application Layer
* Routing
* Configuration Settings
* Service Configuration
* Middlewares
* ActionFilters
* Dependency Injection
* Authentication and Authorization
* Asynchronous Programming
<br/>
##### Vue JS
File.vue (vue component)
```
<template>
	<!--our html goes here-->
</template>

<script>
	// our javascript goes here
</script>

<style>
	/* our css goes here */
</style>
```
<br/>
Import dependency
```
import VueCookies from 'vue-cookies'
Vue.use(VueCookies)
```
<br/>
New Vue
```
const app = new Vue({
    el: "#app",
	data: {
	
	},
	methods: {
	
	},
	mounted: function() {
	
	},
	...
})
```
<br/>
##### Key Elements
Back-End Configuration
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
            HotModuleReplacement = true,
        });
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
    }

    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAuthentication();

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
            name: "spa",
            defaults: new { controller = "Home", action = "Index" });
    });
}
```

<br/>
JavaScript Spells
	
```
{
  "name": "aspnetcore-vuejs",
  "author": {
    "name": "Codific",
    "email": "info@codific.com",
    "url": "https://codific.com"
  },
  "scripts": {
    "dev": "cross-env ASPNETCORE_ENVIRONMENT=Development NODE_ENV=development dotnet run",
    "build": "npm run build-vendor:prod && npm run build:prod",
    "build:prod": "cross-env NODE_ENV=production webpack --progress --hide-modules",
    "build-vendor:prod": "cross-env NODE_ENV=production webpack --config webpack.config.vendor.js --progress",
    "build-vendor:dev": "cross-env NODE_ENV=development webpack --config webpack.config.vendor.js --progress",
    "lint": "eslint -c ./.eslintrc.js App/**/*.js  App/**/*.vue  App/**/*.json webpack*.js",
    "install": "npm run build-vendor:dev",
    "update-packages": "npx npm-check -u"
  },
  "dependencies": {
    "axios": "^0.18.0",
    "core-js": "^2.5.3",
    "vue": "^2.6.10",
    "vue-router": "^3.0.3",
    "vue-server-renderer": "^2.6.10",
    "vue-template-compiler": "^2.6.10",
    "vuex": "^3.1.0",
    "vuex-router-sync": "^5.0.0",
    "bootstrap": "^4.2.1",
    "bootstrap-vue": "^2.0.0-rc.11",
    "vue-cookies": "^1.5.13",
    "jwt-decode": "^2.2.0",
    "@fortawesome/vue-fontawesome": "0.1.6",
    "vue-showdown": "^2.4.1"
  },
  "devDependencies": {
    "@babel/core": "^7.2.2",
    "@babel/plugin-proposal-class-properties": "^7.0.0",
    "@babel/plugin-proposal-decorators": "^7.0.0",
    "@babel/plugin-proposal-export-namespace-from": "^7.0.0",
    "@babel/plugin-proposal-function-sent": "^7.0.0",
    "@babel/plugin-proposal-json-strings": "^7.0.0",
    "@babel/plugin-proposal-numeric-separator": "^7.0.0",
    "@babel/plugin-proposal-throw-expressions": "^7.0.0",
    "@babel/plugin-syntax-dynamic-import": "^7.0.0",
    "@babel/plugin-syntax-import-meta": "^7.0.0",
    "@babel/plugin-transform-async-to-generator": "^7.0.0",
    "@babel/plugin-transform-runtime": "^7.0.0",
    "@babel/preset-env": "^7.0.0",
    "@babel/register": "^7.0.0",
    "@babel/runtime": "^7.3.1",
    "@fortawesome/fontawesome-svg-core": "^1.2.13",
    "@fortawesome/free-brands-svg-icons": "^5.7.0",
    "@fortawesome/free-solid-svg-icons": "^5.7.0",
    "@fortawesome/vue-fontawesome": "^0.1.6",
    "aspnet-webpack": "^3.0.0",
    "babel-eslint": "^10.0.1",
    "babel-loader": "^8.0.5",
    "cross-env": "^5.2.0",
    "css-loader": "^2.1.0",
    "eslint": "^5.12.1",
    "eslint-config-standard": "^12.0.0",
    "eslint-plugin-html": "^5.0.0",
    "eslint-plugin-import": "^2.9.0",
    "eslint-plugin-node": "^8.0.1",
    "eslint-plugin-promise": "^4.0.1",
    "eslint-plugin-standard": "^4.0.0",
    "event-source-polyfill": "^1.0.5",
    "file-loader": "^3.0.1",
    "font-awesome": "^4.7.0",
    "jquery": "^3.3.1",
    "mini-css-extract-plugin": "^0.5.0",
    "node-sass": "^4.8.2",
    "optimize-css-assets-webpack-plugin": "^5.0.1",
    "popper.js": "^1.14.1",
    "sass-loader": "^7.1.0",
    "style-loader": "^0.23.1",
    "url-loader": "^1.1.2",
    "vue-loader": "^15.6.2",
    "webpack": "^4.29.0",
    "webpack-cli": "^3.2.1",
    "webpack-dev-server": "^3.1.14",
    "webpack-hot-middleware": "^2.21.2",
    "bootstrap": "^4.2.1",
    "bootstrap-vue": "^2.0.0-rc.11",
    "vue-cookies": "^1.5.13",
    "jwt-decode": "^2.2.0",
    "vue-showdown": "^2.4.1"
  }
}
```
<br/>
#### Application Architecture
<br/>
![Alt text](/lectures/assets/images/application-architecture.png "Application Architecture")
#### Data Access Layer
<br/>
##### Unit Of Work Pattern
![Alt text](/lectures/assets/images/uow.png "Unit Of Work Pattern")
<br/>
##### Repository Pattern
![Alt text](/lectures/assets/images/repository-pattern.png "Repository Pattern")
<br/>
##### Data Migrations
* add-migration NameOfMigration
* update-database
