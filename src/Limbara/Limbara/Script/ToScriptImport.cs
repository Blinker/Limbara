using Bib3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Limbara.Script
{
	static public class ToScriptImport
	{
		static readonly Type[] AssemblyAndNamespaceAdditionType = new[]
		{
			typeof(Interface.RemoteControl.IApp),
			typeof(Interface.IError),
			typeof(List<>),
			typeof(System.Text.RegularExpressions.Regex),
			typeof(Uri),
			typeof(Enumerable),
			typeof(ToScript.IHostToScript),
			typeof(BotSharp.ToScript.IHostToScript),
			typeof(BotEngine.Common.Extension),
			typeof(ToScript.AppExtension),
			typeof(BotEngine.Interface.HtmlAgilityPack.HAPExtension),
		};

		static readonly Type[] NamespaceStaticAdditionType = new Type[]
		{
			typeof(HtmlAgilityPack.HtmlDocument),
		};

		static IEnumerable<Type> AssemblyAdditionType => new[]
		{
			AssemblyAndNamespaceAdditionType,
			NamespaceStaticAdditionType,
		}.ConcatNullable();

		static public IEnumerable<string> AssemblyName =>
			AssemblyAdditionType?.Select(t => t.Assembly.GetName()?.Name)?.Distinct();

		static public IEnumerable<Microsoft.CodeAnalysis.MetadataReference> ImportAssembly =>
			AssemblyName?.Select(assemblyName => GetAssemblyReference(assemblyName));

		static public IEnumerable<string> ImportNamespace =>
			new[]
			{
				AssemblyAndNamespaceAdditionType?.Select(t => t.Namespace),
				NamespaceStaticAdditionType?.Select(t => t.FullName),
			}.ConcatNullable();

		static readonly Func<string, Stream> CosturaAssemblyResolver = Costura.AssemblyResolverCosturaConstruct();

		static public Microsoft.CodeAnalysis.MetadataReference GetAssemblyReference(string assemblyName)
		{
			var FromCosturaStream = CosturaAssemblyResolver?.Invoke(assemblyName);

			if (null != FromCosturaStream)
				return Microsoft.CodeAnalysis.MetadataReference.CreateFromStream(FromCosturaStream);

			return AssemblyReferenceFromLoadedAssembly(assemblyName);
		}

		static Microsoft.CodeAnalysis.MetadataReference AssemblyReferenceFromLoadedAssembly(string assemblyName)
		{
			var Assembly = AppDomain.CurrentDomain.GetAssemblies()?.FirstOrDefault(candidate => candidate?.GetName()?.Name == assemblyName);

			if (null == Assembly)
				return null;

			return Microsoft.CodeAnalysis.MetadataReference.CreateFromFile(Assembly.Location);
		}
	}
}
