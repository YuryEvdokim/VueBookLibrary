import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";

Vue.use(Vuex);
Axios.defaults.baseURL = "https://localhost:44390/api/";
export default new Vuex.Store({
	state: {
		books: [],
		selectedBook: null,
		searchedBooks: [],
		status: '',
		token: '',
		user: {}
	},
	getters: {
		isLoggedIn: state => !!state.token,
		authStatus: state => state.status,
	},
	mutations: {
		saveBook(currentState, book) {
			let index = currentState.books.findIndex(b => b.bookId == book.bookId);
			if (index == -1) {
				currentState.books.push(book);
			} else {
				Vue.set(currentState.books, index, book);
			}
		},
		deleteBook(currentState, book) {
			let index = currentState.books.findIndex(b => b.bookId == book.bookId);
			currentState.books.splice(index, 1);
		},
		selectBook(currentState, book) {
			currentState.selectedBook = book;
		},
		saveSearchedBook(currentState, book) {
			currentState.searchedBooks.push(book);
		},
		deleteSearchedBook(currentState, book) {
			let index = currentState.searchedBooks.findIndex(sb => sb.bookId == book.bookId);
			currentState.searchedBooks.splice(index, 1);
		},
		authRequest(state) {
			state.status = 'loading';
		},
		authSuccess(state, token, user) {
			state.status = 'success';
			state.token = token;
			state.user = user;
		},
		authError(state) {
			state.status = 'error'
		},
		logout(state) {
			state.status = '';
			state.token = '';
		},
	},

	actions: {
		async getBooksAction(context) {
			(await Axios.get("books")).data.forEach(b => context.commit("saveBook", b));
		},
		async saveBookAction(context, book) {
			let index = context.state.books.findIndex(b => b.bookId == book.bookId);
			if (index == -1) {
				await Axios.post("books", book);
			} else {
				await Axios.put("books", book);
			}
			context.commit("saveBook", book);
		},
		async deleteBookAction(context, book) {
			await Axios.delete(`books${book.bookId}`);
			context.commit("deleteBook", book);
		},
		saveSearchedBook(context, bookTitle) {
			if (context.state.searchedBooks.length > 0) {
				context.state.searchedBooks.forEach(sb => context.commit("deleteSearchedBook", sb));
			}
			let title = bookTitle.toLowerCase().trim();
			context.state.books.filter(book => book.bookTitle.toLowerCase() == title)
				.forEach(b => context.commit("saveSearchedBook", b));
		},
		async increaseDownloadCount(context, book) {
			book.downloadCount++;
			await Axios.put("books", book);
			context.commit("saveBook", book);
		},
		login({ commit }, user) {
			return new Promise((resolve, reject) => {
				commit('authRequest')
				Axios.post("account", user)
					.then(resp => {
						const token = resp.data.token
						const user = resp.data.user
						//localStorage.setItem('token', token)
						Axios.defaults.headers.common['Authorization'] = token
						commit('authSuccess', token, user)
						resolve(resp)
					})
					.catch(err => {
						commit('authError')
						localStorage.removeItem('token')
						reject(err)
					})
			})
		},
		logout({ commit }) {
			return new Promise((resolve, reject) => {
				commit('logout')
				localStorage.removeItem('token')
				delete Axios.defaults.headers.common['Authorization']
				resolve()
			})
		}
	}
})
