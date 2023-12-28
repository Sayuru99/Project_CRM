import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import store from "@/store";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "home",
    component: () => import("@/module/home/view/HomeView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/users/signin",
    name: "signin",
    component: () => import("@/module/user/view/SignInView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/users/signup",
    name: "signup",
    component: () => import("@/module/user/view/SignUpView.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: () => import("@/module/dashboard/views/DashboardView.vue"),
    meta: { requiresAuth: true },
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach(async (to, from, next) => {
  if (to.matched.some((route) => route.meta.requiresAuth)) {
    const isSignedIn = await store.dispatch("isSignedIn");
    if (!isSignedIn) {
      next("/users/signin");
      return;
    }

    next();
    return;
  }

  next();
});

export default router;
