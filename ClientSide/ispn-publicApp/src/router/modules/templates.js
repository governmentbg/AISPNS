import Layout from "@/layouts/Layout.vue";
import templatesList from "@/views/templates/templatesList.vue";

export default {
  path: "/templates",
  component: Layout,
  name: "Образци ",
  redirect: "/templates",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/templates",
      name: "Образци",
      component: templatesList,
      meta: {
        title: "Образци",
      },
      hidden: false
    },
  ]
}