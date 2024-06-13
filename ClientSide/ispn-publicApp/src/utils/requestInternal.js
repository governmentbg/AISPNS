import axios from 'axios'
import store from '@/store'
import { getToken } from '@/utils/auth'
import config from '@/config'
import vue from "@/main";
import qs from "qs";


let currentRequestsCounter = 0;
// creating an axios instance
const request = axios.create({
  baseURL: 'https://localhost:44377/',
  headers: {'Content-Type': 'application/json'},
  //withCredentials: true, 
  timeout: config.requestTimeout * 60000, // request timeout,
  paramsSerializer: params => {
    return qs.stringify(params, { encode: false })
  }
})

// request interceptor
request.interceptors.request.use(config => {
  if(currentRequestsCounter === 0){
    vue.$loading(true);
  }
  currentRequestsCounter++

  if (store.getters.token || getToken()) {
    config.headers['Authorization'] = `Bearer ${getToken()}`;
  }
  return config
}, error => {
    //console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
request.interceptors.response.use(
  response => {
    currentRequestsCounter--;
    if(currentRequestsCounter===0){
      vue.$loading(false);
    }
    const res = response.data;
    // if the code is different from 200,201,204 show error message.
    if ((response.status !== 200 && response.status !== 201 && response.status !== 204) || (response.headers['content-type'].includes('application/json') && res.type != 1)) {
      //vue.$snotify.error(res.Message);
      
      vue.$snotify.error(null, null, {html: res.Message});

      //return Promise.reject(new Error(res.Message || 'Error'))
      //return Promise.reject(new Error('Error'))
      return null;
    } else {
      if(response.headers && ['text/csv', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'application/vnd.ms-excel', 'application/pdf', 'text/html', 'text/html; charset=UTF-8', 'text/xml'].includes(response.headers['content-type'])){
        return response;
      } else {
        if(res.Message)
          vue.$snotify.success(null, null, {html: res.Message});

        return res.resultData;
      }
    }
  },
  error => {
    currentRequestsCounter--;
    if(currentRequestsCounter===0){
      vue.$loading(false);
    }

    if(error.response.status === 400){
      let res = error.response.data;
      if(res.errors){
        var messages = "";
        for(var key in res.errors){
          for(var i=0;i<res.errors[key].length;i++){
            messages += res.errors[key][i] +"<br />"
          }
        }
        vue.$snotify.error(messages);
      }
    } else if(error.response.status === 401){
      let redirectLocation = '';
      if(window.location.hash.includes("#")){
        redirectLocation = `/login?redirect=${window.location.hash.split("#")[1]}&notify=Сесията Ви е изтекла.`
      } else {
        redirectLocation = `/login?redirect=${window.location.hash}&notify=Сесията Ви е изтекла.`
      }

      //var message = "Сесията Ви е изтекла. Моля презаредете прозореца и се логнете наново."
      
      //vue.$snotify.error(message || 'Грешка');
      //vue.$router.push({ path: redirectLocation});

      vue.$store.dispatch('user/logout');
      
      vue.$router.push(redirectLocation);
      return Promise.reject(error);
    } else if(error.response.status === 500){
      let message = error.response.data.Message;
      
      if(message.indexOf("\n") != -1){
        let messages = message.split("\n");
        
        if(messages[0].length === 1) messages.shift();
        
        message = '';
        for(let i=0;i<messages.length;i++){
          message += messages[i] +"<br />"
        }
      }
      vue.$snotify.error(message || 'Грешка');
    } else {
      vue.$snotify.error(error.Message || 'Грешка');
    }
    return Promise.reject(error)
  }
)

export default request;