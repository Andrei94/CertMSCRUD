using System;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class UpdateViewModel : ViewModelBase<IMainView>
	{
		private CertificateService CertificateService { get; } = new CertificateService(new CertificateDao());
		private readonly CertificateParser parser = new CertificateParser();

		public UpdateViewModel(IMainView view) : base(view)
		{
			CertificateService.Save("1234", "test", "me", DateTime.Today, DateTime.Today, null);
		}

		public string PerformUpdate(string data)
		{
			View.Close();
			var dataParams = data.Split(';');
			if(dataParams.Length != 2)
				return "invalid arguments provided";
			return UpdateCertificate(dataParams[0], dataParams[1]) ? "Certificate successfully updated from DB :)" : "Certificate does not exist";
		}

		public bool UpdateCertificate(string serialNumber, string certData)
		{
			var certificate = parser.Convert(certData);
			return CertificateService.Update(serialNumber, certificate.SerialNumber, certificate.Subject, certificate.Issuer, certificate.ValidFrom, certificate.ValidUntil,
				       certificate.ExtraProperties) > 0;
		}
	}
}