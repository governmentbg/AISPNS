import router, { asyncRoutes, constantRoutes } from '@/router'
import { removeDuplicateElementsArray } from '@/utils'

/**
 * Use meta.role to determine if the current user has permission
 * @param actions
 * @param route
 */

function hasPermission(userAccess, route) {
  if (route.meta && route.meta.actions) {
    // return actions.some(role => route.meta.actions.includes(role))
    // console.log(route.meta.actions)
    // console.log(userAccess);
    // console.log(route.meta.actions.includes(userAccess));
    // console.log("__________________________")
    return route.meta.actions.includes(userAccess)
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param actions asyncRoutes
 * @param actions
 */
export function filterAsyncRoutes(routes, userAccess) {
  const res = []
  
  routes.forEach(route => {
    const tmp = { ...route };
    if (hasPermission(userAccess, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, userAccess)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: [],
  lastRoutes: [],
  lastRoute: {}
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    let _constantRoutes = [...constantRoutes]
    state.addRoutes = routes

    let routesToBeAdded = removeDuplicateElementsArray(state.routes, routes);

    if(routesToBeAdded.length){
      state.routes = _constantRoutes.concat(routesToBeAdded)
    }
  },
  RESET_ROUTES: (state) => {
    state.routes = [];
    state.addRoutes = [];
    state.lastRoutes = [];
    state.lastRoute = {};
  },
  ADD_LAST_ROUTE: (state, route) => {
    state.lastRoutes.unshift(route);
    state.lastRoute = route;
    if(state.lastRoutes.length >= 5){
      state.lastRoutes.pop();
    }
  },
  REMOVE_LAST_2_ROUTES: (state) => {
    state.lastRoutes.shift();
    state.lastRoutes.shift();
    state.lastRoute = state.lastRoutes[0];
  }
}

const actions = {
  generateRoutes({ commit }, userAccess) {
    return new Promise(resolve => {
      let accessedRoutes
      accessedRoutes = filterAsyncRoutes(asyncRoutes, userAccess)

      commit('SET_ROUTES', accessedRoutes);
      resolve(accessedRoutes);
    })
  },

  resetRoutes({commit}) {
    commit('RESET_ROUTES');
  },

  addLastRoute({commit}, route) {
    commit('ADD_LAST_ROUTE', route)
  },

  goBack({commit}){
    router.push({path: state.lastRoute.path});
    commit('REMOVE_LAST_2_ROUTES');
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
