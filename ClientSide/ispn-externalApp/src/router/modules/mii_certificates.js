import Layout from "@/layouts/Layout.vue";
import certificatesList from "@/views/mii_certificates/certificatesList.vue";
import messagePreview from "@/views/mii_messages/messagePreview.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/mii_certificates",
  component: Layout,
  name: "Удостоверения  ",
  redirect: "/mii_certificates",
  hidden: true,
  meta: {
    actions: [UserPermissions.MEIEmployee],
  },
  children: [
    {
      path: "/mii_certificates",
      name: "Удостоверения ",
      component: certificatesList,
      meta: {
        title: "Съобщения ",
        actions: [UserPermissions.MEIEmployee],
        icon: "mdi-file-certificate-outline",
      },
      hidden: false
    },
    {
      path: "/mii_certificates/:id",
      name: "Преглед на удостоверение",
      component: messagePreview,
      meta: {
        title: "Преглед на удостоверение",
        actions: [UserPermissions.MEIEmployee],
        goBackTitle: "Обратно към удостоверения",
      },
      hidden: true
    },
  ]
}