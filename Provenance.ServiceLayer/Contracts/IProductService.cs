using Provenance.Common.Responses;
using Provenance.ServiceLayer.DTOs.Product;
using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface IProductService
	{
		Result Add (AddProductDTO inputData);
		Result Get (Guid id);
		Result Update (Guid id, UpdateProductDTO inputData);
		Result Delete (Guid id);
		Result GetAll ();
		PageableResult GetAll (int page, int pageSize);
		Result GetByTransactionId (Guid id);

	}
}
