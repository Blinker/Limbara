using System.Windows.Controls;

namespace Limbara.UI
{
	public partial class Interface : UserControl
	{
		readonly public BotEngine.UI.AutoDependencyPropertyComp<Limbara.Interface.BrowserProcessConfig> ProcessConfigViewModel =
			new BotEngine.UI.AutoDependencyPropertyComp<Limbara.Interface.BrowserProcessConfig>();

		readonly public BotEngine.UI.ViewModel.License LicenseDataContext = new BotEngine.UI.ViewModel.License();

		public Interface()
		{
			InitializeComponent();

			Config.DataContext = ProcessConfigViewModel;
			LicenseView.DataContext = LicenseDataContext;
		}

		public void Present(SimpleInterfaceServerDispatcher dispatcher)
		{
			LicenseDataContext.Dispatcher = dispatcher;
		}
	}
}
