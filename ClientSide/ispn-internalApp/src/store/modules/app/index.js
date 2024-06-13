import config from "@/config";

const state = {
  loading: true,
  drawer: true,
  production: config.isProduction,
  accessibility: {
    modal: {
      open: false,
    },
    font: {
      size: 1,
      maxSize: 1.4,
      minSize: 0.8,
      family: "",
    },
    theme: {
      type: "light",
    },
  },
};


const mutations = {
  TOGGLE_LOADING: (state) => {
    state.loading = !state.loading;
  },
  STOP_LOADING: (state) => {
    state.loading = false;
  },
  START_LOADING: (state) => {
    state.loading = true;
  },
  SET_DRAWER: (state, payload) => {
    state.drawer = payload;
  },
  SET_PRODUCTION: (state, value) => {
    state.isProduction = value;
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
};

const actions = {
  toggleLoading({ commit }) {
    commit("TOGGLE_LOADING");
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
};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
};
