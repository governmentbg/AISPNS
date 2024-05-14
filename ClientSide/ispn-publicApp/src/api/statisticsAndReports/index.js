import request from "@/utils/request";

export function apiGetStatisticsAndReports(data){
  return request({
    url: `/api/PublicApp/StatisticalReport/SearchStatisticalReport`,
    method: 'post',
    data: data.filter,
    params: {pageNumber: data.pageNumber, pageSize: data.pageSize}
  })
}

export function apiGetStatisticsAndReportById(id){
  return request({
    url: `/api/PublicApp/StatisticalReport/GetStatisticalReportById`,
    method: 'get',
    params: {id}
  })
}