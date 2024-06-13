import request from "@/utils/request";

export function apiGetSettingsForm(){
  return request({
    url: '/api/InternalApp/Settings/GetSettingForm',
    method: 'post'
  })
}

export function apiCreateSettingForm(data){
  return request({
    url: '/api/InternalApp/Settings/CreateSettingForm',
    method: 'post',
    data: data
  })
}

export function apiUpdateSettingForm(data){
  return request({
    url: '/api/InternalApp/Settings/UpdateSettingForm',
    method: 'post',
    data: data
  })
}