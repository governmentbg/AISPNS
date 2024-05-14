import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import '@/sass/overrides.sass';
import bg from './vuetify-locale/bg';
import { Intersect, ClickOutside, Mutate, Resize, Ripple, Scroll, Touch } from 'vuetify/lib/directives';

Vue.use(Vuetify, {
  directives: {
    Intersect,
    ClickOutside, 
    Mutate, 
    Resize, 
    Ripple, 
    Scroll, 
    Touch
  }
})

const theme = {
  primary: '#8a6948',
  secondary: '#4b4d4d',
  accent: '#ffde00',
  info: '#45aebb',
  error: '#d50000',
  success: "#63a14c"
}

export default new Vuetify({
  lang: { 
    locales: { bg },
    current: 'bg'
  },
  theme: {
    themes: {
      dark: theme,
      light: theme,
    },
  },
})
