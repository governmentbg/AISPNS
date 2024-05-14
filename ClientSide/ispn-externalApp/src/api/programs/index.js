import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get programs list
export function apiGetPrograms(query) {
  return request({
    url: '/api/ExternalApp/Program/GetAllPrograms',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

// get program data
export function apiGetProgramById(id) {
  return request({
    url: '/api/ExternalApp/Program/GetProgramById',
    method: 'post',
    params: filterQuery({id})
  })
}

export function apiGetProgramCourseApplications(query){
  return request({
    url: '/api/ExternalApp/Program/GetProgramCourseApplications',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}

// get program courses
export function apiGetProgramCourses(query) {
  return request({
    url: '/api/ExternalApp/Program/GetProgramCourses',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}