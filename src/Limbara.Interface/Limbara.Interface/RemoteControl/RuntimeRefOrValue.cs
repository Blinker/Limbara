using System.Collections.Generic;

namespace Limbara.Interface.RemoteControl
{
	public interface IRuntimeRef
	{
		IResultOrError<IRuntimeRefOrValue> JavascriptCallFunction(string function, IEnumerable<IRuntimeRefOrValue> listArg = null);
	}

	public interface IRuntimeRefOrValue
	{
		object value { get; }

		IRuntimeRef reference { get; }
	}

	public class RuntimeValue : IRuntimeRefOrValue
	{
		public object value;

		public IRuntimeRef reference => null;

		object IRuntimeRefOrValue.value => value;

		public RuntimeValue()
		{ }

		public RuntimeValue(object value)
		{
			this.value = value;
		}
	}
}
