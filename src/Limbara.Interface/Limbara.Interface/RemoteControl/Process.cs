using System;
using System.Collections.Generic;

namespace Limbara.Interface.RemoteControl
{
	/// <summary>
	/// a browser process can host multiple browsers and has one address to connect to.
	/// </summary>
	public interface IBrowserProcess : IDisposable
	{
		BrowserProcessConfig Config { get; }

		/// <summary>
		/// id given from operating system.
		/// </summary>
		int? ProcessIdFromOS { get; }

		/// <summary>
		/// requests identifiers of browsers hosted in the process.
		/// </summary>
		/// <returns></returns>
		IResultOrError<IEnumerable<IBrowserIdMeasurement>> GetBrowsersIds();

		/// <summary>
		/// opening the connection will fail if another connection to the browser is open.
		/// </summary>
		/// <param name="browserId"></param>
		/// <returns></returns>
		IResultOrError<IBrowserConnection> OpenConnection(IBrowserIdMeasurement browserId);

		/// <summary>
		/// known connections to browsers in this process.
		/// </summary>
		IEnumerable<IBrowserConnection> Connections { get; }

		bool IsAlive { get; }

		void Kill();
	}
}
