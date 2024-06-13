//@js-check
import Vue from 'vue';
import Router from 'vue-router';

import Layout from "@/layouts/Layout.vue";

// Routes
// import homeRoute from "./modules/home";
import homeRoute from "./modules/home";
import casesRoute from "./modules/cases";
import syndicsRoute from "./modules/syndics";
import entrepreneursRoute from "./modules/entrepreneurs";
import sellAnnouncementsRoute from "./modules/sellAnnouncements";
import statisticsAndReportsRoute from "./modules/statisticsAndReports";
import templatesRoute from "./modules/templates";
import legalBasisRoute from "./modules/legalBasis";
// user permissions
import { UserPermissions } from '@/utils';

import NotFound_404 from "@/views/error/404.vue";



class VueRouterEx extends Router {
  constructor(options) {
    super(options);
    const { addRoutes } = this.matcher;
    const { routes } = options;

    this.routes = routes;

    this.matcher.addRoutes = (newRoutes) => {
      this.routes.push(...newRoutes);
      addRoutes(newRoutes);
    };
  }
}


export const constantRoutes = [
  homeRoute,
  casesRoute,
  syndicsRoute,
  entrepreneursRoute,
  sellAnnouncementsRoute,
  statisticsAndReportsRoute,
  templatesRoute,
  legalBasisRoute,
  {
    path: '*',
    component: Layout,
    hidden: true,
    meta: {
      title: "404 Страницата не е намерена",
    },
    children: [
      {
        name: '404 Error',
        path: '',
        component: NotFound_404,
        meta: {
          title: "404 Страницата не е намерена",
        },
        hidden: true,
      },
    ],
  }
]

export const asyncRoutes = [
];

Vue.use(VueRouterEx)

var _constantRoutes = [...constantRoutes];

const createRouter = () => new VueRouterEx({
  mode: 'history',
  scrollBehavior: (to) => {
    if (to.hash) {
     return { selector: to.hash };
    } else {
      return { x: 0, y: 0 };
    }
  },
  linkExactActiveClass: "nav-item active",
  routes: _constantRoutes
})

const router = createRouter();

export function resetRouter() {
  const newRouter = createRouter();
  router.matcher = newRouter.matcher; // reset router
}

export default router
