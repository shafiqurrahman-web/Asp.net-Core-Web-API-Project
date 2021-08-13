using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Provenance.DataLayer.Base
{
	public interface IDbContext
	{
		void SaveChanges ();
		Task SaveChangesAsync (CancellationToken cancellationToken);
		DbSet<T> GetDbSet<T> () where T : class;
	}
}