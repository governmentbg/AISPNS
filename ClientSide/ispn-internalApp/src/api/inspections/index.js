import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


export function apiGetCurrentInspections(query){
  return request({
    url: '/api/InternalApp/Inspection/SearchCurrentInspection',
    method: 'post',
    params: filterQuery({pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

export function apiGetFinishedInspections(query){
  return request({
    url: '/api/InternalApp/Inspection/SearchFinishedInspection',
    method: 'post',
    params: filterQuery({pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

// get inspection by id
export function apiGetInspectionById(id) {
  return request({
    url: '/api/InternalApp/Inspection/GetInspectionById',
    params: {id}
  })
}

// create Installment
export function apiCreateInstallment(data) {
  return request({
    url: '/api/InternalApp/Inspection/CreateInstallment',
    method: 'post',
    data: data
  })
}

// update Installment
export function apiUpdateInstallment(data) {
  return request({
    url: '/api/InternalApp/Inspection/UpdateInstallment',
    method: 'post',
    data: data
  })
}

// delete Installment
export function apiDeleteInstallment(id) {
  return request({
    url: '/api/InternalApp/Inspection/DeleteInstallment',
    method: 'post',
    params: {id}
  })
}

export function apiGenerateSampleReportForInspection(inspectionId){
  return request({
    url: '/api/InternalApp/Inspection/GenerateSampleReportForInspection',
    responseType: "blob",
    method: 'post',
    params: {inspectionId}
  })
}

export function apiCreateInspection(data){
  return request({
    url: '/api/InternalApp/Inspection/CreateInspection',
    method: 'post',
    data: data
  })
}

export function apiUpdateInspection(data){
  return request({
    url: '/api/InternalApp/Inspection/UpdateInspection',
    method: 'post',
    data: data
  })
}