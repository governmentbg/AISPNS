import request from "@/utils/request";
import { filterQuery } from "@/utils/index";

export function apiRegisterLogin(data){
  return request({
    url: `/api/Account/Login`,
    method: 'POST',
    data: data
  })
}
