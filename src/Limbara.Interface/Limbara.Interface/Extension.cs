using Bib3;
using System.Linq;

namespace Limbara.Interface
{
	static public class Extension
	{
		static public bool AddressBoundsContainAddress(this BrowserProcessConfig config, int address)
		{
			var setBoundNullable = new[] { config?.AddressTcpRangeBoundA, config?.AddressTcpRangeBoundB };

			var setBound =
				setBoundNullable.WhereNotDefault().ToArray();

			if (!(0 < setBound.Length))
				return false;

			return setBound.Min() <= address && address <= setBound.Max();
		}

		static public bool ConfigMatchForReuse(this BrowserProcessConfig existingConfig, BrowserProcessConfig configToReuse) =>
			existingConfig?.ChromeExePath == configToReuse?.ChromeExePath &&
			(configToReuse?.AddressBoundsContainAddress(existingConfig?.AddressTcpRangeBoundA ?? existingConfig?.AddressTcpRangeBoundB ?? 0) ?? false) &&
			existingConfig?.UserDataDir == configToReuse?.UserDataDir;
	}
}
