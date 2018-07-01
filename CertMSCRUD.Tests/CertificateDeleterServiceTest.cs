using System;
using System.Collections.Generic;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CertificateDeleterServiceTest
	{
		private readonly CertificateService service = new CertificateService(new InMemoryCertificateDao());
		public CertificateDeleterServiceTest() => service.Save("123456", "test", "me", DateTime.Today, DateTime.Today.AddDays(1), new Dictionary<string, string>());

		[Theory]
		[InlineData(0, true, "123456")]
		[InlineData(1, false, "72318798379812")]
		[InlineData(1, false, null)]
		public void DeleteValidCertificate(int expectedCount, bool isDeleted, string serialNumber)
		{
			AreEqual(isDeleted, service.Delete(serialNumber));
			AreEqual(expectedCount, service.CertificateCount);
		}
	}
}