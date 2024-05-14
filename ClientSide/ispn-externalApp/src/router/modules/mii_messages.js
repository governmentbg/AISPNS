import Layout from "@/layouts/Layout.vue";
import messagesList from "@/views/mii_messages/messagesList.vue";
import messagePreview from "@/views/mii_messages/messagePreview.vue";
import messageReply from "@/views/mii_messages/messageReply.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/mii_messages",
  component: Layout,
  name: "Съобщения_",
  redirect: "/mii_messages",
  hidden: true,
  meta: {
    actions: [UserPermissions.MEIEmployee],
  },
  children: [
    {
      path: "/mii_messages",
      name: "Съобщения  ",
      component: messagesList,
      meta: {
        title: "Съобщения  ",
        actions: [UserPermissions.MEIEmployee],
        icon: "mdi-message-text-outline",
      },
      hidden: false
    },
    {
      path: "/mii_messages/:id",
      name: "Преглед на съобщение от синдик",
      component: messagePreview,
      meta: {
        title: "Преглед на съобщение от синдик",
        actions: [UserPermissions.MEIEmployee],
        goBackTitle: "Обратно към всички съобщения",
      },
      hidden: true
    },
    {
      path: "/mii_messages/:id/reply",
      name: "Отговор на съобщение към синдик",
      component: messageReply,
      meta: {
        title: "Отговор на съобщение към синдик",
        actions: [UserPermissions.MEIEmployee],
        goBackTitle: "Обратно към всички съобщения",
      },
      hidden: true
    },
  ]
}