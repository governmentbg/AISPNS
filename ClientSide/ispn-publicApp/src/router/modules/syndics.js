import Layout from "@/layouts/Layout.vue";
import syndicsList from "@/views/syndics/syndicsList.vue";
import syndicPreview from "@/views/syndics/syndicPreview.vue"
import { UserPermissions } from "@/utils";

export default {
  path: "/syndics",
  component: Layout,
  name: "Синдици ",
  redirect: "/syndics",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/syndics",
      name: "Синдици",
      component: syndicsList,
      meta: {
        title: "Синдици",
      },
      hidden: false
    },
    {
      path: "/syndics/:id",
      name: "Преглед на синдик",
      component: syndicPreview,
      meta: {
        title: "Преглед на синдик",
      },
      hidden: true
    },
  ]
}