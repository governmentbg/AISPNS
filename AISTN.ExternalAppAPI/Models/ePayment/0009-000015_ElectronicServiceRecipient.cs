﻿//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

using System.Text.Json.Serialization;





namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000015
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000015";
    }




    [XmlType(TypeName = "ElectronicServiceRecipient", Namespace = Declarations.SchemaVersion), Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public partial class ElectronicServiceRecipient
    {

        
		
        [XmlElement(Type = typeof(R_0009_000008.PersonBasicData), ElementName = "Person", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000008.PersonBasicData Person { get; set; }

        
		
        [XmlElement(Type = typeof(R_0009_000013.EntityBasicData), ElementName = "Entity", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000013.EntityBasicData Entity { get; set; }

        
		
        [XmlElement(Type = typeof(R_0009_000011.ForeignCitizenBasicData), ElementName = "ForeignPerson", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000011.ForeignCitizenBasicData ForeignPerson { get; set; }

        
		
        [XmlElement(Type = typeof(R_0009_000014.ForeignEntityBasicData), ElementName = "ForeignEntity", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000014.ForeignEntityBasicData ForeignEntity { get; set; }

        public ElectronicServiceRecipient()
        {
        }
    }
}
