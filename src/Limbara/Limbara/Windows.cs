using Bib3;
using System;
using System.IO;

namespace Limbara
{
	static public class WindowsGoogleChromeStatic
	{
		const string RegistryChromeInstallLocationKeyPathUser = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Google Chrome";
		const string RegistryChromeInstallLocationKeyPathMachine = @"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Google Chrome";
		const string RegistryChromeInstallLocationValueName = "InstallLocation";

		static public string ChromeDirectoryPathFromSystem = ChromeDirectoryPathFromSystemRead();
		static public string ChromeExePathFromSystem =
			null == ChromeDirectoryPathFromSystem ? null :
			(ChromeDirectoryPathFromSystem.EnsureEndsWith(Path.DirectorySeparatorChar.ToString()) + "chrome.exe");

		static string ChromeDirectoryPathFromSystemRead()
		{
			var listePrioKey = new string[]{
			RegistryChromeInstallLocationKeyPathUser,
			RegistryChromeInstallLocationKeyPathMachine};

			foreach (var prioKey in listePrioKey)
			{
				try
				{
					var chromeDirectoryPath = Microsoft.Win32.Registry.GetValue(RegistryChromeInstallLocationKeyPathUser, RegistryChromeInstallLocationValueName, null);

					var chromeDirectoryPathAlsString = chromeDirectoryPath as String;

					if (null != chromeDirectoryPathAlsString)
						return chromeDirectoryPathAlsString;
				}
				catch { }
			}

			return null;
		}
	}
}
