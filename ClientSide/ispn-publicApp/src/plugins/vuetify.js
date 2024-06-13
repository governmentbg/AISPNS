import { getCookie, setCookie } from '@/utils/auth'
import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import '@/sass/overrides.sass';
//import bg from './vuetify-locale/bg';

import bg from 'vuetify/lib/locale/bg';
import en from 'vuetify/lib/locale/en';

Vue.use(Vuetify)

export const themeLight = {
  primary: '#8a6948',
  //secondary: '#5d1b2a',
  secondary: '#4b4d4d',
  primary_accent: '#95858a',
  accent: '#ffde00',
  info: '#45aebb',
  success: "#63a14c",
  error: "#e52e2e",
  warning: "#ff9500"
}

export const themeDark = {
  primary: '#FFFFFF',
  secondary: '#ababab',
  primary_accent: '#95858a',
  accent: '#ffde00',
  info: '#45aebb',
  success: "#63a14c",
  error: "#d16c6c",
  warning: "#FF9500"
}

let currentLocale = getCookie('_locale');

if(!currentLocale){
  setCookie('_locale', 'bg')
  currentLocale = 'bg'
}

export default new Vuetify({
  lang: { 
    locales: { bg, en },
    current: currentLocale
  },
  theme: {
    themes: {
      light: themeLight,
      dark: themeDark
    },
  },
})
