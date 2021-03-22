<template>
    <div>
        <div class="form-group">
            <label>Book Title</label>
            <input v-model="book.bookTitle" class="form-control" />
        </div>
        <div class="form-group">
            <label>Author</label>
            <input v-model="book.author" class="form-control" />
        </div>
        <div class="form-group">
            <label>Publishing Year</label>
            <input v-model="book.publishingYear" class="form-control" />
        </div>
        <div class="form-group">
            <label>Download Count</label>
            <input v-model.number="book.downloadCount" class="form-control" />
        </div>
        <div class="form-group">
            <label>Book Link</label>
            <input v-model="book.bookLink" class="form-control" />
        </div>
        <div>
            <button class="btn btn-primary" v-on:click="save">{{editing ? "Save":"Create"}}</button>
            <button class="btn btn-secondary" v-on:click="cancel">Cancel</button>
        </div>
    </div>
</template>

<script>
    import { mapMutations } from "vuex";
    let unwatcher;
    export default {
        data: function () {
            return {
                editing: false,
                book: {}
            }
        },
        methods: {
            save() {
                this.$store.dispatch("saveBookAction", this.book);
                this.book = {};
            },
            cancel() {
                this.$store.commit("selectBook");
            },
            selectBook(selectedBook) {
                if (selectedBook == null) {
                    this.editing = false;
                    this.book = {};
                } else {
                    this.editing = true;
                    this.book = {};
                    Object.assign(this.book, selectedBook);
                }
            }
        },
        created() {
            unwatcher = this.$store.watch(state => state.selectedBook, this.selectBook);
            this.selectBook(this.$store.state.selectedBook);
        },
        beforeDestroy() {
            unwatcher();    //–азрушить наблюдение за изменением редактировани€ книг при выходе компонента
        }
    }
</script>
