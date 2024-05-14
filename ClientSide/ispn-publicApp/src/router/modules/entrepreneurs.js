import Layout from "@/layouts/Layout.vue";
import entrepreneursBankruptcyList from "@/views/entrepreneurs/entrepreneursBankruptcyList.vue";
import entrepreneurBankruptcyPreview from "@/views/entrepreneurs/entrepreneurBankruptcyPreview.vue"
import entrepreneurBankruptcyProceedingsData from "@/views/entrepreneurs/entrepreneurBankruptcyProceedingsData.vue"
import entrepreneurDeclaredActsOfBankruptcy from "@/views/entrepreneurs/entrepreneurDeclaredActsOfBankruptcy.vue"
import entrepreneurBankruptcyBookPreview from "@/views/entrepreneurs/entrepreneurBankruptcyBookPreview.vue";
import entrepreneurBankruptcyActsPreview from "@/views/entrepreneurs/entrepreneurBankruptcyActsPreview.vue";

import entrepreneursStabilizationList from "@/views/entrepreneurs/entrepreneursStabilizationList.vue";


import entrepreneurStabilizationActsPreview from "@/views/entrepreneurs/entrepreneurStabilizationActsPreview.vue";
import entrepreneurStabilizationProceedingsData from "@/views/entrepreneurs/entrepreneurStabilizationProceedingsData.vue"
import entrepreneurDeclaredActsOfStabilization from "@/views/entrepreneurs/entrepreneurDeclaredActsOfStabilization.vue"
import entrepreneurStabilizationPreview from "@/views/entrepreneurs/entrepreneurStabilizationPreview.vue"

import { UserPermissions } from "@/utils";





export default {
  path: "/entrepreneurs/bankruptcy",
  component: Layout,
  name: "Дела предприемачи",
  redirect: "/entrepreneurs/bankruptcy",
  hidden: false,
  meta: {},
  children: [
    {
      path: "/entrepreneurs/bankruptcy",
      name: "Несъстоятелност по дела предприемачи",
      component: entrepreneursBankruptcyList,
      meta: {
        title: "Дела по несъстоятелност по дела предприемачи",
      },
      hidden: false
    },
    {
      path: "/entrepreneurs/bankruptcy/:id",
      name: "Преглед на дело несъстоятелност предприемач",
      component: entrepreneurBankruptcyPreview,
      meta: {
        title: "Преглед на дело несъстоятелност предприемач",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/bankruptcy/:id/book",
      name: "Преглед на книга по чл.634в предприемач",
      component: entrepreneurBankruptcyBookPreview,
      meta: {
        title: "Преглед на дело",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/bankruptcy/:id/acts",
      name: "Преглед на актове подлежащи на обжалване предприемач",
      component: entrepreneurBankruptcyActsPreview,
      meta: {
        title: "Преглед на дело",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/bankruptcy/:id/proceedings-data",
      name: "Данни за производство по несъстоятелност",
      component: entrepreneurBankruptcyProceedingsData,
      meta: {
        title: "Данни за производство по несъстоятелност",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/bankruptcy/:id/declared-acts",
      name: "Обявени актове по несъстоятелност",
      component: entrepreneurDeclaredActsOfBankruptcy,
      meta: {
        title: "Обявени актове по несъстоятелност",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/stabilization",
      name: "Стабилизация по дела предприемачи",
      component: entrepreneursStabilizationList,
      meta: {
        title: "Дела по стабилизация предприемачи",
      },
      hidden: false
    },
    {
      path: "/entrepreneurs/stabilization/:id",
      name: "Преглед на дело стабилизация предприемач",
      component: entrepreneurStabilizationPreview,
      meta: {
        title: "Преглед на дело стабилизация предприемач",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/stabilization/:id/acts",
      name: "Преглед на актове подлежащи на обжалване предприемач стабилизация",
      component: entrepreneurStabilizationActsPreview,
      meta: {
        title: "Преглед на дело",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/stabilization/:id/proceedings-data",
      name: "Данни за производство по стабилизация",
      component: entrepreneurStabilizationProceedingsData,
      meta: {
        title: "Данни за производство по стабилизация",
      },
      hidden: true
    },
    {
      path: "/entrepreneurs/stabilization/:id/declared-acts",
      name: "Обявени актове по стабилизация",
      component: entrepreneurDeclaredActsOfStabilization,
      meta: {
        title: "Обявени актове по стабилизация",
      },
      hidden: true
    },
  ]
}