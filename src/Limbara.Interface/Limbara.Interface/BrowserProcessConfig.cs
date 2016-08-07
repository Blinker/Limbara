namespace Limbara.Interface
{
	public class BrowserProcessConfig
	{
		/// <summary>
		/// Path to the executable file.
		/// </summary>
		public string ChromeExePath;

		/// <summary>
		/// If the addresses in the range between bound A and bound B are already occupied, connecting to the process will fail.
		/// </summary>
		public int? AddressTcpRangeBoundA;

		/// <summary>
		/// If the addresses in the range between bound A and bound B are already occupied, connecting to the process will fail.
		/// </summary>
		public int? AddressTcpRangeBoundB;

		/// <summary>
		/// https://www.chromium.org/user-experience/user-data-directory
		/// A unique (in the set of currently running processes) user-data-dir should be chosen to create a new process.
		/// </summary>
		public string UserDataDir;

		public string UrlToNavigateTo;
	}
}
