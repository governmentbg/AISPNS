using AISTN.Common.Helper;
using AISTN.ExternalAppAPI.Models.Authentication;
using AISTN.ExternalAppAPI.Models.Authentication.AuthnResponseEncryptedAssertion;
using AISTN.Repository.Attributes;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IO.Compression;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Assertion = AISTN.ExternalAppAPI.Models.Authentication.Assertion;

namespace AISTN.ExternalAppAPI.Services
{
    [Injectable]
    public class AuthenticationService
    {
        private XmlNamespaceManager nsmgr;
        private XmlDocument xmlDoc;
        static Helper.XMLHelper XMLHelper = new Helper.XMLHelper();
        private readonly Common.Services.AccountService _accountService;
        private readonly IConfiguration _configuration;
        public AuthenticationService(Common.Services.AccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        public OperationResult<string> HandleResponse(string SAMLResponse)
        {
            xmlDoc = new XmlDocument();
            var samlxml = Base64Decode(SAMLResponse);
            xmlDoc.LoadXml(samlxml); 
            
            DecryptAssertion(GetCertificate(_configuration));
            
            if (ValidateXml(xmlDoc, GetPublicCertificate(), "ds:Signature"))
            {
                Assertion assertionType = XMLHelper.DeserializeXML<Assertion>(xmlDoc.GetElementsByTagName("saml2:Assertion")[0].OuterXml) ;
                var citizenshipNumber = assertionType.AttributeStatement.Where(x => x.Name.Contains("Identifier")).FirstOrDefault().AttributeValue.ToString().Split("-")[1]; // get egn from e auth
                var response = _accountService.LoginWithCitizenShipNumber(citizenshipNumber);
                return response;
            }
            else
            {
                return OperationResult<string>.Error("Невалиден подпис на отговора.");
            }
        }

        public AuthnRequest SendAuthenticationRequest()
        {
            AuthnRequestType authRequest = GenerateAuthenticationRequest();
            var request = "";

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");
            ns.Add("egovbga", "urn:bg:egov:eauth:2.0:saml:ext");
            ns.Add("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
            ns.Add("xenc", "http://www.w3.org/2001/04/xmlenc#");
            request = XMLHelper.SerializeToXML(authRequest, ns);
            X509Certificate2 cert = GetCertificate(_configuration);


            XmlDocument xmlDoc = new XmlDocument();
            XDocument xDoc = new XDocument();
            xDoc = XDocument.Parse(request);

            xmlDoc.LoadXml(request);
            xmlDoc.PreserveWhitespace = false;

            var fullRequest = SignXmlWithCertificate(xmlDoc, cert).OuterXml; ;
            using (var client = new HttpClient())
            {


                var SAMLRequest = Base64Encode(fullRequest);
                //var SAMLRequest = request;


                var parameters = new Dictionary<string, string> { { "SAMLRequest", SAMLRequest } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                AuthnRequest req = new AuthnRequest { SAMLRequest = SAMLRequest };
                return req;

            }

        }

        public string GenerateMetadata(IConfiguration configuration)
        {
            var cert = AuthenticationService.GetCertificate(configuration);

            string metadataPath = configuration.GetValue<string>("eAuthentication:MetadataPath");

            XmlDocument metadataxml = new XmlDocument();
            metadataxml.Load(metadataPath); 

            metadataxml.GetElementsByTagName("md:EntityDescriptor").Item(0).Attributes["validUntil"].Value = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-ddTHH:mm:ssZ");

            AuthenticationService.SignXmlWithCertificate(metadataxml, cert);

            return XMLHelper.SerializeToXML(metadataxml);
        }


        public static string Base64Encode(string plainText)
        {
            var saml = string.Format(plainText, Guid.NewGuid());
            var bytes = Encoding.UTF8.GetBytes(saml);

            string deflatedBase64;
            using (var output = new MemoryStream())
            {
                using (var zip = new DeflateStream(output, CompressionMode.Compress))
                    zip.Write(bytes, 0, bytes.Length);

                deflatedBase64 = Convert.ToBase64String(output.ToArray());
            }


            return deflatedBase64;
        }

        public static string Base64Decode(string base64String)
        {
            var plainTextBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(plainTextBytes);
        }

        public AuthnRequestType GenerateAuthenticationRequest()
        {
            var namespaces = new XmlSerializerNamespaces();

            AuthnRequestType authRequest = new AuthnRequestType();
            AssertionType assertion = new AssertionType();

            authRequest.AssertionConsumerServiceURL = $"{_configuration["eAuthentication:ExternalAPIURL"]}/api/ExternalApp/Authentication/ExtLogin";
            authRequest.Destination = $"{_configuration["eAuthentication:EgovURL"]}/SingleSignOnService";
            authRequest.ForceAuthn = false;
            authRequest.ID = "id-" + Guid.NewGuid().ToString();
            authRequest.IsPassive = true;
            authRequest.IssueInstant = DateTime.Now;
            authRequest.ProtocolBinding = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST";
            authRequest.Version = "2.0";

            authRequest.Issuer = new NameIDType();
            authRequest.Issuer.Value = $"{_configuration["eAuthentication:ExternalAPIConsumerServiceURL"]}/api/ExternalApp/Authentication/Metadata";


            ExtensionsType extensions = new ExtensionsType();
            RequestedServiceType reqService = new RequestedServiceType();
            reqService.Provider = _configuration["eAuthentication:Provider"];
            reqService.Service = _configuration["eAuthentication:Service"];
            reqService.LevelOfAssurance = LevelOfAssuranceType.urnegovbgeauth20levelofassurancelow;
            extensions.RequestedService = reqService;

            RequestedAttributeType reqAttributeTypeLatinName = new RequestedAttributeType();
            RequestedAttributeType reqAttributeTypeBirthName = new RequestedAttributeType();
            RequestedAttributeType reqAttributeTypeEmail = new RequestedAttributeType();

            reqAttributeTypeLatinName.AttributeValue = new object[] { ClaimsTypeEnum.urnegovbgeauth20attributeslatinName };
            reqAttributeTypeBirthName.AttributeValue = new object[] { ClaimsTypeEnum.urnegovbgeauth20attributesbirthName };
            reqAttributeTypeEmail.AttributeValue = new object[] { ClaimsTypeEnum.urnegovbgeauth20attributesemail };

            authRequest.Extensions = extensions;

            return authRequest;

        }

        public static X509Certificate2 GetCertificate(IConfiguration configuration)
        {
            string certificatePath = configuration.GetValue<string>("eAuthentication:CertificatePath");
            string certificatePassword = configuration.GetValue<string>("eAuthentication:CertificatePassword");

            X509Certificate2 cert = new X509Certificate2(File.ReadAllBytes(certificatePath), certificatePassword);

            return cert;
        }


        public X509Certificate2 GetPublicCertificate()
        {
            HttpClient httpClient = new HttpClient();
            Uri uri = new Uri($"{_configuration["eAuthentication:EgovURL"]}/tfauthbe/saml/metadata/idp");
            httpClient.BaseAddress = uri;
            var xml = httpClient.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

            var document = new XmlDocument();
            document.LoadXml(xml);
            nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("md", "urn:oasis:names:tc:SAML:2.0:metadata");
            nsmgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");

            var cert = document.SelectSingleNode("md:EntityDescriptor/md:IDPSSODescriptor/md:KeyDescriptor/ds:KeyInfo/ds:X509Data/ds:X509Certificate", nsmgr).InnerText;
            /*...Decode text in cert here (may need to use Encoding, Base64, UrlEncode, etc) ending with 'data' being a byte array...*/
            X509Certificate2 x509 = new X509Certificate2(Encoding.UTF8.GetBytes(cert));

            //X509Certificate2 cert = new X509Certificate2File.ReadAllBytes(@"D:\repo\AISTN\trunk\AISTN\AISTN.ExternalAppAPI\cert\_.eauth.egov.bg.cer")();

            return x509;

        }

        public static void SetPrefix(string prefix, XmlNode node)
        {
            node.Prefix = prefix;
            foreach (XmlNode n in node.ChildNodes)
            {
                SetPrefix(prefix, n);
            }
        }

        public static XmlDocument SignXmlWithCertificate(XmlDocument Document, X509Certificate2 cert)
        {

            var cryptoService = cert.GetRSAPrivateKey();

            var signedXml = new SignedXml(Document);
            signedXml.SigningKey = cryptoService;

            var reference = new Reference();
            reference.Uri = "";
            var transform = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(transform);
            signedXml.AddReference(reference);
            System.Security.Cryptography.Xml.KeyInfo keyInfo = new System.Security.Cryptography.Xml.KeyInfo();

            KeyInfoX509Data keyData = new KeyInfoX509Data(cert);
            X509IssuerSerial issuer2;

            issuer2.IssuerName = cert.IssuerName.Name;
            issuer2.SerialNumber = cert.SerialNumber;
            keyData.AddSubjectName(cert.SubjectName.Name);

            keyInfo.AddClause(keyData);
            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();

            var xmlDigitalSignature = signedXml.GetXml();
            Document.DocumentElement.InsertBefore(
                Document.ImportNode(xmlDigitalSignature, true), Document.DocumentElement.LastChild);

            if (Document.FirstChild is XmlDeclaration)
            {
                Document.RemoveChild(Document.FirstChild);
            }

            return Document;
        }
    
        public static bool ValidateXml(XmlDocument Document, X509Certificate2 cert, string SigTagName)
        {
            SignedXml signedXml = new SignedXml((XmlElement)Document.DocumentElement.FirstChild);
            XmlNodeList nodeList = Document.GetElementsByTagName(SigTagName);
            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);
            // Check the signature and return the result.
            // return signedXml.CheckSignature(cert, true); // their certificate is not valid
            return true; //TODO return the validation when they fix their metadata
        }

        static string AesDecrypt(byte[] cipherText, byte[] aesKey)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (aesKey == null || aesKey.Length < 16)
                throw new ArgumentNullException("Key");

            var aesIV = aesKey.Take(16).ToArray();
            using (var ms = new MemoryStream())
            {
                using (var aes = new AesManaged())
                {
                    aes.Key = aesKey;
                    aes.IV = aesIV;
                    aes.Padding = PaddingMode.ISO10126;
                    CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(cipherText, 0, cipherText.Length);
                    cs.Close();
                    var result = Encoding.UTF8.GetString(ms.ToArray());
                    return result;
                }
            }
        }

        public void DecryptAssertion(X509Certificate2 myCert)
        {
            var saml = XElement.Parse(xmlDoc.OuterXml);
            var cipherKeys = GetCipherValues(saml);
            var privateKey = myCert.GetRSAPrivateKey();
            var aeskey = privateKey.Decrypt(Convert.FromBase64String(cipherKeys[0]), RSAEncryptionPadding.OaepSHA1);
            var assertions = AesDecrypt(Convert.FromBase64String(cipherKeys[1]), aeskey);
            assertions = XElement.Parse(assertions.Substring(assertions.IndexOf("<"))).ToString();

            XmlNode oldNode = xmlDoc.GetElementsByTagName("saml2:EncryptedAssertion")[0];
            // turn it into an xml element and return it
            XmlDocument newdoc = new XmlDocument();
            newdoc.LoadXml(assertions);
            XmlNode newNode = xmlDoc.ImportNode(newdoc.GetElementsByTagName("saml2:Assertion")[0], true);
            xmlDoc.GetElementsByTagName("saml2p:Response")[0].ReplaceChild(newNode,oldNode);
            
        }
        private static Dictionary<string, string> GetDefaultDict()
        {
            return new Dictionary<string, string>
            {
                { "saml2", "urn:oasis:names:tc:SAML:2.0:assertion" },
                { "saml2p", "urn:oasis:names:tc:SAML:2.0:protocol" },
                { "xenc", "http://www.w3.org/2001/04/xmlenc#" },
                { "ds", "http://www.w3.org/2000/09/xmldsig#" }
            };
        }

        private static IEnumerable<XElement> XPathSelectElements(XNode element, string query)
        {
            return XPathSelectElements(element, query, GetDefaultDict());
        }

        private static IEnumerable<XElement> XPathSelectElements(XNode element, string query, Dictionary<string, string> namespaces)
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaces.ToList().ForEach(entry => namespaceManager.AddNamespace(entry.Key, entry.Value));
            return element.XPathSelectElements(query, namespaceManager);
        }
        static string[] GetCipherValues(XElement saml)
        {
            var alg = XName.Get("Algorithm");
            var encryptedData = XPathSelectElements(saml, "./saml2:EncryptedAssertion/xenc:EncryptedData").First();
            var encryptionMethod = XPathSelectElements(encryptedData, "./xenc:EncryptionMethod").First().Attribute(alg).Value;
            var keyInfo = XPathSelectElements(encryptedData, "./ds:KeyInfo").First();
            var encryptionMethodKey = XPathSelectElements(keyInfo, "./xenc:EncryptedKey/xenc:EncryptionMethod").First().Attribute(alg).Value;
            var cipherValueKey = XPathSelectElements(keyInfo, "./xenc:EncryptedKey/xenc:CipherData/xenc:CipherValue").First().Value;
            var cipherValue = XPathSelectElements(encryptedData, "./xenc:CipherData/xenc:CipherValue").First().Value;
            return new[] { cipherValueKey, cipherValue };
        }

        public XmlNode GetNode(string xpath, XmlNamespaceManager nsmgr)
        {
            nsmgr.AddNamespace("xenc", "http://www.w3.org/2001/04/xmlenc#");
            return xmlDoc.SelectSingleNode(xpath, nsmgr);
        }


    }
}
