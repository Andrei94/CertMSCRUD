using System;
using System.Collections.Generic;
using System.Linq;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CertificateDaoTest : IDisposable
	{
		private CertificateDao dao;

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
			dao = new InMemoryCertificateDao();
			var changedItems = new List<int>();
			certs.ForEach(cert => changedItems.Add(dao.Save(cert)));
			AreEqual(expectedCerts, dao.Size);
			True(changedItems.SequenceEqual(expectedChangedItems));
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void SaveCertificateToPersistentRepository(int expectedCerts, int[] expectedChangedItems, List<Certificate> certs)
		{
			dao = new MongoCertificateDao();
			var changedItems = new List<int>();
			certs.ForEach(cert => changedItems.Add(dao.Save(cert)));
			AreEqual(expectedCerts, dao.Size);
			True(changedItems.SequenceEqual(expectedChangedItems));
		}

		[Fact]
		public void GetAllInMemory()
		{
			dao = new InMemoryCertificateDao();
			dao.Save(CertificateCreatorDummy.CreateDummyCertificate());
			AreEqual(1, dao.Size);
		}

		[Fact]
		public void GetAllPersistent()
		{
			dao = new MongoCertificateDao();
			dao.Save(CertificateCreatorDummy.CreateDummyCertificate());
			AreEqual(1, dao.Size);
		}

		public void Dispose()
		{
			dao.Delete(CertificateCreatorDummy.CreateDummyCertificate().SerialNumber);
		}
	}

	public class CertificateRemover
	{
		private readonly InMemoryCertificateDao dao = new InMemoryCertificateDao();
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