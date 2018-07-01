using System;
using WPFCommonUI;

namespace CertMSCRUD
{
	public class SaveViewModel : ViewModelBase<IMainView>
	{
		public CertificateService CertificateService { get; set; } = new CertificateService(new MongoCertificateDao());
		private readonly CertificateParser parser = new CertificateParser();

		public SaveViewModel(IMainView view) : base(view)
		{
		}

		public string PerformSave(string data)
		{
			View.Close();
			return SaveGeneratedCertificate(data) ? $"Certificate {parser.Convert(data)} " + Environment.NewLine + "saved to DB :)" : "Certificate already exists";
		}

		public virtual bool SaveGeneratedCertificate(string generatedCertificate)
		{
			var certificate = parser.Convert(generatedCertificate);
			return CertificateService.Save(certificate.SerialNumber, certificate.Subject, certificate.Issuer, certificate.ValidFrom, certificate.ValidUntil,
				       certificate.ExtraProperties) > 0;
		}
	}
}