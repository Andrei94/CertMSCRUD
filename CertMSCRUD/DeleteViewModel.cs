using WPFCommonUI;

namespace CertMSCRUD
{
	public class DeleteViewModel : ViewModelBase<IMainView>
	{
		public CertificateService CertificateService { get; set; } = new CertificateService(new MongoCertificateDao());

		public DeleteViewModel(IMainView view) : base(view)
		{
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
