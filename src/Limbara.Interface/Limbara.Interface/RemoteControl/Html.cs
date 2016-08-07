namespace Limbara.Interface.RemoteControl
{
	/// <summary>
	/// Represents any HTML element. HTML elements may implement additional interfaces.
	/// </summary>
	public interface IHTMLElement : IRuntimeRef
	{
		/// <summary>
		/// Serialized HTML fragment describing the element including its descendants. It can be set to replace the element with nodes parsed from the given string.
		/// </summary>
		string outerHTML { set; get; }

		/// <summary>
		/// Serialized HTML fragment describing the elements contents including its descendants.
		/// </summary>
		string innerHTML { set; get; }

		string outerText { get; }

		string innerText { get; }

		/// <summary>
		/// Sends a mouse click event to the element.
		/// </summary>
		IError click();

		/// <summary>
		/// Makes the element the current keyboard focus.
		/// </summary>
		IError focus();

		string tagName { get; }

		string id { set; get; }

		/// <summary>
		/// Sets the value of a named attribute of the current node.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <param name="value"></param>
		IError setAttribute(string attributeName, string value);

		/// <summary>
		/// value of a specified attribute on the element. If the given attribute does not exist, the value returned will either be null or an empty string.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		IResultOrError<string> getAttribute(string attributeName);

		IHTMLElement parentElement { get; }
	}

	/// <summary>
	/// provides properties for manipulating the layout and presentation of inline frame elements.
	/// </summary>
	public interface IHTMLIFrameElement : IHTMLElement
	{
		/// <summary>
		/// The active document in the inline frame's nested browsing context.
		/// </summary>
		IDocument contentDocument { get; }

		/// <summary>
		/// URL of the embedded page.
		/// </summary>
		string src { set; get; }
	}
}
