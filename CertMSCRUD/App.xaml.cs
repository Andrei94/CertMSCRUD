using System;
using System.Text;
using System.Windows;
using WPFCommonUI;

namespace CertMSCRUD
{
	public partial class App
	{
		private ViewModelBase<IMainView> viewModel;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			if(e.Args[0].Equals("save"))
			{
				viewModel = new SaveViewModel(new MainWindow());
				((SaveViewModel) viewModel).PerformSave(Encoding.ASCII.GetString(Convert.FromBase64String(e.Args[1])));
			}
			else if(e.Args[0].Equals("delete"))
			{
				viewModel = new DeleteViewModel(new MainWindow());
				((DeleteViewModel) viewModel).PerformDelete(e.Args[1]);
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			viewModel.View.Close();
		}
	}
}