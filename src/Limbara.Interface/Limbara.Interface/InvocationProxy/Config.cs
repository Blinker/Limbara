using BotEngine.InvocationProxy;
using Limbara.Interface.RemoteControl;
using System.Collections.Generic;

namespace Limbara.Interface.InvocationProxy
{
	static public class Config
	{
		static public RemotingConfig RemotingConfig => new RemotingConfig()
		{
			ListInterfaceEnabled = new[]
			{
				typeof(IError),
				typeof(ITimeoutError),
				typeof(INotConnectedError),

				typeof(IApp),
				typeof(IBrowserProcess),
				typeof(IBrowserIdMeasurement),
				typeof(IBrowserConnection),
				typeof(IDocument),
				typeof(IHTMLIFrameElement),
				typeof(IHTMLElement),
				typeof(IRuntimeRef),
				typeof(IRuntimeRefOrValue),

				typeof(IResultOrError<IApp>),
				typeof(IResultOrError<IBrowserProcess>),
				typeof(IResultOrError<IBrowserIdMeasurement>),
				typeof(IResultOrError<IBrowserConnection>),
				typeof(IResultOrError<IDocument>),
				typeof(IResultOrError<IHTMLIFrameElement>),
				typeof(IResultOrError<IHTMLElement>),
				typeof(IResultOrError<IRuntimeRef>),
				typeof(IResultOrError<IRuntimeRefOrValue>),

				typeof(IEnumerable<IBrowserProcess>),
				typeof(IEnumerable<IBrowserIdMeasurement>),
				typeof(IEnumerable<IBrowserConnection>),
				typeof(IEnumerable<IDocument>),
				typeof(IEnumerable<IHTMLIFrameElement>),
				typeof(IEnumerable<IHTMLElement>),
				typeof(System.Collections.IEnumerable),

				typeof(IResultOrError<IEnumerable<IBrowserProcess>>),
				typeof(IResultOrError<IEnumerable<IBrowserIdMeasurement>>),
				typeof(IResultOrError<IEnumerable<IBrowserConnection>>),
				typeof(IResultOrError<IEnumerable<IDocument>>),
				typeof(IResultOrError<IEnumerable<IHTMLIFrameElement>>),
				typeof(IResultOrError<IEnumerable<IHTMLElement>>),
				typeof(IResultOrError<System.Collections.IEnumerable>),

				typeof(IEnumerator<IBrowserProcess>),
				typeof(IEnumerator<IBrowserIdMeasurement>),
				typeof(IEnumerator<IBrowserConnection>),
				typeof(IEnumerator<IDocument>),
				typeof(IEnumerator<IHTMLIFrameElement>),
				typeof(IEnumerator<IHTMLElement>),
				typeof(System.Collections.IEnumerator),
			},

			SetValueTypeEnabled = new[]
			{
				typeof(BrowserProcessConfig),
				typeof(RuntimeValue),
			},
		};
	}
}
