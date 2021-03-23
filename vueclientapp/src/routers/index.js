import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../components/Home";
import Admin from "../components/Admin";
import Login from "../components/Login";
import store from '../store/index.js'
Vue.use(VueRouter);

let router = new VueRouter({
	mode: "history",
	routes: [
		{ path: "/", component: Home },
		{ path: "/logout", component: Home },
		{ path: "/login", component: Login },
		{ path: "/admin", component: Admin, meta: { requiresAuth: true } }
	]
})
router.beforeEach((to, from, next) => {
	if (to.matched.some(record => record.meta.requiresAuth)) {
		if (store.getters.isLoggedIn) {
			next()
			return
		}
		next('/login')
	} else {
		next()
	}
})
export default router
