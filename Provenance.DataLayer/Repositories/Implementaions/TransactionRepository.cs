using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.DataObjects;
using Provenance.DataLayer.Entities;
using System;
using System.Linq;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class TransactionRepository : Repository<Transaction>, ITransactionRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<Transaction> _dbSet;

		public TransactionRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<Transaction>();
		}

		public TransactionDataObject GetTransaction (Guid id)
		{
			return _dbSet.Where(e => e.Id.Equals(id))
				.Include(e => e.Provider)
				.Include(e => e.Customer)
				.Select(e => new TransactionDataObject
				{
					Id = e.Id,
					CreateDate = e.CreateDate,
					CustomerId = e.CustomerId,
					CustomerName = e.Customer.Name,
					ProviderId = e.ProviderId,
					ProviderName = e.Provider.Name,
					IsCompleted = e.IsCompleted
				})
				.FirstOrDefault();
		}
	}

}
