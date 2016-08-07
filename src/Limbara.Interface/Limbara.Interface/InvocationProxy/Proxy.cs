using BotEngine;
using BotEngine.InvocationProxy;
using BotEngine.InvocationProxy.SerialStruct;
using Limbara.Interface.RemoteControl;

namespace Limbara.Interface.InvocationProxy
{
	public interface IProxy
	{
		IApp AppObject { get; }
	}

	public class Proxy : BotEngine.InvocationProxy.Proxy, IProxy
	{
		readonly new IHost Host;

		public IApp AppObject
		{
			get
			{
				var remoteObject = Host?.GetAppObject()?.DeserializeFromString<RemoteObject>();

				if (remoteObject == null)
					return null;

				return (IApp)ClrRefFromRemoteObject(remoteObject);
			}
		}

		public Proxy(IHost host)
			:
			base(host, InvocationProxy.Config.RemotingConfig)
		{
			this.Host = host;
		}
	}
}
