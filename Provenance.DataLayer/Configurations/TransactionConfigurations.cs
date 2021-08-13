using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{

	public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
	{
		public void Configure (EntityTypeBuilder<Transaction> builder)
		{

			builder.ToTable("Transaction");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.CreateDate).IsRequired().HasColumnType("datetime");

			builder.HasOne(e => e.Provider)
				.WithMany(e => e.Transactions)
				.HasForeignKey(e => e.ProviderId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.Customer)
				.WithMany(e => e.Transactions)
				.HasForeignKey(e => e.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);


		}
	}

}
