<template>
    <div class="p-3">
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(user, index) in users" :key="index">
                    <th>{{user.id}}</th>
                    <td>{{user.firstName}}</td>
                    <td>{{user.lastName}}</td>
                    <td>{{user.email}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
export default {
  data () {
        return {
            users: []
        }
        },
        methods: {
            getAllUsers: function () {
                let self = this;
                this.$http.get("api/users")
                    .then(response => {
                        self.users = response.data;
                    })
                    .catch(e => {
                        this.errors.push(e)
                    })
            }
        },
        mounted: function () {
            this.getAllUsers();
        }
    }
</script>
<style>
</style>