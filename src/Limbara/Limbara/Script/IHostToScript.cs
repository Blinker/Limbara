using System;
using Limbara.Interface.RemoteControl;
using Limbara.Script.ToScript;

namespace Limbara.Script
{
	public class HostToScript : IHostToScript
	{
		public Func<IApp> CallbackGetApp;

		public IApp App => CallbackGetApp?.Invoke();
	}

	public class ToScriptGlobals : BotSharp.ScriptRun.ScriptRun.ToScriptGlobals
	{
		static ToScriptGlobals()
		{
			HtmlAgilityPackStatic.StaticConfig();
		}

		public IHostToScript Limbara;
	}
}
