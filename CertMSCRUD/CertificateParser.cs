using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertMSCRUD
{
	public class CertificateParser
	{
		public Certificate Convert(string certData)
		{
			var certificateEntries = ConvertFromBase64(certData).Split(new[] { ": ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if (certificateEntries.Length < 10)
				return new MalformedCertificate();
			return new Certificate
			{
				SerialNumber = certificateEntries[1],
				Subject = certificateEntries[3],
				Issuer = certificateEntries[5],
				ValidFrom = DateTime.Parse(certificateEntries[7]),
				ValidUntil = DateTime.Parse(certificateEntries[9]),
				ExtraProperties = ParseExtraProperties(certificateEntries)
			};
		}

		private string ConvertFromBase64(string str)
		{
			try
			{
				return Encoding.ASCII.GetString(System.Convert.FromBase64String(str));
			}
			catch(FormatException)
			{
				return string.Empty;
			}
		}

		public string Convert(Certificate certificate)
		{
			return Convert(new List<Certificate> {certificate});
		}

		public string Convert(IEnumerable<Certificate> certs)
		{
			return string.Join(";", certs.Select(cert => System.Convert.ToBase64String(Encoding.ASCII.GetBytes(cert.ToString()))));
		}

		private static Dictionary<string, string> ParseExtraProperties(IReadOnlyList<string> certificateEntries)
		{
			var properties = new Dictionary<string, string>();
			for (var entry = 10; entry < certificateEntries.Count - 1; entry+=2)
				properties[certificateEntries[entry]] = certificateEntries[entry + 1];
			return properties;
		}

		private class MalformedCertificate : Certificate
		{
		}
	}
}
