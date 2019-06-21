<template>
    <div class="container py-4 text-white lecture-page">
        <a @click="$router.go(-1)" class="btn btn-primary"><icon icon="arrow-circle-left" /> GO BACK TO LECTURES</a>
        <hr />
        <VueShowdown :markdown="pageMarkDownContent" />
        <hr />
        <a @click="$router.go(-1)" class="btn btn-primary"><icon icon="arrow-circle-left" /> GO BACK TO LECTURES</a>
    </div>
</template>
<script>
export default {
   components: {

   },
  data () {
        return {
            pageMarkDownContent: ''
        }
        },
        mounted: function () {
            let self = this;
            this.$http.get("/api/lectures/" + this.$route.params.day)
                .then(response => {
                    if (response.data != 'undefined') {
                        self.pageMarkDownContent = response.data;
                    }
                })
                .catch(e => {
                    this.$router.push({ name: "redirect"});
                    this.errors.push(e);
                })
        }
}</script>
<style>
        .lecture-page .btn {
        border-radius: 0px;
        font-size: 18px;
        background: #44b684;
        color: #fff;
        border: none !important;
        font-weight: bold;
        cursor: pointer;
    }

    .lecture-page .btn:hover,
    .lecture-page .btn:active,
    .lecture-page .btn:focus {
        color: #1f2f48 !important;
    }
    .lecture-page hr {
        border-color: #fff;
    }
    .lecture-page img {
        width: 100% !important;
    }
    .lecture-page a {
        color: #fff !important;
        font-weight: bold;
        text-decoration: underline;
    }
    .lecture-page pre {
        background: #334a5e;
        color: #fff;
        padding: 10px;
    }
</style>