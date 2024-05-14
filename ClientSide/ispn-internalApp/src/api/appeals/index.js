import request from "@/utils/request";

export function apiGetAppealById(id) {
  return request({
    url: '/api/InternalApp/Appeal/GetAppealById',
    method: 'post',
    params: {id}
  })
}

export function apiCreateAppeal(data) {
  return request({
    url: '/api/InternalApp/Appeal/CreateAppeal',
    method: 'post',
    data: data
  })
}

export function apiUpdateAppeal(data) {
  return request({
    url: '/api/InternalApp/Appeal/UpdateAppeal',
    method: 'post',
    data: data
  })
}