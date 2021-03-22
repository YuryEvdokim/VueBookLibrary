<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
                    <div class="col-lg-5 nav-item active">
                        <router-link class="navbar-brand text-light" to="/">Home</router-link>
                    </div>
                    <div class="col-lg-3 nav-item active">
                        <router-link v-if="isLoggedIn" class="navbar-brand text-light" to="/admin">Admin</router-link>
                    </div>
                    <div class="col-lg-4 text-right">
                        <router-link class="navbar-brand text-light" to="/login">Log in</router-link>
                    </div>
                </nav>
            </div>
        </div>
        <router-view></router-view>
    </div>
</template>

<script>
    // import BookDisplay from './components/BookDisplay.vue';
    // import BookEditor from './components/BookEditor.vue';

    export default {
        computed: {
            isLoggedIn: function () { return this.$store.getters.isLoggedIn }
        },
        methods: {
            logout: function () {
                this.$store.dispatch('logout')
                    .then(() => {
                        this.$router.push('/login')
                    })
            }
        },
        created() {
            this.$store.dispatch("getBooksAction");
        }
    }
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
