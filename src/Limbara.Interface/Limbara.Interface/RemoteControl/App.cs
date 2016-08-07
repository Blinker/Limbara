using System;
using System.Collections.Generic;

namespace Limbara.Interface.RemoteControl
{
	public interface IApp : IDisposable
	{
		IResultOrError<IBrowserProcess> CreateProcess(BrowserProcessConfig config);

		IResultOrError<IBrowserProcess> ReuseProcess(BrowserProcessConfig config);

		IEnumerable<IBrowserProcess> Processes { get; }
	}

}
