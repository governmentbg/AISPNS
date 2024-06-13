
import Layout from "@/layouts/Layout.vue";
import documentsList from "@/views/documents/documentsList.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/documents",
  component: Layout,
  name: "Документи ",
  redirect: "/documents",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/documents",
      name: "Документи",
      component: documentsList,
      meta: {
        title: "Документи",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-file-document-multiple-outline",
      },
      hidden: false
    },
  ]
}