//import { login, logout, getInfo } from '@/api/user';
import { removeDuplicateElementsArray } from '@/utils/';
import { getToken, setToken, removeToken, setCookie, getCookie } from '@/utils/auth';
import router, { resetRouter } from '@/router';
import { apiFakeLogin, apiGetCurrentUserInfo } from '@/api/authenticate';

const state = {
  token: getToken(),
  currentUser: {
    fullName: null,
    ipAddress: null,
    isAuthenticated: null,
    personId: null,
    roles: null,
    userId: null,
    userName: null
  },
  name: '',
  username: '',
  id: '',
  access: -1,
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_ID: (state, id) => {
    state.id = id;
  },
  SET_USERNAME: (state, username) => {
    state.username = username
  },
  SET_ACCESS: (state, access) => {
    state.access = access
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  SET_CURRENT_USER: (state, person) => {
    state.currentUser = person
  },
  RESET_STATE: (state) => {
    state = {
      token: getToken(),
      currentUser: {
        fullName: null,
        ipAddress: null,
        isAuthenticated: null,
        personId: null,
        roles: null,
        userId: null,
        userName: null
      },
      name: '',
      username: '',
      id: '',
      access: -1,
      roles: []
    }
  }
}


const actions = {
  // user login
  login({ commit, dispatch }, credentials) {
    const { username, password } = credentials;

    return new Promise(async (resolve, reject) => {
      apiFakeLogin({ username: username.trim(), password: password }).then(async response => {
        if(response && response.token){
          commit('SET_TOKEN', response.token);
          setToken(response.token, response.expiration);
          
          apiGetCurrentUserInfo().then(async (userData) => {
            commit('SET_CURRENT_USER', userData);
            commit('SET_NAME', userData.fullName);
            commit('SET_USERNAME', userData.userName);
            setCookie("LastUser", userData.userName.includes("\\") ? userData.userName.split("\\")[1] : userData.userName);
            
            if(!userData.roles.length)
              reject('Нямате приложени позволени действия към Вашата роля!');

            
            commit('SET_ROLES', userData.roles);

            // generate accessible routes map based on roles
            const accessRoutes = await dispatch('permission/generateRoutes', userData.roles, { root: true })
            
            let currentRoutes = router.options.routes;
            let routesToAdd = removeDuplicateElementsArray(currentRoutes, accessRoutes);
            // dynamically add accessible routes
            if(routesToAdd.length)
              router.addRoutes(routesToAdd);

            resolve()
          })
        }
      }).catch(error => {
        //reject(error)
        reject("Грешно потребителско име или парола");
      })
    })
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      apiGetCurrentUserInfo().then(async (userData) => {     
        if(userData){   
          commit('SET_CURRENT_USER', userData);
          commit('SET_NAME', userData.fullName);
          commit('SET_USERNAME', userData.userName);
          setCookie("LastUser", userData.userName.includes("\\") ? userData.userName.split("\\")[1] : userData.userName);
          
          // allowedActions must be a non-empty array
          if (!userData.roles.length)
            reject('Нямате приложени позволени действия към Вашата роля!');
          
          commit('SET_ROLES', userData.roles);
            
          resolve(userData);
        } else {
          commit('SET_TOKEN', '')
          removeToken()
          resetRouter();
          reject('Моля логнете се наново')
        }
      })
    })
  },

  // user signout
  signout({ commit, state, dispatch }) {
    return new Promise(async (resolve, reject) => {
      try {
        commit('SET_TOKEN', '')
        removeToken()
        resetRouter();
        resolve(true);
      } catch(e){
        reject(e)
      }
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      commit('SET_TOKEN', '')
      commit('RESET_STATE')
      commit('SET_ROLES', [])
      removeToken();
      resetRouter();

      resolve();
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('RESET_STATE')
      commit('SET_ROLES', [])
      removeToken()
      resolve()
    })
  },
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
