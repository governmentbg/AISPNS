import request from "@/utils/request.js";
import { filterQuery } from "@/utils/index";

export function uploadFile(
  file,
  documentContentType,
  id,
  fileType,
  attachedId,
  description,
  attachedDocumentKindId,
  attachedDocumentTypeId
) {
  let formData = new FormData();
  formData.append("file", file);
  formData.append("documentContentType", documentContentType);
  formData.append("id", id);
  formData.append("fileType", fileType);
  formData.append("description", description);

  if (attachedDocumentKindId) {
    formData.append("attachedDocumentKindId", attachedDocumentKindId);
  }
  if (attachedDocumentTypeId) {
    formData.append("attachedDocumentTypeId", attachedDocumentTypeId);
  }
  if (attachedId != null) {
    formData.append("attachedId", attachedId);
  }
  return request({
    url: `/api/Document/SubmitDocument`,
    headers: { "Content-Type": "application/json" },
    data: formData,
    method: "post",
  });
}

export function downloadAttachedFile(id) {
  return request({
    url: `/api/Document/DownloadAttachedFile?id=${id}`,
    method: "get",
    responseType: "blob",
  });
}
export function getFileName(id) {
  return request({
    url: `/api/Document/GetFileName?id=${id}`,
    method: "get",
  });
}
export function getBusinessTripFiles(id) {
  return request({
    url: `/api/Document/GetBusinessTripsFiles?id=${id}`,
    method: "get",
  });
}

export function deleteDocument(id) {
  return request({
    url: `/api/Document/DeleteDocument?id=${id}`,
    method: "delete",
  });
}

export function getDocumentFiles(id) {
  return request({
    url: `/api/Document/GetDocumentFiles?id=${id}`,
    method: "get",
  });
}

export function getAllDocumentFiles(guids) {
  return request({
    url: `/api/Document/GetAllDocumentFiles`,
    method: "post",
    data: filterQuery(guids),
  });
}
