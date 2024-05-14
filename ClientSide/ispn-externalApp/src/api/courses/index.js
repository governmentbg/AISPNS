import request from "@/utils/request";

export function apiGetCourseById(id){
  return request({
    url: "/api/ExternalApp/Course/GetCourseById",
    method: 'post',
    params: {id},
  });

}

export function apiGetCoursesThatUserParticipatesIn(query){
  return request({
    url: "/api/ExternalApp/Course/GetCoursesThatUserParticipatesIn",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetCoursesUserHasAppliedTo(query){
  return request({
    url: "/api/ExternalApp/Course/GetCoursesUserHasAppliedTo",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetCourseResults(query){
  return request({
    url: "/api/ExternalApp/Course/GetCourseResults",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetSyndicCourseResults(query){
  return request({
    url: "/api/ExternalApp/Course/GetSyndicCourseResults",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiGetSyndicCourseApplications(query){
  return request({
    url: "/api/ExternalApp/Course/GetSyndicCourseApplications",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}

export function apiCreateCourseApplication(data){
  return request({
    url: "/api/ExternalApp/Course/CreateCourseApplication",
    method: "post",
    data,
  });
}

export function apiGetCourseApplicationById(id){
  return request({
    url: "/api/ExternalApp/Course/GetCourseApplicationById",
    method: 'post',
    params: {id},
  });
}

export function apiGetUserCourseParticipations(query){
  return request({
    url: "/api/ExternalApp/Course/GetUserCourseParticipations",
    params: {pageNumber: query.pageNumber, pageSize: query.pageSize},
  });
}