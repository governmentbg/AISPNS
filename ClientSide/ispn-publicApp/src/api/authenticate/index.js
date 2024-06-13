import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// sign in to the system
export function signIn(data) {
  return request({
    url: '/api/auth/signIn',
    method: 'post',
    data: data
  })
}

// sign out out the system
export function signOut(data) {
  return request({
    url: '/api/auth/signOut',
    method: 'get',
  })
}

// get user info
export function getUserInfo() {
  return request({
    url: '/api/auth/getCurrentUserInfo',
    method: 'get'
  })
}

// get app environment
export function getAppEnvironment(){
  return request({
    url: '/api/auth/IsProductionEnvironment',
    method: 'get'
  })
}