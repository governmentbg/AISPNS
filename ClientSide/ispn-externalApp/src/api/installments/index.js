import request from "@/utils/request";

export function apiGetSyndicInstallments(query){
  return request({
    url: "/api/ExternalApp/Installment/GetSyndicInstallments",
    method: "post",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetInstallmentById(id){
  return request({
    url: "/api/ExternalApp/Installment/GetInstallmentById",
    method: "post",
    params: {id},
  });
}

export function apiCreateInstallment(data){
  return request({
    url: "/api/ExternalApp/Installment/CreateInstallment",
    method: "post",
    data,
  });
}

export function apiUpdateInstallment(data){
  return request({
    url: "/api/ExternalApp/Installment/UpdateInstallment",
    method: "post",
    data,
  });
}