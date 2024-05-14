using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000005;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000006;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000008;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000015;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_10018;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_10024;
using AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_10046;
using AISTN.ExternalAppAPI.Helper;
using AISTN.ExternalAppAPI.Models.ePayment;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Nodes;
using System.Web;
using static AISTN.ExternalAppAPI.Models.ePayment.PaymentReceipt.R_10052_PaymentRequestUnacceptedReceipt;
using static AISTN.ExternalAppAPI.Models.ePayment.PaymentReceipt.R_10055_PaymentRequestAcceptedReceipt;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class PaymentService : ServiceBase
    {
        private readonly IGenericRepository<Installment> _installment;
        private readonly IGenericRepository<Setting> _setting;
        private readonly IConfiguration _configuration;

        public PaymentService(IMapper mapper,
                              ExceptionLogger logger,
                              ExcelGenerator excelGenerator,
                              IGenericRepository<Installment> installment,
                              IGenericRepository<Setting> setting,
                              IConfiguration configuration) : base(mapper, logger, excelGenerator)
        {
            _installment = installment;
            _setting = setting;
            _configuration = configuration;
        }

        internal OperationResult<string> SendPaymentRequest(Guid installmentId)
        {
            Installment installment = _installment.Get(filter: x => x.Id == installmentId && x.PaymentRequestId == null, source => source.Include(x => x.Syndic)).FirstOrDefault();
            if (installment == null) return Error<string>("Няма намерено плащане");
            var paymentRequest = CreatePaymentRequest(installment);

            string requestXml = new XMLHelper().XmlSerializeObjectToString(paymentRequest);
            var dataParam = new
            {
                RequestXml = requestXml
            };
            var jsonDataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dataParam));
            var base64Data = Convert.ToBase64String(jsonDataBytes);
            var ePaymentsSecretKey = _configuration["ePayment:SecretKey"];
            var ePaymentsClientId = _configuration["ePayment:ClientId"];
            var hmac = CalculateHmac(ePaymentsSecretKey, base64Data);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["ePayment:ePaymentRequestAPI"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    var encodedHttpBody = String.Format("ClientId={0}&Hmac={1}&Data={2}", HttpUtility.UrlEncode(ePaymentsClientId), HttpUtility.UrlEncode(hmac), HttpUtility.UrlEncode(base64Data));

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var content = new StringContent(encodedHttpBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = client.PostAsync(_configuration["ePayment:ePaymentRequestAPI"], content).Result;
                    client.Dispose();

                    if (response.IsSuccessStatusCode)
                    {
                        var reponseContent = response.Content.ReadAsStringAsync().Result;
                        JObject responseJson = JObject.Parse(reponseContent);

                        string unacceptedReceiptXml = (string)responseJson.Properties().ToList().Single(e => e.Name.ToLower() == "unacceptedReceiptXml".ToLower()).Value;
                        string acceptedReceiptXml = (string)responseJson.Properties().ToList().Single(e => e.Name.ToLower() == "acceptedReceiptXml".ToLower()).Value;

                        if (!String.IsNullOrWhiteSpace(acceptedReceiptXml))
                        {
                            PaymentRequestAcceptedReceipt acceptedReceipt = new XMLHelper().DeserializeXML<PaymentRequestAcceptedReceipt>(acceptedReceiptXml);
                            installment.PaymentRequestId = acceptedReceipt.PaymentRequestID;

                            installment.PaymentAccessCode = GetPaymentAccessCode(acceptedReceipt.PaymentRequestID, client); // attach access code for future payment
                            _installment.Update(installment);
                            _installment.Save();
                            return Success(acceptedReceipt.DocumentTypeName);
                        }
                        else
                        {
                            PaymentRequestUnacceptedReceipt unacceptedReceipt = new XMLHelper().DeserializeXML<PaymentRequestUnacceptedReceipt>(unacceptedReceiptXml);

                            return Success(unacceptedReceipt.DocumentTypeName);
                        }
                    }
                    else
                    {
                        return Error<string>("Грешка при заявка за плащане");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<string>(ex);
            }
        }

        /// <summary>
        /// This method is used to get payment code after we create payment request
        /// </summary>
        /// <param name="paymentRequestID"></param>
        /// <returns></returns>
        private string GetPaymentAccessCode(string paymentRequestID, HttpClient client)
        {
            try
            {
                client = new HttpClient();
                var dataParam = new
                {
                    id = paymentRequestID
                };
                var jsonData = JsonConvert.SerializeObject(dataParam);
                var jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);
                var base64Data = Convert.ToBase64String(jsonDataBytes);
                var ePaymentsSecretKey = _configuration["ePayment:SecretKey"];
                var ePaymentsClientId = _configuration["ePayment:ClientId"];
                var hmac = CalculateHmac(ePaymentsSecretKey, base64Data);


                client.BaseAddress = new Uri(_configuration["ePayment:ePaymentAccessCodeAPI"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var encodedHttpBody = String.Format("ClientId={0}&Hmac={1}&Data={2}", HttpUtility.UrlEncode(ePaymentsClientId), HttpUtility.UrlEncode(hmac), HttpUtility.UrlEncode(base64Data));

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                var content = new StringContent(encodedHttpBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = client.PostAsync(_configuration["ePayment:ePaymentAccessCodeAPI"], content).Result;
                client.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var reponseContent = response.Content.ReadAsStringAsync().Result;

                    JObject responseJson = JObject.Parse(reponseContent);

                    var accessCode = (string)responseJson.Properties().ToList().Single(e => e.Name.ToLower() == "accessCode".ToLower()).Value;

                    return accessCode;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                throw ex;
            }
        }

        public PaymentRequestPublicData GetVposPaymentData(string paymentRequestId, string urlScheme)
        {
            var dataParam = new
            {
                id = paymentRequestId,
                //id = "21022247",
                okUrl = urlScheme + "/Payment/ePayment/PaymentResult",
                cancelUrl = urlScheme + "/Payment/ePayment/PaymentResult"
            };

            var jsonData = JsonConvert.SerializeObject(dataParam);
            var jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);

            var base64Data = Convert.ToBase64String(jsonDataBytes);

            //var ePaymentsClientId = "epayments_ais_client_c8462686-d7e3-47fc-971b-f889508c0526";
            //var ePaymentsSecretKey = "JBDMJ6KDC2CYPJ78GCEI2W13KM6TVD22";
            var ePaymentsClientId = _configuration["ePayment:ClientId"];
            var ePaymentsSecretKey = _configuration["ePayment:SecretKey"];
            var hmac = CalculateHmac(ePaymentsSecretKey, base64Data);

            PaymentRequestPublicData result = new PaymentRequestPublicData();
            result.clientId = ePaymentsClientId;
            result.hmac = hmac;
            result.Data = base64Data;

            return result;
        }

        public PaymentRequestPublicData GetPaymentOrderData(string paymentRequestId)
        {
            var dataParam = new
            {
                id = paymentRequestId
                //TODO - remove in future this line
                //id = "21022247"
            };

            var jsonData = JsonConvert.SerializeObject(dataParam);
            var jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);
            var base64Data = Convert.ToBase64String(jsonDataBytes);

            var ePaymentsClientId = _configuration["ePayment:ClientId"];
            var ePaymentsSecretKey = _configuration["ePayment:SecretKey"];

            // TODO: Remove in future bottom two lines
            //var ePaymentsClientId = "epayments_ais_client_c8462686-d7e3-47fc-971b-f889508c0526";
            //var ePaymentsSecretKey = "JBDMJ6KDC2CYPJ78GCEI2W13KM6TVD22";

            var hmac = CalculateHmac(ePaymentsSecretKey, base64Data);

            PaymentRequestPublicData result = new PaymentRequestPublicData();
            result.clientId = ePaymentsClientId;
            result.hmac = hmac;
            result.Data = base64Data;

            return result;
        }

        private ExternalAppAPI.ePayment.R_10046.PaymentRequest CreatePaymentRequest(Installment installment)
        {
            Setting setting = _setting.GetAll().FirstOrDefault();
            ElectronicServiceRecipientDTO recipient = new ElectronicServiceRecipientDTO();

            recipient.ServiceRecipientTypeId = eRecipientType.BulgarianCitizen;
            recipient.FirstName = installment.Syndic.FirstName;
            recipient.MiddleName = installment.Syndic.SecondLastName == null ? installment.Syndic.SecondName : $"{installment.Syndic.SecondName} {installment.Syndic.SecondLastName}";
            recipient.LastName = installment.Syndic.LastName;
            recipient.CitizenUin = installment.Syndic.Egn;

            ExternalAppAPI.ePayment.R_10046.PaymentRequest paymentRequest = new ExternalAppAPI.ePayment.R_10046.PaymentRequest()
            {
                DocumentTypeName = "Заявка за плащане",
                Currency = _configuration["ePayment:ePaymentCurrency"],
                PaymentAmount = (double)installment.Amount,
                PaymentReason = "Плащане",
                ElectronicServiceProviderBankAccount = new ExternalAppAPI.ePayment.R_10010.ElectronicServiceProviderBankAccount
                {
                    BIC = setting.Bic,
                    IBAN = setting.Iban,
                    EntityBasicData = new ExternalAppAPI.ePayment.R_0009_000013.EntityBasicData
                    {
                        Name = setting.BankName
                    }
                },
                ElectronicServiceProviderBasicData = new ExternalAppAPI.ePayment.R_0009_000002.ElectronicServiceProviderBasicData
                {
                    ElectronicServiceProviderType = "0006-000031", // got it from old payment service don't know what is it
                    EntityBasicData = new ExternalAppAPI.ePayment.R_0009_000013.EntityBasicData
                    {
                        Name = "Министерство на правосъдието"
                    }
                },
                PaymentRequestExpirationDate = DateTime.Now.AddDays(14),
            };


            PaymentReference paymentReference = new PaymentReference();
            paymentReference.PaymentReferenceType = "1";
            paymentReference.PaymentReferenceNumber = installment.Id.ToString();
            paymentReference.PaymentReferenceDate = installment.DateCreated;


            paymentRequest.PaymentReference = paymentReference;


            PaymentPeriod paymentPeriod = new PaymentPeriod();
            paymentPeriod.PaymentPeriodFromDate = DateTime.Now;
            paymentPeriod.PaymentPeriodToDate = DateTime.Now.AddDays(365);
            paymentRequest.PaymentPeriod = paymentPeriod;

            paymentRequest.ElectronicServiceRecipient = GetElectronicServiceRecipient(recipient);

            return paymentRequest;
        }

        /// <summary>
        /// This method is used to hash body with secret so we can post it
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="xmlBody"></param>
        /// <returns></returns>
        internal static string CalculateHmac(string secretKey, string xmlBody)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretKey);
            var valueBytes = Encoding.UTF8.GetBytes(xmlBody);
            string signature;

            using (var hmac = new HMACSHA256(secretBytes))
            {
                var hash = hmac.ComputeHash(valueBytes);
                signature = Convert.ToBase64String(hash);
            }

            return signature;
        }

        private ElectronicServiceRecipient GetElectronicServiceRecipient(ElectronicServiceRecipientDTO recipient)
        {
            ElectronicServiceRecipient result = new ElectronicServiceRecipient();

            result.Person = new PersonBasicData()
            {
                Identifier = new PersonIdentifier
                {
                    EGN = recipient.CitizenUin
                    //EGN = "6911110934" // this egn is valid for testing we should not have not valid egns in the system
                },
                Names = new PersonNames
                {
                    First = recipient.FirstName,
                    Middle = recipient.MiddleName,
                    Last = recipient.LastName
                }
            };

            return result;
        }

        public static class eRecipientType
        {
            public static readonly int BulgarianCitizen = 1;
            public static readonly int Foreigner = 2;
            public static readonly int LegalEntity = 3;
        }
    }
}
