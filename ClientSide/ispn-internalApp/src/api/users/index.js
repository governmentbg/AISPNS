import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// getting users list
export function getUsersList(query) {
  return request({
    url: '/api/users/list',
    method: 'get',
    params: filterQuery(query)
  })
}

// getting user
export function getUser(id) {
  return request({
    url: `/api/users/${id}`,
    method: 'get'
  })
}

// saving user
export function saveUser(data) {
  return request({
    url: `/api/users/save`,
    method: 'post',
    data: data
  })
}
