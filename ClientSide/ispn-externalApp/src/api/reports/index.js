import request from "@/utils/request";

export function apiSearchReports(query){
  return request({
    url: "/api/ExternalApp/Report/SearchReport",
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter,
  });
}

export function apiGetReportById(id){
  return request({
    url: "/api/ExternalApp/Report/GetReportById",
    params: {id}
  });
}

export function apiCreateReport(data){
  return request({
    url: "/api/ExternalApp/Report/CreateReport",
    method: "post",
    data,
  });
}

export function apiUpdateReport(data){
  return request({
    url: "/api/ExternalApp/Report/UpdateReport",
    method: "post",
    data,
  });
}

export function apiGenerateReportTemplate(reportTypeId){
  return request({
    url: "/api/ExternalApp/Report/GenerateReportTemplate",
    method: "post",
    responseType: "blob",
    params: {reportTypeId},
  });
}