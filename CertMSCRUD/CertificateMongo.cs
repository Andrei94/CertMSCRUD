using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CertMSCRUD
{
	[BsonIgnoreExtraElements]
	public class CertificateMongo : Certificate
	{
		public CertificateMongo(Certificate certificate)
		{
			SerialNumber = certificate.SerialNumber;
			Subject = certificate.Subject;
			Issuer = certificate.Issuer;
			ValidFrom = certificate.ValidFrom;
			ValidUntil = certificate.ValidUntil;
			ExtraProperties = certificate.ExtraProperties;
		}

		public override string SerialNumber { get; set; }
		public override string Subject { get; set; }
		public override string Issuer { get; set; }
		public override DateTime? ValidFrom { get; set; }
		public override DateTime? ValidUntil { get; set; }
		public override IDictionary<string, string> ExtraProperties { get; set; }
	}
}
