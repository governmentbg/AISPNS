import request from "@/utils/request";

export function apiGetCasesByPerson(data){
  return request({
    url: `/api/ExternalAPP/Case/SearchCaseByPerson`,
    method: 'post',
    data: data
  })
}

export function apiGetCasesByEntity(data){
  return request({
    url: `/api/ExternalAPP/Case/SearchCaseByEntity`,
    method: 'post',
    data: data
  })
}

export function apiGetCasesBySyndic(query){
  return request({
    url: `/api/ExternalAPP/Case/SearchCaseBySyndic`,
    method: 'post',
    params: {
      pageNumber: query.pageNumber,
      pageSize: query.pageSize
    },
    data: query.filter
  })
}

export function apiGetCases(data){
  if(!data.filter.isStabilization)
    data.filter.isStabilization = false;
  
  return request({
    url: `/api/ExternalAPP/Case/SearchCase`,
    method: 'post',
    params: {
      pageNumber: data.pageNumber,
      pageSize: data.pageSize
    },
    data: data.filter
  })
}

export function apiGetCaseById(id){
  return request({
    url: `/api/ExternalAPP/Case/GetCaseById`,
    params: {id}
  })
}

export function apiGetCaseActsByCaseId(caseId){
  return request({
    url: `/api/ExternalAPP/Case/GetImportedDeedsByCase`,
    method: 'post',
    params: {caseId}
  })
}

export function apiGetCaseBook(id){
  return request({
    url: `/api/ExternalAPP/Case/GetCaseBook`,
    method: 'get',
    params: {id}
  })
}

export function apiGetEntityStatisticalDataByEntityId(entityId){
  return request({
    url: `/api/ExternalAPP/Case/GetEntityStatisticalDataByEntityId`,
    method: 'post',
    params: {entityId}
  })
}

export function apiSaveEntityStatisticalData(data){
  return request({
    url: `/api/ExternalAPP/Case/SaveEntityStatisticalData`,
    method: 'post',
    data: data
  })
}