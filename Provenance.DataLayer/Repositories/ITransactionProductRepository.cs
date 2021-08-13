using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;

namespace Provenance.DataLayer.Repositories
{
	public interface ITransactionProductRepository : IRepository<TransactionProduct>
	{
		TransactionProductDataObject GetTransactionProductData (Guid id);
	}
}
