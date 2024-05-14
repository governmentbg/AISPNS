import Vue from 'vue'
import App from './App.vue'

// router
import router from './router/index'
//import '@/permissions' // permission control for router

// store 
import store from './store'

// plugins
import VueTheMask from 'vue-the-mask';
import './plugins/base'
import Snotify, { SnotifyPosition } from 'vue-snotify';
import './plugins/loading';
import i18n from './plugins/i18n'

import VCalendar from 'v-calendar';

// theme
import 'vue-snotify/styles/material.css';
import vuetify from './plugins/vuetify'

const snotify_options = {
  toast: {
    position: SnotifyPosition.rightBottom,
    timeout: 4000
  }
}

Vue.use(VCalendar, {
  componentPrefix: 'vc'
})

//Vue.prototype.$log = console.log

Vue.use(VueTheMask);
Vue.use(Snotify, snotify_options);

Vue.config.productionTip = false;

export default new Vue({
  router,
  store,
  vuetify,
  i18n,
  render: h => h(App),
}).$mount('#app')
