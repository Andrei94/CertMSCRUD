using System;
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
			var response = AppProperties.FailureMsg;
			if(e.Args[0].Equals(AppProperties.Save))
			{
				viewModel = new SaveViewModel(new MainWindow());
				response = ((SaveViewModel) viewModel).PerformSave(e.Args[1]);
			}
			else if(e.Args[0].Equals(AppProperties.SaveDuplicate))
			{
				viewModel = new SaveViewModelDuplicate(new MainWindow());
				response = ((SaveViewModelDuplicate) viewModel).PerformSave(null);
			}
			else if(e.Args[0].Equals(AppProperties.Delete))
			{
				viewModel = new DeleteViewModel(new MainWindow());
				response = ((DeleteViewModel) viewModel).PerformDelete(e.Args[1]);
			}
			else if(e.Args[0].Equals(AppProperties.Update))
			{
				viewModel = new UpdateViewModel(new MainWindow());
				response = ((UpdateViewModel) viewModel).PerformUpdate(e.Args[1] + e.Args[2]);
			}
			else if(e.Args[0].Equals(AppProperties.GetAll))
			{
				viewModel = new GetAllViewModel(new MainWindow());
				response = ((GetAllViewModel) viewModel).PerformGetAll();
			}
			Console.WriteLine(response);
		}

		private class SaveViewModelDuplicate : SaveViewModel
		{
			public SaveViewModelDuplicate(IMainView view) : base(view)
			{
			}

			public override bool SaveGeneratedCertificate(string generatedCertificate)
			{
				return false;
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			viewModel.View.Close();
		}
	}
}