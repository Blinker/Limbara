using Bib3;
using Limbara.Interface;
using Limbara.Interface.RemoteControl;
using System.Linq;

namespace Limbara.Script.ToScript
{
	static public class ProcessExtension
	{
		static public string[] SetBrowserTypeToIgnore => new[]
		{
			"background_page",
			"service_worker",
		};

		static public bool BrowserIdSuitable(IBrowserIdMeasurement browserId) =>
			null == browserId ? false :
			!SetBrowserTypeToIgnore.Contains(browserId.Type);

		static public IResultOrError<IBrowserConnection> ReuseConnection(this IBrowserProcess process)
		{
			foreach (var Connection in (process?.Connections).EmptyIfNull())
			{
				if (null == Connection)
					continue;

				if (null == Connection.Ping())
					return Connection.AsResultSuccess();
			}

			return null;
		}

		static public IResultOrError<IBrowserConnection> OpenConnection(this IBrowserProcess process)
		{
			var SetBrowserId = process?.GetBrowsersIds();

			if (null == SetBrowserId?.Result)
				return SetBrowserId.MapResult(_ => (IBrowserConnection)null);

			var SetBrowserIdSuitable =
				SetBrowserId?.Result?.Where(BrowserIdSuitable)?.ToArray();

			var BrowserId = SetBrowserIdSuitable?.FirstOrDefault(c => c?.ConnectionOffered ?? false);

			return process?.OpenConnection(BrowserId);
		}

		static public IResultOrError<IBrowserConnection> ReuseOrOpenConnection(this IBrowserProcess process) =>
			process?.ReuseConnection() ?? process?.OpenConnection();
	}
}
