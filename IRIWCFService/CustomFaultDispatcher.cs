using SoapCore.Extensibility;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

//public class CustomFaultDispatcher : DefaultFaultBodyWriter
//{
//    public CustomFaultDispatcher(MessageFault messageFault, string defaultFaultReason)
//        : base(messageFault, defaultFaultReason) { }

//    protected override void WriteDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
//    {
//        // Get the custom fault detail from the MessageFault
//        var detail = MessageFault.GetDetail<MyFancyExceptionDetail>();

//        // Write the custom detail XML
//        writer.WriteStartElement("detail", version.Namespace);
//        writer.WriteStartElement("NoSuchSessionException", "http://mySoapNamespace/");
//        writer.WriteElementString("ResidualText", detail.ResidualText);
//        writer.WriteElementString("LoggedReason", detail.LoggedReason);
//        writer.WriteEndElement(); // NoSuchSessionException
//        writer.WriteEndElement(); // detail
//    }
//}