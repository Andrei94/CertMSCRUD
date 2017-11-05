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

		public int Save(string serialNumber, string subject, string issuer, DateTime? validFrom, DateTime? validUntil, IDictionary<string, string> extraProperties)
		{
			return dao.Save(new Certificate
			{
				SerialNumber = serialNumber,
				Subject = subject,
				Issuer = issuer,
				ValidFrom = validFrom,
				ValidUntil = validUntil,
				ExtraProperties = extraProperties
			});
		}

		public int CertificateCount => dao.Size;

		public bool Delete(string serialNumber)
		{
			return dao.Delete(serialNumber);
		}
	}
}