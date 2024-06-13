import Layout from "@/layouts/Layout.vue";
import messagesList from "@/views/messages/messagesList.vue";
import messagePreview from "@/views/messages/messagePreview.vue";
import messageReply from "@/views/messages/messageReply.vue";
import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/messages",
  component: Layout,
  name: "Съобщения ",
  redirect: "/messages",
  hidden: true,
  messages: true,
  meta: {
    actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
  },
  children: [
    {
      path: "/messages",
      name: "Съобщения",
      component: messagesList,
      meta: {
        title: "Съобщения",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        icon: "mdi-message-text-outline",
      },
      badgeItem: 'unreadMessagesCount',
      hidden: false
    },
    {
      path: "/messages/:id",
      name: "Преглед на съобщение",
      component: messagePreview,
      meta: {
        title: "Преглед на съобщение",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички съобщения",
      },
      hidden: true
    },
    {
      path: "/messages/:id/:reply",
      name: "Отговор на съобщение",
      component: messageReply,
      meta: {
        title: "Отговор на съобщение",
        actions: [UserPermissions.ADMIN, UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR],
        goBackTitle: "Обратно към всички съобщения",
      },
      hidden: true
    },
  ]
}