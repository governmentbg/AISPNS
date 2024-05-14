import moment from "moment";

/**
   * @param {string} url
   * @returns {Object}
   */
export function getQueryObject(url) {
  url = url == null ? window.location.href : url
  const search = url.substring(url.lastIndexOf('?') + 1)
  const obj = {}
  const reg = /([^?&=]+)=([^?&=]*)/g
  search.replace(reg, (rs, $1, $2) => {
    const name = decodeURIComponent($1)
    let val = decodeURIComponent($2)
    val = String(val)
    obj[name] = val
    return rs
  })
  return obj
}

/**
 * @param {string} input value
 * @returns {number} output value
 */
export function byteLength(str) {
  // returns the byte length of an utf8 string
  let s = str.length
  for (var i = str.length - 1; i >= 0; i--) {
    const code = str.charCodeAt(i)
    if (code > 0x7f && code <= 0x7ff) s++
    else if (code > 0x7ff && code <= 0xffff) s += 2
    if (code >= 0xDC00 && code <= 0xDFFF) i--
  }
  return s
}

/**
 * @param {Array} actual
 * @returns {Array}
 */
export function cleanArray(actual) {
  const newArray = [];
  for (let i = 0; i < actual.length; i++) {
    if (actual[i]) {
      newArray.push(actual[i]);
    }
  }
  return newArray
}

/**
 * @param {Object} json
 * @returns {Array}
 */
export function param(json) {
  if (!json) return ''
  return cleanArray(
    Object.keys(json).map(key => {
      if (json[key] === undefined) return ''
      return encodeURIComponent(key) + '=' + encodeURIComponent(json[key])
    })
  ).join('&')
}

/**
 * @param {string} url
 * @returns {Object}
 */
export function param2Obj(url) {
  const search = url.split('?')[1]
  if (!search) {
    return {}
  }
  return JSON.parse(
    '{"' +
    decodeURIComponent(search)
      .replace(/"/g, '\\"')
      .replace(/&/g, '","')
      .replace(/=/g, '":"')
      .replace(/\+/g, ' ') +
    '"}'
  )
}

/**
 * @param {string} val
 * @returns {string}
 */
export function html2Text(val) {
  const div = document.createElement('div')
  div.innerHTML = val
  return div.textContent || div.innerText
}

/**
 * Merges two objects, giving the last one precedence
 * @param {Object} target
 * @param {(Object|Array)} source
 * @returns {Object}
 */
export function objectMerge(target, source) {
  if (typeof target !== 'object') {
    target = {}
  }
  if (Array.isArray(source)) {
    return source.slice()
  }
  Object.keys(source).forEach(property => {
    const sourceProperty = source[property]
    if (typeof sourceProperty === 'object') {
      target[property] = objectMerge(target[property], sourceProperty)
    } else {
      target[property] = sourceProperty
    }
  })
  return target
}

/**
 * @param {HTMLElement} element
 * @param {string} className
 */
export function toggleClass(element, className) {
  if (!element || !className) {
    return
  }
  let classString = element.className
  const nameIndex = classString.indexOf(className)
  if (nameIndex === -1) {
    classString += '' + className
  } else {
    classString =
      classString.substr(0, nameIndex) +
      classString.substr(nameIndex + className.length)
  }
  element.className = classString
}

/**
 * @param {string} type
 * @returns {Date}
 */
export function getTime(type) {
  if (type === 'start') {
    return new Date().getTime() - 3600 * 1000 * 24 * 90
  } else {
    return new Date(new Date().toDateString())
  }
}

/**
 * @param {Function} func
 * @param {number} wait
 * @param {boolean} immediate
 * @return {*}
 */
export function debounce(func, wait, immediate) {
  let timeout, args, context, timestamp, result

  const later = function () {
    const last = +new Date() - timestamp

    if (last < wait && last > 0) {
      timeout = setTimeout(later, wait - last)
    } else {
      timeout = null
      if (!immediate) {
        result = func.apply(context, args)
        if (!timeout) context = args = null
      }
    }
  }

  return function (...args) {
    context = this
    timestamp = +new Date()
    const callNow = immediate && !timeout

    if (!timeout) timeout = setTimeout(later, wait)
    if (callNow) {
      result = func.apply(context, args)
      context = args = null
    }

    return result
  }
}

/**
 * This is just a simple version of deep copy
 * Has a lot of edge cases bug
 * If you want to use a perfect deep copy, use lodash's _.cloneDeep
 * @param {Object} source
 * @returns {Object}
 */
export function deepClone(source) {
  if (!source && typeof source !== 'object') {
    throw new Error('error arguments', 'deepClone')
  }
  const targetObj = source.constructor === Array ? [] : {}
  Object.keys(source).forEach(keys => {
    if (source[keys] && typeof source[keys] === 'object') {
      targetObj[keys] = deepClone(source[keys])
    } else {
      targetObj[keys] = source[keys]
    }
  })
  return targetObj
}

/**
 * @param {Array} arr
 * @returns {Array}
 */
export function uniqueArr(arr) {
  return Array.from(new Set(arr))
}

/**
 * @returns {string}
 */
export function createUniqueString() {
  const timestamp = +new Date() + ''
  const randomNum = parseInt((1 + Math.random()) * 65536) + ''
  return (+(randomNum + timestamp)).toString(32)
}

/**
 * Check if an element has a class
 * @param {HTMLElement} elm
 * @param {string} cls
 * @returns {boolean}
 */
export function hasClass(ele, cls) {
  return !!ele.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'))
}

/**
 * Add class to element
 * @param {HTMLElement} elm
 * @param {string} cls
 */
export function addClass(ele, cls) {
  if (!hasClass(ele, cls)) ele.className += ' ' + cls
}

/**
 * Remove class from element
 * @param {HTMLElement} elm
 * @param {string} cls
 */
export function removeClass(ele, cls) {
  if (hasClass(ele, cls)) {
    const reg = new RegExp('(\\s|^)' + cls + '(\\s|$)')
    ele.className = ele.className.replace(reg, ' ')
  }
}

export function pad(n) { return n < 10 ? '0' + n : n }

export function formatDate(d){
  if(d){
    var date = moment(d)

    return date.format("DD.MM.YYYY");
  }

  return "";
}

export function formatDateTime(d){
  if(d){
    var date = moment(d)

    return date.format("DD.MM.YYYYг. HH:mmч.");
  }

  return "";
}

export function formatDateTimeSeconds(d){
  if(d){
    var date = moment(d)

    return date.format("DD.MM.YYYYг. HH:mm:ssч.");
  }

  return "";
}

export function isValidGUID(guid) {
  if(guid)
    return /^[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}$/i.test(guid);


  return false;
}

export function ISODateString(d) {
  if (d) {
    var date = d;
    if(typeof d === 'string') {
      d = new Date(d);
      if(moment(d).isValid()){
        return pad(d.getDate()) + '.' +
          pad(d.getMonth() + 1) + '.' +
          d.getFullYear()
      } else {
        if(date.length === 8){
          d = moment(date, "DDMMYYYY");
          if(!d.isValid()){
            d = moment(date, "YYYYMMDD");
          }
        } else {
          d = moment(date);
        }


        return d.format("DD.MM.YYYY");
      }
    } else {
      return pad(d.getDate()) + '.' +
        pad(d.getMonth() + 1) + '.' +
        d.getFullYear()
    }
  } else {
    return ''
  }
}

export function dateToDominoString(d) {
  if (d) {
    return d.getFullYear() + '-' +
      pad(d.getMonth() + 1) + '-' +
      pad(d.getDate())
  } else {
    return ''
  }
}

export function translateDateToISO(date) {
  if(date){
      return new Date(date.replace(/(\d{2}).(\d{2}).(\d{4})/, "$3/$2/$1"));
  } else {
      return '';
  }
}

export function roundNumber(n, digits) {
  var negative = false
  if (digits === undefined) {
    digits = 0
  }

  if (n < 0) {
    negative = true
    n = n * -1
  }

  var multiplicator = Math.pow(10, digits)
  n = parseFloat((n * multiplicator).toFixed(11))
  n = (Math.round(n) / multiplicator).toFixed(2)
  if (negative) {
    n = (n * -1).toFixed(2)
  }

  return n
}

export function objectToURLparams(obj){
  var result = "?";
  let count = 0;
  for (var key in obj) {
    if (result != "" && count > 0) {
      result += "&";
    }
    if(obj[key] instanceof Array){
      for(let i=0;i<obj[key].length;i++){
        if(i !== 0){
          result += key +`[${i}]=${encodeURIComponent(obj[key])}&`; 
        } else {
          result += key +`[${i}]=${encodeURIComponent(obj[key])}`;
        }
      }

      if(obj[key].length > 1){
        result = result.slice(0, -1);
      }
    } else {
      result += key + "=" + encodeURIComponent(obj[key]);
    }

    count++
  }

  return result;
}

export function filterQuery(query) {
  Object.keys(query).forEach(key => {
    if (query[key] === '') {
      delete query[key]
    }
  })
  return query
}

export function euroToWords(amount) {
  var words = []
  words[0] = ''
  words[1] = 'One'
  words[2] = 'Two'
  words[3] = 'Three'
  words[4] = 'Four'
  words[5] = 'Five'
  words[6] = 'Six'
  words[7] = 'Seven'
  words[8] = 'Eight'
  words[9] = 'Nine'
  words[10] = 'Ten'
  words[11] = 'Eleven'
  words[12] = 'Twelve'
  words[13] = 'Thirteen'
  words[14] = 'Fourteen'
  words[15] = 'Fifteen'
  words[16] = 'Sixteen'
  words[17] = 'Seventeen'
  words[18] = 'Eighteen'
  words[19] = 'Nineteen'
  words[20] = 'Twenty'
  words[30] = 'Thirty'
  words[40] = 'Forty'
  words[50] = 'Fifty'
  words[60] = 'Sixty'
  words[70] = 'Seventy'
  words[80] = 'Eighty'
  words[90] = 'Ninety'
  amount = amount.toString()
  var atemp = amount.split('.')
  var number = atemp[0].split(',').join('')
  var n_length = number.length
  var words_string = ''
  if (n_length <= 9) {
    var n_array = [0, 0, 0, 0, 0, 0, 0, 0, 0]
    var received_n_array = []
    for (var z = 0; z < n_length; z++) {
      received_n_array[z] = number.substr(z, 1)
    }
    for (var a = 9 - n_length, h = 0; a < 9; a++, h++) {
      n_array[a] = received_n_array[h]
    }
    for (var c = 0, j = 1; c < 9; c++, j++) {
      if (c === 0 || c === 2 || c === 4 || c === 7) {
        if (n_array[c] === 1) {
          n_array[j] = 10 + parseInt(n_array[j])
          n_array[c] = 0
        }
      }
    }
    var value = ''
    for (var i = 0; i < 9; i++) {
      if (i === 0 || i === 2 || i === 4 || i === 7) {
        value = n_array[i] * 10
      } else {
        value = n_array[i]
      }
      if (value != 0) {
        words_string += words[value] + ' '
      }
      if ((i === 1 && value != 0) || (i === 0 && value != 0 && n_array[i + 1] === 0)) {
        words_string += 'Crores '
      }
      if ((i === 3 && value != 0) || (i === 2 && value != 0 && n_array[i + 1] === 0)) {
        words_string += 'Lakhs '
      }
      if ((i === 5 && value != 0) || (i === 4 && value != 0 && n_array[i + 1] === 0)) {
        words_string += 'Thousand '
      }
      if (i === 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
        words_string += 'Hundred and '
      } else if (i === 6 && value != 0) {
        words_string += 'Hundred '
      }
    }
    words_string = words_string.split('  ').join(' ')
  }
  return words_string
}

export function bytesToSize(bytes) {
  var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
  if (bytes == 0) return '0 Byte';
  var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
  return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
}

export function withDecimal(n) {
  var nums = n.toString().split('.')
  var whole = euroToWords(nums[0])
  if (nums.length === 2) {
    var fraction = euroToWords(nums[1])
    return capitalizeFirstLetter(whole.toLowerCase()) + 'EURO and ' + fraction + 'cents'
  } else {
    return capitalizeFirstLetter(whole.toLowerCase()) + 'EURO'
  }
}

export function capitalizeFirstLetter(string) {
  return string.charAt(0).toUpperCase() + string.slice(1)
}

export function getParameter(name, url) {
  if (!url) url = window.location.href;
  name = name.replace(/[\\[\]]/g, '\\$&');
  var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
      results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return '';
  return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

export function isEmptyObject(obj){
  let isEmpty = true;
  for(var prop in obj) {
    if({}.hasOwnProperty.call(obj, prop) && (obj[prop] && obj[prop] != "" || obj[prop] === 0 ) && ('undefined' !== typeof obj[prop]) || (typeof obj[prop] === "boolean")){
        isEmpty = false;
    }
  }
  return isEmpty;
}

export function notify(vue, message, type){
  var notifyType = "success";
  if('undefined' !== typeof type){
    switch(type){
      case 0: 
        notifyType = "danger";
      break;
      case 1:
        notifyType = "warning";
      break;
    }
  }

  vue.$notify({
    timeout: 2500,
    message: message,
    icon: "add_alert",
    horizontalAlign: 'right',
    verticalAlign: 'bottom',
    type: notifyType
  })
}

export function validateEmail(email){
  var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
}

String.prototype.replaceAll = function (search, replacement) {
  var str1 = this.replace(search, replacement);
  var str2 = this;
  while(str1 != str2) {
      str2 = str1;
      str1 = str1.replace(search, replacement);
  }
  return str1;
}


export function base64ToPdfFile(base64){
  let dbase64 = atob(base64);
  let length = dbase64.length;
  let bit8Arr = new Uint8Array(length);

  while(length--){
    bit8Arr[length] = dbase64.charCodeAt(length)
  }

  return new File([bit8Arr], "Scan Document - "+(formatDate(new Date()).replaceAll(".", "-"))+".pdf", {type: "application/pdf"})
}

export function guid() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function getArrayField(item, arr, hasComma){
  let str = '';
  for(let i=0;i<arr.length;i++){
    if(i < (arr.length-1)){
      str += hasComma ? enumerateString(item[arr[i]]) : item[arr[i]] + ' ';
    } else {
      str += item[arr[i]];
    }
  }

  return str;
}

export function getClientAddress(addrData){
  if(Object.keys(addrData).length)
    return enumerateString(addrData.country ? addrData.country.fieldDescription : '') + 
      enumerateString(addrData.municipality) + 
      enumerateString(addrData.location) + 
      enumerateString(addrData.zipCode) + 
      enumerateString(addrData.addressPart1) + 
      (addrData.addressPart2 ? addrData.addressPart2 : '')

  return '';
}

export function getClientEmail(emailObj){
  if(Object.keys(emailObj).length)
    return emailObj.email;

  return '';
}

export function getClientPhone(phoneObj){
  if(Object.keys(phoneObj).length)
    return phoneObj.phoneNo;

  return '';
}

export function getClientSex(sex){
  if(sex && sex.length){
    return sex.toLowerCase() === "m" ? "Мъж" : "Жена"
  }

  return null;
}

export function enumerateString(str){
  return str ? str + ', ' : '';
}

export function isValidIBANNumber(input) {
  var CODE_LENGTHS = {
      AD: 24, AE: 23, AT: 20, AZ: 28, BA: 20, BE: 16, BG: 22, BH: 22, BR: 29,
      CH: 21, CR: 21, CY: 28, CZ: 24, DE: 22, DK: 18, DO: 28, EE: 20, ES: 24,
      FI: 18, FO: 18, FR: 27, GB: 22, GI: 23, GL: 18, GR: 27, GT: 28, HR: 21,
      HU: 28, IE: 22, IL: 23, IS: 26, IT: 27, JO: 30, KW: 30, KZ: 20, LB: 28,
      LI: 21, LT: 20, LU: 20, LV: 21, MC: 27, MD: 24, ME: 22, MK: 19, MR: 27,
      MT: 31, MU: 30, NL: 18, NO: 15, PK: 24, PL: 28, PS: 29, PT: 25, QA: 29,
      RO: 24, RS: 22, SA: 24, SE: 24, SI: 19, SK: 24, SM: 27, TN: 24, TR: 26,   
      AL: 28, BY: 28, EG: 29, GE: 22, IQ: 23, LC: 32, SC: 31, ST: 25, SV: 28, 
      TL: 23, UA: 29, VA: 22, VG: 24, XK: 20
  };
  var iban = String(input).toUpperCase().replace(/[^A-Z0-9]/g, ''),
          code = iban.match(/^([A-Z]{2})(\d{2})([A-Z\d]+)$/),
          digits;
  // check syntax and length
  if (!code || iban.length !== CODE_LENGTHS[code[1]]) {
    return false;
  }
  // rearrange country code and check digits, and convert chars to ints
  digits = (code[3] + code[1] + code[2]).replace(/[A-Z]/g, function (letter) {
    return letter.charCodeAt(0) - 55;
  });
  // final check
  return mod97(digits);
}

function mod97(digital) {
  digital = digital.toString();
  let checksum = digital.slice(0, 2);
  let fragment = '';
  for (let offset = 2; offset < digital.length; offset += 7) {
    fragment = checksum + digital.substring(offset, offset + 7);
    checksum = parseInt(fragment, 10) % 97;
  }
  return checksum === 1 ? true : false;
}

export function generateYears(startYear){
  let currentYear = new Date().getFullYear();
  startYear = startYear || 2000;
  let years = [];
  
  while (startYear <= currentYear) {
    years.push(startYear++);
  }

  return years.reverse();
}


export function removeDuplicateElementsArray(a, b) {
  b = b.filter( function( item ) {
      for( var i=0, len=a.length; i<len; i++ ){
          if( a[i].name == item.name ) {
              return false;
          }
      }
      return true;
  });

  return b;
}

//export function downloadFile(data, filename, mime, bom) {
export function downloadFileFromResponse(response){
  // eslint-disable-next-line no-extra-boolean-cast
  if(!!!response)
    return
  let bom;
  let mime = response.headers['content-type'];
  let { data } = response;

  let filename = '';
  if (response.headers['content-disposition'] && response.headers['content-disposition'].indexOf('attachment') !== -1) {
    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
    var matches = filenameRegex.exec(response.headers['content-disposition']);
    if (matches != null && matches[1]) { 
      filename = matches[1].replace(/['"]/g, '');
    }
  }
  // eslint-disable-next-line no-extra-boolean-cast
  if(!!!filename)
    return;

  var blobData = (typeof bom !== 'undefined') ? [bom, data] : [data]
  var blob = new Blob(blobData, {type: mime || 'application/octet-stream'});
  if (typeof window.navigator.msSaveBlob !== 'undefined') {
      window.navigator.msSaveBlob(blob, filename);
  } else {
    var blobURL = (window.URL && window.URL.createObjectURL) ? window.URL.createObjectURL(blob) : window.webkitURL.createObjectURL(blob);
    var tempLink = document.createElement('a');
    tempLink.style.display = 'none';
    tempLink.href = blobURL;
    tempLink.setAttribute('download', filename);

    if (typeof tempLink.download === 'undefined') {
      tempLink.setAttribute('target', '_blank');
    }

    document.body.appendChild(tempLink);
    tempLink.click();

    setTimeout(function() {
        document.body.removeChild(tempLink);
        window.URL.revokeObjectURL(blobURL);
    }, 200)
  }
}

export const UserPermissions = {
  ADMIN: 0,
  USER: 1
}

Object.freeze(UserPermissions);



export function secondsToDHMS(seconds) {
  seconds = Number(seconds);
  var d = Math.floor(seconds / (3600*24));
  var h = Math.floor(seconds % (3600*24) / 3600);
  var m = Math.floor(seconds % 3600 / 60);
  var s = Math.floor(seconds % 60);
  
  var dDisplay = d > 0 ? d+ (d == 1 ? ` ${i18n.t("day")} ` : ` ${i18n.t("days")} `) : "";
  var hDisplay = h > 0 ? padWithLeadingZeros(h, 2) : "00";
  var mDisplay = m > 0 ? ":"+padWithLeadingZeros(m, 2) : ":00";
  //var sDisplay = s > 0 ? ":"+s : ":00";
  
  return dDisplay + ((h + m ) > 0 ? hDisplay + mDisplay : '');
}


export function getSyndicFullName(syndic){
  let name = syndic.firstName;

  name += syndic.secondName ? ' '+syndic.secondName : ''

  name += syndic.lastName ? ' '+syndic.lastName : ''

  return name
}