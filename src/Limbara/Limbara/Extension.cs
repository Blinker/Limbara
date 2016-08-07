namespace Limbara
{
	static public class Extension
	{
		static public BotEngine.Client.LicenseClientConfig CompletedWithDefault(
			this BotEngine.Client.LicenseClientConfig config)
		{
			config = config ?? ExeConfig.LicenseClientDefault;

			config.Request = config?.Request ?? ExeConfig.InterfaceLicenseClientRequestDefault;

			//	force use default ServiceId to prevent problems with old config file when user exchanges executable.
			config.Request.ServiceId = ExeConfig.ConfigServiceId;

			config.Request.Consume = true;

			return config;
		}
	}
}
