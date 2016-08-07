using System.Collections.Generic;

namespace Limbara.Interface.RemoteControl
{
	public interface IDocument
	{
		/// <summary>
		/// The whole URL.
		/// Assign to this member to initiate navigation to the passed url.
		/// Assigning to this member will eventually result in creation of a new document. This document reference will then not be useful anymore.
		/// </summary>
		string locationHref { set; get; }

		/// <summary>
		/// starts reloading the resource from the current URL.
		/// This will eventually result in creation of a new document. This document reference will then not be useful anymore.
		/// </summary>
		IError locationReload();

		/// <summary>
		/// Element which is the root element of the document (for example, the <html> element for HTML documents).
		/// </summary>
		IHTMLElement documentElement { get; }

		/// <summary>
		/// Element which is the root element of the document (for example, the <html> element for HTML documents).
		/// </summary>
		IResultOrError<IHTMLElement> getDocumentElement();

		/// <summary>
		/// Returns the list of elements which match the passed XPath.
		/// https://en.wikipedia.org/wiki/XPath
		/// </summary>
		/// <param name="xPath"></param>
		/// <returns></returns>
		IResultOrError<IEnumerable<IHTMLElement>> GetListElementFromXPath(string xPath);

		/// <summary>
		/// Returns the first element which matches the passed XPath.
		/// https://en.wikipedia.org/wiki/XPath
		/// </summary>
		/// <param name="xPath"></param>
		/// <returns></returns>
		IResultOrError<IHTMLElement> GetElementFromXPath(string xPath);

		/// <summary>
		/// https://developer.mozilla.org/en-US/docs/Web/API/Document/getElementById
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IResultOrError<IHTMLElement> getElementById(string id);

		/// <summary>
		/// loading state of the document.
		/// http://www.w3.org/TR/html51/dom.html#dom-document-readystate
		/// </summary>
		string readyState { get; }
	}

}
