using System;
using System.Collections.Generic;
using CertMS.Helpers;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class SaveViewModel : ViewModelBase<IMainView>
	{
		private IDialogHelper DialogHelper { get; }
		private CertificateService CertificateService { get; } = new CertificateService(new CertificateDao());

		public SaveViewModel(IMainView view) : base(view)
		{
			DialogHelper = new DialogHelper();
		}

		public void PerformSave(string data)
		{
			DialogHelper.ShowMessageBox(SaveGeneratedCertificate(data) ? $"Certificate {data} saved to DB :)" : "Certificate already exists");
			View.Close();
		}

		public bool SaveGeneratedCertificate(string generatedCertificate)
		{
			var certificateEntries = generatedCertificate.Split(new[] { ": ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if(certificateEntries.Length < 10)
				return false;
			return CertificateService.Save(certificateEntries[1], certificateEntries[3], certificateEntries[5], DateTime.Parse(certificateEntries[7]), DateTime.Parse(certificateEntries[9]),
				       ParseExtraProperties(certificateEntries)) > 0;
		}

		private static Dictionary<string, string> ParseExtraProperties(IReadOnlyList<string> certificateEntries)
		{
			var properties = new Dictionary<string, string>();
			for (var entry = 10; entry < certificateEntries.Count - 1; entry++)
				properties[certificateEntries[entry]] = certificateEntries[entry + 1];
			return properties;
		}
	}
}