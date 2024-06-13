import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get orders list
export function apiSearchOrders(data) {
  return request({
    url: '/api/InternalApp/Order/SearchOrder',
    method: 'post',
    data: data.filter,
    params: {pageSize: data.pageSize, pageNumber: data.pageNumber}
  })
}

// get order by id
export function apiGetOrderById(id) {
  return request({
    url: '/api/InternalApp/Order/GetOrderById',
    params: {id}
  })
}

// create order
export function apiCreateOrder(data) {
  return request({
    url: '/api/InternalApp/Order/CreateOrder',
    method: 'post',
    data: data
  })
}

// update order
export function apiUpdateOrder(data) {
  return request({
    url: '/api/InternalApp/Order/UpdateOrder',
    method: 'post',
    data: data
  })
}

// delete order
export function apiDeleteOrder(id) {
  return request({
    url: '/api/InternalApp/Order/DeleteOrder',
    method: 'post',
    params: {id}
  })
}