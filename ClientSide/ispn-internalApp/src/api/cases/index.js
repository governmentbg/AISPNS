import request from "@/utils/request";

export function apiGetCasesByPerson(data){
  return request({
    url: `/api/InternalApp/Case/SearchCaseByPerson`,
    method: 'post',
    data: data
  })
}

export function apiGetCasesByEntity(data){
  return request({
    url: `/api/InternalApp/Case/SearchCaseByEntity`,
    method: 'post',
    data: data
  })
}

export function apiGetCases(data){
  return request({
    url: `/api/InternalApp/Case/SearchCase`,
    method: 'post',
    data: data.filter,
    params: {
      pageNumber: data.pageNumber,
      pageSize: data.pageSize
    }
  })
}

export function apiGetCaseById(id){
  return request({
    url: `/api/InternalApp/Case/GetCaseById`,
    params: {id}
  })
}

export function apiGetCaseActsByCaseId(caseId){
  return request({
    url: `/api/InternalApp/Case/GetImportedDeedsByCase`,
    method: 'post',
    params: {caseId}
  })
}

export function apiGetCaseBook(id){
  return request({
    url: `/api/InternalApp/Case/GetCaseBook`,
    method: 'get',
    params: {id}
  })
}

export function apiGetEntityStatisticalDataByEntityId(entityId){
  return request({
    url: `/api/InternalApp/Case/GetEntityStatisticalDataByEntityId`,
    method: 'post',
    params: {entityId}
  })
}

export function apiGetCaseReports(query){
  return request({
    url: `/api/InternalApp/Case/GetCaseReports`,
    method: 'post',
    params: {id: query.id, pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter
  })
}