import { UserPermissions } from "@/utils/enums/enumerators";

const getters = {
  loading: (state) => state.app.loading,
  drawer: (state) => state.app.drawer,
  isProduction: (state) =>  state.app.production,
  sidebarColumns: (state) => state.app.sidebarColumns,
  accessibility: (state) => state.app.accessibility,
  token: (state) => state.user.token,
  user: (state) => state.user,
  currentUser: (state) => state.user.currentUser,
  roles: (state) => state.user.roles,
  permissionRoutes: (state) => state.permission.routes,
  lastRoute: (state) => state.permission.lastRoute,


  // user roles
  isAdministrator: (state) => state.user.roles.includes(UserPermissions.ADMIN),

  // Employee
  isEmployee: (state) => state.user.roles.some(role => [UserPermissions.EMPLOYEE, UserPermissions.INSPECTOR].includes(role)),

  // Inspector
  isInspector: (state) => state.user.roles.includes(UserPermissions.INSPECTOR),
}

export default getters;
