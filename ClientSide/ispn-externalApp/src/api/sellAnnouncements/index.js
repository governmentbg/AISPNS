import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get upcoming announcements - by mii
export function apiGetUpcomingAnnouncements(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchCurrentAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get finished announcements - by mii
export function apiGetFinishedAnnouncements(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchFinishedAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get drafts announcements - by mii
export function apiGetDraftsAnnouncements(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchDraftAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}



// get new announcements by syndics - by mii
export function apiGetNewAnnouncementsBySyndics(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchRawAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get on hould announcements by syndics - by mii
export function apiGetOnHoldAnnouncementsBySyndics(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchAnnouncementOnHold',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

// get published announcements - by mii
export function apiGetPublishedAnnouncementsBySyndics(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchPublishedAnnouncement',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })
}

export function apiSearchAnnouncementForCorrection(query) {
  return request({
    url: '/api/ExternalApp/Announcement/SearchAnnouncementForCorrection',
    method: 'post',
    params: {pageSize: query.pageSize, pageNumber: query.pageNumber},
    data: query.filter
  })

}


// get announcement by id
export function apiGetAnnouncementById(id) {
  return request({
    url: '/api/ExternalApp/Announcement/GetAnnouncementById',
    params: {id}
  })
}

// get announcement case by id
export function apiGetAnnouncementCaseById(id) {
  return request({
    url: '/api/ExternalApp/Announcement/GetAnnouncementCaseById',
    params: {id}
  })

}

// create announcement
export function apiCreateAnnouncement(data) {
  return request({
    url: '/api/ExternalApp/Announcement/CreateAnnouncement',
    method: 'post',
    data
  })
}

// Update Announcement
export function apiUpdateAnnouncement(data) {
  return request({
    url: '/api/ExternalApp/Announcement/UpdateAnnouncement',
    method: 'post',
    data
  })
}

// Cancel Announcement
export function apiCancelAnnouncement(id) {
  return request({
    url: '/api/ExternalApp/Announcement/CancelAnnouncement',
    method: 'post',
    params: {id}
  })
}

//SendAnnouncementForCorrectionInDraft
export function apiSendAnnouncementForCorrectionInDraft(id){
  return request({
    url: '/api/ExternalApp/Announcement/SendAnnouncementForCorrectionInDraft',
    method: 'post',
    params: {id}
  })
}

// publish announcement
export function apiPublishAnnouncement(id) {  
  return request({
    url: '/api/ExternalApp/Announcement/PublishAnnouncement',
    method: 'post',
    params: {id}
  })
}

// send announcement for correction
export function apiSendAnnouncementForCorrection(data) {
  return request({
    url: '/api/ExternalApp/Announcement/SendAnnouncementForCorrection',
    method: 'post',
    data: data
  })

}

// create announcement object
export function apiCreateAnnouncementObject(data) {  
  return request({
    url: '/api/ExternalApp/Announcement/CreateObject',
    method: 'post',
    data: data
  })
}

// update announcement object
export function apiUpdateSellAnnouncementObject(data) {  
  return request({
    url: '/api/ExternalApp/Announcement/UpdateObject',
    method: 'post',
    data: data
  })
}

// delete announcement object
export function apiDeleteObject(id) {
  return request({
    url: '/api/ExternalApp/Announcement/DeleteObject',
    method: 'post',
    params: {id}
  })
}



// get announcement objects
export function apiGetAnnouncementObjects(query) {
  return request({
    url: '/api/ExternalApp/Announcement/GetObjectsByAnnouncementId',
    method: 'post',
    params: filterQuery({pageNumber: query.pageNumber, pageSize: query.pageSize, announcementId: query.id})
  })
}

// get announcement object bu id
export function apiGetAnnouncementObjectById(id) {
  return request({
    url: '/api/ExternalApp/Announcement/GetObjectById',
    method: 'get',
    params: {id}
  })
}


/**
 * SYNDIC
 */

export function apiSendAnnouncementForPublish(id) {
  return request({
    url: '/api/ExternalApp/Announcement/SendAnnouncementForPublish',
    method: 'post',
    params: {id}
  })
}