using System.Windows.Controls;

namespace Limbara.UI
{
	public partial class BrowserProcessConfig : UserControl
	{
		public BrowserProcessConfig()
		{
			InitializeComponent();

			ChromeExePathFromSystemView.Text = WindowsGoogleChromeStatic.ChromeExePathFromSystem;
		}
	}
}
