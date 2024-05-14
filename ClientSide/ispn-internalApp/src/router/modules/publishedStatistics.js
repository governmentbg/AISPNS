import Layout from "@/layouts/Layout.vue";
import publishedStatisticsList from "@/views/publishedStatistics/publishedStatisticsList.vue";
import publishedStatisticPreview from "@/views/publishedStatistics/publishedStatisticPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/published-statistics",
  component: Layout,
  name: "Статистики и справки ",
  redirect: "/published-statistics",
  hidden: true,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/published-statistics",
      name: "Статистики и справки",
      component: publishedStatisticsList,
      meta: {
        title: "Статистики и справки",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        icon: "mdi-chart-line",
      },
      hidden: false
    },
    {
      path: "/published-statistics/:id",
      name: "Преглед на публикувана статистика/справка",
      component: publishedStatisticPreview,
      meta: {
        title: "Преглед на публикувана статистика/справка",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към публикуване на статистики",
      },
      hidden: true
    },
  ]
}