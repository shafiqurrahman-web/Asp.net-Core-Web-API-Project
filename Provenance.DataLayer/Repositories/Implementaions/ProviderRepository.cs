using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class ProviderRepository : Repository<Provider>, IProviderRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<Provider> _dbSet;

		public ProviderRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<Provider>();
		}
	}

}
