import request from "@/utils/request";
import { filterQuery } from "@/utils/index";

export function apiGetNomenclatureEnums() {
  return request({
    url: `/api/InternalApp/Nomenclature/GetNomenclatureEnums`,
    method: "get",
  });
}

export function apiGetNomenclatures(type) {
  return request({
    url: `/api/InternalApp/Nomenclature/GetNomenclatures`,
    method: "get",
    params: { type: type },
  });
}

export function apiGetNomenclatureById(id, type) {
  return request({
    url: `/api/InternalApp/Nomenclature/GetNomenclatureById`,
    method: "get",
    params: filterQuery({ id: id, type: type }),
  });
}

export function apiInsertNomenclature(data, type) {
  return request({
    url: `/api/InternalApp/Nomenclature/InsertNomenclature`,
    method: "POST",
    data: data,
    params: filterQuery({ type: type }),
  });
}

export function apiUpdateNomenclature(data, type) {
  return request({
    url: `/api/InternalApp/Nomenclature/UpdateNomenclature`,
    method: "POST",
    data: data,
    params: filterQuery({ type: type }),
  });
}

export function apiDeleteNomenclature(id, type) {
  return request({
    url: `/api/InternalApp/Nomenclature/DeleteNomenclature`,
    method: "POST",
    params: filterQuery({ id: id, type: type }),
  });
}