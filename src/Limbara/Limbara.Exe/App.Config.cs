using Bib3;
using BotEngine;
using BotEngine.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Limbara.Exe
{
	public partial class App
	{
		static public string ConfigFilePath =>
			Bib3.FCL.Glob.ZuProcessSelbsctMainModuleDirectoryPfaadBerecne().PathToFilesysChild("config");

		BotEngine.UI.WriteToOrReadFromFile ConfigFileControl =>
			Window?.Main?.ConfigFileControl;

		string ScriptDirectoryPath => AssemblyDirectoryPath.PathToFilesysChild(@"script\");

		string LicenseKeyStoreFilePath => AssemblyDirectoryPath.PathToFilesysChild(@"license.key");

		string DefaultScriptPath => ScriptDirectoryPath.PathToFilesysChild("default.cs");

		KeyValuePair<string, string>[] ListScriptIncluded =
			SetScriptIncludedConstruct()?.ExceptionCatch(Bib3.FCL.GBS.Extension.MessageBoxException)
			?.OrderBy(scriptNameAndContent => !scriptNameAndContent.Key.RegexMatchSuccessIgnoreCase("travel"))
			?.ToArray();

		static IEnumerable<KeyValuePair<string, string>> SetScriptIncludedConstruct()
		{
			var assembly = typeof(App).Assembly;

			var setResourceName = assembly?.GetManifestResourceNames();

			var scriptPrefix = assembly.GetName().Name + ".sample.script.";

			foreach (var resourceName in setResourceName.EmptyIfNull())
			{
				var scriptIdMatch = resourceName.RegexMatchIfSuccess(Regex.Escape(scriptPrefix) + @"(.*)");

				if (null == scriptIdMatch)
					continue;

				var scriptUTF8 = assembly.GetManifestResourceStream(resourceName)?.LeeseGesamt();

				if (null == scriptUTF8)
					continue;

				yield return new KeyValuePair<string, string>(scriptIdMatch?.Groups?[1]?.Value, Encoding.UTF8.GetString(scriptUTF8));
			}
		}

		public ExeConfig ConfigReadFromUI()
		{
			return Window?.Main?.ConfigFromViewToModel();
		}

		public void ConfigWriteToUI(ExeConfig config)
		{
			Window?.Main?.ConfigFromModelToView(config);
		}

		public byte[] ConfigReadFromUISerialized() => ConfigReadFromUI().SerializeToUtf8();

		public void ConfigWriteToUIDeSerialized(byte[] config) => ConfigWriteToUI(config.DeserializeFromUtf8<ExeConfig>());
	}
}
