import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get Installment by id
export function apiGetInstallmentById(id) {
  return request({
    url: '/api/InternalApp/Installment/GetInstallmentById',
    method: 'post',
    params: {id}
  })
}

// create Installment
export function apiCreateInstallment(data) {
  return request({
    url: '/api/InternalApp/Installment/CreateInstallment',
    method: 'post',
    data: data
  })
}

// update Installment
export function apiUpdateInstallment(data) {
  return request({
    url: '/api/InternalApp/Installment/UpdateInstallment',
    method: 'post',
    data: data
  })
}

// delete Installment
export function apiDeleteInstallment(id) {
  return request({
    url: '/api/InternalApp/Installment/DeleteInstallment',
    method: 'post',
    params: {id}
  })
}