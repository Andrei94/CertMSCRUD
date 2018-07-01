using System;
using System.Collections.Generic;
using CertMSCRUD.Tests.Utils;
using Xunit;
using static CertMSCRUD.Tests.Utils.TestUtils;

namespace CertMSCRUD.Tests
{
	public class CrudViewModelTest
	{
		private readonly CertificateService service = new CertificateService(new InMemoryCertificateDao());

		private static readonly CertificateParser parser = new CertificateParser();
		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProvider()
		{
			yield return new object[] {true, parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
			yield return new object[] {true, parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())};
			yield return new object[] {false, "jsksklkls"};
		}

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> SaveProviderIntegration()
		{
			yield return new object[] {$"Certificate {CertificateCreatorDummy.CreateDummyCertificate()} {Environment.NewLine}saved to DB :)", parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
			yield return new object[]
			{
				$"Certificate {CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties()} {Environment.NewLine}saved to DB :)",
				parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())
			};
			yield return new object[] {"Certificate already exists", "jsksklkls"};
		}

		public CrudViewModelTest()
		{
			service.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
		}

		[Theory]
		[MemberData(nameof(SaveProvider))]
		public void ExecuteSave(bool certSaved, string genCert)
		{
			var viewModel = new SaveViewModel(new ViewDummy());
			viewModel.CertificateService = service;
			AreEqual(certSaved, viewModel.SaveGeneratedCertificate(genCert));
		}

		[Theory]
		[MemberData(nameof(SaveProviderIntegration))]
		public void ExecuteSaveIntegration(string expectedMessage, string genCert)
		{
			var viewModel = new SaveViewModel(new ViewDummy());
			viewModel.CertificateService = service;
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

			private readonly CertificateService service = new CertificateService(new InMemoryCertificateDao());

			public DeleteTestClass()
			{
				deleteViewModel.CertificateService = service;
				service.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
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
			var viewModel = new UpdateViewModel(new ViewDummy()) {CertificateService = service};
			AreEqual(certUpdated, viewModel.UpdateCertificate("1234", genCert));
		}

		public class UpdateTestClass
		{
			private readonly UpdateViewModel viewModel = new UpdateViewModel(new ViewDummy());

			// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
			public static IEnumerable<object[]> UpdateProviderIntegration()
			{
				yield return new object[] {"Certificate successfully updated from DB :)", parser.Convert(CertificateCreatorDummy.CreateDummyCertificate())};
				yield return new object[] {"Certificate successfully updated from DB :)", parser.Convert(CertificateCreatorDummy.CreateDummyCertificateWithoutExtraProperties())};
				yield return new object[] {"Certificate does not exist", "jsksklkls"};
			}

			private readonly CertificateService service = new CertificateService(new InMemoryCertificateDao());

			public UpdateTestClass()
			{
				service.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
			}

			[Theory]
			[MemberData(nameof(UpdateProviderIntegration))]
			public void ExecuteUpdateIntegration(string expectedMessage, string genCert)
			{
				viewModel.CertificateService = service;
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
			var getAllViewModel = new GetAllViewModel(new ViewDummy()) {CertificateService = service};
			True(!string.IsNullOrWhiteSpace(getAllViewModel.PerformGetAll()));
		}
	}
}