import Layout from "@/layouts/Layout.vue";
import casesBankruptcyList from "@/views/cases/casesBankruptcyList.vue";
import caseBankruptcyPreview from "@/views/cases/caseBankruptcyPreview.vue"
import caseBankruptcyBookPreview from "@/views/cases/caseBankruptcyBookPreview.vue";
import caseBankruptcyActsPreview from "@/views/cases/caseBankruptcyActsPreview.vue";

import casesStabilizationList from "@/views/cases/casesStabilizationList.vue";
import caseStabilizationPreview from "@/views/cases/caseStabilizationPreview.vue"
import caseStabilizationActsPreview from "@/views/cases/caseStabilizationActsPreview.vue";

import caseActAnnouncements from "@/views/cases/caseActAnnouncements.vue";
import caseActRegisteredEntires from "@/views/cases/caseActRegisteredEntires.vue";

import { UserPermissions } from "@/utils/enums/enumerators";





export default {
  path: "/cases",
  component: Layout,
  name: "Дела ",
  redirect: "/cases/bankruptcy",
  hidden: false,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
    icon: "mdi-gavel",
  },
  children: [
    {
      path: "/cases/bankruptcy",
      name: "Несъстоятелност",
      component: casesBankruptcyList,
      meta: {
        title: "Дела по несъстоятелност",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/cases/bankruptcy/:id",
      name: "Преглед на дело несъстоятелност",
      component: caseBankruptcyPreview,
      meta: {
        title: "Преглед на дело несъстоятелност",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички дела",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/book",
      name: "Преглед на книга по чл.634в",
      component: caseBankruptcyBookPreview,
      meta: {
        title: "Преглед на дело",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Преглед на книга по чл.634в",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/acts",
      name: "Преглед на актове подлежащи на обжалване",
      component: caseBankruptcyActsPreview,
      meta: {
        title: "Преглед на дело",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Преглед на актове подлежащи на обжалване",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/actsAnnounced",
      name: "Преглед на обявени актове към дело",
      component: caseActAnnouncements,
      meta: {
        title: "Преглед на дело",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Преглед на обявени актове към дело",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/registredEntries",
      name: "Преглед на вписани данни към дело",
      component: caseActRegisteredEntires,
      meta: {
        title: "Преглед на дело",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Преглед на вписани данни към дело",
      },
      hidden: true
    },
    {
      path: "/cases/stabilization",
      name: "Стабилизация",
      component: casesStabilizationList,
      meta: {
        title: "Дела по стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: false
    },
    {
      path: "/cases/stabilization/:id",
      name: "Преглед на дело стабилизация",
      component: caseStabilizationPreview,
      meta: {
        title: "Преглед на дело стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички дела",
      },
      hidden: true
    },
    {
      path: "/cases/stabilization/:id/acts",
      name: "Преглед на актове подлежащи на обжалване стабилизация",
      component: caseStabilizationActsPreview,
      meta: {
        title: "Преглед на актове по дело стабилизация",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
      },
      hidden: true
    },
  ]
}