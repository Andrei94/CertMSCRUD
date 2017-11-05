using System;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CertificateTest
	{
		[Fact]
		public void CreateCertificate()
		{
			var cert = CertificateCreatorDummy.CreateDummyCertificate();
			AreEqual("1234567890", cert.SerialNumber);
			AreEqual("test", cert.Subject);
			AreEqual("me", cert.Issuer);
			AreEqual(DateTime.Today, cert.ValidFrom);
			AreEqual(DateTime.Today.AddDays(1), cert.ValidUntil);
			AreEqual("test", cert.ExtraProperties["Category"]);
			AreEqual("qwerty", cert.ExtraProperties["Identifier"]);
			AreEqual("1234", cert.ExtraProperties["Token"]);
		}
	}
}