<template>
    <div>
        <div class="form-group">
            <label>Enter the title of the required book in this field:</label>
            <input v-model="bookTitle" class="form-control" />
        </div>
        <button class="btn btn-sm btn-info" v-on:click="searchBook">Search</button>
    </div>
</template>
<script>
    import { mapState, mapMutations, mapActions } from "vuex";
    export default {
        data: function () {
            return {
                bookTitle: ""
            }
        },
        methods: {
            searchBook(bookTitle) {
                if (this.bookTitle && this.bookTitle.trim()) {
                    this.$store.dispatch("saveSearchedBook", this.bookTitle);
                    this.$emit("changeBookTable", this.bookTitle);
                    this.bookTitle = "";
                } else {
                    alert("Firsr you need to enter the title of the book.")
                }
            }
        }
    }
</script>