import { filterQuery } from "@/utils";
import request from "@/utils/request";
import fileSaver from "file-saver";

export function apiExportDocumentToExcel(endpoint, fileType, query) {
  if (query.headerRename != null) {
    query.headerRename = query.headerRename
      .filter((x) => x.field && x.field != "")
      .map(function (item) {
        return { key: item.field, value: item.title };
      });
  }
  return request({
    url: endpoint,
    method: "POST",
    responseType: "blob",
    data: filterQuery(query),
  }).then((response) => {
    const blob = new Blob([response.data]);
    const contentDisposition = decodeURIComponent(
      response.headers["content-disposition"]
    );
    if (contentDisposition != "undefined") {
      const fileName = contentDisposition.split(";")[2].split("''")[1];
      fileSaver.saveAs(blob, `${fileName}.${fileType}`);
    } else {
      const fileName = "File";
      fileSaver.saveAs(blob, `${fileName}.${fileType}`);
    }
  })
  .catch((e) => {});
}