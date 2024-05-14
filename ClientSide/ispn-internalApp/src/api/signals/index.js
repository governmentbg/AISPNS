import request from "@/utils/request";

export function apiGetSignalById(id) {
  return request({
    url: '/api/InternalApp/Signal/GetSignalById',
    method: 'post',
    params: {id}
  })
}

export function apiCreateSignal(data) {
  return request({
    url: '/api/InternalApp/Signal/CreateSignal',
    method: 'post',
    data: data
  })
}

export function apiUpdateSignal(data) {
  return request({
    url: '/api/InternalApp/Signal/UpdateSignal',
    method: 'post',
    data: data
  })
}