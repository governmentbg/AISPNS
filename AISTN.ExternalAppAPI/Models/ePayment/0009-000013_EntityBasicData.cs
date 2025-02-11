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

namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_0009_000013
{

    public struct Declarations
    {
        public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000013";
    }

    [Serializable]
    public enum ModificationTypeNomenclature
    {
        [XmlEnum(Name = "Empty")] Empty,
        [XmlEnum(Name = "Create")] Create,
        [XmlEnum(Name = "Update")] Update,
        [XmlEnum(Name = "Delete")] Delete
    }




    [XmlType(TypeName = "EntityBasicData", Namespace = Declarations.SchemaVersion), Serializable]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public partial class EntityBasicData
    {

        
        
        [XmlAttribute(AttributeName = "modificationType")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public R_7066.ModificationTypeNomenclature modificationType { get; set; }

        
        
        [XmlElement(ElementName = "Name", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Name { get; set; }

        
        
        [XmlElement(ElementName = "Identifier", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = Declarations.SchemaVersion)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string Identifier { get; set; }

        public EntityBasicData()
        {
        }

        public static implicit operator EntityBasicData(Models.ePayment.EntityBasicData v)
        {
            throw new NotImplementedException();
        }
    }
}
