import Layout from "@/layouts/Layout.vue";
import syndicsList from "@/views/syndics/syndicsList.vue";
import syndicPreview from "@/views/syndics/syndicPreview.vue"
import { UserPermissions } from "@/utils/enums/enumerators";

let syndicsRoute = {
  path: "/syndics",
  component: Layout,
  name: "Синдици ",
  redirect: "/syndics",
  hidden: true,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/syndics",
      name: "Синдици",
      component: syndicsList,
      meta: {
        title: "Синдици",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        icon: "mdi-account-tie",
      },
      hidden: false
    },
    {
      path: "/syndics/:id",
      name: "Преглед на синдик",
      component: syndicPreview,
      meta: {
        title: "Преглед на синдик",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички синдици",
      },
      hidden: true
    },
  ]
}

export default syndicsRoute;