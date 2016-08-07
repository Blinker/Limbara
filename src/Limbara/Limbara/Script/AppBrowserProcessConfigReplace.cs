using System;
using Limbara.Interface.RemoteControl;
using Limbara.Interface;
using System.Text.RegularExpressions;
using Bib3;
using System.Collections.Generic;

namespace Limbara.Script
{
	public class AppBrowserProcessConfigReplace : IApp
	{
		readonly public IApp Base;

		readonly public Func<BrowserProcessConfig> CallbackGetBrowserProcessConfigDefault;

		/// <summary>
		/// whitespaces don't count.
		/// </summary>
		/// <param name="candidateToReplace"></param>
		/// <param name="replacement"></param>
		/// <returns></returns>
		static public string IfEmptyReplaceWith(string candidateToReplace, string replacement) =>
			Regex.Match(candidateToReplace ?? "", @"[^\s]").Success ? candidateToReplace : replacement;

		public AppBrowserProcessConfigReplace(
			IApp @base,
			Func<BrowserProcessConfig> callbackGetBrowserProcessConfigDefault)
		{
			this.Base = @base;
			this.CallbackGetBrowserProcessConfigDefault = callbackGetBrowserProcessConfigDefault;
		}

		public BrowserProcessConfig CombinedConfig(BrowserProcessConfig fromScriptConfig)
		{
			var ConfigDefault = CallbackGetBrowserProcessConfigDefault?.Invoke();

			fromScriptConfig = fromScriptConfig ?? ConfigDefault ?? new BrowserProcessConfig();

			fromScriptConfig.ChromeExePath = IfEmptyReplaceWith(fromScriptConfig.ChromeExePath, ConfigDefault?.ChromeExePath);
			fromScriptConfig.UserDataDir = IfEmptyReplaceWith(fromScriptConfig.UserDataDir, ConfigDefault?.UserDataDir);
			fromScriptConfig.AddressTcpRangeBoundA = fromScriptConfig.AddressTcpRangeBoundA ?? ConfigDefault?.AddressTcpRangeBoundA;
			fromScriptConfig.AddressTcpRangeBoundB = fromScriptConfig.AddressTcpRangeBoundB ?? ConfigDefault?.AddressTcpRangeBoundB;

			return fromScriptConfig;
		}

		const int GetProcessForScriptDelayMax = 10000;

		public IEnumerable<IBrowserProcess> Processes => Base.Processes;

		public IResultOrError<IBrowserProcess> GetProcessForScript(
			BrowserProcessConfig fromScriptConfig,
			Func<BrowserProcessConfig, IResultOrError<IBrowserProcess>> callbackProcessFromConfig)
		{
			var Config = CombinedConfig(fromScriptConfig);

			if (!(0 < Config?.ChromeExePath?.Length))
				throw new ArgumentException("no path to chrome.exe supplied.", "ChromeExePath");

			var Process = callbackProcessFromConfig(Config);

			var BeginTime = Bib3.Glob.StopwatchZaitMiliSictInt();

			if (Process.Failed())
				return Process;

			for (;;)
			{
				System.Threading.Thread.Sleep(111);

				var Duration = Bib3.Glob.StopwatchZaitMiliSictInt() - BeginTime;

				if (GetProcessForScriptDelayMax < Duration)
					break;

				try
				{
					var BrowsersIds = Process?.Result?.GetBrowsersIds();

					if (!(BrowsersIds?.Result).IsNullOrEmpty())
						break;  //	connection to process is already possible.
				}
				catch
				{
				}
			}

			return Process;
		}

		public IResultOrError<IBrowserProcess> CreateProcess(BrowserProcessConfig fromScriptConfig) =>
			GetProcessForScript(fromScriptConfig, Base.CreateProcess);

		public IResultOrError<IBrowserProcess> ReuseProcess(BrowserProcessConfig fromScriptConfig) =>
			GetProcessForScript(fromScriptConfig, Base.ReuseProcess);

		public void Dispose()
		{
		}
	}
}
