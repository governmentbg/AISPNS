﻿//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;






namespace AISTN.ExternalAppAPI.ExternalAppAPI.ePayment.R_10024
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-10024";
	}




	[XmlType(TypeName="PaymentPeriod",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PaymentPeriod
	{

		
		
		[XmlElement(ElementName="PaymentPeriodFromDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? PaymentPeriodFromDate { get; set; }

		
		
		[XmlElement(ElementName="PaymentPeriodToDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? PaymentPeriodToDate { get; set; }

		public PaymentPeriod()
		{
		}
	}
}
