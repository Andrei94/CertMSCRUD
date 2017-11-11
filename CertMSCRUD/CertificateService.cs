using System;
using System.Collections.Generic;

namespace CertMSCRUD
{
	public class CertificateService
	{
		private readonly CertificateDao dao;

		public CertificateService(CertificateDao dao)
		{
			this.dao = dao;
		}

		public int Save(string serialNumber, string subject, string issuer, DateTime? validFrom, DateTime? validUntil, IDictionary<string, string> extraProperties) => dao.Save(
			new Certificate
			{
				SerialNumber = serialNumber,
				Subject = subject,
				Issuer = issuer,
				ValidFrom = validFrom,
				ValidUntil = validUntil,
				ExtraProperties = extraProperties
			});

		public int CertificateCount => dao.Size;

		public bool Delete(string serialNumber) => dao.Delete(serialNumber);

		public int Update(string serialNumber, string newSerialNumber, string newSubject, string newIssuer, DateTime? newValidFrom, DateTime? newValidUntil,
			IDictionary<string, string> newExtraProperties) => dao.Update(serialNumber,
			new Certificate
			{
				SerialNumber = newSerialNumber,
				Subject = newSubject,
				Issuer = newIssuer,
				ValidFrom = newValidFrom,
				ValidUntil = newValidUntil,
				ExtraProperties = newExtraProperties
			});

		public IEnumerable<Certificate> GetAll()
		{
			return dao.GetAll();
		}
	}
}