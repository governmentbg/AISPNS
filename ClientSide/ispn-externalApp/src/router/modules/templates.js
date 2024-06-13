import Layout from "@/layouts/Layout.vue";
import templatesList from "@/views/templates/templatesList.vue";
import templatePreview from "@/views/templates/templatePreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/templates",
  component: Layout,
  name: "Образци ",
  redirect: "/templates",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/templates",
      name: "Образци",
      component: templatesList,
      meta: {
        title: "Образци",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-file-document-edit-outline",
      },
      hidden: false
    },
    {
      path: "/templates/:id",
      name: "Преглед на образец",
      component: templatePreview,
      meta: {
        title: "Преглед на образец",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички образци",
      },
      hidden: true
    },
  ]
}