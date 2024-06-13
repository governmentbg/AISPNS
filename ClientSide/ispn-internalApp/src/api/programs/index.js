import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get programs list
export function apiGetPrograms(query) {
  return request({
    url: '/api/InternalApp/Program/GetAllPrograms',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

// get program data
export function apiGetProgramById(id) {
  return request({
    url: '/api/InternalApp/Program/GetProgramById',
    method: 'post',
    params: filterQuery({id})
  })
}

// create program
export function apiGetCreateProgram(data) {
  return request({
    url: '/api/InternalApp/Program/CreateProgram',
    method: 'post',
    data: data
  })
}

// update program
export function apiGetUpdateProgram(data) {
  return request({
    url: '/api/InternalApp/Program/UpdateProgram',
    method: 'post',
    data: data
  })
}

// delete program
export function apiGetDeleteProgram(id) {
  return request({
    url: '/api/InternalApp/Program/DeleteProgram',
    method: 'post',
    params: {id}
  })
}

// get program courses
export function apiGetProgramCourses(query) {
  return request({
    url: '/api/InternalApp/Program/GetProgramCourses',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}

// get program course
export function apiGetProgramCourseById(id) {
  return request({
    url: '/api/InternalApp/Course/GetCourseById',
    method: 'post',
    params: filterQuery({id})
  })
}

// get program course participants
export function apiGetCourseParticipants(id) {
  return request({
    url: '/api/InternalApp/Course/GetCourseParticipants',
    method: 'post',
    params: filterQuery({id})
  })
}

export function apiGetProgramCourseApplications(query){
  return request({
    url: '/api/InternalApp/Program/GetProgramCourseApplications',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}

export function apiPublishProgram(id){
  return request({
    url: `/api/InternalApp/Program/PublishProgram/${id}`,
    method: 'patch'
  })
}

export function apiEnrollCourseParticipants(programId){
  return request({
    url: '/api/InternalApp/Program/EnrollCourseParticipants',
    method: 'post',
    params: {programId}
  })
}

export function apiDiscardCourseParticipants(programId){
  return request({
    url: '/api/InternalApp/Program/DiscardCourseParticipants',
    method: 'post',
    params: {programId}
  })
}

export function apiGetProgramEnrolledParticipants(query){
  return request({
    url: '/api/InternalApp/Program/GetProgramEnrolledParticipants',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}

export function apiGetProgramDiscardedParticipants(query){
  return request({
    url: '/api/InternalApp/Program/GetProgramDiscardedParticipants',
    method: 'post',
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize, id: query.id}
  })
}

export function apiSendPublishedProgramEmail(id){
  return request({
    url: `/api/InternalApp/Program/SendPublishedProgramEmail`,
    method: 'post',
    params: {id}
  })
}

export function apiSendEnrolledSyndicsEmail(id){
  return request({
    url: `/api/InternalApp/Program/SendEnrolledSyndicsEmail`,
    method: 'post',
    params: {id}
  })
}