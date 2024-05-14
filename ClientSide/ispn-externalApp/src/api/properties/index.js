import request from "@/utils/request";

export function apiSearchProperty(query){
  return request({
    url: "/api/ExternalApp/Property/SearchProperty",
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter,
  });
}

export function apiGetPropertiesByClass(query){
  return request({
    url: "/api/ExternalApp/Property/GetPropertiesByClass",
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter,
  });
}

export function apiCreateProperty(data){
  return request({
    url: "/api/ExternalApp/Property/CreateProperty",
    method: 'post',
    data
  });
}

export function apiUpdateProperty(data){
  return request({
    url: "/api/ExternalApp/Property/UpdateProperty",
    method: 'post',
    data
  });
}

export function apiDeleteProperty(id){
  return request({
    url: "/api/ExternalApp/Property/DeleteProperty",
    method: 'post',
    params: {id}
  });
}

export function apiGetPropertyById(id){
  return request({
    url: "/api/ExternalApp/Property/GetPropertyById",
    params: {id}
  });
}

export function apiGeneratePropertyDocument(caseId){
  return request({
    url: "/api/ExternalApp/Property/GeneratePropertyDocument",
    method: "post",
    responseType: "blob",
    params: {caseId},
  });
}