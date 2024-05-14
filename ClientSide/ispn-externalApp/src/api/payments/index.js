import request from '@/utils/request';

export function apiSendPaymentRequest(installmentId){
  return request({
    url: '/api/ExternalApp/Payment/SendPaymentRequest',
    method: 'post',
    params: {installmentId},
  });
}

export function apiGetPaymentOrderData(paymentRequestId){
  return request({
    url: '/api/ExternalApp/Payment/GetPaymentOrderData',
    method: 'post',
    params: {paymentRequestId},
  });
}

export function apiGetPaymentVPOSData(paymentRequestId){
  return request({
    url: '/api/ExternalApp/Payment/GetPaymentVPOSData',
    method: 'post',
    params: {paymentRequestId},
  });
}