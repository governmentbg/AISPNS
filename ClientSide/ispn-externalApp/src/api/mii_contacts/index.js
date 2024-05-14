import request from "@/utils/request";

export function apiGetContactForm(){
  return request({
    url: `/api/ExternalAPP/ContactForm/GetContactForm`,
    method: 'get'
  })
}

export function apiCreateContactForm(data){
  return request({
    url: `/api/ExternalAPP/ContactForm/CreateContactForm`,
    method: 'post',
    data: data
  })
}

export function apiUpdateContactForm(data){
  return request({
    url: `/api/ExternalAPP/ContactForm/UpdateContactForm`,
    method: 'post',
    data: data
  })
}