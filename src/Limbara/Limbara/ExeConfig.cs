namespace Limbara
{
	public class ExeConfig
	{
		public const string ConfigLicenseKeyDefault = "Limbara.Free";
		public const string ConfigServiceId = "Limbara.16-05-06";
		public const string ConfigApiVersionAddressDefault = @"http://service.botengine.de:4074/api";

		public const int RemoteControlAddressTcpDefaultBoundA = 3444;

		static public BotEngine.Client.AuthRequest InterfaceLicenseClientRequestDefault => new BotEngine.Client.AuthRequest
		{
			LicenseKey = ConfigLicenseKeyDefault,
			ServiceId = ConfigServiceId,
			Consume = true,
		};

		static public BotEngine.Client.LicenseClientConfig LicenseClientDefault => new BotEngine.Client.LicenseClientConfig
		{
			ApiVersionAddress = ConfigApiVersionAddressDefault,
			Request = InterfaceLicenseClientRequestDefault,
		};

		static public ExeConfig Default => new ExeConfig
		{
			LicenseClient = LicenseClientDefault,
			BrowserProcess = new Limbara.Interface.BrowserProcessConfig
			{
				AddressTcpRangeBoundA = RemoteControlAddressTcpDefaultBoundA,
			},
		};

		public BotEngine.Client.LicenseClientConfig LicenseClient;

		public Interface.BrowserProcessConfig BrowserProcess;

		static public Interface.BrowserProcessConfig BrowserProcessConfigDefaultCreate(
			Interface.BrowserProcessConfig @default,
			string userDataDir)
		{
			@default = @default ?? new Interface.BrowserProcessConfig();

			@default.UserDataDir = userDataDir;
			@default.ChromeExePath = @default.ChromeExePath ?? WindowsGoogleChromeStatic.ChromeExePathFromSystem;

			if (!@default.AddressTcpRangeBoundA.HasValue && !@default.AddressTcpRangeBoundB.HasValue)
				@default.AddressTcpRangeBoundA = ExeConfig.RemoteControlAddressTcpDefaultBoundA;

			return @default;
		}
	}
}
