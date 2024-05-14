import Vue from "vue";
import App from "./App.vue";

// router
import router from "./router/index";
import "@/permissions"; // permission control for router

// store
import store from "./store";

// plugins
import "./plugins/base";
import Snotify, { SnotifyPosition } from "vue-snotify";
import "./plugins/loading";

// theme
import "vue-snotify/styles/material.css";
import vuetify from "./plugins/vuetify";

const snotify_options = {
  toast: {
    position: SnotifyPosition.rightBottom,
    timeout: 4000,
  },
};

Vue.use(Snotify, snotify_options);

Vue.config.productionTip = false;

export default new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
