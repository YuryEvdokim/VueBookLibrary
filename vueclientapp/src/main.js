import Vue from 'vue'
import App from './App.vue'
import store from "./store";
import router from "./routers";
import Axios from "axios";
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";

Vue.config.productionTip = false

Vue.prototype.$http = Axios;
const token = localStorage.getItem('token')
if (token) {
	Vue.prototype.$http.defaults.headers.common['Authorization'] = token
}

new Vue({
	render: h => h(App),
	data: {
		eventBus: new Vue()
	},
	store,
	router,
	
}).$mount('#app')
