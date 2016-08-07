using System;

namespace Limbara.Interface
{
	public interface IError
	{
		string Message { get; }
	}

	public interface IResultOrError<ResultT>
	{
		ResultT Result { get; }

		IError Error { get; }
	}

	public interface IResultOrError<ResultT, ErrorT> : IResultOrError<ResultT>
		where ErrorT : IError
	{
		new ResultT Result { get; }

		new ErrorT Error { get; }
	}

	public interface INotConnectedError : IError
	{
	}

	public interface ITimeoutError : IError
	{
		int Duration { get; }
	}

	public class Error : IError
	{
		public Error(string message)
		{
			this.Message = message;
		}

		public string Message { private set; get; }
	}

	public class TimeoutError : Error, ITimeoutError
	{
		public int Duration { private set; get; }

		public TimeoutError(string message, int duration)
			:
			base(message)
		{
			this.Duration = duration;
		}
	}

	public class ResultOrError<ResultT> : IResultOrError<ResultT>
	{
		public ResultOrError(ResultT result, IError error = null)
		{
			this.Result = result;
			this.Error = error;
		}

		public ResultT Result { private set; get; }

		public IError Error { private set; get; }
	}

	static public class ErrorExtension
	{
		static public IResultOrError<ResultT> AsResultSuccess<ResultT>(this ResultT result) => new ResultOrError<ResultT>(result);

		static public IResultOrError<ResultT> AsResultError<ResultT>(this IError error) => new ResultOrError<ResultT>(default(ResultT), error);

		static public bool Succeeded<ResultT>(this IResultOrError<ResultT> resultOrError) => !resultOrError.Failed();

		static public bool Failed<ResultT>(this IResultOrError<ResultT> resultOrError) => null != resultOrError?.Error;

		static public IResultOrError<OutT> MapResult<InT, OutT>(this IResultOrError<InT> origin, Func<InT, OutT> map) =>
			new ResultOrError<OutT>(null == map ? default(OutT) : map.Invoke(origin.Result), origin.Error);
	}
}
