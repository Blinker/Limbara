using Bib3;
using Limbara.Interface;
using Limbara.Interface.RemoteControl;

namespace Limbara.Script.ToScript
{
	/// <summary>
	/// Extension methods for consumption by scripts.
	/// </summary>
	static public class AppExtension
	{
		/// <summary>
		/// Hosting App provides the BrowserProcessConfig as specified in UI.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		static public IResultOrError<IBrowserProcess> CreateProcess(this IApp app) => app.CreateProcess(null);

		/// <summary>
		/// Hosting App provides the BrowserProcessConfig as specified in UI.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		static public IResultOrError<IBrowserProcess> ReuseProcess(this IApp app) => app.ReuseProcess(null);

		static public IResultOrError<IBrowserProcess> ReuseOrCreateProcess(
			this IApp app,
			Interface.BrowserProcessConfig browserProcessConfig = null)
		{
			var Process = app?.ReuseProcess(browserProcessConfig);

			if (!(Process?.Result).CanReuseProcess(browserProcessConfig))
			{
				Process?.Result?.Kill();

				Process = app?.CreateProcess(browserProcessConfig);
			}

			return Process;
		}

		static bool CanReuseProcess(
			this IBrowserProcess process,
			Interface.BrowserProcessConfig browserProcessConfig = null)
		{
			try
			{
				if (null == process)
					return false;

				if (null != browserProcessConfig)
					if (process?.Config?.ConfigMatchForReuse(browserProcessConfig) ?? false)
						return false;

				{
					var Connection = process?.ReuseConnection()?.Result;

					if (null != Connection)
						return true;
				}

				var SetBrowserId = process?.GetBrowsersIds();

				if (SetBrowserId.Failed())
					return false;

				foreach (var BrowserId in (SetBrowserId?.Result).EmptyIfNull())
				{
					if (!(BrowserId?.ConnectionOffered ?? false))
						continue;

					using (var Connection = process?.OpenConnection(BrowserId)?.Result)
					{
						if (Connection?.IsOpen ?? false)
							return true;
					}
				}
			}
			catch { }

			return false;
		}
	}
}
