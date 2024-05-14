import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// sign in to the system
export function apiFakeLogin(data) {
  return request({
    url: '/api/ExternalAPP/Account/FakeLogin',
    method: 'post',
    params: {username: data.username, password: data.password}
  })
}

export function apiEAuthentication(){
  return request({
    url: `/api/ExternalAPP/Authentication/Login`,
    method: "get"
  })
}


export function apiGetCurrentUserInfo(){
  return request({
    url: `/api/ExternalAPP/Account/GetCurrentUserInfo`,
    method: "post"
  })
}

// sign out out the system
// export function apiSignOut() {
//   return request({
//     url: '/api/ExternalAPP/Account/Logout',
//     method: 'post',
//   })
// }


// // get user info
// export function getUserInfo() {
//   return request({
//     url: '/api/ExternalAPP/Account/FakeLogin',
//     method: 'get'
//   })
//   // return new Promise((resolve) => {
//   //   resolve({
//   //     oaRoles: ['FullAccess'],
//   //     // name: 'P+ user',
//   //     // fullName: 'P+ user',
//   //     access: 0
//   //   })
//   // })
// }