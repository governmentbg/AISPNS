import Layout from "@/layouts/Layout.vue";
import profilePreview from "@/views/profile/profile.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/profile",
  component: Layout,
  name: "Профил ",
  redirect: "/profile",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/profile",
      name: "Профил",
      component: profilePreview,
      meta: {
        title: "Профил",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-account-circle-outline",
      },
      hidden: false
    },
  ]
}