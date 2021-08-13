using Provenance.Common.Responses;
using Provenance.ServiceLayer.DTOs.ProductHistory;
using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface IProductHistoryService
	{
		Result Add (AddProductHistoryDTO inputData);
		Result Get (Guid id);
		Result Update (Guid id, UpdateProductHistoryDTO inputData);
		Result Delete (Guid id);
		Result GetAll ();
		PageableResult GetAll (int page, int pageSize);
		Result GetProductHistoryByProductId (Guid id);
	}
}
