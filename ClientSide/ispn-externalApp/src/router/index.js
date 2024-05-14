//@js-check
import Vue from "vue";
import Router from "vue-router";

import Layout from "@/layouts/Layout.vue";
import LockedLayout from "@/layouts/LockedLayout.vue";

// Pages
import Login from "@/views/login/login.vue";

// Routes
import paymentsRoute from "./modules/payments";
import trainingsRoute from "./modules/trainings";
import templatesRoute from "./modules/templates";
import reportsRoute from "./modules/reports";
import activitiesAndPropertiesRoute from "./modules/activitiesAndProperties";
import profileRoute from "./modules/profile";
import messagesRoute from "./modules/messages";
import sellAnnouncementsRoute from "./modules/sellAnnouncements";
import documentsRoute from "./modules/documents";

import mii_sellAnnouncementsRoute from "./modules/mii_sellAnnouncements";
import mii_sellAnnouncementsBySyndicsRoute from "./modules/mii_sellAnnouncementsBySyndics";
import mii_messagesRoute from "./modules/mii_messages";
import mii_certificatesRoute from "./modules/mii_certificates";
import mii_contactInformationRoute from "./modules/mii_contactInformation";

// user permissions
import { UserPermissions } from "@/utils";

import NotFound_404 from "@/views/error/404.vue";
import { eUserRoles } from "@/utils/enums/enumerators";

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
  {
    path: "/login",
    component: LockedLayout,
    name: "Authentication",
    hidden: true,
    children: [
      {
        path: "/login",
        name: "Login",
        component: Login,
        meta: {
          title: "Вход",
        },
        hidden: true,
      },
    ],
  },
];

export const ErrorRoute = {
  path: "*",
  component: Layout,
  hidden: true,
  meta: {
    title: "404 Страницата не е намерена",
    roles: [
      eUserRoles.ADMINISTRATOR, 
    ],
  },
  children: [
    {
      name: "404 Error",
      path: "",
      component: NotFound_404,
      meta: {
        title: "404 Страницата не е намерена",
        roles: [
          eUserRoles.ADMINISTRATOR, 
        ],
      },
      hidden: true,
    },
  ],
}

export const asyncRoutes = [
  paymentsRoute,
  trainingsRoute,
  templatesRoute,
  reportsRoute,
  activitiesAndPropertiesRoute,
  profileRoute,
  messagesRoute,
  sellAnnouncementsRoute,
  documentsRoute,
  
  mii_sellAnnouncementsRoute,
  mii_sellAnnouncementsBySyndicsRoute,
  mii_messagesRoute,
  mii_certificatesRoute,
  mii_contactInformationRoute,
];

Vue.use(VueRouterEx);

const _constantRoutes = [...constantRoutes];

const createRouter = () =>
  new VueRouterEx({
    mode: 'history',
    scrollBehavior: (to) => {
      if (to.hash) {
        return { selector: to.hash };
      } else {
        return { x: 0, y: 0 };
      }
    },
    linkExactActiveClass: "nav-item active",
    routes: _constantRoutes,
  });

const router = createRouter();

export function resetRouter() {
  const newRouter = createRouter();
  router.matcher = newRouter.matcher; // reset router
}

export default router;
