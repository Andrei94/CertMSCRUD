using System.Collections.Generic;

namespace CertMSCRUD
{
	public class CertificateDao
	{
		private readonly IDictionary<string, Certificate> certificates = new Dictionary<string, Certificate>();

		public int Save(Certificate certificate)
		{
			if (certificates.ContainsKey(certificate.SerialNumber)) return 0;

			certificates.Add(certificate.SerialNumber, certificate);
			return 1;
		} 

		public bool Delete(string key) => !string.IsNullOrWhiteSpace(key) && certificates.Remove(key);
		public int Size => certificates.Count;

		public Certificate FindBySerial(string serialNumber)
		{
			return certificates.ContainsKey(serialNumber) ? certificates[serialNumber] : new InnexistentCertificate();
		}
	}
}