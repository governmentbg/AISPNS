import Layout from "@/layouts/Layout.vue";
import trainingsList from "@/views/trainings/trainingsList.vue";
import trainingPreview from "@/views/trainings/trainingPreview.vue";
import trainingProgramPreview from "@/views/trainings/trainingProgramPreview.vue";
import trainingRequestPreview from "@/views/trainings/trainingRequestPreview.vue";
import trainingCoursePreview from "@/views/trainings/trainingCoursePreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/trainings",
  component: Layout,
  name: "Обучения ",
  redirect: "/trainings",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/trainings",
      name: "Обучения",
      component: trainingsList,
      meta: {
        title: "Обучения",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-school-outline",
      },
      hidden: false
    },
    {
      path: "/trainings/requests/create",
      name: "Нова заявка за обучение",
      component: trainingRequestPreview,
      meta: {
        title: "Нова заявка за обучение",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към обучения",
      },
      hidden: true
    },
    {
      path: "/trainings/requests/:id",
      name: "Преглед на заявка",
      component: trainingRequestPreview,
      meta: {
        title: "Преглед на заявка",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към обучения",
      },
      hidden: true
    },
    {
      path: "/trainings/programs/:id",
      name: "Преглед на програма",
      component: trainingProgramPreview,
      meta: {
        title: "Преглед на програма",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към обучения",
      },
      hidden: true
    },
    {
      path: "/trainings/:id",
      name: "Преглед на обучение",
      component: trainingCoursePreview,
      meta: {
        title: "Преглед на обучение",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към обучения",
      },
      hidden: true
    },
  ]
}