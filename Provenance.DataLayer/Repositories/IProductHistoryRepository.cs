using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Provenance.DataLayer.Repositories
{
	public interface IProductHistoryRepository : IRepository<ProductHistory>
	{
		ProductHistoryDataObject GetById (Guid id);
		IEnumerable<ProductHistoryDataObject> GetByCondition (Expression<Func<ProductHistory, bool>> predicate);
		IEnumerable<ProductHistoryDataObject> GetProductHistoryByProductId (Guid id);

	}
}
