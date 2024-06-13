import Layout from "@/layouts/Layout.vue";
import legalBasisList from "@/views/legalBasis/legalBasisList.vue";

export default {
  path: "/legal-basis",
  component: Layout,
  name: "Нормативна уредба ",
  redirect: "/legal-basis",
  hidden: true,
  meta: {},
  children: [
    {
      path: "/legal-basis",
      name: "Нормативна уредба",
      component: legalBasisList,
      meta: {
        title: "Нормативна уредба",
      },
      hidden: false
    },
  ]
}