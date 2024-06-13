using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

using System.Web;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class PaymentCheckerService
    {
        private readonly AistnContext _db;
        private readonly IConfiguration _configuration;
        protected readonly ExceptionLogger _logger;

        public PaymentCheckerService(AistnContext db, IConfiguration configuration, ExceptionLogger logger)
        {
            _db = db;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<int> checkPaymentsAsync()
        {
            try
            {
                var paymentRequests = _db.Installments.Where(x => x.PaymentRequestId != null && x.DateCreated >= new DateTime(2024, 03, 28) && (x.PaymentCompletedFlag == null || x.PaymentCompletedFlag == false)).ToList();


                var checkIDsDict = new Dictionary<string, Installment>();

                foreach (Installment pr in paymentRequests)
                {
                    checkIDsDict.Add(pr.PaymentRequestId, pr);

                }
                //checkIDsDict.Add("2403216", null);

                var jsonResult = GetPaymentsStatusById(checkIDsDict.Keys.ToArray());
            
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


                PaymentStatusesContainer container = System.Text.Json.JsonSerializer.Deserialize<PaymentStatusesContainer>(jsonResult, options);

                if (container != null && container.PaymentStatuses != null)
                {
                    foreach (var status in container.PaymentStatuses)
                    {
                        checkIDsDict[status.Id].PaymentStatus = status.Status;
                        checkIDsDict[status.Id].PaymentStatusDate = status.ChangeTime;
                        if (status.Status == "paid") checkIDsDict[status.Id].PaymentCompletedFlag = true;
                    }
                }
                await _db.SaveChangesAsync();
                return container.PaymentStatuses.Count;
            } catch (Exception ex)
            {
                _logger.LogException(ex);
                return -1;
            }
        }

        private string GetPaymentsStatusById(string[] requestIds)
        {
            try
            {
                var dataParam = new
                {
                    requestIds = requestIds
                };

                var jsonData = JsonConvert.SerializeObject(dataParam);
                var jsonDataBytes = Encoding.UTF8.GetBytes(jsonData);
                var base64Data = Convert.ToBase64String(jsonDataBytes);


                var ePaymentsSecretKey = _configuration["ePayment:SecretKey"].ToString();
                var hmac = PaymentService.CalculateHmac(ePaymentsSecretKey, base64Data);


                using (var client = new HttpClient())
                {
                    var ePaymentsApiUrl = _configuration["ePayment:ePaymentAPI"].ToString();
                    client.BaseAddress = new Uri(ePaymentsApiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var ePaymentsClientId = _configuration["ePayment:ClientId"].ToString();

                    var encodedHttpBody = String.Format("ClientId={0}&Hmac={1}&Data={2}", HttpUtility.UrlEncode(ePaymentsClientId), HttpUtility.UrlEncode(hmac), HttpUtility.UrlEncode(base64Data));

                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (
                        object s,
                        X509Certificate certificate,
                        X509Chain chain,
                        SslPolicyErrors sslPolicyErrors
                    )
                    {
                        return true;
                    };


                    var response = client.PostAsync(_configuration["ePayment:PaymentsGetPaymentsStatusByIdEndpoint"].ToString(), new StringContent(encodedHttpBody, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var reponseContent = response.Content.ReadAsStringAsync().Result;

                        JObject responseJson = JObject.Parse(reponseContent);

                        // LogPaymentRequest(jsonData, reponseContent, String.Join(", ", requestIds.ToArray()), null, null);

                        return responseJson.ToString();
                    }
                    else
                    {
                        var reponseContent = response.Content.ReadAsStringAsync().Result;

                        // LogPaymentRequest(jsonData, reponseContent, String.Join(", ", requestIds.ToArray()), null, null);

                        return String.Empty;
                    }


                }

            } catch (Exception ex)
            {
                _logger.LogException(ex);
                return null;
            }

        }

        private class PaymentStatus
        {
            public string Id { get; set; }
            public string Status { get; set; }
            public DateTime ChangeTime { get; set; }
        }

        private class PaymentStatusesContainer
        {
            public List<PaymentStatus> PaymentStatuses { get; set; }
        }


    }
}
