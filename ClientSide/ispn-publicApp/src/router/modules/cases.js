import Layout from "@/layouts/Layout.vue";
import casesBankruptcyByCaseList from "@/views/cases/casesBankruptcyByCaseList.vue";
import casesBankruptcyBySideList from "@/views/cases/casesBankruptcyBySideList.vue";
import caseBankruptcyPreview from "@/views/cases/caseBankruptcyPreview.vue"
import caseBankruptcyBookPreview from "@/views/cases/caseBankruptcyBookPreview.vue";
import caseBankruptcyActsPreview from "@/views/cases/caseBankruptcyActsPreview.vue";

import casesStabilizationByCaseList from "@/views/cases/casesStabilizationByCaseList.vue";
import casesStabilizationBySideList from "@/views/cases/casesStabilizationBySideList.vue";
import caseStabilizationPreview from "@/views/cases/caseStabilizationPreview.vue"
import caseStabilizationActsPreview from "@/views/cases/caseStabilizationActsPreview.vue";

import { UserPermissions } from "@/utils";





export default {
  path: "/cases",
  component: Layout,
  name: "Дела ",
  redirect: "/cases/bankruptcy/by-case",
  hidden: false,
  meta: {},
  children: [
    {
      path: "/cases/bankruptcy/by-case",
      name: "Несъстоятелност по дело",
      component: casesBankruptcyByCaseList,
      meta: {
        title: "Дела по несъстоятелност по дело",
      },
      hidden: false
    },
    {
      path: "/cases/bankruptcy/by-side",
      name: "Несъстоятелност по страна",
      component: casesBankruptcyBySideList,
      meta: {
        title: "Дела по несъстоятелност по страна",
      },
      hidden: false
    },
    {
      path: "/cases/bankruptcy/:id",
      name: "Преглед на дело несъстоятелност",
      component: caseBankruptcyPreview,
      meta: {
        title: "Преглед на дело несъстоятелност",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/book",
      name: "Преглед на книга по чл.634в",
      component: caseBankruptcyBookPreview,
      meta: {
        title: "Преглед на дело",
      },
      hidden: true
    },
    {
      path: "/cases/bankruptcy/:id/acts",
      name: "Преглед на актове подлежащи на обжалване",
      component: caseBankruptcyActsPreview,
      meta: {
        title: "Преглед на дело",
      },
      hidden: true
    },
    {
      path: "/cases/stabilization/by-case",
      name: "Стабилизация по дело",
      component: casesStabilizationByCaseList,
      meta: {
        title: "Дела по стабилизация по дело",
      },
      hidden: false
    },
    {
      path: "/cases/stabilization/by-side",
      name: "Стабилизация по страна",
      component: casesStabilizationBySideList,
      meta: {
        title: "Дела по стабилизация по страна",
      },
      hidden: false
    },
    {
      path: "/cases/stabilization/:id",
      name: "Преглед на дело стабилизация",
      component: caseStabilizationPreview,
      meta: {
        title: "Преглед на дело стабилизация",
      },
      hidden: true
    },
    {
      path: "/cases/stabilization/:id/acts",
      name: "Преглед на актове подлежащи на обжалване стабилизация",
      component: caseStabilizationActsPreview,
      meta: {
        title: "Преглед на актове по дело стабилизация",
      },
      hidden: true
    },
  ]
}