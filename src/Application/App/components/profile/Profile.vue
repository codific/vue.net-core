<template>
    <div class="container text-center d-flex h-100">
        <div class="card card-profile mt-5 ml-auto mr-auto mb-auto">
            <div class="card-header">
                <h5>Profile Info</h5>
            </div>
            <div class="card-body">
                <table class="text-left w-100 table-striped">
                    <tr>
                        <td>First Name</td>
                        <td class="font-weight-bold p-1 text-right">{{userData.firstName}}</td>
                    </tr>
                    <tr>
                        <td>Last Name</td>
                        <td class="font-weight-bold p-1 text-right">{{userData.lastName}}</td>
                    </tr>
                    <tr>
                        <td>Email</td>
                        <td class="font-weight-bold p-1 text-right">{{userData.email}}</td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</template>
<script>
export default {
  data () {
        return {
            userData: {
                firstName: '',
                lastName: '',
                email: '',
                id: ''
            }
        }
        },
        methods: {
            initUserData: function () {
                let self = this;
                this.$http.get("api/users/current")
                    .then(response => {
                        self.userData = response.data;
                    })
                    .catch(e => {

                    });
                console.log(self.userData);
            }
        },
        mounted: function () {
            this.initUserData();
        }
}</script>
<style>
    .card-profile {
        width: 412px;
        margin: 10vh auto;
        background-color: #334a5e;
        color: #fff;
        border-radius: 0px;
    }
</style>