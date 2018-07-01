using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;

namespace CertMSCRUD
{
	public class MongoCertificateDao : CertificateDao
	{
		private readonly IMongoCollection<CertificateMongo> certificates;

		public MongoCertificateDao() : this(ConfigurationManager.AppSettings["DBName"])
		{
		}

		public MongoCertificateDao(string databaseName)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var db = client.GetDatabase(databaseName);
			certificates = db.GetCollection<CertificateMongo>("Certificates");
		}

		public int Save(Certificate certificate)
		{
			if (certificate.SerialNumber == null || certificates.Count(c => c.SerialNumber.Equals(certificate.SerialNumber)) > 0) return 0;
			
			certificates.InsertOne(new CertificateMongo(certificate));

			return 1;
		} 

		public bool Delete(string key) => !string.IsNullOrWhiteSpace(key) && certificates.DeleteOne(c => c.SerialNumber.Equals(key)).DeletedCount > 0;

		public long Size => certificates.Count(c => true);

		public int Update(string serialNumber, Certificate newCertificate) => Delete(serialNumber) ? Save(newCertificate) : 0;

		public IEnumerable<Certificate> GetAll()
		{
			var cursor = certificates.FindSync(x => true);
			var certs = new List<Certificate>();
			while(cursor.MoveNext())
				certs.AddRange(cursor.Current);
			return certs;
		}
	}
}
