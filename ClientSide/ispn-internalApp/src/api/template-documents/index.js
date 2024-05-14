import request from "@/utils/request";
import { filterQuery } from "@/utils/index";

export function apiGetProtocolTemplates(params, data = {}) {
  return request({
    url: `/api/TemplateDocument/GetProtocolTemplates`,
    method: "POST",
    params: filterQuery(params),
    data: data
  });
}

export function apiGetProtocolTemplate(protocolTemplateId) {
  return request({
    url: `/api/TemplateDocument/GetProtocolTemplatesById`,
    method: "GET",
    params: filterQuery({id: protocolTemplateId}),
  });
}

export function apiInsertProtocolTemplate(data) {
  return request({
    url: `/api/TemplateDocument/InsertProtocolTemplate`,
    method: "POST",
    data: data,
    headers: {"Content-Type": "multipart/form-data"},
  });
}

export function apiUpdateProtocolTemplate(data) {
  return request({
    url: `/api/TemplateDocument/UpdateProtocolTemplate`,
    method: "PUT",
    data: data,
    headers: {"Content-Type": "multipart/form-data"},
  });
}

export function apiDeleteProtocolTemplate(protocolId){
  return request({
    url: `/api/TemplateDocument/DeleteProtocolTemplate`,
    method: "DELETE",
    params: filterQuery({id: protocolId})
  })
}
