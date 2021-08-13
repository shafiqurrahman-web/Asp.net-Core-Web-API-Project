using System.Collections.Generic;

namespace Provenance.Common.Responses
{
	public class PageableResult : Result
	{
		protected PageableResult (object data, long count, int page, int pageSize, int code = 200, string message = null) : base(data, code, message)
		{
			Count = count;
			Page = page;
			PageSize = pageSize;
		}

		protected PageableResult (IEnumerable<string> errors, int code = 500, string message = null) : base(errors, code, message)
		{
		}

		protected PageableResult (string error, int code = 500, string message = null) : base(error, code, message)
		{
		}


		public long Count { get; private set; }
		public int Page { get; private set; }
		public int PageSize { get; private set; }


		public static PageableResult Ok (object data, long count, int page, int pageSize, int code = 200, string message = null)
		{
			var item = new PageableResult(data, count, page, pageSize, code, message);
			return item;
		}

	}
}
