import request from "@/utils/request";

export function apiGetTemplates(query){
  return request({
    url: "/api/ExternalApp/Template/GetTemplates",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetAdminTemplates(query){
  return request({
    url: "/api/ExternalApp/Template/GetAdminTemplates",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetTemplateById(id){
  return request({
    url: "/api/ExternalApp/Template/GetTemplateById",
    params: {id}
  });
}

export function apiCreateTemplate(data){
  return request({
    url: "/api/ExternalApp/Template/CreateTemplate",
    method: "post",
    data,
  });
}

export function apiUpdateTemplate(data){
  return request({
    url: "/api/ExternalApp/Template/UpdateTemplate",
    method: "post",
    data,
  });
}

export function apiGenerateAdminTemplate(templateId){
  return request({
    url: "/api/ExternalApp/Template/GenerateAdminTemplate",
    method: "post",
    responseType: "blob",
    params: {templateId},
  });
}