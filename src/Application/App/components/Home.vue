<template>
    <div class="container">
        <div class="row py-4">
            <div v-for="(lecture, index) in courseLectures" :key="index" class="col-12 col-sm-6 py-3">
                <div class="card lecture-card">
                    <div class="card-body">
                        <h5 class="card-title">Day {{lecture.day}}</h5>
                        <p class="card-text">{{lecture.title}}</p>
                        <div class="progress mx-5 mb-4">
                            <div class="progress-bar" role="progressbar" :style="'width: ' + lecture.frontEnd + '%;'" v-on:aria-valuenow="lecture.frontEnd" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                        <router-link tag="a" :to="'/lectures/day/' + lecture.day" class="btn btn-primary"><icon icon="laptop-code" /> GO TO THE LECTURE</router-link>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: 'App',
        data () {
            return {
                courseLectures: []
            }
        },
        mounted: function () {
            let self = this;
            this.$http.get("api/lectures")
                .then(response => {
                    self.courseLectures = response.data;
                })
                .catch(e => {
                    this.errors.push(e)
                })
        }
    }
</script>
<style>
    .lecture-card {
        border-radius: 0px !important;
        background: #334a5e;
        border: none !important;
    }

    .lecture-card .card-body {
        padding: 0px;
    }

    .lecture-card .card-body .card-title {
        padding: 4px 9px;
        font-size: 16px;
        background: #1f2f48;
        color: #fff;
        display: inline-block;
        margin: 10px;
        width: auto;
    }

    .lecture-card .card-body .card-text {
        font-size: 28px;
        color: #fff;
        font-weight: bold;
        text-align: center;
        padding: 10px;
        -webkit-transition: all 1s;
        transition: all 1s;
    }

    .lecture-card .card-body .btn {
        border-radius: 0px;
        font-size: 18px;
        background: #44b684;
        color: #fff;
        border: none !important;
        font-weight: bold;
        float: right;
    }

    .lecture-card .card-body .btn:hover,
    .lecture-card .card-body .btn:active,
    .lecture-card .card-body .btn:focus {
        color: #1f2f48;
    }

    .lecture-card .progress {
        border-radius: 0px;
        background: #8055af;
    }

    .lecture-card .progress-bar {
        background: #44b684;
    }
</style>