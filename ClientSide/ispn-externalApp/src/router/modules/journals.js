import Layout from "@/layouts/Layout.vue";
import journalsList from "@/views/journals/journalsList.vue";
import journalPreview from "@/views/journals/journalPreview.vue";
import bankruptcyPreview from "@/views/journals/bankruptcyPreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/journals",
  component: Layout,
  name: "Дневници ",
  redirect: "/journals",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/journals",
      name: "Дневници",
      component: journalsList,
      meta: {
        title: "Дневници",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-notebook-outline",
      },
      hidden: false
    },
    {
      path: "/journals/create",
      name: "Създаване на дневник",
      component: journalPreview,
      meta: {
        title: "Създаване на дневник",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/journals/:id",
      name: "Преглед на дневник",
      component: journalPreview,
      meta: {
        title: "Преглед на дневник",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/journals/create/bankruptcy",
      name: "Създаване на Маса по несъстоятелност",
      component: bankruptcyPreview,
      meta: {
        title: "Създаване на Маса по несъстоятелност",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/journals/:id/bankruptcy",
      name: "Преглед на Маса по несъстоятелност",
      component: bankruptcyPreview,
      meta: {
        title: "Преглед на Маса по несъстоятелност",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
  ]
}