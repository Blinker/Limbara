using System.Windows;

namespace Limbara.Exe
{
	public partial class MainWindow : Window
	{
		public string TitleComputed =>
			"Limbara v" + (TryFindResource("AppVersionId") ?? "");

		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
