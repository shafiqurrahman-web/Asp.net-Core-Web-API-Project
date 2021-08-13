using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<Role> _dbSet;

		public RoleRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<Role>();
		}
	}
}
