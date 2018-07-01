using System.Configuration;

namespace CertMSCRUD
{
	internal static class AppProperties
	{
		internal static string Save => ConfigurationManager.AppSettings["save"];
		internal static string SaveDuplicate => ConfigurationManager.AppSettings["saveDuplicate"];
		internal static string Delete => ConfigurationManager.AppSettings["delete"];
		internal static string Update => ConfigurationManager.AppSettings["update"];
		internal static string GetAll => ConfigurationManager.AppSettings["getAll"];
		internal static string FailureMsg => ConfigurationManager.AppSettings["failureMsg"];
	}
}
