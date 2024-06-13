import Layout from "@/layouts/Layout.vue";
import sellAnnouncementsList from "@/views/mii_sellAnnouncements/sellAnnouncementsList.vue";
import sellAnnouncementPreview from "@/views/mii_sellAnnouncements/sellAnnouncementPreview.vue"
import { UserPermissions } from "@/utils";

export default {
  path: "/mii-sell-announcements",
  component: Layout,
  name: "Обяви за продажба_",
  redirect: "/mii-sell-announcements",
  hidden: true,
  meta: {
    actions: [UserPermissions.MEIEmployee],
  },
  children: [
    {
      path: "/mii-sell-announcements",
      name: "Обяви за продажба  ",
      component: sellAnnouncementsList,
      meta: {
        title: "Обяви за продажба  ",
        actions: [UserPermissions.MEIEmployee],
        icon: "mdi-bullhorn-outline",
      },
      hidden: false
    },
    {
      path: "/mii-sell-announcements/:id",
      name: "Преглед на обява за продажба ",
      component: sellAnnouncementPreview,
      meta: {
        title: "Преглед на обява за продажба ",
        actions: [UserPermissions.MEIEmployee],
        goBackTitle: "Обратно към всички обяви за продажби",
      },
      hidden: true
    },
  ]
}