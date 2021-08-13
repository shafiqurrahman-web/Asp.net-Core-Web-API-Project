using Microsoft.EntityFrameworkCore;
using Provenance.DataLayer.Base;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Repositories.Implementaions
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly DbSet<User> _dbSet;

		public UserRepository (IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_dbSet = unitOfWork.GetCollection<User>();
		}
	}


}
