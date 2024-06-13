import request from '@/utils/request';
import requestNoLoading from '@/utils/request_no_loading';
import { filterQuery } from '@/utils/index';


// get received messages
export function apiGetReceivedMessages(query) {
  return request({
    url: '/api/ExternalApp/Message/GetReceivedMessages',
    method: 'get',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

// get sent messages
export function apiGetSentMessages(query) {
  return request({
    url: '/api/ExternalApp/Message/GetSentMessages',
    method: 'get',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

// get message by id
export function apiGetMessageById(id) {
  return request({
    url: '/api/ExternalApp/Message/GetMessageById',
    params: {id}
  })
}

// send message
export function apiSendMessage(data) {
  return request({
    url: '/api/ExternalApp/Message/SendMessage',
    method: 'post',
    headers: {"Content-Type": "multipart/form-data"},
    data: data
  })
}


// reply message
export function apiReplyToMessage(data) {
  return request({
    url: '/api/ExternalApp/Message/ReplyToMessage',
    method: 'post',
    headers: {"Content-Type": "multipart/form-data"},
    data: data
  })
}

// get unread messages count
export function apiGetUnreadMessages(){
  return requestNoLoading({
    url: '/api/ExternalApp/Message/GetUnreadMessages'
  })
}

export function apiGetAllMessageReplies(messageId){
  return request({
    url: '/api/ExternalApp/Message/GetAllMessageReplies',
    method: 'post',
    params: {messageId}
  })
}