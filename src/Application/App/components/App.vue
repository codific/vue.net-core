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
          return {
              message: "hello vue"   
          }
      },
      mounted: function () {
        console.log(this.message);
      }
    }
</script>
<style>
    #page-content {
        flex: 1 0 auto;
    }
</style>
