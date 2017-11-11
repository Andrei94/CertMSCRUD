using System;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class GetAllViewModel : ViewModelBase<IMainView>
	{
		private CertificateService CertificateService { get; } = new CertificateService(new CertificateDao());
		private readonly CertificateParser parser = new CertificateParser();

		public GetAllViewModel(IMainView view) : base(view)
		{
			CertificateService.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
		}

		public string PerformGetAll()
		{
			View.Close();
			return parser.Convert(CertificateService.GetAll());
		}
	}
}
