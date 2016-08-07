namespace Limbara.Interface.RemoteControl
{
	static public class Extension
	{
		static public bool IsReference(this IRuntimeRefOrValue runtimeRefOrValue) => null != runtimeRefOrValue?.reference;

		static public bool IsValue(this IRuntimeRefOrValue runtimeRefOrValue) => !(runtimeRefOrValue.IsReference());
	}
}
