using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;
using System;
using System.Collections.Generic;

namespace Provenance.DataLayer.Repositories
{
	public interface IProductRepository : IRepository<Product>
	{
		IEnumerable<Product> GetProductByTransactionId (Guid id);
	}
}
