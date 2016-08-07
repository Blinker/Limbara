using System;

namespace Limbara.Interface.RemoteControl
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public interface IBrowserIdMeasurement
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		string Id { get; }

		/// <summary>
		/// The type accessible via the UI seems to be "page".
		/// </summary>
		string Type { get; }

		/// <summary>
		/// url at the time of measurement.
		/// </summary>
		string Url { get; }

		/// <summary>
		/// title at the time of measurement.
		/// </summary>
		string Title { get; }

		/// <summary>
		/// whether the hosting process has offered to connect to this browser.
		/// </summary>
		bool ConnectionOffered { get; }
	}

	/// <summary>
	/// Keep in mind that the browser will close the connection when chrome devtools are attached to the browser.
	/// </summary>
	public interface IBrowserConnection : IDisposable
	{
		/// <summary>
		/// A past measurement. changing members such as Url or Title do not reflect the current state.
		/// </summary>
		IBrowserIdMeasurement BrowserId { get; }

		bool IsOpen { get; }

		IResultOrError<IDocument> Document { get; }

		/// <summary>
		/// close the connection, making it easier for others to connect to the browser.
		/// </summary>
		void Close();

		/// <summary>
		/// returns null if ping was successful.
		/// </summary>
		/// <returns></returns>
		IError Ping();

		IResultOrError<IRuntimeRefOrValue> JavascriptEval(string expression);

		int? BrowserAddressTcp { get; }
	}
}
