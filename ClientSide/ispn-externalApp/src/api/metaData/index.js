import { filterQuery } from "@/utils";
import request from "@/utils/request.js";


// nomenclatures
export function apiMetaDataGetCourts() {
  return request({
    url: `/api/ExternalAPP/MetaData/GetCourtsList`,
  })
}

export function apiMetaDataGetCompanySizes(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetCompanySizes`,
  })
}

export function apiMetaDataGetSyndicStatuses(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetSyndicStatuses`,
  })
}

export function apiGetCountries() {
  return request({
    url: "/api/ExternalAPP/MetaData/GetCountries",
    method: "GET",
  });
}

export function apiGetDistricts() {
  return request({
    url: "/api/ExternalAPP/MetaData/GetDistricts",
    method: "GET",
  });
}

export function apiGetMunicipalities(districtId) {
  return request({
    url: `/api/ExternalAPP/MetaData/GetMunicipalities?districtId=${districtId}`,
    method: "GET",
  });
}

export function apiGetSettlements(municipalityId) {
  return request({
    url: `/api/ExternalAPP/MetaData/GetSettlements?municipalityId=${municipalityId}`,
    method: "GET",
  });
}

export function apiGetAddressTypes() {
  return request({
    url: "/api/ExternalAPP/MetaData/GetAddressTypes",
    method: "GET",
  });
}

export function apiMetaDataGetSyndics(query = null){
  if(!query)
    query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: null}

  if(!query.searchCriteria)
    query.searchCriteria = null;

  if(!query.syndicId)
    query.syndicId = null;

  return request({
    url: `/api/ExternalAPP/MetaData/GetSyndics`,
    params: query
  })
}

export function apiMetaDataGetSyndicsShortInfo(){
  return request(`/api/ExternalAPP/MetaData/GetSyndicsShortInfo`)
}

export function apiGetAttachedDocumentKinds(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetAttachedDocumentKinds`,
  })
}

export function apiMetaDataGetUserShortInfo(){
  return request(`/api/ExternalAPP/MetaData/GetUserShortInfo`)
}


/**
 * announcements
 */



export function apiMetaDataGetAnnouncementStatus(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetAnnouncementStatus`,
  })
}

export function apiMetaDataGetLegalBasis(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetLegalBasis`,
  })
}

export function apiMetaDataGetOfferingLocationType(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetOfferingLocationType`,
  })
}

export function apiMetaDataGetOfferingProcedure(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetOfferingProcedure`,
  })
}

export function apiMetaDataGetOfferingKind(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetOfferingKind`,
  })
}

export function apiMetaDataGetObjectKind(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetObjectKind`,
  })
}

export function apiMetaDataGetObjectType(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetObjectType`,
  })
}

export function apiMetaDataGetPapersForSale(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetPapersForSale`,
  })
}

export function apiMetaDataGetSalesProcedure(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetSalesProcedure`,
  })
}

export function apiMetaDataGetSellAnnouncementTemplates(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetSellAnnouncementTemplate`,
  })
}

/* installments */
export function apiMetaDataGetInstallmentKinds(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetInstallmentKinds`,
  })
}

export function apiMetaDataGetInstallmentYears(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetInstallmentYears`,
  })
}

/* templates */

export function apiMetaDataGetSyndicTemplateKinds(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetSyndicTemplateKinds`,
  })
}

/* Reports */
export function apiMetaDataGetSyndicReportTypes(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetSyndicReportTypes`,
  })
}

/* activities */
export function apiMetaDataGetActivityKinds(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetActivityKinds`,
  })
}

/* entities */
export function apiMetaDataGetEntities(query){
  if(!query)
    query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: null}

  if(!query.searchCriteria)
    query.searchCriteria = null;

  return request({
    url: `/api/ExternalAPP/MetaData/GetEntities`,
    params: query
  })
}

export function apiMetaDataCreateEntity(data){
  return request({
    url: `/api/ExternalAPP/MetaData/CreateEntity`,
    method: 'post',
    data
  })
}

/* persons */
export function apiMetaDataGetPersons(query){
  if(!query)
    query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: null}

  if(!query.searchCriteria)
    query.searchCriteria = null;

  return request({
    url: `/api/ExternalAPP/MetaData/GetPersons`,
    params: query
  })
}

export function apiMetaDataCreatePerson(data){
  return request({
    url: `/api/ExternalAPP/MetaData/CreatePerson`,
    method: 'post',
    data
  })
}


export function apiGetProfileInformation(){
  return request({
    url: `/api/ExternalAPP/Account/GetProfileInformation`,
  })
}

/* programs */

export function apiGetCurrentPrograms(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetCurrentPrograms`,
  })
}

/* courses */


export function apiMetaDataGetCourseKinds(){
  return request({
    url: `/api/ExternalAPP/MetaData/GetCourseKinds`,
  })
}