using Limbara.Interface.InvocationProxy;

namespace Limbara
{
	public class SimpleInterfaceServerDispatcher : BotEngine.Interface.SimpleInterfaceServerDispatcher
	{
		readonly ProxyManager ProxyManager = new ProxyManager();

		public BotEngine.Interface.InterfaceAppManager InterfaceAppManagerOverride;

		BotEngine.Interface.InterfaceAppManager InterfaceAppManagerCumulative => InterfaceAppManagerOverride ?? base.InterfaceAppManager;

		IProxy InterfaceRemoteControlProxy =>
			ProxyManager?.GetProxy(InterfaceAppManagerCumulative?.AppImplementationOfType<BotEngine.InvocationProxy.IHost>());

		Interface.RemoteControl.IApp appInterface;

		public Interface.RemoteControl.IApp AppInterface
		{
			get
			{
				return appInterface = InterfaceRemoteControlProxy?.AppObject;
			}
		}

		public override bool AppInterfaceAvailable => null != AppInterface;
	}
}
