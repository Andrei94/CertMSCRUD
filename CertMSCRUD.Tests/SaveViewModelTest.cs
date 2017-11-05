using System.Collections.Generic;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public partial class SaveViewModelTest
	{
		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProvider()
		{
			yield return new object[] {true, CertificateCreatorDummy.CreateDummyCertificate().ToString()};
			yield return new object[] {true, CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties().ToString()};
			yield return new object[] {false, "jsksklkls"};
		}

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> DeleteProvider()
		{
			yield return new object[] {true, "1234"};
			yield return new object[] {false, "jsksklkls"};
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void ExecuteSave(bool certSaved, string genCert)
		{
			var viewModel = new SaveViewModel(new ViewDummy());
			AreEqual(certSaved, viewModel.SaveGeneratedCertificate(genCert));
		}

		[Theory]
		[MemberData(nameof(DeleteProvider))]
		public void ExecuteDelete(bool certDeleted, string sn)
		{
			var viewModel = new DeleteViewModel(new ViewDummy());
			AreEqual(certDeleted, viewModel.DeleteCertificate(sn));
		}
	}
}