using System;
using System.Collections.Generic;

namespace CertMSCRUD.Tests.Utils
{
	internal static class CertificateCreatorDummy
	{
		internal static Certificate CreateDummyCertificate()
		{
			return new Certificate
			{
				SerialNumber = "1234567890",
				Subject = "test",
				Issuer = "me",
				ValidFrom = DateTime.Today,
				ValidUntil = DateTime.Today.AddDays(1),
				ExtraProperties = new Dictionary<string, string>
				{
					{"Category", "test"},
					{"Identifier", "qwerty"},
					{"Token", "1234"}
				}
			};
		}

		internal static Certificate CreateDummyCertificateWithoutExtraProperties()
		{
			return new Certificate
			{
				SerialNumber = "1234567890",
				Subject = "test",
				Issuer = "me",
				ValidFrom = DateTime.Today,
				ValidUntil = DateTime.Today.AddDays(1)
			};
		}
	}
}
