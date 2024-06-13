import request from "@/utils/request";

export function apiSendCustomEmail(data){
  return request({
    url: `/api/InternalApp/Email/SendCustomEmail`,
    method: 'post',
    data: data
  })
}