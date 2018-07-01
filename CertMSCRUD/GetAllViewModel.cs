using WPFCommonUI;

namespace CertMSCRUD
{
	public class GetAllViewModel : ViewModelBase<IMainView>
	{
		public CertificateService CertificateService { private get; set; } = new CertificateService(new MongoCertificateDao());
		private readonly CertificateParser parser = new CertificateParser();

		public GetAllViewModel(IMainView view) : base(view)
		{
		}

		public string PerformGetAll()
		{
			View.Close();
			return parser.Convert(CertificateService.GetAll());
		}
	}
}
