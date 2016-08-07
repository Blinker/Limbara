using Bib3;
using Bib3.FCL.GBS;
using BotEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Limbara.Exe
{
	public partial class App : Application
	{
		static public Int64 GetTimeStopwatch() => Bib3.Glob.StopwatchZaitMiliSictInt();

		BotEngine.Client.LicenseClientConfig LicenseClientConfig =>
			(ConfigReadFromUI()?.LicenseClient).CompletedWithDefault().WithRequestLicenseKey(LicenseKeyStore?.Load() ?? ExeConfig.ConfigLicenseKeyDefault);

		public MainWindow Window => base.MainWindow as MainWindow;

		Bib3.FCL.GBS.ToggleButtonHorizBinär ToggleButtonMotionEnable => Window?.Main?.ToggleButtonMotionEnable;

		BotSharp.UI.Wpf.IDE ScriptIDE => Window?.Main?.Bot?.IDE;

		BotSharp.ScriptRun.ScriptRun ScriptRun => ScriptIDE?.ScriptRun;

		bool wasActivated = false;

		DispatcherTimer Timer;

		string AssemblyDirectoryPath => Bib3.FCL.Glob.ZuProcessSelbsctMainModuleDirectoryPfaadBerecne().PathToFilesysChild(@"\");

		Limbara.Script.HostToScript UIAPI;

		readonly ISingleValueStore<string> LicenseKeyStore;

		public App()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

			LicenseKeyStore =
				new SingleValueStoreCached<string>
				{
					BaseStore =
						new SingleValueStoreRelayWithExceptionToDelegate<string>
						{
							BaseStore = new StringStoreToFilePath
							{
								FilePath = LicenseKeyStoreFilePath,
							},

							ExceptionDelegate = e => e.MessageBoxException(),
						}
				};

			UIAPI = new Limbara.Script.HostToScript
			{
				CallbackGetApp = new Func<Interface.RemoteControl.IApp>(() => ToScriptApp),
			};
		}

		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			var matchFullName =
				AppDomain.CurrentDomain.GetAssemblies()
				?.FirstOrDefault(candidate => string.Equals(candidate.GetName().FullName, args?.Name));

			if (null != matchFullName)
				return matchFullName;

			var matchName =
				AppDomain.CurrentDomain.GetAssemblies()
				?.FirstOrDefault(candidate => string.Equals(candidate.GetName().Name, args?.Name));

			return matchName;
		}

		void TimerConstruct()
		{
			Timer = new DispatcherTimer(TimeSpan.FromSeconds(1.0 / 16), DispatcherPriority.Normal, Timer_Tick, Dispatcher);

			Timer.Start();
		}

		private void Application_Activated(object sender, EventArgs e)
		{
			if (wasActivated)
				return;

			wasActivated = true;

			ActivatedFirstTime();
		}

		Script.ToScriptGlobals ToScriptGlobalsConstruct(Action scriptExecutionCheck) =>
			new Script.ToScriptGlobals
			{
				Limbara = new Limbara.Script.HostToScript
				{
					CallbackGetApp = () =>
					{
						scriptExecutionCheck?.Invoke();
						return ToScriptApp;
					},
				}
			};

		BotSharp.ScriptRun.IScriptRunClient ScriptRunClientBuild(BotSharp.ScriptRun.ScriptRun run)
		{
			return new BotSharp.ScriptRun.ScriptRunClientDelegate
			{
				ToScriptGlobals = () => ToScriptGlobalsConstruct(() => run?.FromScriptExecutionControlCheck()),
			};
		}

		void ActivatedFirstTime()
		{
			ScriptIDE.ScriptParamBase = new BotSharp.ScriptParam
			{
				ImportAssembly = Script.ToScriptImport.ImportAssembly?.ToArray(),
				ImportNamespace = Limbara.Script.ToScriptImport.ImportNamespace?.ToArray(),
				CompilationOption = BotSharp.CodeAnalysis.CompilationOption.Default,

				ScriptRunClientBuildDelegate = ScriptRunClientBuild,
				CompilationGlobalsType = ToScriptGlobalsConstruct(null)?.GetType(),
			};

			ScriptIDE.ChooseScriptFromIncludedScripts.SetScript =
				ListScriptIncluded?.Select(scriptIdAndContent => new KeyValuePair<string, Func<string>>(scriptIdAndContent.Key, () => scriptIdAndContent.Value))?.ToArray();
			ScriptIDE.ScriptWriteToOrReadFromFile.DefaultFilePath = DefaultScriptPath;
			ScriptIDE.ScriptWriteToOrReadFromFile.ReadFromFile();

			if ((ScriptIDE.Editor.Document.Text).IsNullOrEmpty())
				ScriptIDE.Editor.Document.Text = ListScriptIncluded?.FirstOrDefault().Value ?? "";

			Window?.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new RoutedEventHandler(ButtonClicked));

			Window?.Main?.ConfigFromModelToView(ExeConfig.Default);

			ConfigFileControl.DefaultFilePath = ConfigFilePath;
			ConfigFileControl.CallbackGetValueToWrite = ConfigReadFromUISerialized;
			ConfigFileControl.CallbackValueRead = ConfigWriteToUIDeSerialized;
			ConfigFileControl.ReadFromFile();

			MainControl.Interface.LicenseDataContext.LicenseKeyStore = LicenseKeyStore;

			TimerConstruct();
		}

		void Timer_Tick(object sender, object e)
		{
			BrowserProcessConfigDefault = BrowserProcessConfigDefaultCreate();

			ScriptExchange();

			var licenseClientConfig = this.LicenseClientConfig;

			Task.Run(() => InterfaceServerDispatcher?.Exchange(licenseClientConfig, null == InterfaceServerDispatcher?.AppInterface ? (int?)null : 1000));

			UIPresent();
		}

		void ButtonClicked(object sender, RoutedEventArgs e)
		{
			var originalSource = e?.OriginalSource;

			if (null != originalSource)
			{
				if (originalSource == ToggleButtonMotionEnable?.ButtonLinx)
					ScriptRunPause();

				if (originalSource == ToggleButtonMotionEnable?.ButtonRecz)
					ScriptRunPlay();
			}
		}

		void ScriptRunPlay()
		{
			ScriptIDE.ScriptRunContinueOrStart();

			ScriptExchange();
		}

		void ScriptRunPause()
		{
			ScriptIDE.ScriptPause();

			ScriptExchange();
		}

		void ScriptExchange()
		{
			ToggleButtonMotionEnable?.SetValue(Bib3.FCL.GBS.ToggleButtonHorizBinär.ButtonReczIsCheckedProperty, ScriptRun?.IsRunning ?? false);

			ScriptIDE?.Present();
		}

		private void Application_DispatcherUnhandledException(Object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			Bib3.FCL.GBS.Extension.MessageBoxException(e.Exception);

			e.Handled = true;
		}
	}
}
