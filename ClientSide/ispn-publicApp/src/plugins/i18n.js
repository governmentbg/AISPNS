import { getCookie, setCookie } from '@/utils/auth'
import Vue from 'vue'
import VueI18n from 'vue-i18n'

import bg from 'vuetify/lib/locale/bg'
import en from 'vuetify/lib/locale/en'
import bg_locale from '@/locales/bg.json';
import en_locale from '@/locales/en.json';

Vue.use(VueI18n)

const messages = {
  bg: {
    ...bg_locale,
    $vuetify: bg,
  },
  en: {
    ...en_locale,
    $vuetify: en,
  },
}

let currentLocale = getCookie('_locale');

if(!currentLocale){
  setCookie('_locale', 'bg')
  currentLocale = 'bg'
}

export default new VueI18n({
  locale: currentLocale,
  fallbackLocale: currentLocale,
  messages,
})