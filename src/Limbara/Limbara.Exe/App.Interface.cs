using Bib3;
using Limbara.Script;

namespace Limbara.Exe
{
	/// <summary>
	/// This Type must reside in an Assembly that can be resolved by the default assembly resolver.
	/// </summary>
	public class InterfaceAppDomainSetup
	{
		static InterfaceAppDomainSetup()
		{
			BotEngine.Interface.InterfaceAppDomainSetup.Setup();
		}
	}

	partial class App
	{
		SimpleInterfaceServerDispatcher InterfaceServerDispatcher = new SimpleInterfaceServerDispatcher
		{
			InterfaceAppDomainSetupType = typeof(InterfaceAppDomainSetup),
			InterfaceAppDomainSetupTypeLoadFromMainModule = true,
		};

		InterfaceAppDomainSetup TriggerSetup = new InterfaceAppDomainSetup();

		public UI.Interface InterfaceControl => Window?.Main?.Interface;

		Interface.RemoteControl.IApp ToScriptApp
		{
			get
			{
				var BaseApp = InterfaceServerDispatcher?.AppInterface;

				if (null == BaseApp)
					return null;

				return new AppBrowserProcessConfigReplace(BaseApp, () => BrowserProcessConfigDefault);
			}
		}

		Interface.BrowserProcessConfig BrowserProcessConfigDefault;

		Interface.BrowserProcessConfig BrowserProcessConfigDefaultCreate() =>
			ExeConfig.BrowserProcessConfigDefaultCreate(
				ConfigReadFromUI()?.BrowserProcess,
				Bib3.FCL.Glob.ZuProcessSelbsctMainModuleDirectoryPfaadBerecne().PathToFilesysChild("Browser.UserData"));
	}
}
