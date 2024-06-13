import Layout from "@/layouts/Layout.vue";
import NomenclaturesList from "@/views/admin/nomenclatures/nomenclatureTabs.vue"
import NomenclaturesList1 from "@/views/admin/nomenclatures/nomenclatureTabs1.vue"
import UsersList from "@/views/admin/users/usersList.vue";
import TemplatesList from "@/views/admin/templates/templatesList.vue";
import WebServiceList from "@/views/admin/webService/webServiceList.vue";
import WebServicePreview from "@/views/admin/webService/webServicePreview.vue";
import DeadlinesList from "@/views/admin/deadlines/deadlinesList.vue";
import DocumentTemplatesList from "@/views/admin/documentTemplates/documentTemplatesList.vue"
import UserPreview from "@/views/admin/users/previewUser.vue";
import SettingsPreview from "@/views/admin/settings/settingsPreview.vue";
import UserActionsLog from "@/views/admin/audit/userActionsLog.vue";
import LegalBasisList from "@/views/admin/legalBasis/legalBasisList.vue";

import { UserPermissions } from "@/utils/enums/enumerators";

export default {
  path: "/admin",
  component: Layout,
  name: "Администрация",
  redirect: "/admin/nomenclatures/list",
  hidden: false,
  meta: {
    title: "Администрация",
    actions: [UserPermissions.ADMIN],
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
        actions: [UserPermissions.ADMIN],
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
        actions: [UserPermissions.ADMIN],
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
        actions: [UserPermissions.ADMIN],
        icon: "mdi-text-box-check-outline",
      },
      hidden: false
    },
    // {
    //   path: "/admin/nomenclatures/list1",
    //   name: "Номенклатури1",
    //   component: NomenclaturesList1,
    //   meta: {
    //     title: "Номенклатури1",
    //     actions: [UserPermissions.ADMIN],
    //     icon: "mdi-text-box-check-outline",
    //   },
    //   hidden: false
    // },
    // {
    //   path: "/admin/deadlines",
    //   name: "Срокове",
    //   component: DeadlinesList,
    //   meta: {
    //     title: "Срокове",
    //     actions: [UserPermissions.ADMIN],
    //     icon: "mdi-clipboard-text-clock-outline",
    //   },
    //   hidden: false
    // },
    {
      path: "/admin/document-templates",
      name: "Образци",
      component: DocumentTemplatesList,
      meta: {
        title: "Образци",
        actions: [UserPermissions.ADMIN],
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
        actions: [UserPermissions.ADMIN],
        icon: "mdi-target-account",
      },
      hidden: false
    },
    {
      path: "/admin/settings",
      name: "Настройки",
      component: SettingsPreview,
      meta: {
        title: "Настройки",
        actions: [UserPermissions.ADMIN],
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
        actions: [UserPermissions.ADMIN],
        icon: "mdi-text-box-multiple-outline",
      },
      hidden: false
    },
    {
      path: "/admin/legal-basis",
      name: "Нормативна уредба",
      component: LegalBasisList,
      meta: {
        title: "Нормативна уредба",
        actions: [UserPermissions.ADMIN],
        icon: "mdi-file-document-outline",
      },
      hidden: false
    },
    {
      path: "/admin/web-service",
      name: "Контрол на WEB сървиса",
      component: WebServiceList,
      meta: {
        title: "Контрол на WEB сървиса",
        actions: [UserPermissions.ADMIN],
        icon: "mdi-relation-one-or-many-to-zero-or-many",
      },
      hidden: false
    },
    {
      path: "/admin/web-service/:id",
      name: "Контрол на WEB сървиса ",
      component: WebServicePreview,
      meta: {
        title: "Контрол на WEB сървиса",
        actions: [UserPermissions.ADMIN],
        icon: "mdi-relation-one-or-many-to-zero-or-many",
      },
      hidden: true
    },
    // {
    //   path: "/admin/acts-configuration",
    //   name: "Конфигуратор на актове",
    //   component: UserActionsLog,
    //   meta: {
    //     title: "Конфигуратор на актове",
    //     actions: [UserPermissions.ADMIN],
    //     icon: "mdi-monitor-account",
    //   },
    //   hidden: false
    // },
  ]
}