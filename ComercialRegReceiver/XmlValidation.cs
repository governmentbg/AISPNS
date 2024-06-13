using System.Xml.Schema;
using System.Xml;

namespace ComercialRegReceiver
{
    public class XmlValidation
    {
        public static bool ValidateXml(string xmlFileContent, string[] xsdFilePaths)
        {
            bool isValid = true; // Assume XML is valid until proven otherwise

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            foreach (var xsdFilePath in xsdFilePaths)
            {
                schemaSet.Add(null, XmlReader.Create(new StringReader(File.ReadAllText(xsdFilePath))));
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.MaxCharactersFromEntities = 100000000;
            settings.ValidationEventHandler += (sender, e) =>
            {
                // Any validation error or warning is considered a failure for this example
                // This can be adjusted based on requirements
                if (e.Severity == XmlSeverityType.Error || e.Severity == XmlSeverityType.Warning)
                {
                    Console.WriteLine($"Validation error: {e.Message}");
                    isValid = false; // Set the flag to false on any error
                }
            };

            // Validate the XML file
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlFileContent), settings))
            {
                try
                {
                    while (reader.Read()) ; // Simply loop through the XML file to trigger validation
                }
                catch (XmlException xmlEx)
                {
                    Console.WriteLine($"XML Exception: {xmlEx.Message}");
                    isValid = false; // Set the flag to false on XML parsing errors
                }
            }

            return isValid; // Return the validation result
        }
    }

}
