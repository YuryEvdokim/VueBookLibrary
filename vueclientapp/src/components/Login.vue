<template>
    <div>
        <h4>Login</h4>
        <form @submit.prevent="login">
            <label for="email">E-Mail</label>
            <div>
                <input id="email" type="email" v-model="email" required>
            </div>
            <label for="password">Password</label>
            <div>
                <input id="password" type="password" v-model="password" required>
            </div>
            <div>
                <button type="submit">Login</button>
                <button type="cancel" v-on:click="logout">Logout</button>
            </div>
        </form>

    </div>
</template>

<script>
    export default {
        data() {
            return {
                email: "",
                password: "",
            }
        },
        methods: {
            login: function () {
                let email = this.email
                let password = this.password
                this.$store.dispatch('login', { email, password })
                    .then(() => this.$router.push('/admin'))
                    .catch(err => console.log(err))

            },
            logout: function () {
                this.$store.dispatch('logout')
                    .then(() => {
                        this.$router.push('/')
                    })
            }

        }

    }
</script>

