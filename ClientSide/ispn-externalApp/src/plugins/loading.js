
import Vue from "vue";
import VueLoading from "./vuejs-loading-plugin";

Vue.use(VueLoading, {
  dark: true,
  text: 'Моля изчакайте...',
  loading: false,
  background: 'rgba(41, 41, 41, 0.65)'
})