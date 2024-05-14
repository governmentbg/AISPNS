import Layout from "@/layouts/Layout.vue";
import activitiesAndPropertiesList from "@/views/activitiesAndProperties/activitiesAndPropertiesList.vue";
import activityPreview from "@/views/activitiesAndProperties/activityPreview.vue";
import propertyPreview from "@/views/activitiesAndProperties/propertyPreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/activities",
  component: Layout,
  name: "Дневници ",
  redirect: "/activities",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/activities",
      name: "Дневници",
      component: activitiesAndPropertiesList,
      meta: {
        title: "Дневници",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-notebook-outline",
      },
      hidden: false
    },
    {
      path: "/activities/create",
      name: "Създаване на дневник",
      component: activityPreview,
      meta: {
        title: "Създаване на дневник",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/activities/:id",
      name: "Преглед на дневник",
      component: activityPreview,
      meta: {
        title: "Преглед на дневник",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/activities/create/property",
      name: "Създаване на Маса по несъстоятелност",
      component: propertyPreview,
      meta: {
        title: "Създаване на Маса по несъстоятелност",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
    {
      path: "/activities/:id/property",
      name: "Преглед на Маса по несъстоятелност",
      component: propertyPreview,
      meta: {
        title: "Преглед на Маса по несъстоятелност",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички дневници",
      },
      hidden: true
    },
  ]
}