import request from "@/utils/request";

export function apiSearchActivities(query){
  return request({
    url: "/api/ExternalApp/Activity/SearchActivities",
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter,
  });
}

export function apiGetActivityById(id){
  return request({
    url: `/api/ExternalApp/Activity/GetActivityById`,
    params: {id}
  });
}

export function apiCreateActivity(data){
  return request({
    url: `/api/ExternalApp/Activity/CreateActivity`,
    method: 'post',
    data
  });
}

export function apiUpdateActivity(data){
  return request({
    url: `/api/ExternalApp/Activity/UpdateActivity`,
    method: 'post',
    data
  });
}

export function apiDeleteActivity(id){
  return request({
    url: `/api/ExternalApp/Activity/DeleteActivity`,
    method: 'post',
    params: {id}
  });
}

export function apiGenerateActivityDocument(caseId){
  return request({
    url: `/api/ExternalApp/Activity/GenerateActivityDocument`,
    method: 'post',
    params: {caseId}
  });
}