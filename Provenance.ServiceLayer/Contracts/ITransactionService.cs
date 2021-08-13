using Provenance.Common.Responses;
using Provenance.ServiceLayer.DTOs.Transaction;
using System;

namespace Provenance.ServiceLayer.Contracts
{
	public interface ITransactionService
	{
		Result Add (AddTransactionDTO inputData);
		Result Get (Guid id);
		Result Update (Guid id, UpdateTransactionDTO inputData);
		Result Delete (Guid id);
		Result GetAll ();
		PageableResult GetAll (int page, int pageSize);


		Result CompleteTransaction (Guid providerId);


		Result ScanShopCode (Guid customerId, Guid providerId);
		Result ScanProduct (Guid providerId, Guid productId);

	}
}
