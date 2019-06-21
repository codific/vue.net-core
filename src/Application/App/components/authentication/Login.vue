<template>
    <div>
        <div id="logreg-forms">
            <form class="form-signin" method="post" @submit.prevent="submitLoginForm">
                <h1 class="h3 mb-3 font-weight-normal" style="text-align: center"> Login</h1>
                <input type="email" v-model="userEmail" class="form-control mb-3" placeholder="Email address" required="" autofocus="">
                <input type="password" v-model="userPassword" class="form-control mb-3" placeholder="Password" required="">
                <button class="btn btn-main btn-block" type="submit"><i class="fas fa-sign-in-alt"></i> Login</button>
                <hr>
                <span>Don't have an account? <router-link to="/register" tag="a" class="d-inline">Register Now</router-link>.</span>
            </form>
        </div>
    </div>
</template>
<script>
    export default {
        components: {

        },
        data() {
            return {
                userEmail: '',
                userPassword: '',
            }
        },
        methods: {
            submitLoginForm: function () {
                let self = this;
                this.$store.dispatch('retrieveToken', {
                    email: self.userEmail,
                    password: self.userPassword
                })
                    .then(response => {
                        if (response.data.success == true) {
                            this.$router.push({ name: 'home' })
                        }
                        else {
                            alert(response.data.message);
                        }
                })
            }
        }
    }
</script>
<style>

</style>