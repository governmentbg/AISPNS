import request from "@/utils/request";

export function apiGetSyndics(data){
  return request({
    url: `/api/PublicApp/Syndic/SearchSyndic`,
    method: 'post',
    data: data.filter,
    params: {pageNumber: data.pageNumber, pageSize: data.pageSize}
  })
}

export function apiGetSyndicById(id){
  return request({
    url: `/api/PublicApp/Syndic/GetSyndicById`,
    method: 'get',
    params: {id}
  })
}

export function apiSearchSyndicTemplate(data){
  return request({
    url: `/api/PublicApp/Syndic/SearchSyndicTemplate`,
    method: 'post',
    params: {pageNumber: data.pageNumber, pageSize: data.pageSize},
  })
}

export function apiDownloadSyndicTemplate(id){
  return request({
    url: `/api/PublicApp/Syndic/DownloadSyndicTemplate`,
    method: 'post',
    params: {id}
  })
}

export function apiGetAllDocumentLegalBasis(query){
  return request({
    url: `/api/PublicApp/Syndic/GetAllDocumentLegalBasis`,
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}