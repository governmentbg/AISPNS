import { UserPermissions } from "@/utils";

const getters = {
  loading: (state) => state.app.loading,
  drawer: (state) => state.app.drawer,
  sidebarColumns: (state) => state.app.sidebarColumns,
  isProduction: (state) => state.app.production,
  accessibility: (state) => state.app.accessibility,
  token: (state) => state.user.token,
  user: (state) => state.user,
  isAdminUser: (state) => state.user.access === 0,
  actions: (state) => state.user.access,
  permissionRoutes: (state) => state.permission.routes,
  lastRoute: (state) => state.permission.lastRoute,
  actionsAccess: function(state) {
    return {
      Admin: [UserPermissions.ADMIN].includes(state.user.access),
    }
  },
}

export default getters;
