import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// sign in to the system
export function apiLogin(data) {
  return request({
    url: '/api/InternalApp/Account/Login',
    method: 'post',
    data: data
  })
}

export function apiGetCurrentUserInfo() {
  return request({
    url: '/api/InternalApp/Account/GetCurrentUserInfo',
    method: 'post'
  })
}

// sign out out the system
export function apiSignOut() {
  return request({
    url: '/api/InternalApp/Account/Logout',
    method: 'post',
  })
}


// USERS //

export function apiGetUsers(query){
  return request({
    url: '/api/InternalApp/Account/GetUsers',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

export function apiGetUserById(id){
  return request({
    url: '/api/InternalApp/Account/GetUserById',
    method: 'post',
    params: {id}
  })
}

export function apiCreateUser(data){
  return request({
    url: '/api/InternalApp/Account/CreateUser',
    method: 'post',
    data: data
  })
}

export function apiUpdateUser(data){
  return request({
    url: '/api/InternalApp/Account/UpdateUser',
    method: 'post',
    data: data
  })
}