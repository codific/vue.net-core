<template>
    <div>
        <div id="logreg-forms">
            <form class="form-signin" method="post" @submit.prevent="submitRegisterForm">
                <h1 class="h3 mb-3 font-weight-normal text-center"> Register</h1>
                <input v-model="email" type="email" class="form-control mb-3" placeholder="Email address" required="" autofocus="">
                <input v-model="password" type="password" class="form-control mb-3" placeholder="Password" required="">
                <input v-model="confirmedPassword" type="password" class="form-control mb-3" placeholder="Confirm Password" required="">
                <input v-model="firstName" type="text" class="form-control mb-3" placeholder="First Name" required="">
                <input v-model="lastName" type="text" class="form-control mb-3" placeholder="Last Name" required="">
                <button class="btn btn-main btn-block" type="submit"><i class="fas fa-sign-in-alt"></i> Register</button>
                <hr>
                <span>Already have an account? <router-link to="/login" tag="a" class="d-inline">Login</router-link>.</span>
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
                email: '',
                password: '',
                confirmedPassword: '',
                firstName: '',
                lastName: ''
            }
        },
        methods: {
            submitRegisterForm: function () {
                let self = this;
                this.$store.dispatch('registerUser', {
                    email: self.email,
                    password: self.password,
                    confirmedPassword: self.confirmedPassword,
                    firstName: self.firstName,
                    lastName: self.lastName
                })
                    .then(response => {
                        if (response.data.success == true) {
                            alert("You have been successfully registrated!");
                            this.$router.push({ name: 'login' })
                        }
                        else {
                            alert(response.data.message);
                        }
                    })
                    .catch(e => {
                        alert("Invalid registration try");
                    })
                }
            }
        }
</script>
<style>
</style>