import Vue from 'vue'
import Vuex from 'vuex'
import getters from './getters'
import createPersistedState from "vuex-persistedstate";

import userStore from './modules/user/index.js';
import appStore from './modules/app/index.js';
import permissionStore from './modules/permission/index.js';

Vue.use(Vuex);

const store = new Vuex.Store({
  getters,
  modules: {
    user: userStore,
    app: appStore,
    permission: permissionStore,
  },
  plugins: [createPersistedState({
    paths: [
      "app.drawer",
      "app.accessibility",
      "user.name",
    ]
  })]
})

export default store;
