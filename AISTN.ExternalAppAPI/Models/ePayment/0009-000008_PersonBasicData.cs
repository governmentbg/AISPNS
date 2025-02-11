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


namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000008
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000008";
    }




    [XmlType(TypeName = "PersonBasicData", Namespace = Declarations.SchemaVersion), Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public partial class PersonBasicData
    {

        
		
        [XmlElement(Type = typeof(R_0009_000005.PersonNames), ElementName = "Names", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000005.PersonNames Names { get; set; }

        
		
        [XmlElement(Type = typeof(R_0009_000006.PersonIdentifier), ElementName = "Identifier", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_0009_000006.PersonIdentifier Identifier { get; set; }

        public PersonBasicData()
        {
        }
    }
}
