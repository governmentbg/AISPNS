import request from "@/utils/request";

export function apiGetStatisticsAndReports(data){
  return request({
    url: `/api/InternalApp/StatisticalReport/SearchStatisticalReport`,
    method: 'post',
    data: data.filter,
    params: {pageNumber: data.pageNumber, pageSize: data.pageSize}
  })
}

export function apiGetStatisticsAndReportById(id){
  return request({
    url: `/api/InternalApp/StatisticalReport/GetStatisticalReportById`,
    method: 'post',
    params: {id}
  })
}

export function apiCreateStatisticsAndReport(data){
  return request({
    url: `/api/InternalApp/StatisticalReport/CreateStatisticalReport`,
    method: 'post',
    data: data
  })
}

export function apiUpdateStatisticsAndReport(data){
  return request({
    url: `/api/InternalApp/StatisticalReport/UpdateStatisticalReport`,
    method: 'post',
    data: data
  })
}

export function apiPublishStatisticalReport(id){
  return request({
    url: `/api/InternalApp/StatisticalReport/PublishStatisticalReport`,
    method: 'post',
    params: {id}
  })
}

export function apiUnpublishStatisticalReport(id){
  return request({
    url: `/api/InternalApp/StatisticalReport/UnpublishStatisticalReport`,
    method: 'post',
    params: {id}
  })
}