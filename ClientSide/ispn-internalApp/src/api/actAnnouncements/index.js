import request from "@/utils/request";

export function apiSearchActAnnouncement(query){
  return request({
    url: "/api/InternalApp/ActAnnouncement/SearchActAnnouncement",
    method: "post",
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  });
}

export function apiGetActById(id){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetActById",
    params: {id}
  });
}

export function apiGetActAnnouncementById(id){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetActAnnouncementById",
    params: {id}
  });
}

export function apiAnnounceAct(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/AnnounceAct",
    method: "post",
    data
  });
}

export function apiRegisterAct(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/RegisterAct",
    method: "post",
    data
  });

}

export function apiNoSubjectToAnnouncement(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/NoSubjectToAnnouncement",
    method: "post",
    data
  });
}

export function apiNoSubjectToRegistration(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/NoSubjectToRegistration",
    method: "post",
    data
  });
}

export function apiCreateRegisterEntry(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/CreateRegisterEntry",
    method: "post",
    data
  });
}

export function apiUpdateRegisterEntry(data){
  return request({
    url: "/api/InternalApp/ActAnnouncement/UpdateRegisterEntry",
    method: "post",
    data
  });
}

export function apiDeleteRegisterEntry(id){
  return request({
    url: "/api/InternalApp/ActAnnouncement/DeleteRegisterEntry",
    method: "post",
    params: {id}
  });
}

export function apiGetAllRegisterEntries(query){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetAllRegisterEntries",
    params: {actId: query.actId, pageSize: query.pageSize, pageNumber: query.pageNumber}
  });
}

export function apiGetRegisterEntryById(id){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetRegisterEntryById",
    params: {id}
  });
}

export function apiDownloadActImage(actId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/DownloadActImage",
    method: 'post',
    responseType: 'blob',
    params: {actId}
  });
}

export function apiDownloadActLetterImage(actId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/DownloadActLetterImage",
    method: 'post',
    responseType: 'blob',
    params: {actId}
  });
}

export function apiDownloadRedactedActLetterImage(actId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/DownloadRedactedActLetterImage",
    method: 'post',
    responseType: 'blob',
    params: {actId}
  });
}

export function apiGetActAnnouncementsByCaseId(caseId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetActAnnouncementsByCaseId",
    method: 'post',
    params: {caseId}
  });
}

export function apiGetRegisterEntriesByCaseId(caseId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetRegisterEntriesByCaseId",
    method: 'post',
    params: {caseId}
  });
}

export function apiGetRegisterEntryHistory(registerEntryId){
  return request({
    url: "/api/InternalApp/ActAnnouncement/GetRegisterEntryHistory",
    method: 'post',
    params: {registerEntryId}
  });
}