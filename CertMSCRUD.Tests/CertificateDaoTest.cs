using System.Collections.Generic;
using System.Linq;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CertificateDaoTest
	{
		private readonly CertificateDao dao = new CertificateDao();

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProvider()
		{
			yield return new object[] {0, new int[] {}, new List<Certificate>()};
			yield return new object[] {1, new[] {1}, new List<Certificate> {CertificateCreatorDummy.CreateDummyCertificate()}};
			yield return new object[] {1, new[] {1, 0}, new List<Certificate> {CertificateCreatorDummy.CreateDummyCertificate(), CertificateCreatorDummy.CreateDummyCertificate()}};
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void SaveCertificateToRepository(int expectedCerts, int[] expectedChangedItems, List<Certificate> certs)
		{
			var changedItems = new List<int>();
			certs.ForEach(cert => changedItems.Add(dao.Save(cert)));
			AreEqual(expectedCerts, dao.Size);
			True(changedItems.SequenceEqual(expectedChangedItems));
		}
	}

	public class CertificateRemover
	{
		private readonly CertificateDao dao = new CertificateDao();
		public CertificateRemover() => dao.Save(CertificateCreatorDummy.CreateDummyCertificate());

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> DeleteProvider()
		{
			yield return new object[] {1, null};
			yield return new object[] {1, string.Empty};
			yield return new object[] {1, "         "};
			yield return new object[] {0, CertificateCreatorDummy.CreateDummyCertificate().SerialNumber};
		}

		[Theory]
		[MemberData(nameof(DeleteProvider))]
		public void DeleteCertificate(int expectedCertCount, string serialNumber)
		{
			dao.Delete(serialNumber);
			AreEqual(expectedCertCount, dao.Size);
		}
	}
}