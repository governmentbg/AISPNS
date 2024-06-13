using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AISTN.ExternalAppAPI.Models.ePayment.PaymentReceipt
{
    public class R_10052_PaymentRequestUnacceptedReceipt
    {
        public struct Declarations
        {
            public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10052";
        }


        [Serializable]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public partial class PaymentRequestErrorCollection : System.Collections.Generic.List<ExternalAppAPI.ePayment.R_10036.PaymentRequestError>
        {
        }



        [XmlRoot(ElementName = "PaymentRequestUnacceptedReceipt", Namespace = Declarations.SchemaVersion, IsNullable = false), Serializable]
        [XmlType(TypeName = "PaymentRequestUnacceptedReceipt", Namespace = Declarations.SchemaVersion)]
        public partial class PaymentRequestUnacceptedReceipt
        {

            [XmlElement(Type = typeof(ExternalAppAPI.ePayment.R_0009_000003.DocumentTypeURI), ElementName = "DocumentTypeURI", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public ExternalAppAPI.ePayment.R_0009_000003.DocumentTypeURI DocumentTypeURI { get; set; }

            [XmlElement(ElementName = "DocumentTypeName", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public string DocumentTypeName { get; set; }

            [XmlElement(Type = typeof(PaymentRequestErrors), ElementName = "PaymentRequestErrors", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public PaymentRequestErrors PaymentRequestErrors { get; set; }

            [XmlElement(ElementName = "PaymentRequestValidationTime", Form = XmlSchemaForm.Qualified, DataType = "dateTime", Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public DateTime? PaymentRequestValidationTime { get; set; }

            [XmlElement(Type = typeof(ExternalAppAPI.ePayment.R_10046.PaymentRequest), ElementName = "PaymentRequest", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public ExternalAppAPI.ePayment.R_10046.PaymentRequest PaymentRequest { get; set; }

            [XmlElement(Type = typeof(ExternalAppAPI.ePayment.R_10049.PaymentRequestMultiple), ElementName = "PaymentRequestMultiple", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public ExternalAppAPI.ePayment.R_10049.PaymentRequestMultiple PaymentRequestMultiple { get; set; }

            public PaymentRequestUnacceptedReceipt()
            {
            }
        }


        [XmlType(TypeName = "PaymentRequestErrors", Namespace = Declarations.SchemaVersion), Serializable]
        public partial class PaymentRequestErrors
        {


            [XmlElement(Type = typeof(ExternalAppAPI.ePayment.R_10036.PaymentRequestError), ElementName = "PaymentRequestError", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
            [EditorBrowsable(EditorBrowsableState.Advanced)]
            public PaymentRequestErrorCollection PaymentRequestErrorCollection { get; set; }

            public PaymentRequestErrors()
            {
            }
        }
    }
}
