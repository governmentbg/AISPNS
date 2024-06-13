import Cookies from 'js-cookie';
import config from '@/config';

export const TokenKey = 'Auth_Token'

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token, expirationDate) {
  if(config.removeTokenAfterBrowserClose){
    return Cookies.set(TokenKey, token, {expires: '', secure: true});
  } else {
    return Cookies.set(TokenKey, token, {expires: new Date(expirationDate), secure: true });
  }
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}

export function setCookie(key, value) {
  return Cookies.set(key, value)
}

export function getCookie(key) {
  return Cookies.get(key)
}

export function removeCookie(key) {
  return Cookies.remove(key)
}