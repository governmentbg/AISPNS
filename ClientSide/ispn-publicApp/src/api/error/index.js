import request from '@/utils/request';


// push not found path
export function saveWrongPath(path) {
  return request({
    url: `/api/error/notFound?path=${path}`,
    method: 'get'
  })
}