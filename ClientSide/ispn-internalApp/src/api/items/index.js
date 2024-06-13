import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// getting items
export function getItems(query) {
  return request({
    url: '/api/items/list',
    method: 'get',
    params: filterQuery(query)
  })
}

// getting searched items
export function getSearchItems(data){
  return request({
    url: '/api/items/search',
    method: 'post',
    data: data
  })
}

// getting item
export function getItem(id) {
  return request({
    url: `/api/items/${id}`,
    method: 'get'
  })
}

// getting item by EPC
export function getItemByEPC(epc) {
  return request({
    url: `/api/items/epc/${epc}`,
    method: 'get'
  })
}

// importing items
export function importItems(data){
  return request({
    url: `/api/items/import`,
    method: 'post',
    headers: {
      'Content-Type': 'multipart/form-data'
    },
    data: data
  })
}

// write item epc tag
export function writeItemEPC(data){
  return request({
    url: `/api/items/write`,
    method: 'post',
    data: data
  })
}

// delete item
export function deleteItem(data){
  return request({
    url: `/api/items/delete`,
    method: 'post',
    data: data
  })
}
