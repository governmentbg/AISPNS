import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


// get nomenclature keys
export function apiMetaDataGetCourtsList() {
  return request({
    url: `/api/PublicApp/MetaData/GetCourtsList`,
    method: 'post'
  })
}

export function apiMetaDataGetReportSources(){
  return request({
    url: `/api/PublicApp/MetaData/GetReportSources`,
    method: 'post'
  })
}

export function apiMetaDataGetReportTypes(){
  return request({
    url: `/api/PublicApp/MetaData/GetReportTypes`,
    method: 'post'
  })
}

export function apiMetaDataGetSyndicSpecialities(){
  return request({
    url: `/api/PublicApp/MetaData/GetSpecialties`,
    method: 'post'
  })
}

export function apiMetaDataGetObjectKind(){
  return request({
    url: `/api/PublicApp/MetaData/GetObjectKind`
  })
}