import Layout from "@/layouts/Layout.vue";
import reportsList from "@/views/reports/reportsList.vue";
import reportPreview from "@/views/reports/reportPreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/reports",
  component: Layout,
  name: "Отчети ",
  redirect: "/reports",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/reports",
      name: "Отчети",
      component: reportsList,
      meta: {
        title: "Отчети",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-file-chart-outline",
      },
      hidden: false
    },
    {
      path: "/reports/:id",
      name: "Преглед на отчет",
      component: reportPreview,
      meta: {
        title: "Преглед на отчет",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички отчети",
      },
      hidden: true
    },
  ]
}