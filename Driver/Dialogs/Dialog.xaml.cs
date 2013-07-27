using System;
using System.Linq;
using System.Net;
using System.Windows;

namespace SwaggerDriver.Dialogs
{
	public partial class Dialog
	{
		int page;
		readonly ConnectionLogger logger;
		readonly DiscoveryWorker worker;

		public Dialog(ConnectionModel model)
		{
			DataContext = model;
			InitializeComponent();
			worker = new DiscoveryWorker();
			logger = new ConnectionLogger(LogBox);
		}

		ConnectionModel Model
		{
			get { return DataContext as ConnectionModel; }
		}

		void Connect_Click(object sender, EventArgs e)
		{
			if (Uri.IsWellFormedUriString(Model.Uri, UriKind.Absolute))
			{
				SetVisiblePage(1);
				Connect();
			}
		}

		void Finish_Click(object sender, EventArgs e)
		{
            DialogResult = true;
        }

		void Back_Click(object sender, EventArgs e)
		{
			SetVisiblePage(page - 1);
		}

		void Connect()
		{
			RestartButton.IsEnabled = false;
			Progress.IsIndeterminate = true;

			if (!worker.Busy)
			{
				logger.Clear();
				logger.Write("Connecting to " + Model.Uri);

				worker
					.Failure(Discovery_Failure)
					.Complete(Discovery_Connect)
					.Connect(Model.Uri, CredentialCache.DefaultCredentials);
			}
		}

		void Discovery_Failure(DiscoveryFailureEventArgs e)
		{
			logger.Error(e.Reason);
			RestartButton.IsEnabled = true;
		}

		void Discovery_Connect(DiscoveryCompleteEventArgs e)
		{
			//var reference = e.Reference;

            //foreach (var warning in reference.Warnings) logger.Warn(warning);		
            //foreach (var error in reference.Errors) logger.Error(error);

            //if (reference.HasErrors)
            //    logger.Write("Cannot create a connection.");
            //else
            //{
				logger.Write("Click \"Next\" to continue.");

            //    var reset = string.IsNullOrEmpty(Model.BindingName) ||
            //        !reference.Bindings.Any(b => b.Name == Model.BindingName);
            //    if (reset)
            //    {
            //        Model.BindingName = reference.Bindings.Count > 0
            //            ? reference.Bindings[0].Name : "";
            //    }

            //    BindingBox.ItemsSource = reference.Bindings.Select(b => b.Name);
            //    BindingBox.SelectedItem = Model.BindingName;
            //}

			RestartButton.IsEnabled = true;
			Progress.IsIndeterminate = false;
			LogBox.ScrollToEnd();

            //if (!reference.HasErrors && !reference.HasWarnings)
            //{
				//SetVisiblePage(2);
                FinishButton.IsEnabled = true;
            //}		
		}

		Visibility GetPageVisibility(int which)
		{
		    return which == page ? Visibility.Visible : Visibility.Hidden;
		}

		void SetVisiblePage(int p)
		{
			if (p < 0 || p > 1) return;
			page = p;
			Page1.Visibility = GetPageVisibility(0);
			Page2.Visibility = GetPageVisibility(1);
		}
	}
}
