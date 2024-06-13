import request from "@/utils/request";

export function apiDownloadDocument(id){
  return request({
    url: `/api/PublicApp/Document/DownloadAttachedFile`,
    method: 'get',
    responseType: "blob",
    params: {id}
  })
}