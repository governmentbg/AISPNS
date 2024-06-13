import Layout from "@/layouts/Layout.vue";
import paymentsList from "@/views/payments/payments.vue";
import paymentPreview from "@/views/payments/payment.vue";
import { UserPermissions } from "@/utils";

export default {
  path: "/payments",
  component: Layout,
  name: "Вноски ",
  redirect: "/payments",
  hidden: true,
  meta: {
    actions: [UserPermissions.SYNDIC],
  },
  children: [
    {
      path: "/payments",
      name: "Вноски",
      component: paymentsList,
      meta: {
        title: "Вноски",
        actions: [UserPermissions.SYNDIC],
        icon: "mdi-cash-sync",
      },
      hidden: false
    },
    {
      path: "/payments/create",
      name: "Нова вноска",
      component: paymentPreview,
      meta: {
        title: "Нова вноска",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички вноски",
      },
      hidden: true
    },
    {
      path: "/payments/:id",
      name: "Преглед на вноска",
      component: paymentPreview,
      meta: {
        title: "Преглед на вноска",
        actions: [UserPermissions.SYNDIC],
        goBackTitle: "Обратно към всички вноски",
      },
      hidden: true
    },
  ]
}