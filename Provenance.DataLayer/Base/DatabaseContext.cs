using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Provenance.DataLayer.Configurations;
using Provenance.DataLayer.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Provenance.DataLayer.Base
{
	public class DatabaseContext : DbContext, IDbContext
	{
		public DatabaseContext () : base() { }
		public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options) { }


		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", false, false)
				.Build();

			var appSettingRecord = "ConnectionStrings:default";
			var con = configuration[appSettingRecord];
			optionsBuilder.UseSqlServer(con);
		}


		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductHistory> ProductHistories { get; set; }
		public virtual DbSet<Provider> Providers { get; set; }
		public virtual DbSet<TransactionProduct> TransactionProducts { get; set; }
		public virtual DbSet<Transaction> Transactions { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Role> Roles { get; set; }


		protected override void OnModelCreating (ModelBuilder builder)
		{

			// it should be placed here, otherwise it will rewrite the following settings!
			base.OnModelCreating(builder);


			// Custom mappings

			builder.ApplyConfiguration(new ProductConfigurations());

			builder.ApplyConfiguration(new ProductHistoryConfigurations());

			builder.ApplyConfiguration(new ProviderConfigurations());

			builder.ApplyConfiguration(new RoleConfigurations());

			builder.ApplyConfiguration(new TransactionConfigurations());

			builder.ApplyConfiguration(new TransactionProductConfigurations());

			builder.ApplyConfiguration(new UserConfigurations());



		}

		void IDbContext.SaveChanges () => SaveChanges();

		async Task IDbContext.SaveChangesAsync (CancellationToken cancellationToken) => await SaveChangesAsync();

		DbSet<T> IDbContext.GetDbSet<T> () => Set<T>();
	}
}
