using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AISTN.ExternalAppAPI.Models.ePayment.PaymentReceipt
{
    public class R_10055_PaymentRequestAcceptedReceipt
    {
        public struct Declarations
        {
            public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10055";
        }




        [XmlRoot(ElementName = "PaymentRequestAcceptedReceipt", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
        [XmlType(TypeName = "PaymentRequestAcceptedReceipt", Namespace = Declarations.SchemaVersion)]
        public partial class PaymentRequestAcceptedReceipt
        {

            [XmlElement(Type = typeof(ExternalAppAPI.ePayment.R_0009_000003.DocumentTypeURI), ElementName = "DocumentTypeURI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public ExternalAppAPI.ePayment.R_0009_000003.DocumentTypeURI DocumentTypeURI { get; set; }

            [XmlElement(ElementName = "DocumentTypeName", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public string DocumentTypeName { get; set; }

            [XmlElement(ElementName = "PaymentRequestID", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public string PaymentRequestID { get; set; }

            [XmlElement(ElementName = "PaymentRequestRegistrationTime", Form = XmlSchemaForm.Qualified, DataType = "dateTime", Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public DateTime? PaymentRequestRegistrationTime { get; set; }

            public PaymentRequestAcceptedReceipt()
            {
            }
        }
    }
}
