<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-8 m-3"><book-display v-bind:isAdmin="isLoggedIn" v-bind:bookTitle="searchTitle" /></div>
            <div class="col-3 m-3">
                <book-searcher v-on:changeBookTable="changeBookDisplayTable" />
                <book-editor />
            </div>
        </div>
    </div>
</template>

<script>
    import BookDisplay from './BookDisplay.vue';
    import BookEditor from './BookEditor.vue';
    import BookSearcher from './BookSearcher.vue';

    export default {

        components: { BookEditor, BookDisplay, BookSearcher },
        data: function () {
            return {
                searchTitle: ""
            }
        },
        computed: {
            isLoggedIn: function () { return this.$store.getters.isLoggedIn }
        },
        methods: {
            changeBookDisplayTable: function (title) {
                this.searchTitle = title;
            }
        }
    }
</script>

<style>
</style>