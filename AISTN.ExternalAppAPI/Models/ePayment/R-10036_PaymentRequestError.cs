﻿//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;






namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_10036
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10036";
	}




	[XmlType(TypeName="PaymentRequestError",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PaymentRequestError
	{

		
		
		[XmlElement(ElementName="TermName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string TermName { get; set; }

		
		
		[XmlElement(Type=typeof(R_0009_000023.TermURI),ElementName="TermURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000023.TermURI TermURI { get; set; }

		public PaymentRequestError()
		{
		}
	}
}
