import Layout from "@/layouts/Layout.vue";
import sellAnnouncementsList from "@/views/sellAnnouncements/sellAnnouncementsList.vue";
import sellAnnouncementPreview from "@/views/sellAnnouncements/sellAnnouncementPreview.vue"
import { UserPermissions } from "@/utils";

export default {
  path: "/sell-announcements",
  component: Layout,
  name: "Обяви за продажба ",
  redirect: "/sell-announcements",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/sell-announcements",
      name: "Обяви за продажба",
      component: sellAnnouncementsList,
      meta: {
        title: "Обяви за продажба",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-bullhorn-outline",
      },
      hidden: false
    },
    {
      path: "/sell-announcements/:id",
      name: "Преглед на обява за продажба",
      component: sellAnnouncementPreview,
      meta: {
        title: "Преглед на обява за продажба",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички обяви за продажби",
      },
      hidden: true
    },
  ]
}