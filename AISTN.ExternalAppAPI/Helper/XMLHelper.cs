using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace AISTN.ExternalAppAPI.Helper
{
    public class XMLHelper
    {
        /// <summary>
        /// Method serializing object to xml string
        /// </summary>
        /// <param name="obj">The object to be serialized</param>
        /// <returns>XML formated string</returns>
        public string SerializeToXML(object obj, XmlSerializerNamespaces ns = null)
        {
            if (obj == null)
            {
                return null;
            }
            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Encoding = new UTF8Encoding(true);
            settings.OmitXmlDeclaration = false;
            settings.DoNotEscapeUriAttributes = true;

            var output = new Utf8StringWriter();
            var writer = XmlWriter.Create(output, settings);
            new XmlSerializer(obj.GetType()).Serialize(writer, obj, ns);
            return output.ToString();


            //    using (var ms = new MemoryStream())
            //using (var writer = XmlWriter.Create(ms, settings))
            //{
            //    //var serializer = new XmlSerializer(obj.GetType(), new Type[] { typeof(Field) }); Don't use this type of constructor. It registers uncollectable assemlies and memmory leaks 

            //    //var streamWriter = new StreamWriter(ms, );

            //    var serializer = new XmlSerializer(obj.GetType());
            //    serializer.Serialize(writer, obj, ns);
            //    return Encoding.UTF8.GetString(ms.ToArray());
            //}
        }

        /// <summary>
        /// Method deserializing xml string to object
        /// </summary>
        /// <typeparam name="T">Type of the recipient object</typeparam>
        /// <param name="objectData">xml string</param>
        /// <returns>object of type T</returns>        
        public T DeserializeXML<T>(string objectData)
        {
            object result;

            var serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return (T)result;
        }


        /// <summary>
        /// Method deserializing xml string to object
        /// </summary>
        /// <param name="objectData">xml string</param>
        /// <param name="toType">the output type</param>
        /// <returns></returns>
        public object DeserializeXML(string objectData, Type toType)
        {
            object result;

            var serializer = new XmlSerializer(toType);

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }



        /// <summary>
        /// removes the prolog (<?xml version="1.0" encoding="UTF-8"?>) from a string in order to be insertable in xml filed in sql db
        /// </summary>
        /// <returns></returns>
        public string RemoveXMLProlog(string xml)
        {
            if (xml.Contains("?>"))
                return xml.Substring(xml.IndexOf("?>") + 2);
            else
                return xml;

        }


        /// <summary>
        /// Clone any object using XML
        /// </summary>
        /// <param name="input">object to be cloned</param>
        /// <returns>Cloned object</returns>
        public object CloneObject(object input)
        {
            if (input == null) return null;

            XMLHelper xh = new XMLHelper();

            string xml = xh.RemoveXMLProlog(xh.SerializeToXML(input));

            var pe = DeserializeXML(xml, input.GetType());

            return pe;
        }

        public string XmlSerializeObjectToString(object document)
        {
            var xmlSerializer = new XmlSerializer(document.GetType());
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, document);
                return stringWriter.ToString();
            }
        }
    }
}
