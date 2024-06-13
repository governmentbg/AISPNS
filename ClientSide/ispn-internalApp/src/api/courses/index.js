import request from "@/utils/request";

export function apiGetCourseById(id) {
  return request({
    url: '/api/InternalApp/Course/GetCourseById',
    method: 'post',
    params: {id}
  })
}

export function apiCreateCourse(data){
  return request({
    url: '/api/InternalApp/Course/CreateCourse',
    method: 'post',
    data: data
  })
}

export function apiUpdateCourse(data){
  return request({
    url: '/api/InternalApp/Course/UpdateCourse',
    method: 'post',
    data: data
  })
}

export function apiGetCourseApplications(query){
  return request({
    url: '/api/InternalApp/Course/GetCourseApplications',
    method: 'post',
    params: {courseId: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetEnrolledDate1(query){
  return request({
    url: '/api/InternalApp/Course/GetEnrolledDate1',
    method: 'post',
    params: {courseId: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetEnrolledDate2(query){
  return request({
    url: '/api/InternalApp/Course/GetEnrolledDate2',
    method: 'post',
    params: {courseId: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiGetEnrolledParticipants(query){
  return request({
    url: '/api/InternalApp/Course/GetEnrolledParticipants',
    method: 'post',
    params: {courseId: query.id, pageSize: query.pageSize, pageNumber: query.pageNumber}
  })
}

export function apiMarkCourseParticipantCourseAsPassed(courseParticipantId){
  return request({
    url: '/api/InternalApp/Course/MarkCourseParticipantCourseAsPassed',
    method: 'post',
    params: {courseParticipantId}
  })
}

export function apiMarkCourseParticipantCourseAsNotPassed(courseParticipantId){
  return request({
    url: '/api/InternalApp/Course/MarkCourseParticipantCourseAsNotPassed',
    method: 'post',
    params: {courseParticipantId}
  })
}