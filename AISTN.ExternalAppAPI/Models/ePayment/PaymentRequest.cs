using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AISTN.ExternalAppAPI.Models.ePayment
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/R-0015", IsNullable = false)]
    public partial class PaymentRequest
    {

        private string currencyField;

        private byte paymentAmountField;

        private PaymentRequestElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;

        private PaymentRequestElectronicServiceProvideBankAccount electronicServiceProvideBankAccountField;

        private PaymentRequestElectronicServiceApplicant electronicServiceApplicantField;

        private string paymentReasonField;

        private PaymentRequestPaymentReference paymentReferenceField;

        private PaymentRequestPaymentPeriod paymentPeriodField;

        private DateTime paymentRequestExpirationDateField;

        private string additionalInformationInPaymentRequestField;

        private string electronicAdministrativeServiceUriSUNAUField;

        private string electronicAdministrativeServiceSupplierUriRAField;

        /// <remarks/>
        public string Currency
        {
            get
            {
                return currencyField;
            }
            set
            {
                currencyField = value;
            }
        }

        /// <remarks/>
        public byte PaymentAmount
        {
            get
            {
                return paymentAmountField;
            }
            set
            {
                paymentAmountField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
        {
            get
            {
                return electronicServiceProviderBasicDataField;
            }
            set
            {
                electronicServiceProviderBasicDataField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestElectronicServiceProvideBankAccount ElectronicServiceProvideBankAccount
        {
            get
            {
                return electronicServiceProvideBankAccountField;
            }
            set
            {
                electronicServiceProvideBankAccountField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestElectronicServiceApplicant ElectronicServiceApplicant
        {
            get
            {
                return electronicServiceApplicantField;
            }
            set
            {
                electronicServiceApplicantField = value;
            }
        }

        /// <remarks/>
        public string PaymentReason
        {
            get
            {
                return paymentReasonField;
            }
            set
            {
                paymentReasonField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestPaymentReference PaymentReference
        {
            get
            {
                return paymentReferenceField;
            }
            set
            {
                paymentReferenceField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestPaymentPeriod PaymentPeriod
        {
            get
            {
                return paymentPeriodField;
            }
            set
            {
                paymentPeriodField = value;
            }
        }

        /// <remarks/>
        public DateTime PaymentRequestExpirationDate
        {
            get
            {
                return paymentRequestExpirationDateField;
            }
            set
            {
                paymentRequestExpirationDateField = value;
            }
        }

        /// <remarks/>
        public string AdditionalInformationInPaymentRequest
        {
            get
            {
                return additionalInformationInPaymentRequestField;
            }
            set
            {
                additionalInformationInPaymentRequestField = value;
            }
        }

        /// <remarks/>
        public string ElectronicAdministrativeServiceUriSUNAU
        {
            get
            {
                return electronicAdministrativeServiceUriSUNAUField;
            }
            set
            {
                electronicAdministrativeServiceUriSUNAUField = value;
            }
        }

        /// <remarks/>
        public string ElectronicAdministrativeServiceSupplierUriRA
        {
            get
            {
                return electronicAdministrativeServiceSupplierUriRAField;
            }
            set
            {
                electronicAdministrativeServiceSupplierUriRAField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestElectronicServiceProviderBasicData
    {

        private string sUNAUServiceURIField;

        private DocumentTypeURI documentTypeURIField;

        private string documentTypeNameField;

        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;

        private ElectronicServiceApplicant electronicServiceApplicantField;

        private ElectronicServiceApplicantContactData electronicServiceApplicantContactDataField;

        private string applicationTypeField;

        private string sUNAUServiceNameField;

        private DocumentURI documentURIField;

        private bool sendApplicationWithReceiptAcknowledgedMessageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public string SUNAUServiceURI
        {
            get
            {
                return sUNAUServiceURIField;
            }
            set
            {
                sUNAUServiceURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public DocumentTypeURI DocumentTypeURI
        {
            get
            {
                return documentTypeURIField;
            }
            set
            {
                documentTypeURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public string DocumentTypeName
        {
            get
            {
                return documentTypeNameField;
            }
            set
            {
                documentTypeNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
        {
            get
            {
                return electronicServiceProviderBasicDataField;
            }
            set
            {
                electronicServiceProviderBasicDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public ElectronicServiceApplicant ElectronicServiceApplicant
        {
            get
            {
                return electronicServiceApplicantField;
            }
            set
            {
                electronicServiceApplicantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public ElectronicServiceApplicantContactData ElectronicServiceApplicantContactData
        {
            get
            {
                return electronicServiceApplicantContactDataField;
            }
            set
            {
                electronicServiceApplicantContactDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public string ApplicationType
        {
            get
            {
                return applicationTypeField;
            }
            set
            {
                applicationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public string SUNAUServiceName
        {
            get
            {
                return sUNAUServiceNameField;
            }
            set
            {
                sUNAUServiceNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public DocumentURI DocumentURI
        {
            get
            {
                return documentURIField;
            }
            set
            {
                documentURIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
        public bool SendApplicationWithReceiptAcknowledgedMessage
        {
            get
            {
                return sendApplicationWithReceiptAcknowledgedMessageField;
            }
            set
            {
                sendApplicationWithReceiptAcknowledgedMessageField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152", IsNullable = false)]
    public partial class DocumentTypeURI
    {

        private byte registerIndexField;

        private byte batchNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000022")]
        public byte RegisterIndex
        {
            get
            {
                return registerIndexField;
            }
            set
            {
                registerIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000022")]
        public byte BatchNumber
        {
            get
            {
                return batchNumberField;
            }
            set
            {
                batchNumberField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152", IsNullable = false)]
    public partial class ElectronicServiceProviderBasicData
    {

        private EntityBasicData entityBasicDataField;

        private string electronicServiceProviderTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000002")]
        public EntityBasicData EntityBasicData
        {
            get
            {
                return entityBasicDataField;
            }
            set
            {
                entityBasicDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000002")]
        public string ElectronicServiceProviderType
        {
            get
            {
                return electronicServiceProviderTypeField;
            }
            set
            {
                electronicServiceProviderTypeField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000002")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000002", IsNullable = false)]
    public partial class EntityBasicData
    {

        private string nameField;

        private byte identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public byte Identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152", IsNullable = false)]
    public partial class ElectronicServiceApplicant
    {

        private RecipientGroup[] recipientGroupField;

        private string emailAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("RecipientGroup", Namespace = "http://ereg.egov.bg/segment/0009-000016")]
        public RecipientGroup[] RecipientGroup
        {
            get
            {
                return recipientGroupField;
            }
            set
            {
                recipientGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000016")]
        public string EmailAddress
        {
            get
            {
                return emailAddressField;
            }
            set
            {
                emailAddressField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000016")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000016", IsNullable = false)]
    public partial class RecipientGroup
    {

        private RecipientGroupAuthor[] authorField;

        private string authorQualityField;

        private RecipientGroupRecipient[] recipientField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Author")]
        public RecipientGroupAuthor[] Author
        {
            get
            {
                return authorField;
            }
            set
            {
                authorField = value;
            }
        }

        /// <remarks/>
        public string AuthorQuality
        {
            get
            {
                return authorQualityField;
            }
            set
            {
                authorQualityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Recipient")]
        public RecipientGroupRecipient[] Recipient
        {
            get
            {
                return recipientField;
            }
            set
            {
                recipientField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000016")]
    public partial class RecipientGroupAuthor
    {

        private ForeignCitizen foreignCitizenField;

        private PaymentPerson personField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000012")]
        public ForeignCitizen ForeignCitizen
        {
            get
            {
                return foreignCitizenField;
            }
            set
            {
                foreignCitizenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000012")]
        public PaymentPerson Person
        {
            get
            {
                return personField;
            }
            set
            {
                personField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000012")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000012", IsNullable = false)]
    public partial class ForeignCitizen
    {

        private object namesField;

        private DateTime birthDateField;

        private object placeOfBirthField;

        private object identityDocumentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000011")]
        public object Names
        {
            get
            {
                return namesField;
            }
            set
            {
                namesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000011", DataType = "date")]
        public DateTime BirthDate
        {
            get
            {
                return birthDateField;
            }
            set
            {
                birthDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000011")]
        public object PlaceOfBirth
        {
            get
            {
                return placeOfBirthField;
            }
            set
            {
                placeOfBirthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000011")]
        public object IdentityDocument
        {
            get
            {
                return identityDocumentField;
            }
            set
            {
                identityDocumentField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000012")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000012", IsNullable = false)]
    public partial class PaymentPerson
    {

        private object namesField;

        private Identifier identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000008")]
        public object Names
        {
            get
            {
                return namesField;
            }
            set
            {
                namesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000008")]
        public Identifier Identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000008")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000008", IsNullable = false)]
    public partial class Identifier
    {

        private uint eGNField;

        private bool eGNFieldSpecified;

        private uint lNChField;

        private bool lNChFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000006")]
        public uint EGN
        {
            get
            {
                return eGNField;
            }
            set
            {
                eGNField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool EGNSpecified
        {
            get
            {
                return eGNFieldSpecified;
            }
            set
            {
                eGNFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000006")]
        public uint LNCh
        {
            get
            {
                return lNChField;
            }
            set
            {
                lNChField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool LNChSpecified
        {
            get
            {
                return lNChFieldSpecified;
            }
            set
            {
                lNChFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000016")]
    public partial class RecipientGroupRecipient
    {

        private Entity entityField;

        private Person1 personField;

        private object foreignPersonField;

        private ForeignEntity foreignEntityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000015")]
        public Entity Entity
        {
            get
            {
                return entityField;
            }
            set
            {
                entityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000015")]
        public Person1 Person
        {
            get
            {
                return personField;
            }
            set
            {
                personField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000015")]
        public object ForeignPerson
        {
            get
            {
                return foreignPersonField;
            }
            set
            {
                foreignPersonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000015")]
        public ForeignEntity ForeignEntity
        {
            get
            {
                return foreignEntityField;
            }
            set
            {
                foreignEntityField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000015")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000015", IsNullable = false)]
    public partial class Entity
    {

        private string nameField;

        private byte identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public byte Identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000015")]
    [System.Xml.Serialization.XmlRoot("Person", Namespace = "http://ereg.egov.bg/segment/0009-000015", IsNullable = false)]
    public partial class Person1
    {

        private object namesField;

        private Identifier identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000008")]
        public object Names
        {
            get
            {
                return namesField;
            }
            set
            {
                namesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000008")]
        public Identifier Identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000015")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000015", IsNullable = false)]
    public partial class ForeignEntity
    {

        private string foreignEntityNameField;

        private string countryISO3166TwoLetterCodeField;

        private string countryNameCyrillicField;

        private string foreignEntityRegisterField;

        private string foreignEntityIdentifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
        public string ForeignEntityName
        {
            get
            {
                return foreignEntityNameField;
            }
            set
            {
                foreignEntityNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
        public string CountryISO3166TwoLetterCode
        {
            get
            {
                return countryISO3166TwoLetterCodeField;
            }
            set
            {
                countryISO3166TwoLetterCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
        public string CountryNameCyrillic
        {
            get
            {
                return countryNameCyrillicField;
            }
            set
            {
                countryNameCyrillicField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
        public string ForeignEntityRegister
        {
            get
            {
                return foreignEntityRegisterField;
            }
            set
            {
                foreignEntityRegisterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
        public string ForeignEntityIdentifier
        {
            get
            {
                return foreignEntityIdentifierField;
            }
            set
            {
                foreignEntityIdentifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152", IsNullable = false)]
    public partial class ElectronicServiceApplicantContactData
    {

        private string districtCodeField;

        private string districtNameField;

        private string municipalityCodeField;

        private string municipalityNameField;

        private byte settlementCodeField;

        private string settlementNameField;

        private string postCodeField;

        private string addressDescriptionField;

        private string postOfficeBoxField;

        private string[] phoneNumbersField;

        private string[] faxNumbersField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string DistrictCode
        {
            get
            {
                return districtCodeField;
            }
            set
            {
                districtCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string DistrictName
        {
            get
            {
                return districtNameField;
            }
            set
            {
                districtNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string MunicipalityCode
        {
            get
            {
                return municipalityCodeField;
            }
            set
            {
                municipalityCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string MunicipalityName
        {
            get
            {
                return municipalityNameField;
            }
            set
            {
                municipalityNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public byte SettlementCode
        {
            get
            {
                return settlementCodeField;
            }
            set
            {
                settlementCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string SettlementName
        {
            get
            {
                return settlementNameField;
            }
            set
            {
                settlementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string PostCode
        {
            get
            {
                return postCodeField;
            }
            set
            {
                postCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string AddressDescription
        {
            get
            {
                return addressDescriptionField;
            }
            set
            {
                addressDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        public string PostOfficeBox
        {
            get
            {
                return postOfficeBoxField;
            }
            set
            {
                postOfficeBoxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArray(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        [System.Xml.Serialization.XmlArrayItem("PhoneNumber", IsNullable = false)]
        public string[] PhoneNumbers
        {
            get
            {
                return phoneNumbersField;
            }
            set
            {
                phoneNumbersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArray(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
        [System.Xml.Serialization.XmlArrayItem("ElectronicServiceApplicantFaxNumber", IsNullable = false)]
        public string[] FaxNumbers
        {
            get
            {
                return faxNumbersField;
            }
            set
            {
                faxNumbersField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152", IsNullable = false)]
    public partial class DocumentURI
    {

        private byte registerIndexField;

        private byte sequenceNumberField;

        private DateTime receiptOrSigningDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000001")]
        public byte RegisterIndex
        {
            get
            {
                return registerIndexField;
            }
            set
            {
                registerIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000001")]
        public byte SequenceNumber
        {
            get
            {
                return sequenceNumberField;
            }
            set
            {
                sequenceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000001", DataType = "date")]
        public DateTime ReceiptOrSigningDate
        {
            get
            {
                return receiptOrSigningDateField;
            }
            set
            {
                receiptOrSigningDateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestElectronicServiceProvideBankAccount
    {

        private string bICField;

        private string iBANField;

        private PaymentRequestElectronicServiceProvideBankAccountEntityName entityNameField;

        /// <remarks/>
        public string BIC
        {
            get
            {
                return bICField;
            }
            set
            {
                bICField = value;
            }
        }

        /// <remarks/>
        public string IBAN
        {
            get
            {
                return iBANField;
            }
            set
            {
                iBANField = value;
            }
        }

        /// <remarks/>
        public PaymentRequestElectronicServiceProvideBankAccountEntityName EntityName
        {
            get
            {
                return entityNameField;
            }
            set
            {
                entityNameField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestElectronicServiceProvideBankAccountEntityName
    {

        private string nameField;

        private byte identifierField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
        public byte Identifier
        {
            get
            {
                return identifierField;
            }
            set
            {
                identifierField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestElectronicServiceApplicant
    {

        private RecipientGroup[] recipientGroupField;

        private string emailAddressField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("RecipientGroup", Namespace = "http://ereg.egov.bg/segment/0009-000016")]
        public RecipientGroup[] RecipientGroup
        {
            get
            {
                return recipientGroupField;
            }
            set
            {
                recipientGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/0009-000016")]
        public string EmailAddress
        {
            get
            {
                return emailAddressField;
            }
            set
            {
                emailAddressField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestPaymentReference
    {

        private byte paymentReferenceTypeField;

        private string paymentReferenceNumberField;

        private DateTime paymentReferenceDateField;

        /// <remarks/>
        public byte PaymentReferenceType
        {
            get
            {
                return paymentReferenceTypeField;
            }
            set
            {
                paymentReferenceTypeField = value;
            }
        }

        /// <remarks/>
        public string PaymentReferenceNumber
        {
            get
            {
                return paymentReferenceNumberField;
            }
            set
            {
                paymentReferenceNumberField = value;
            }
        }

        /// <remarks/>
        public DateTime PaymentReferenceDate
        {
            get
            {
                return paymentReferenceDateField;
            }
            set
            {
                paymentReferenceDateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class PaymentRequestPaymentPeriod
    {

        private DateTime paymentPeriodFromDateField;

        private DateTime paymentPeriodToDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/R-0008")]
        public DateTime PaymentPeriodFromDate
        {
            get
            {
                return paymentPeriodFromDateField;
            }
            set
            {
                paymentPeriodFromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://ereg.egov.bg/segment/R-0008")]
        public DateTime PaymentPeriodToDate
        {
            get
            {
                return paymentPeriodToDateField;
            }
            set
            {
                paymentPeriodToDateField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000137")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000137", IsNullable = false)]
    public partial class PhoneNumbers
    {

        private string[] phoneNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("PhoneNumber")]
        public string[] PhoneNumber
        {
            get
            {
                return phoneNumberField;
            }
            set
            {
                phoneNumberField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000137")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000137", IsNullable = false)]
    public partial class FaxNumbers
    {

        private string[] electronicServiceApplicantFaxNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ElectronicServiceApplicantFaxNumber")]
        public string[] ElectronicServiceApplicantFaxNumber
        {
            get
            {
                return electronicServiceApplicantFaxNumberField;
            }
            set
            {
                electronicServiceApplicantFaxNumberField = value;
            }
        }
    }


}
