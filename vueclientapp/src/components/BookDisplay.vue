<template>
    <div>
        <div v-if="isAdmin" class="text-right m-3">
            <button class="btn btn-primary" v-on:click="createNew">Create New</button>
        </div>
        <table class="table table-sm table-striped table-bordered">
            <tr>
                <th>#</th>
                <th>Book Title</th>
                <th>Author</th>
                <th>Publishing Year</th>
                <th v-if="isAdmin">Download Count</th>
                <th>Book Link</th>
                <th v-if="isAdmin"></th>
            </tr>
            <tbody>
                <tr v-for="(book, i) in books" v-bind:key="book.bookId">
                    <td>{{ i + 1 }}</td>
                    <td class="text-left">{{ book.bookTitle }}</td>
                    <td class="text-left">{{ book.author }}</td>
                    <td>{{ book.publishingYear }}</td>
                    <td v-if="isAdmin">{{ book.downloadCount }}</td>
                    <td><a v-bind:href="book.bookLink" v-on:click="increaseDownloadCount(book)">Download</a></td>
                    <td v-if="isAdmin">
                        <button class="btn btn-sm btn-info" v-on:click="editBook(book)">Edit</button>
                        <button class="btn btn-sm btn-warning" v-on:click="deleteBook(book)">Delete</button>
                    </td>
                </tr>
                <tr v-if="books.length == 0">
                    <td colspan="5" class="text-center">No Data</td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapState, mapMutations, mapActions } from "vuex";
    import Axios from "axios";
    export default {

        props: ["isAdmin", "bookTitle"],

        computed: {
            books() {
                return this.bookTitle == "" ? this.$store.state.books : this.$store.state.searchedBooks;
            }
        },
        methods: {
            ...mapMutations({
                editBook: "selectBook",
                createNew: "selectBook"
            }),
            ...mapActions({
                deleteBook: "deleteBookAction",
                increaseDownloadCount: "increaseDownloadCount"
            }),
        },
    }
</script>
