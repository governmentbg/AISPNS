import Layout from "@/layouts/Layout.vue";
import trainingsList from "@/views/trainings/trainingsList.vue";
import trainingPreview from "@/views/trainings/trainingPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/trainings",
  component: Layout,
  name: "Обучения ",
  redirect: "/trainings",
  hidden: true,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/trainings",
      name: "Обучения",
      component: trainingsList,
      meta: {
        title: "Програми за обучения",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        icon: "mdi-school-outline",
      },
      hidden: false
    },
    {
      path: "/trainings/create",
      name: "Нова програма",
      component: trainingPreview,
      meta: {
        title: "Нова програма",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички програми",
      },
      hidden: true
    },
    {
      path: "/trainings/:id",
      name: "Преглед на програма",
      component: trainingPreview,
      meta: {
        title: "Преглед на програма",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички програми",
      },
      hidden: true
    },
  ]
}