import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get upcoming announcements
export function apiGetUpcomingAnnouncements(query) {
  return request({
    url: '/api/InternalApp/Announcement/SearchCurrentAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get finished announcements
export function apiGetFinishedAnnouncements(query) {
  return request({
    url: '/api/InternalApp/Announcement/SearchFinishedAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get announcement by id
export function apiGetAnnouncementById(id) {
  return request({
    url: '/api/InternalApp/Announcement/GetAnnouncementById',
    params: {id}
  })
}

// send message
export function apiSendMessage(data) {
  return request({
    url: '/api/InternalApp/Announcement/SendMessage',
    method: 'post',
    data: data
  })
}