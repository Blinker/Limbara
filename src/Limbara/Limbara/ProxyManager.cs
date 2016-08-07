using BotEngine.InvocationProxy;
using Limbara.Interface.InvocationProxy;
using System;

namespace Limbara
{
	public class ProxyManager
	{
		Tuple<IHost, IProxy> Current;

		public IProxy GetProxy(IHost host)
		{
			if (null == host)
				return null;

			if (!(Current?.Item1 == host))
				Current = new Tuple<IHost, IProxy>(host, new Interface.InvocationProxy.Proxy(host));

			return Current?.Item2;
		}
	}
}
