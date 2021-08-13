using System.Collections.Generic;

namespace Provenance.Common.Responses
{
	public class Result
	{

		protected Result (object data, int code = 200, string message = null)
		{
			Data = data;
			StatusCode = code;
			Status = "Successful";
			Message = message;
			Errors = null;
		}

		protected Result (IEnumerable<string> errors, int code = 500, string message = null)
		{
			Data = null;
			StatusCode = code;
			Status = "Failed";
			Message = message;
			Errors = errors;
			HasError = true;
		}

		protected Result (string error, int code = 500, string message = null)
		{
			Data = null;
			StatusCode = code;
			Status = "Failed";
			Message = message;
			var errors = new List<string>();
			errors.Add(error);
			Errors = errors;
			HasError = true;
		}




		public object Data { get; private set; }
		public int StatusCode { get; private set; }
		public string Status { get; private set; }
		public string Message { get; private set; }
		public bool HasError { get; set; }
		public IEnumerable<string> Errors { get; private set; }




		public static Result Ok (object data, int code = 200, string message = null)
		{
			var item = new Result(data, code, message);
			return item;
		}

		public static Result Error (IEnumerable<string> errors, int code = 500, string message = null)
		{
			var item = new Result(errors, code, message);
			return item;
		}

		public static Result Error (string error, int code = 500, string message = null)
		{
			var item = new Result(error, code, message);
			return item;
		}


	}
}
