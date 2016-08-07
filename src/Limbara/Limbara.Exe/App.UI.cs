using Bib3.FCL.UI;
using BotEngine.UI;
using BotSharp.UI.Wpf;
using Limbara.UI;
using System.Linq;

namespace Limbara.Exe
{
	partial class App
	{
		Main MainControl => Window?.Main;

		StatusIcon.StatusEnum InterfaceLicensePortionConnectionStatus =>
			MainControl?.Interface?.LicenseDataContext?.StatusIcon ?? StatusIcon.StatusEnum.None;

		StatusIcon.StatusEnum AppInterfaceStatus =>
			null == ToScriptApp ? StatusIcon.StatusEnum.Progress : StatusIcon.StatusEnum.Accept;

		StatusIcon.StatusEnum InterfaceLicenseStatus =>
			(BotEngine.UI.Extension.AggregateStatus(new[]
			{
				InterfaceLicensePortionConnectionStatus,
				AppInterfaceStatus,
			})
			?.FirstOrDefault()).GetValueOrDefault(StatusIcon.StatusEnum.Reject);

		StatusIcon.StatusEnum BotStatus => ScriptEngineStatus;

		StatusIcon.StatusEnum ScriptEngineStatus =>
			ScriptRun?.StatusIcon() ?? StatusIcon.StatusEnum.None;

		void UIPresent()
		{
			MainControl?.InterfaceHeader?.SetStatus(InterfaceLicenseStatus);
			MainControl?.Interface?.LicenseHeader?.SetStatus(InterfaceLicenseStatus);
			MainControl?.BotHeader?.SetStatus(BotStatus);
			MainControl?.Bot?.ScriptEngineHeader?.SetStatus(ScriptEngineStatus);

			MainControl?.Interface?.Present(InterfaceServerDispatcher);
		}
	}
}
