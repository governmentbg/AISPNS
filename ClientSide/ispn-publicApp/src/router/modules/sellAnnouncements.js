import Layout from "@/layouts/Layout.vue";
import sellAnnouncementsList from "@/views/sellAnnouncements/sellAnnouncementsList.vue";
import sellAnnouncementPreview from "@/views/sellAnnouncements/sellAnnouncementPreview.vue";
import sellAnnouncementContacts from "@/views/sellAnnouncements/sellAnnouncementContacts.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/sell-announcements",
  component: Layout,
  name: "Обяви за продажба ",
  redirect: "/sell-announcements",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/sell-announcements",
      name: "Обяви за продажба",
      component: sellAnnouncementsList,
      meta: {
        title: "Обяви за продажба",
      },
      hidden: false
    },
    {
      path: "/sell-announcements/contacts",
      name: "Контакти обяви за продажба",
      component: sellAnnouncementContacts,
      meta: {
        title: "Контакти обяви за продажба",
      },
      hidden: true
    },
    {
      path: "/sell-announcements/:id",
      name: "Преглед на обява за продажба",
      component: sellAnnouncementPreview,
      meta: {
        title: "Преглед на обява за продажба",
      },
      hidden: true
    },
  ]
}