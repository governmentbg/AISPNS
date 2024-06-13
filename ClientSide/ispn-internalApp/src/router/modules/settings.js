import Layout from "@/layouts/Layout.vue";
import NomenclaturesList from "@/views/settings/nomenclatures/nomenclatureTabs.vue"
import UsersList from "@/views/settings/users/usersList.vue";
import TemplatesList from "@/views/settings/templates/templatesList.vue";
import DeadlinesList from "@/views/settings/deadlines/deadlinesList.vue";
import DocumentTemplatesList from "@/views/settings/documentTemplates/documentTemplatesList.vue"
import UserPreview from "@/views/settings/users/previewUser.vue";
import UserActionsLog from "@/views/settings/audit/userActionsLog.vue";

import { eUserRoles } from "@/utils/enums/enumerators";

export default {
  path: "/admin",
  component: Layout,
  name: "Администрация",
  redirect: "/admin/nomenclatures/list",
  hidden: false,
  meta: {
    title: "Администрация",
    roles: [eUserRoles.ADMINISTRATOR],
    icon: "mdi-cogs",
    divider: true
  },
  children: [
    {
      path: "/admin/users",
      name: "Потребители",
      component: UsersList,
      meta: {
        title: "Потребители",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: 'mdi-account-supervisor'
      },
      hidden: false
    },
    {
      path: "/admin/users/:id",
      name: "Преглед на потребител",
      component: UserPreview,
      meta: {
        title: "Преглед на потребител",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: 'mdi-account'
      },
      hidden: true
    },
    {
      path: "/admin/nomenclatures/list",
      name: "Номенклатури",
      component: NomenclaturesList,
      meta: {
        title: "Номенклатури",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-text-box-check-outline",
      },
      hidden: false
    },
    {
      path: "/admin/deadlines",
      name: "Срокове",
      component: DeadlinesList,
      meta: {
        title: "Срокове",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-clipboard-text-clock-outline",
      },
      hidden: false
    },
    {
      path: "/admin/document-templates",
      name: "Образци",
      component: DocumentTemplatesList,
      meta: {
        title: "Образци",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-file-alert-outline",
      },
      hidden: false
    },
    {
      path: "/admin/userActionsLog",
      name: "Журнал",
      component: UserActionsLog,
      meta: {
        title: "Журнал",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-target-account",
      },
      hidden: false
    },
    {
      path: "/admin/settings",
      name: "Настройки",
      component: UserActionsLog,
      meta: {
        title: "Настройки",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-cogs",
      },
      hidden: false
    },
    {
      path: "/admin/templates",
      name: "Шаблони",
      component: TemplatesList,
      meta: {
        title: "Шаблони",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-text-box-multiple-outline",
      },
      hidden: false
    },
    {
      path: "/admin/web-service",
      name: "Контрол на WEB сървиса",
      component: UserActionsLog,
      meta: {
        title: "Контрол на WEB сървиса",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-account-wrench-outline",
      },
      hidden: false
    },
    {
      path: "/admin/acts-configuration",
      name: "Конфигуратор на актове",
      component: UserActionsLog,
      meta: {
        title: "Конфигуратор на актове",
        roles: [eUserRoles.ADMINISTRATOR],
        icon: "mdi-monitor-account",
      },
      hidden: false
    },
  ]
}