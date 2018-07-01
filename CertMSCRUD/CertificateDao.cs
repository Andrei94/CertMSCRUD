using System.Collections.Generic;

namespace CertMSCRUD
{
	public interface CertificateDao
	{
		int Save(Certificate certificate);
		bool Delete(string key);
		long Size { get; }
		int Update(string serialNumber, Certificate newCertificate);
		IEnumerable<Certificate> GetAll();
	}
}
