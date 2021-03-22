<template>
    <div id="app" class="container-fluid">
        <div class="row">
            <div class="col-8 m-3">
                <book-display isAdmin="" v-bind:bookTitle="searchTitle" />
            </div>
            <div class="col-3 m-3">
                <book-searcher v-on:changeBookTable="changeBookDisplayTable" />
            </div>
        </div>
    </div>
</template>

<script>
    import BookDisplay from './BookDisplay.vue';
    import BookSearcher from './BookSearcher.vue';

    export default {

        components: { BookDisplay, BookSearcher },
        data: function () {
            return {
                searchTitle: ""
            }
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