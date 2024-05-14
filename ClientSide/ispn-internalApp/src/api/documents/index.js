import request from "@/utils/request";

export function apiSubmitDocument(data) {
  return request({
    url: "/api/InternalApp/Document/SubmitDocument",
    method: "post",
    headers: {"Content-Type": "multipart/form-data"},
    data: data,
  });
}

export function apiGetAllDocumentFiles(query){
  return request({
    url: `/api/InternalApp/Document/GetAllDocumentFiles`,
    method: "post",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
    data: query.filter
  })

}

export function apiDownloadDocument(id){
  return request({
    url: `/api/InternalApp/Document/DownloadAttachedFile`,
    responseType: "blob",
    params: {id}
  })
}

export function apiDeleteDocument(id){
  return request({
    url: `/api/InternalApp/Document/DeleteDocumentImplementation`,
    method: "post",
    params: {id}
  })
}

export function apiGetFileName(id){
  return request({
    url: `/api/InternalApp/Document/GetFileName`,
    method: "post",
    params: {id}
  })
}

export function apiGetDocumentContentById(id){
  return request({
    url: `/api/InternalApp/Document/GetDocumentContentById`,
    params: {id}
  })
}

export function apiUpdateDocumentContent(data){
  return request({
    url: `/api/InternalApp/Document/UpdateDocumentContent`,
    method: 'post',
    data: data
  })
}