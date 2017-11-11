using System;
using CertMS.Helpers;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class DeleteViewModel : ViewModelBase<IMainView>
	{
		private CertificateService CertificateService { get; } = new CertificateService(new CertificateDao());

		public DeleteViewModel(IMainView view) : base(view)
		{
			CertificateService.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
		}

		public string PerformDelete(string data)
		{
			View.Close();
			return DeleteCertificate(data) ? "Certificate successfully deleted from DB :)" : "Certificate does not exist";
		}

		public bool DeleteCertificate(string serialNumber)
		{
			return CertificateService.Delete(serialNumber);
		}
	}
}
