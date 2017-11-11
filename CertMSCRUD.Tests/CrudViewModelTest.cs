using System.Collections.Generic;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CrudViewModelTest
	{
		private static readonly CertificateParser Parser = new CertificateParser();
		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProvider()
		{
			yield return new object[] {true, Parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
			yield return new object[] {true, Parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())};
			yield return new object[] {false, "jsksklkls"};
		}

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProviderIntegration()
		{
			yield return new object[] {$"Certificate {CertificateCreatorDummy.CreateDummyCertificate()} saved to DB :)", Parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
			yield return new object[]
			{
				$"Certificate {CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties()} saved to DB :)",
				Parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())
			};
			yield return new object[] {"Certificate already exists", "jsksklkls"};
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void ExecuteSave(bool certSaved, string genCert)
		{
			var viewModel = new SaveViewModel(new ViewDummy());
			AreEqual(certSaved, viewModel.SaveGeneratedCertificate(genCert));
		}

		[Theory]
		[MemberData(nameof(SaveProviderIntegration))]
		public void ExecuteSaveIntegration(string expectedMessage, string genCert)
		{
			var viewModel = new SaveViewModel(new ViewDummy());
			AreEqual(expectedMessage, viewModel.PerformSave(genCert));
		}

		public class DeleteTestClass
		{
			private readonly DeleteViewModel deleteViewModel = new DeleteViewModel(new ViewDummy());
			// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers

			public static IEnumerable<object[]> DeleteProvider()
			{
				yield return new object[] { true, "1234" };
				yield return new object[] { false, "jsksklkls" };
			}

			// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers

			public static IEnumerable<object[]> DeleteProviderIntegration()
			{
				yield return new object[] { "Certificate successfully deleted from DB :)", "1234" };
				yield return new object[] { "Certificate does not exist", "jsksklkls" };
			}

			[Theory]
			[MemberData(nameof(DeleteProvider))]
			public void ExecuteDelete(bool certDeleted, string sn)
			{
				AreEqual(certDeleted, deleteViewModel.DeleteCertificate(sn));
			}

			[Theory]
			[MemberData(nameof(DeleteProviderIntegration))]
			public void ExecuteDeleteIntegration(string expectedMessage, string sn)
			{
				AreEqual(expectedMessage, deleteViewModel.PerformDelete(sn));
			}
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void ExecuteUpdate(bool certUpdated, string genCert)
		{
			var viewModel = new UpdateViewModel(new ViewDummy());
			AreEqual(certUpdated, viewModel.UpdateCertificate("1234", genCert));
		}

		public class UpdateTestClass
		{
			private readonly UpdateViewModel viewModel = new UpdateViewModel(new ViewDummy());

			// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
			public static IEnumerable<object[]> UpdateProviderIntegration()
			{
				yield return new object[] {"Certificate successfully updated from DB :)", Parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
				yield return new object[] {"Certificate successfully updated from DB :)", Parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())};
				yield return new object[] {"Certificate does not exist", "jsksklkls"};
			}

			[Theory]
			[MemberData(nameof(UpdateProviderIntegration))]
			public void ExecuteUpdateIntegration(string expectedMessage, string genCert)
			{
				AreEqual(expectedMessage, viewModel.PerformUpdate("1234;" + genCert));
			}

			[Fact]
			public void WrongFormatForUpdate()
			{
				AreEqual("invalid arguments provided", viewModel.PerformUpdate("1234" + CertificateCreatorDummy.CreateDummyCertificate()));
			}
		}

		[Fact]
		public void ExecuteGetAll()
		{
			var getAllViewModel = new GetAllViewModel(new ViewDummy());
			True(!string.IsNullOrWhiteSpace(getAllViewModel.PerformGetAll()));
		}
	}
}