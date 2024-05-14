import Layout from "@/layouts/Layout.vue";
import contactPreview from "@/views/mii_contacts/contact.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/mii_contacts",
  component: Layout,
  name: "Контактна информация  ",
  redirect: "/mii_contacts",
  hidden: true,
  meta: {
    actions: [UserPermissions.MEIEmployee],
  },
  children: [
    {
      path: "/mii_contacts",
      name: "Контактна информация ",
      component: contactPreview,
      meta: {
        title: "Контактна информация ",
        actions: [UserPermissions.MEIEmployee],
        icon: "mdi-book-open-blank-variant",
      },
      hidden: false
    },
  ]
}