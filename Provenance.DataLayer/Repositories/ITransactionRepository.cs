using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Provenance.DataLayer.Repositories
{
	public interface ITransactionRepository : IRepository<Transaction>
	{
		TransactionDataObject GetTransaction (Guid id);
	}

}
