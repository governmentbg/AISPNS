import Layout from "@/layouts/Layout.vue";
import entrepreneursActsBankruptcyList from "@/views/entrepreneursActs/entrepreneursActsBankruptcyList.vue";
import entrepreneurActsBankruptcyPreview from "@/views/entrepreneursActs/entrepreneurActsBankruptcyPreview.vue";
import entrepreneursActsStabilizationList from "@/views/entrepreneursActs/entrepreneursActsStabilizationList.vue";
import entrepreneurActsStabilizationPreview from "@/views/entrepreneursActs/entrepreneurActsStabilizationPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/entrepreneurs-acts",
  component: Layout,
  name: "Обявяване на актове предприемачи ",
  redirect: "/entrepreneurs-acts",
  hidden: false,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
    icon: "mdi-card-account-details-star-outline",
  },
  children: [
    {
      path: "/entrepreneurs-acts/bankruptcy",
      name: "Актове по несъстоятелност за предприемачи ",
      component: entrepreneursActsBankruptcyList,
      meta: {
        title: "Обявяване на актове за предприемачи несъстоятелност",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/entrepreneurs-acts/bankruptcy/:id",
      name: "Преглед на обявен акт на предприемач несъстоятелност",
      component: entrepreneurActsBankruptcyPreview,
      meta: {
        title: "Преглед на обявен акт на предприемач несъстоятелност",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички обявени актове на предприемачи несъстоятелност",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs-acts/stabilization",
      name: "Актове по стабилизация за предприемачи  ",
      component: entrepreneursActsStabilizationList,
      meta: {
        title: "Обявяване на актове за предприемачи стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/entrepreneurs-acts/stabilization/:id",
      name: "Преглед на обявен акт на предприемач стабилизация",
      component: entrepreneurActsStabilizationPreview,
      meta: {
        title: "Преглед на обявен акт на предприемач стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички обявени актове на предприемачи стабилизация",
      },
      hidden: true
    },
  ]
}