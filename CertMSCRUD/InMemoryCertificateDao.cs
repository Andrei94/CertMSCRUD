using System.Collections.Generic;

namespace CertMSCRUD
{
	public class InMemoryCertificateDao : CertificateDao
	{
		private readonly IDictionary<string, Certificate> certificates = new Dictionary<string, Certificate>();

		public int Save(Certificate certificate)
		{
			if (certificate.SerialNumber == null || certificates.ContainsKey(certificate.SerialNumber)) return 0;

			certificates.Add(certificate.SerialNumber, certificate);
			return 1;
		} 

		public bool Delete(string key) => !string.IsNullOrWhiteSpace(key) && certificates.Remove(key);
		
		public long Size => certificates.Count;

		public int Update(string serialNumber, Certificate newCertificate) => Delete(serialNumber) ? Save(newCertificate) : 0;

		public IEnumerable<Certificate> GetAll()
		{
			return certificates.Values;
		}
	}
}