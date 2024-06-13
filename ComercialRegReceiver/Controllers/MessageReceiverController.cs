using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime;
using System.Xml.Linq;

namespace ComercialRegReceiver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageReceiverController : ControllerBase
    {


        private readonly ILogger<MessageReceiverController> _logger;
        private readonly FolderSettings _folderSettings;

        public MessageReceiverController(ILogger<MessageReceiverController> logger, IOptions<FolderSettings> folderSettings)
        {
            _logger = logger;
            _folderSettings = folderSettings.Value ;
        }


        [HttpPost(Name = "ReceiveMessage")]
        public IActionResult ReceiveMessage([FromBody] string messageB64)
        {
           

            try
            {
                // Decoding from Base64
                string fileContent = Base64Converter.Base64ToString(messageB64);

                if (fileContent.StartsWith("\uFEFF"))
                {
                    fileContent = fileContent.Substring(1);
                }

                // Defining paths to XSD files
                string[] xsdFilePaths =
                {
                    "xsd/DeedV2.xsd",
                    "xsd/Envelopev2.xsd",
                    "xsd/FieldsSchema.xsd",
                    "xsd/Notifications.xsd",
                    "xsd/xmldsig-core-schema.xsd"
                };

                if (XmlValidation.ValidateXml(fileContent, xsdFilePaths))
                {

                    DateTime now = DateTime.Now;
                    string filename = _folderSettings.TargetFolderName + "\\" + now.ToString("yyyyMMdd_HHmmssfff")+".xml";

                    System.IO.File.WriteAllText(filename, fileContent);



                    return Ok("Message received and validated successfully.");
                }
                else
                {
                    return BadRequest("Message validation against the provided XSDs failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
