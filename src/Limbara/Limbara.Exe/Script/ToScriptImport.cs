using Bib3;
using System.Collections.Generic;
using System.Reflection;

namespace Limbara.Exe.Script
{
	static public class ToScriptImport
	{
		static public Microsoft.CodeAnalysis.MetadataReference AssemblySelfReference = Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.GetCallingAssembly().Location);

		static public IEnumerable<Microsoft.CodeAnalysis.MetadataReference> ImportAssembly =>
			Limbara.Script.ToScriptImport.ImportAssembly.ConcatNullable(new[] { AssemblySelfReference });
	}
}
