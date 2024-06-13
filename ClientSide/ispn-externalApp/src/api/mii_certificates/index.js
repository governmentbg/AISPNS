import request from "@/utils/request";
import { filterQuery } from "@/utils/index";

export function apiGetAnnouncementCertificates(query){
  return request({
    url: `/api/ExternalAPP/MIICertificates/GetAnnouncementCertificates`,
    method: "post",
    params: filterQuery({pageNumber: query.pageNumber, pageSize: query.pageSize})
  })
}

export function apiGetFinishedAnnouncementCertificates(query){
  return request({
    url: `/api/ExternalAPP/MIICertificates/GetFinishedAnnouncementCertificates`,
    method: "post",
    params: filterQuery({pageNumber: query.pageNumber, pageSize: query.pageSize})
  })
}

export function apiGenerateCertificateForAnnouncement(announcementId) {
  return request({
    url: '/api/ExternalApp/MIICertificates/GenerateCertificateForAnnouncement',
    responseType: "blob",
    method: 'post',
    params: {announcementId}
  })
}