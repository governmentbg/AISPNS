import request from "@/utils/request";

export function apiGetCasesByPerson(data){
  return request({
    url: `/api/PublicApp/Case/SearchCaseByPerson`,
    method: 'post',
    data: data.filter,
    params: {
      pageNumber: data.pageNumber,
      pageSize: data.pageSize
    }
  })
}

export function apiGetCasesByEntity(data){
  return request({
    url: `/api/PublicApp/Case/SearchCaseByEntity`,
    method: 'post',
    data: data.filter,
    params: {
      pageNumber: data.pageNumber,
      pageSize: data.pageSize
    }
  })
}

export function apiGetCases(data){
  return request({
    url: `/api/PublicApp/Case/SearchCase`,
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
    url: `/api/PublicApp/Case/GetCaseById`,
    params: {id: id}
  })
}

export function apiGetCaseActsByCaseId(caseId){
  return request({
    url: `/api/PublicApp/Case/GetImportedDeedsByCase`,
    method: 'post',
    params: {caseId}
  })
}

export function apiGetCaseBook(id){
  console.log("id = "+id)
  return request({
    url: `/api/PublicApp/Case/GetCaseBook`,
    params: {id: id}
  })
}