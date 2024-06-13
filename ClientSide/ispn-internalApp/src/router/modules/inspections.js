import Layout from "@/layouts/Layout.vue";
import inspectionsList from "@/views/inspections/inspectionsList.vue";
import inspectionPreview from "@/views/inspections/inspectionPreview.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/inspections",
  component: Layout,
  name: "Инспекции ",
  redirect: "/inspections",
  hidden: true,
  meta: {
    actions: [UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/inspections",
      name: "Инспекции",
      component: inspectionsList,
      meta: {
        title: "Инспекции",
        actions: [UserPermissions.INSPECTOR],
        icon: "mdi-magnify-expand",
      },
      hidden: false
    },
    {
      path: "/inspections/:id",
      name: "Преглед на инспекция",
      component: inspectionPreview,
      meta: {
        title: "Преглед на инспекция",
        actions: [UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички инспекции",
      },
      hidden: true
    },
  ]
}