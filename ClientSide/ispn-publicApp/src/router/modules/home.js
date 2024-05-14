import Layout from "@/layouts/Layout.vue";
import HomePage from "@/views/home/index.vue"
import Login from "@/views/login/index.vue";

import i18n from "@/plugins/i18n";

export default {
  path: "/",
  component: Layout,
  name: `${i18n.t('home')} `,
  redirect: "/",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/",
      name: i18n.t('home'),
      component: HomePage,
      meta: {
        title: i18n.t('home'),
      },
      hidden: false
    },
    {
      path: "/sign-in",
      name: i18n.t("sign_in"),
      component: Login,
      meta: {
        title: i18n.t("sign_in"),
      },
      hidden: true
    },
  ]
}