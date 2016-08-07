using HtmlAgilityPack;

namespace Limbara
{
	static public class HtmlAgilityPackStatic
	{
		static public void StaticConfig()
		{
			HtmlNode.ElementsFlags.Remove("form");
		}
	}
}
