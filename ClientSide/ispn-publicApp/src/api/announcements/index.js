import { filterQuery } from '@/utils'
import request from '@/utils/request'

export function apiGetCurrentAnnouncementsList(data) {
  return request({
    url: `/api/PublicApp/Announcement/SearchCurrentAnnouncement`,
    method: 'post',
    params: filterQuery({pageSize: data.pageSize, pageNumber: data.pageNumber}),
    data: data.filter
  })
}

export function apiGetFinishedAnnouncementsList(data) {
  return request({
    url: `/api/PublicApp/Announcement/SearchFinishedAnnouncement`,
    method: 'post',
    params: filterQuery({pageSize: data.pageSize, pageNumber: data.pageNumber}),
    data: data.filter
  })
}

export function apiGetAnnouncementById(id) {
  return request({
    url: `/api/PublicApp/Announcement/GetAnnouncementById`,
    method: 'get',
    params: filterQuery({id})
  })
}

export function apiGetContactForm() {
  return request({
    url: `/api/PublicApp/Announcement/GetContactForm`,
    method: 'post',
  })
}