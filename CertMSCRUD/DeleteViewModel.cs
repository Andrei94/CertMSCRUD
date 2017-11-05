using System;
using CertMS.Helpers;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class DeleteViewModel : ViewModelBase<IMainView>
	{
		private IDialogHelper DialogHelper { get; }
		private CertificateService CertificateService { get; } = new CertificateService(new CertificateDao());

		public DeleteViewModel(IMainView view) : base(view)
		{
			DialogHelper = new DialogHelper();
			CertificateService.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
		}

		public void PerformDelete(string data)
		{
			DialogHelper.ShowMessageBox(DeleteCertificate(data) ? $"Certificate successfully deleted from DB :)" : "Certificate does not exist");
			View.Close();
		}

		public bool DeleteCertificate(string serialNumber)
		{
			return CertificateService.Delete(serialNumber);
		}
	}
}
