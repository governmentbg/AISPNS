import { UserPermissions } from "@/utils";
import { eUserRoles } from "@/utils/enums/enumerators";

const getters = {
  loading: (state) => state.app.loading,
  drawer: (state) => state.app.drawer,
  isProduction: (state) => state.app.production,
  sidebarColumns: (state) => state.app.sidebarColumns,
  accessibility: (state) => state.app.accessibility,
  token: (state) => state.user.token,
  user: (state) => state.user,
  currentUser: (state) => state.user.currentUser,
  roles: (state) => state.user.roles,
  permissionRoutes: (state) => state.permission.routes,
  lastRoute: (state) => state.permission.lastRoute,


  // is mei employee
  isMEIEmployee: (state) => state.user.roles.includes(eUserRoles.MEI_EMPLOYEE),

  // isSyndic
  isSyndic: (state) => state.user.roles.some(role => [eUserRoles.SYNDIC].includes(role)),
}

export default getters;
