import { getAppEnvironment } from "@/api/authenticate";
import vue from "../../../main";

const state = {
  loading: true,
  drawer: false,
  sidebarColumns: 4,
  production: true,
  accessibility: {
    modal: {
      open: false,
    },
    font: {
      size: 1,
      maxSize: 1.8,
      minSize: 0.8,
      family: ''
    },
    theme: {
      type: 'light'
    }
    // fontSize: 1,
    // maxFontSize: 1.8,
    // minFontSize: 0.8,
    // blindColor: false
  }
}

const mutations = {
  TOGGLE_LOADING: state => {
    state.loading = !state.loading;
  },
  STOP_LOADING: state => {
    state.loading = false;
  },
  START_LOADING: state => {
    state.loading = true;
  },
  SET_DRAWER: (state, payload) => {
    state.drawer = payload;
  },
  SET_PRODUCTION: (state, value) => {
    state.production = value;
    // if(!state.production){
    //   let theme = {
    //     primary: '#005538',
    //     secondary: '#f60',
    //     accent: '#9C27b0',
    //     info: '#00CAE3',
    //     success: '#52ae30'
    //   }
    //   vue.$vuetify.theme.themes.dark = theme;
    //   vue.$vuetify.theme.themes.light = theme;
    // }
  },
  SIDEBAR_COLUMNS: (state, value) => {
    state.sidebarColumns = value;
  },
  TOGGLE_ACCESSIBILITY_MODAL: state => {
    state.accessibility.modal.open = !state.accessibility.modal.open;
  },
  CLOSE_ACCESSIBILITY_MODAL: state => {
    state.accessibility.modal.open = false;
  },
  BIGGER_FONT_SIZE: state => {
    if(state.accessibility.font.size < state.accessibility.font.maxSize)
      state.accessibility.font.size += 0.1
  },
  SMALLER_FONT_SIZE: state => {
    if(state.accessibility.font.size > state.accessibility.font.minSize)
      state.accessibility.font.size -= 0.1
  },
  DEFAULT_FONT_SIZE: state => {
    state.accessibility.font.size = 1;
  },
  SET_LIGHT_THEME: state => {
    state.accessibility.theme.type = "light"
  },
  SET_DARK_THEME: state => {
    state.accessibility.theme.type = "dark"
  },
  SET_COLOR_BLIND_THEME: state => {
    state.accessibility.theme.type = "colorBlind"
  },
  SET_FONT_FAMILY: (state, value) => {
    if(value){
      state.accessibility.font.family = value;
    } else {
      state.accessibility.font.family = '';
    }
  },
}

const actions = {
  toggleLoading({ commit }) {
    commit('TOGGLE_LOADING')
  },
  toggleAccessibilityModal({ commit }) {
    commit('TOGGLE_ACCESSIBILITY_MODAL')
  },
  closeAccessibilityModal({ commit }) {
    commit('CLOSE_ACCESSIBILITY_MODAL')
  },
  biggerFontSize({commit}){
    commit("BIGGER_FONT_SIZE")
  },
  smallerFontSize({commit}){
    commit("SMALLER_FONT_SIZE")
  },
  defaultFontSize({commit}){
    commit("DEFAULT_FONT_SIZE")
  },
  setLightTheme({commit}){
    commit("SET_LIGHT_THEME")
  },
  setDarkTheme({commit}){
    commit("SET_DARK_THEME")
  },
  setColorBlindTheme({commit}){
    commit("SET_COLOR_BLIND_THEME")
  },
  setFontFamily({commit}, fontFamily){
    commit("SET_FONT_FAMILY", fontFamily)
  },
  stopLoading({ commit }){
    commit('STOP_LOADING');
  },
  startLoading({ commit }){
    commit('START_LOADING');
  },
  setSidebarColumns({commit}, columns){
    commit('SIDEBAR_COLUMNS', columns)
  },
  // get app environment
  getAppEnvironment({ commit, state }) {
    return new Promise((resolve, reject) => {
      getAppEnvironment().then(data => {

        commit('SET_PRODUCTION', data);
        resolve(data);
      }).catch(error => {
        reject(error)
      })
    })
  },
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}