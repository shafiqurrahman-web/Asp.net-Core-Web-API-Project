using Microsoft.Extensions.DependencyInjection;
using Provenance.DataLayer.Base;
using System.Linq;

namespace Provenance.ServiceLayer.Bootstrapper
{
	public static class DependecyInitializer
	{

		public static IServiceCollection Init (this IServiceCollection services)
		{
			services.AddDataServices();
			services.AddBusinessServices();

			return services;
		}


		public static IServiceCollection AddBusinessServices (this IServiceCollection services)
		{

			var assm = typeof(DependecyInitializer).Assembly;

			//services
			var repoSet =
					 from type in assm.GetExportedTypes()
					 where type.Namespace.EndsWith("ServiceLayer.Services")
					 where type.GetInterfaces().Any()
					 select new { Service = type.GetInterfaces().Single(), Implementation = type };


			foreach (var reg in repoSet)
				services.AddScoped(reg.Service, reg.Implementation);

			return services;
		}


		public static IServiceCollection AddDataServices (this IServiceCollection services)
		{
			services.AddDbContext<IDbContext, DatabaseContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			var assm = typeof(IUnitOfWork).Assembly;

			//repositories
			var repoSet =
					 from type in assm.GetExportedTypes()
					 where type.Namespace.EndsWith("Repositories.Implementaions")
					 where type.GetInterfaces().Any()
					 select new { Service = type.GetInterfaces().Where(e => e.Name.EndsWith(type.Name)).Single(), Implementation = type };

			foreach (var reg in repoSet)
				services.AddScoped(reg.Service, reg.Implementation);

			return services;

		}


	}


}


