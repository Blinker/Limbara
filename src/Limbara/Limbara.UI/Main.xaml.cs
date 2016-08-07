using BotEngine.UI;
using System.Windows.Controls;

namespace Limbara.UI
{
	public partial class Main : UserControl
	{
		public void BotMotionDisable() => ToggleButtonMotionEnable?.LeftButtonDown();

		public void BotMotionEnable() => ToggleButtonMotionEnable?.RightButtonDown();

		public Main()
		{
			InitializeComponent();
		}

		public void ConfigFromModelToView(ExeConfig config)
		{
			Interface?.ProcessConfigViewModel?.PropagateFromClrMemberToDependencyProperty(config?.BrowserProcess);
		}

		public ExeConfig ConfigFromViewToModel() =>
			new ExeConfig
			{
				BrowserProcess = Interface?.ProcessConfigViewModel?.PropagateFromDependencyPropertyToClrMember(),
			};
	}
}
