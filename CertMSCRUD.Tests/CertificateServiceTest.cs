using System;
using System.Collections.Generic;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CertificateServiceTest
	{
		private readonly CertificateService service = new CertificateService(new CertificateDao());

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> ValidCertificateProvider()
		{
			yield return new object[] {"123456", "test", "me", DateTime.Today, DateTime.Today.AddDays(1), null};
			yield return new object[] {"123456", "test", "me", DateTime.Today, DateTime.Today.AddDays(1), new Dictionary<string, string>()};
			yield return new object[]
			{
				"123456", "test", "me", DateTime.Today, DateTime.Today.AddDays(1), new Dictionary<string, string>
				{
					{"key", "value"}
				}
			};
		}

		[Theory]
		[MemberData(nameof(ValidCertificateProvider))]
		public void SaveValidCertificate(string sn, string subj, string issuer, DateTime? from, DateTime? until, IDictionary<string, string> eProperties)
		{
			AreEqual(1, service.Save(sn, subj, issuer, from, until, eProperties));
		}
	}
}