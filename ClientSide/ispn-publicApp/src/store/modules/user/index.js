//import { login, logout, getInfo } from '@/api/user';
import { removeDuplicateElementsArray } from '@/utils/';
import { getToken, setToken, removeToken, setCookie } from '@/utils/auth';
import router, { resetRouter } from '@/router';
import { signIn, getUserInfo, signOut } from '@/api/authenticate';

const state = {
  token: getToken(),
  name: '',
  username: '',
  id: '',
  access: -1
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
  }
}


const actions = {
  // user login
  login({ commit, dispatch }, credentials) {
    const { username, password } = credentials;

    return new Promise((resolve, reject) => {
      signIn({ username: username.trim(), password: password }).then(async response => {
        if(response && response.token){
          commit('SET_TOKEN', response.token);
          setToken(response.token, response.expiration);
          getUserInfo().then(async (userData) => {
            //userData.oaRoles = ['FullAccess']
            //userData.name = 'Bobo'
            if(userData){
              commit('SET_ACCESS', userData.access);
              commit('SET_NAME', userData.fullName);
              commit('SET_USERNAME', username);

              setCookie("LastUser", username);
              // generate accessible routes map based on roles
              const accessRoutes = await dispatch('permission/generateRoutes', userData.access, { root: true })
              
              let currentRoutes = router.options.routes;

              let routesToAdd = removeDuplicateElementsArray(currentRoutes, accessRoutes);
              
              // dynamically add accessible routes
              if(routesToAdd.length){
                router.addRoutes(routesToAdd);
              }
              resolve()
            }
          })
        } else {
          //reject("Грешно потребителско име или парола");
          reject(response)
        }
      }).catch(error => {
        //reject(error)
        //reject("Грешно потребителско име или парола");
      })
    })
  },

  // get user info
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {

      commit('SET_ACCESS', 0);
      commit('SET_NAME', "Boril Kolev");
      commit('SET_ID', 0);

      commit('app/SET_PRODUCTION', false, { root: true })

      resolve();


      // getUserInfo().then(response => {
      //   if (!response) {
      //     reject('Сесията Ви е изтекла.');
      //   }

      //   // const { roles, name, avatar, introduction } = data
      //   var { fullName, access, isProduction, id } = response;
        
      //   // allowedActions must be a non-empty array
      //   if (access < 0) {
      //     reject('Нямате приложени позволени действия към Вашата роля!');
      //   }

      //   commit('SET_ACCESS', access);
      //   commit('SET_NAME', fullName);
      //   commit('SET_ID', id);

      //   commit('app/SET_PRODUCTION', isProduction, { root: true })
        
      //   resolve(response);
      // }).catch(error => {
      //   //console.log(error);
      //   reject(error)
      // })
    })
  },

  // user signout
  signout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {

      //signOut().then(async () => {
        commit('SET_TOKEN', '');
        // generate accessible routes map based on roles
        //await dispatch('permission/resetRoutes', null, { root: true })
        removeToken();
        resetRouter();

        resolve(true);
      //})
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
        commit('SET_TOKEN', '');
        removeToken();
        resetRouter();

        resolve();
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      //commit('SET_ROLES', [])
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
