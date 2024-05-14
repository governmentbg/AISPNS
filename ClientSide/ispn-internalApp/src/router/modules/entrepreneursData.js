import Layout from "@/layouts/Layout.vue";
import entrepreneursDataBankruptcyList from "@/views/entrepreneursData/entrepreneursDataBankruptcyList.vue";
import entrepreneurDataBankruptcyPreview from "@/views/entrepreneursData/entrepreneurDataBankruptcyPreview.vue";
import entrepreneursDataStabilizationList from "@/views/entrepreneursData/entrepreneursDataStabilizationList.vue";
import entrepreneurDataStabilizationPreview from "@/views/entrepreneursData/entrepreneurDataStabilizationPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/entrepreneurs-data",
  component: Layout,
  name: "Вписване на данни предприемачи ",
  redirect: "/entrepreneurs-data/bankruptcy",
  hidden: false,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
    icon: "mdi-badge-account-horizontal-outline",
  },
  children: [
    {
      path: "/entrepreneurs-data/bankruptcy",
      name: "Данни по несъстоятелност на предприемача ",
      component: entrepreneursDataBankruptcyList,
      meta: {
        title: "Данни по несъстоятелност на предприемача за вписване",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/entrepreneurs-data/bankruptcy/:id",
      name: "Преглед на вписани данни на предприемач несъстоятелност",
      component: entrepreneurDataBankruptcyPreview,
      meta: {
        title: "Преглед на вписани данни на предприемач несъстоятелност",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички вписвани данни на предприемачи",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs-data/stabilization",
      name: "Данни по стабилизация на предприемача ",
      component: entrepreneursDataStabilizationList,
      meta: {
        title: "Данни по стабилизация на предприемача за вписване",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/entrepreneurs-data/stabilization/:id",
      name: "Преглед на вписани данни на предприемач стабилизация",
      component: entrepreneurDataStabilizationPreview,
      meta: {
        title: "Преглед на вписани данни на предприемач стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички вписвани данни на предприемачи",
      },
      hidden: true
    },
  ]
}