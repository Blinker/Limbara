using BotSharp.ToScript.Extension;
using Limbara.Interface.RemoteControl;
using System;

namespace Limbara.Script.ToScript
{
	static public class DocumentExtension
	{
		static public bool WaitForReadyStateCompleteWithTimeout(
			this IDocument document,
			int durationMaxMilli)
		{
			if (null == document)
				return true;

			return new Func<bool>(() => !(document.readyState == "complete")).WaitWhile(durationMaxMilli);
		}
	}
}
