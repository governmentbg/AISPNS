import Layout from "@/layouts/Layout.vue";
import statisticsAndReportsList from "@/views/statisticsAndReports/statisticsAndReportsList.vue";
import statisticsAndReportsPreview from "@/views/statisticsAndReports/statisticsAndReportsPreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/statistics-and-reports",
  component: Layout,
  name: "Статистики и репорти ",
  redirect: "/statistics-and-reports",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/statistics-and-reports",
      name: "Статистики и репорти",
      component: statisticsAndReportsList,
      meta: {
        title: "Статистики и репорти",
      },
      hidden: false
    },
    {
      path: "/statistics-and-reports/:id",
      name: "Преглед на статистики и репорти",
      component: statisticsAndReportsPreview,
      meta: {
        title: "Преглед на статистики и репорти",
      },
      hidden: true
    },
  ]
}