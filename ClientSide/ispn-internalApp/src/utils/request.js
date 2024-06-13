import axios from "axios";
import store from "@/store";
import { getToken } from "@/utils/auth";
import config from "@/config";
import vue from "@/main";
import qs from "qs";

if(typeof window.requestsCounter !== 'number')
  window.requestsCounter = 0;
// creating an axios instance
const request = axios.create({
  baseURL: config.apiBaseURL,
  headers: { "Content-Type": "application/json" },
  withCredentials: true,
  timeout: config.requestTimeout * 60000, // request timeout,
  paramsSerializer: (params) => {
    return qs.stringify(params, { encode: false });
  },
});

// request interceptor
request.interceptors.request.use((config) => {
  if (window.requestsCounter === 0)
    vue.$loading(true);

  window.requestsCounter++;
  
  if (store.getters.token || getToken())
    config.headers["Authorization"] = `Bearer ${getToken() || store.getters.token}`;
  
  return config;
}, (error) => {
  return Promise.reject(error);
});

// response interceptor
request.interceptors.response.use((response) => {
  window.requestsCounter--;
  if(window.requestsCounter === 0)
    vue.$loading(false);

  const res = response.data;
  let noResultData = false;

  if (res.type == undefined) {
    try {
      res.type = 1;
      noResultData = true;
    } catch(e){
      noResultData = true;
    }
  }
  // Validation messages
  //
  if (res.type == 0 && res.additionalFieldMessages !== undefined) {
    vue.$snotify.error(res.message);

    return Object.prototype.hasOwnProperty.call(res, "resultData") ? res.resultData : res;
  }
  // if the code is different from 200,201,204 show error message.  
  if ((
      response.status !== 200 && 
      response.status !== 201 && 
      response.status !== 204
    ) || 
    (
      res && 
      res.hasOwnProperty('type') && 
      res.type != 1
    )) {
    //vue.$snotify.error(res.Message);

    vue.$snotify.error(null, null, { html: res.message });

    //return Promise.reject(new Error(res.Message || 'Error'))
    //return Promise.reject(new Error('Error'))
    return null;
  } else {
    const acceptedContentTypes = [
      "text/csv",
      "text/plain",
      "application/zip",
      "application/vnd.rar",
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
      "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
      "application/x-7z-compressed",
      "application/vnd.ms-excel",
      "application/msword",
      "application/pdf",
      "text/html",
      "text/html; charset=UTF-8",
      "text/xml",
      "image/jpeg",
      "image/png",
    ];
    if (
      response.headers &&
      acceptedContentTypes.includes(response.headers["content-type"])
    ) {
      return response;
    } else {
      if (res.message)
        res.type == 0
          ? vue.$snotify.error(null, null, { html: res.message })
          : vue.$snotify.success(null, null, { html: res.message });

      if (noResultData) {
        return res;
      } else {
        return res.resultData;
      }
    }
  }
}, (error) => {
  window.requestsCounter--;
  if(window.requestsCounter === 0)
    vue.$loading(false);

  if(error.response.status === 400){
    let res = error.response.data;
    if(res.errors){
      let messages = "";
      for (let key in res.errors){
        for (let i = 0; i < res.errors[key].length; i++){
          messages += res.errors[key][i] + "<br />";
        }
      }
      vue.$snotify.error(messages);
    }
  } else if ([401, 403].includes(error.response.status)) {
    let redirectLocation = "";
    if (window.location.hash.includes("#") && !window.location.href.includes('redirect')) {
      redirectLocation = `/login?redirect=${window.location.hash.split("#")[1]}&notify=Сесията Ви е изтекла.`;
    } else if(!window.location.href.includes('redirect')) {
      redirectLocation = `/login?redirect=${window.location.pathname}&notify=Сесията Ви е изтекла.`;
    }

    //let message = "Сесията Ви е изтекла. Моля презаредете прозореца и се логнете наново."

    //vue.$snotify.error(message || 'Грешка');
    //vue.$router.push({ path: redirectLocation});
    if(redirectLocation.length){
      vue.$store.dispatch("user/logout");
      vue.$router.push(redirectLocation);
    }
    return Promise.reject(error);
  } else if (error.response.status === 500) {
    let message = error.response.data.message;

    if (message.indexOf("\n") != -1) {
      let messages = message.split("\n");

      if (messages[0].length === 1) messages.shift();

      message = "";
      for (let i = 0; i < messages.length; i++) {
        message += messages[i] + "<br />";
      }
    }
    vue.$snotify.error(message || "Грешка");
  } else {
    vue.$snotify.error(error.Message || "Грешка");
  }
  return Promise.reject(error);
});

export default request;
