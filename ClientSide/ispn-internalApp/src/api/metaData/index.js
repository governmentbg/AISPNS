import request from '@/utils/request';
import { filterQuery } from '@/utils/index';


export function apiMetaDataGetUsers(){
  return request({
    url: `/api/InternalApp/MetaData/GetUsers`,
  })
}

// user roles
export function apiMetaDataGetUserRoles() {
  return request({
    url: `/api/InternalApp/MetaData/GetRoles`,
  })
}

export function apiMetaDataGetLogUserActions(){
  return request({
    url: `/api/InternalApp/MetaData/GetLogUserActions`,
  })

}

// get nomenclature keys
export function apiMetaDataGetCourts() {
  return request({
    url: `/api/InternalApp/MetaData/GetCourtsList`,
  })
}

export function apiMetaDataGetReportSources(){
  return request({
    url: `/api/InternalApp/MetaData/GetReportSources`,
  })
}

export function apiMetaDataGetReportTypes(){
  return request({
    url: `/api/InternalApp/MetaData/GetReportTypes`,
  })
}

export function apiMetaDataGetSpecialties(){
  return request({
    url: `/api/InternalApp/MetaData/GetSpecialties`,
  })
}

export function apiMetaDataGetSyndics(query = null){
  if(!query)
    query = {pageSize: 10, pageNumber: 1}

  return request({
    url: `/api/InternalApp/MetaData/GetSyndics`,
    params: filterQuery(query)
  })
}

export function apiMetaDataGetSyndicsShortInfo(){
  return request(`/api/InternalApp/MetaData/GetSyndicsShortInfo`)
}


export function apiMetaDataGetDistricts() {
  return request({
    url: "/api/InternalApp/MetaData/GetDistricts",
    method: "GET",
  });
}

export function apiMetaDataGetMunicipalities(districtId) {
  return request({
    url: `/api/InternalApp/MetaData/GetMunicipalities?districtId=${districtId}`,
    method: "GET",
  });
}

export function apiMetaDataGetSettlements(municipalityId) {
  return request({
    url: `/api/InternalApp/MetaData/GetSettlements?municipalityId=${municipalityId}`,
    method: "GET",
  });
}

/**
 * announcements
 */



export function apiMetaDataGetAnnouncementStatus(){
  return request({
    url: `/api/InternalApp/MetaData/GetAnnouncementStatus`,
  })
}

export function apiMetaDataGetLegalBasis(){
  return request({
    url: `/api/InternalApp/MetaData/GetLegalBasis`,
  })
}

export function apiMetaDataGetOfferingLocationType(){
  return request({
    url: `/api/InternalApp/MetaData/GetOfferingLocationType`,
  })
}

export function apiMetaDataGetOfferingProcedure(){
  return request({
    url: `/api/InternalApp/MetaData/GetOfferingProcedure`,
  })
}

export function apiMetaDataGetOfferingKind(){
  return request({
    url: `/api/InternalApp/MetaData/GetOfferingKind`,
  })
}

export function apiMetaDataGetObjectKind(){
  return request({
    url: `/api/InternalApp/MetaData/GetObjectKind`,
  })
}

export function apiMetaDataGetObjectType(){
  return request({
    url: `/api/InternalApp/MetaData/GetObjectType`,
  })
}

export function apiMetaDataGetPapersForSale(){
  return request({
    url: `/api/InternalApp/MetaData/GetPapersForSale`,
  })
}

export function apiMetaDataGetSalesProcedure(){
  return request({
    url: `/api/InternalApp/MetaData/GetSalesProcedure`,
  })
}

export function apiMetaDataGetSellAnnouncementTemplates(){
  return request({
    url: `/api/InternalApp/MetaData/GetSellAnnouncementTemplate`,
  })
}



/**
 * 
 * orders
 */

export function apiMetaDataGetOrderKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetOrderKinds`,
  })
}


/**
 * 
 * installments
 */

export function apiMetaDataGetInstallmentKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetInstallmentKinds`,
  })
}

export function apiMetaDataGetInstallmentYears(){
  return request({
    url: `/api/InternalApp/MetaData/GetInstallmentYears`,
  })
}

export function apiMetaDataGetSyndicStatuses(){
  return request({
    url: `/api/InternalApp/MetaData/GetSyndicStatuses`,
  })
}

export function apiGetSignalSenderTypes(){
  return request({
    url: `/api/InternalApp/MetaData/GetSignalSenderTypes`,
  })
}

export function apiGetAttachedDocumentSignalKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetAttachedDocumentSignalKinds`,
  })
}

/**
 * inspections
 */

export function apiMetaDataGetInspections(){
  return request({
    url: `/api/InternalApp/MetaData/GetInspectionTypes`,
  })
}


export function apiMetaDataGetDirectiveTemplateKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetDirectiveTemplateKinds`,
  })
}

export function apiMetaDataGetSyndicTemplateKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetSyndicTemplateKinds`,
  })
}

export function apiMetaDataGetNomCaseReports(){
  return request({
    url: `/api/InternalApp/MetaData/GetNomCaseReports`,
  })
}

export function apiMetaDataGetCourseKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetCourseKinds`,
  })
}

/* activity */

export function apiMetaDataGetActivityKinds(){  
  return request({
    url: `/api/InternalApp/MetaData/GetActivityKinds`,
  })
}

/* Reports */
export function apiMetaDataGetSyndicReportTypes(){
  return request({
    url: `/api/InternalApp/MetaData/GetSyndicReportTypes`,
  })
}


/* entities */
export function apiMetaDataGetEntities(query){
  if(!query)
    query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: null}

  if(!query.searchCriteria)
    query.searchCriteria = null;

  return request({
    url: `/api/InternalApp/MetaData/GetEntities`,
    params: query
  })
}


/* persons */
export function apiMetaDataGetPersons(query){
  if(!query)
    query = {pageSize: 10, pageNumber: 1, searchCriteria: null, syndicId: null}

  if(!query.searchCriteria)
    query.searchCriteria = null;

  return request({
    url: `/api/InternalApp/MetaData/GetPersons`,
    params: query
  })
}

/* act announcements */

export function apiMetaDataGetActAnnouncementCategories(isStabilization = false){
  return request({
    url: `/api/InternalApp/MetaData/GetActAnnouncementCategories`,
    params: {isStabilization}
  })
}

export function apiMetaDataGetActAnnouncementStatuses(){
  return request({
    url: `/api/InternalApp/MetaData/GetActAnnouncementStatus`,
  })
}

export function apiMetaDataGetActFields(isStabilization = false){
  return request({
    url: `/api/InternalApp/MetaData/GetActContractors`,
    params: {isStabilization}
  })
}

export function apiMetaDataGetRegistrationLegalBasis(){
  return request({
    url: `/api/InternalApp/MetaData/GetRegistrationLegalBasis`,
  })
}

export function apiMetaDataGetSyndicKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetSyndicKinds`,
  })
}

export function apiMetaDataGetRegisterSyndicKinds(){
  return request({
    url: `/api/InternalApp/MetaData/GetRegisterSyndicKinds`,
  })
}