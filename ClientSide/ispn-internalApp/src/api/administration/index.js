import request from "@/utils/request";

export function apiGetUserActions(query){
  return request({
    url: `/api/InternalApp/Administration/GetUserActions`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

export function apiGetApiRequests(query){
  return request({
    url: `/api/InternalApp/Administration/GetApiRequests`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

export function apiGetApiRequestById(id){
  return request({
    url: `/api/InternalApp/Administration/GetApiRequestById`,
    method: 'post',
    params: {id}
  })
}

export function apiGetExceptionById(id){
  return request({
    url: `/api/InternalApp/Administration/GetExceptionById`,
    method: 'post',
    params: {id}
  })
}

export function apiGetTemplateArticles28(query){
  return request({
    url: `/api/InternalApp/Administration/GetTemplateArticles28`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetTemplateArticles28ById(id){
  return request({
    url: `/api/InternalApp/Administration/GetTemplateArticles28ById`,
    method: 'get',
    params: {id}
  })

}

export function apiCreateTemplateArticle28(data){
  return request({
    url: `/api/InternalApp/Administration/CreateTemplateArticle28`,
    method: 'post',
    data
  })
}

export function apiUpdateTemplateArticle28(data){
  return request({
    url: `/api/InternalApp/Administration/UpdateTemplateArticle28`,
    method: 'post',
    data
  })
}

export function apiDeleteTemplateArticle28(id){
  return request({
    url: `/api/InternalApp/Administration/DeleteTemplateArticle28`,
    method: 'post',
    params: {id}
  })
}

// document templates

export function apiGetTemplateDocuments(query){
  return request({
    url: `/api/InternalApp/Administration/GetTemplateDocuments`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetTemplateDocumentById(id){
  return request({
    url: `/api/InternalApp/Administration/GetTemplateDocumentById`,
    method: 'post',
    params: {id}
  })
}

export function apiCreateTemplateDocument(data){
  return request({
    url: `/api/InternalApp/Administration/CreateTemplateDocument`,
    method: 'post',
    data: data
  })
}

export function apiUpdateTemplateDocument(data){
  return request({
    url: `/api/InternalApp/Administration/UpdateTemplateDocument`,
    method: 'post',
    data: data
  })
}

export function apiDeleteTemplateDocument(id){
  return request({
    url: `/api/InternalApp/Administration/DeleteTemplateDocument`,
    method: 'post',
    params: {id}
  })

}

export function apiDownloadAttachedTemplateDocument(id){
  return request({
    url: `/api/InternalApp/Administration/DownloadAttachedTemplateDocument`,
    responseType: "blob",
    method: 'get',
    params: {id}
  })
}


// syndic templates

export function apiGetAdminTemplate(query){
  return request({
    url: `/api/InternalApp/Administration/GetAdminTemplate`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetAdminTemplateById(id){
  return request({
    url: `/api/InternalApp/Administration/GetAdminTemplateById`,
    method: 'get',
    params: {id}
  })

}

export function apiCreateAdminTemplate(data){
  return request({
    url: `/api/InternalApp/Administration/CreateAdminTemplate`,
    method: 'post',
    data
  })
}

export function apiUpdateAdminTemplate(data){
  return request({
    url: `/api/InternalApp/Administration/UpdateAdminTemplate`,
    method: 'post',
    data
  })
}

export function apiDeleteAdminTemplate(id){
  return request({
    url: `/api/InternalApp/Administration/DeleteAdminTemplate`,
    method: 'post',
    params: {id}
  })
}


// syndic report templates

export function apiGetReportTemplate(query){
  return request({
    url: `/api/InternalApp/Administration/GetReportTemplate`,
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetReportTemplateById(id){
  return request({
    url: `/api/InternalApp/Administration/GetReportTemplateById`,
    method: 'get',
    params: {id}
  })

}

export function apiCreateReportTemplate(data){
  return request({
    url: `/api/InternalApp/Administration/CreateReportTemplate`,
    method: 'post',
    data
  })
}

export function apiUpdateReportTemplate(data){
  return request({
    url: `/api/InternalApp/Administration/UpdateReportTemplate`,
    method: 'post',
    data
  })
}

export function apiDeleteReportTemplate(id){
  return request({
    url: `/api/InternalApp/Administration/DeleteReportTemplate`,
    method: 'post',
    params: {id}
  })
}


// document legal basis
export function apiGetAllDocumentLegalBasis(query){
  return request({
    url: `/api/InternalApp/Administration/GetAllDocumentLegalBasis`,
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetDocumentLegalBasisById(id){
  return request({
    url: `/api/InternalApp/Administration/GetDocumentLegalBasisById`,
    params: {id}
  })
}

export function apiCreateDocumentLegalBasis(data){
  return request({
    url: `/api/InternalApp/Administration/CreateDocumentLegalBasis`,
    method: 'post',
    data
  })
}

export function apiUpdateDocumentLegalBasis(data){
  return request({
    url: `/api/InternalApp/Administration/UpdateDocumentLegalBasis`,
    method: 'post',
    data
  })
}

export function apiDeleteDocumentLegalBasis(id){
  return request({
    url: `/api/InternalApp/Administration/DeleteDocumentLegalBasis`,
    method: 'post',
    params: {id}
  })
}