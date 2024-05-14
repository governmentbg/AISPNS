import Layout from "@/layouts/Layout.vue";
import sellAnnouncementsBySyndicsList from "@/views/mii_sellAnnouncementsBySyndics/sellAnnouncementsBySyndicsList.vue";
import sellAnnouncementBySyndicsPreview from "@/views/mii_sellAnnouncementsBySyndics/sellAnnouncementBySyndicPreview.vue"
import { UserPermissions } from "@/utils";

export default {
  path: "/mii-sell-announcements-by-syndics",
  component: Layout,
  name: "Обяви за продажба от синдици",
  redirect: "/mii-sell-announcements",
  hidden: true,
  meta: {
    actions: [UserPermissions.MEIEmployee],
  },
  children: [
    {
      path: "/mii-sell-announcements-by-syndics",
      name: "Обяви от синдици за обработка",
      component: sellAnnouncementsBySyndicsList,
      meta: {
        title: "Обяви за продажба от синдици",
        actions: [UserPermissions.MEIEmployee],
        icon: "mdi-clipboard-account-outline",
      },
      hidden: false
    },
    {
      path: "/mii-sell-announcements-by-syndics/:id",
      name: "Преглед на обява за продажба от синдик",
      component: sellAnnouncementBySyndicsPreview,
      meta: {
        title: "Преглед на обява за продажба от синдик",
        actions: [UserPermissions.MEIEmployee],
        goBackTitle: "Обратно към всички обяви за продажби",
      },
      hidden: true
    },
  ]
}