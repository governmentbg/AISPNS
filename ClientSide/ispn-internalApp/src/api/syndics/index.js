import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get syndics list
export function apiSearchSyndics(data) {
  return request({
    url: '/api/InternalApp/Syndic/SearchSyndic',
    method: 'post',
    data: data.filter,
    params: {pageSize: data.pageSize, pageNumber: data.pageNumber}
  })
}

// get syndic data
export function apiGetSyndicById(id) {
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicById',
    params: filterQuery({id})
  })
}

// create syndic data
export function apiCreateSyndic(data) {
  return request({
    url: '/api/InternalApp/Syndic/CreateSyndic',
    method: 'post',
    data: data
  })
}

// update syndic data
export function apiUpdateSyndic(data) {
  return request({
    url: '/api/InternalApp/Syndic/UpdateSyndic',
    method: 'post',
    data: data
  })
}

export function apiGetSyndicOrders(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicOrders',
    method: 'post',
    params: filterQuery(query)
  })
}

export function apiGetSyndicInstallments(query) {
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicInstallments',
    method: 'post',
    params: filterQuery(query)
  })
}

export function apiSendExpirationDateEmail(id){
  return request({
    url: '/api/InternalApp/Syndic/SendExpirationDateEmail',
    params: {id}
  })

}

export function apiGetSyndicCourses(query) {
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCourses',
    method: 'post',
    params: filterQuery(query)
  })
}

export function apiGetSyndicCases(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCases',
    method: 'post',
    data: query.filter,
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber})
  })
}

export function apiGetSyndicTemplates(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicTemplates',
    method: 'post',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

export function apiGetSyndicTemplateById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicTemplateById',
    params: {id}
  })
}

export function apiGetSyndicReports(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicReports',
    method: 'post',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

export function apiGetSyndicReportById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicReportById',
    params: {id}
  })
}

export function apiGetSyndicSignals(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicSignals',
    method: 'post',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
  })
}


export function apiGetSyndicAppeals(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicAppeals',
    method: 'post',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
  })
}

export function apiExportSyndicOrdersToExcel(syndicId){
  return request({
    url: '/api/InternalApp/Syndic/ExportSyndicOrdersToExcel',
    method: 'POST',
    responseType: "blob",
    params: {syndicId}
  })
}

export function apiExportSyndicCoursesToExcel(syndicId){
  return request({
    url: '/api/InternalApp/Syndic/ExportSyndicCoursesToExcel',
    method: 'POST',
    responseType: "blob",
    params: {syndicId}
  })
}

export function apiExportSyndicInstallmentsToExcel(syndicId){
  return request({
    url: '/api/InternalApp/Syndic/ExportSyndicInstallmentsToExcel',
    method: 'POST',
    responseType: "blob",
    params: {syndicId}
  })
}

export function apiCreateSyndicCourseApplication(data){
  return request({
    url: '/api/InternalApp/Syndic/CreateSyndicCourseApplication',
    method: 'POST',
    data: data
  })
}

export function apiUpdateSyndicCourseApplication(data){
  return request({
    url: '/api/InternalApp/Syndic/UpdateSyndicCourseApplication',
    method: 'POST',
    data: data
  })
}

export function apiGetSyndicCourseResults(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCourseResults',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
  })
}

export function apiExportSyndicCourseResultsToExcel(syndicId){
  return request({
    url: '/api/InternalApp/Syndic/ExportSyndicCourseResultsToExcel',
    method: 'POST',
    responseType: "blob",
    params: {syndicId}
  })
}

export function apiGetSyndicCourseApplications(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCourseApplications',
    params: filterQuery({id: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}),
  })
}

export function apiGetSyndicApplicationById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicApplicationById',
    params: {id}
  })
}


/* SYNDIC PLEA */
export function apiGetSyndicPleas(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicPleas',
    params: filterQuery(query)
  })
}

export function apiGetSyndicPleaById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicPleaById',
    params: {id}
  })
}

export function apiCreatePlea(data){
  return request({
    url: '/api/InternalApp/Syndic/CreatePlea',
    method: 'POST',
    data: data
  })
}

export function apiUpdatePlea(data){
  return request({
    url: '/api/InternalApp/Syndic/UpdatePlea',
    method: 'POST',
    data: data
  })
}


/* SYNDIC CASE ACTIVITIES */
export function apiGetSyndicCaseActivities(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCaseActivities',
    method: 'post',
    params: filterQuery({pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

export function apiGetSyndicActivityById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCaseActivityById',
    params: {id}
  })

}

/* SYNDIC CASE PROPERTIES */
export function apiGetSyndicCaseProperties(query){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCaseProperties',
    method: 'post',
    params: filterQuery({pageSize: query.pageSize, pageNumber: query.pageNumber}),
    data: query.filter
  })
}

export function apiGetSyndicCasePropertyById(id){
  return request({
    url: '/api/InternalApp/Syndic/GetSyndicCasePropertyById',
    params: {id}
  })

}


/* EXPORTS */

export function apiExportActiveSyndicsToExcel(){
  return request({
    url: '/api/InternalApp/Syndic/ExportActiveSyndicsToExcel',
    method: 'POST',
    responseType: "blob",
  })
}

export function apiExportActiveSyndicsWithoutPaidInstallmentToExcel(){
  return request({
    url: '/api/InternalApp/Syndic/ExportActiveSyndicsWithoutPaidInstallmentToExcel',
    method: 'POST',
    responseType: "blob",
  })
}

export function apiCreateCustodian(syndicId){
  return request({
    url: '/api/InternalApp/Syndic/CreateCustodian',
    method: 'POST',
    params: {syndicId}
  })
}