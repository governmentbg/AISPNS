using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AISTN.Test.Services
{
    public class ApiTestService
    {
        public ApiTestService() { }

        public async Task TestCourtLegacyApi()
        {
            //const string API_ADDRESS = "https://test-aistn.mjs.bg/ws2"; //Test deployment
            const string API_ADDRESS = "https://localhost:7036/ws2"; //Dev Legacy

            const string xmlDir = "XMLs";

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, xmlDir);

            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] Files = d.GetFiles("*.xml");

            foreach (FileInfo file in Files)
            {
                string filePath = Path.Combine(path, file.Name);

                string xmlString = await File.ReadAllTextAsync(filePath);

                //correct encoding
                xmlString = xmlString.Replace(@"<?xml version=""1.0"" encoding=""Windows-1251""?>", @"<?xml version=""1.0"" encoding=""utf-8""?>");

                var responseCode = await callApi(API_ADDRESS, xmlString);

                if (responseCode == HttpStatusCode.OK)
                {
                    File.Delete(filePath);
                }
            }

            int a = 5;
        }

        private async Task<HttpStatusCode> callApi(string address, string XML)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(address);

            var content = new StringContent(XML, Encoding.UTF8, "application/xml");

            var response = await client.PostAsync(address, content);

            return response.StatusCode;
        }

        public async Task TestGuidsInFile()
        {
            const string xmlDir = "AllXMLs";

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, xmlDir);

            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] Files = d.GetFiles("*.xml");

            int i = 0;
            foreach (FileInfo file in Files)
            {
                i++;
                string filePath = Path.Combine(path, file.Name);

                string xmlString = await File.ReadAllTextAsync(filePath);
                string pattern = @">[0-9A-F]{32}?<";

                MatchCollection matches = Regex.Matches(xmlString, pattern);

                foreach (var match in matches)
                {
                    string guid = match.ToString().Replace("<", "").Replace(">", "");
                    if (Regex.Matches(xmlString, guid).Count > 1
                        && guid != "73E81089DBF142D3B44BB91E339DBB70" && guid != "E09C7B643845471CA22F29E2C5F36992" && guid != "000C4563990626C00000000000000212" && guid != "000C4563990626C0034438CAA5C8A212" && guid != "3F99F6C36CC526C00000000000000212" && guid != "DBC7711FCE8841F286F91CDD23081808" && guid != "0004A5A388C726C00000000000000208" && guid != "0004A5A388C726C0034438CB42B8BA08" && guid != "1730DC1A186241A5B25681BE59DA9121" && guid != "0002F6639A6826C000000000000000D2" && guid != "0002F6639A6826C0034439A5C44E30D2" && guid != "003904C3990826C00000000000000212" && guid != "3F9B26C36CC726C00000000000000212" && guid != "003904C3990826C0034438CAA5C8A212" && guid != "000F5583990826C00000000000000212" && guid != "000F5583990826C0034438CAA5C8A212"
                        && guid != "3F9B16C36CC726C00000000000000212" && guid != "BFE03F5C1BE74AC3841C794D1C53776A" && guid != "BFE03F5C1BE74AC3841C794D1C53776A" && guid != "55C9A37E689B472D99BA4F0AA355E004" && guid != "97AD9E79F1804F9D88EA4E75D518D248" && guid != "CFD284B52D9D4824B3DB55419A99DB64" && guid != "5B07013D861B40948F05829F5582DB6A" && guid != "10321AE3ECA04DC89B313ACAE18AC64D" && guid != "AFE20C3315424D6BB4513C699C6B8FEF" && guid != "769E4AD2A4774EFCB8B83FE56DE423A5" && guid != "000C4563990626C00000000000000212" && guid != "000C4563990626C00000000000000212" && guid != "000C4563990626C00000000000000212" && guid != "000C4563990626C00000000000000212"
                        && guid != "5867AE32C84B449E866508A74EF4A7A4"
                        && guid != "6D465EC308344547A0C6DB85708BB08D"
                        && guid != "755641C8973347DE87A3E9AC4AAB2AF6"
                        && guid != "89C2EAFA12944E6785517D23C95C1E33"
                        && guid != "53BACD18B8B04B3BADF6F235E5B4F703"
                        && guid != "F427C74FFA944FED8F77D965C779F84D"
                        && guid != "B0DF8E3E1D514EE58323C2D10A896C0A"
                        && guid != "0027E603991B26C0034438ED18E2BA12")
                    {
                        int b = 5;
                    }
                }
            }

            int a = 5;
        }
    }
}
