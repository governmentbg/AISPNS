import Layout from "@/layouts/Layout.vue";
import trustedPersonsList from "@/views/trustedPersons/trustedPersonsList.vue";
import trustedPersonPreview from "@/views/trustedPersons/trustedPersonPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/trusted-persons",
  component: Layout,
  name: "Доверени лица ",
  redirect: "/trusted-persons",
  hidden: true,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/trusted-persons",
      name: "Доверени лица",
      component: trustedPersonsList,
      meta: {
        title: "Доверени лица",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        icon: "mdi-account-star",
      },
      hidden: false
    },
    {
      path: "/trusted-persons/:id",
      name: "Преглед на доверено лице",
      component: trustedPersonPreview,
      meta: {
        title: "Преглед на доверено лице",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички доверени лица",
      },
      hidden: true
    },
  ]
}