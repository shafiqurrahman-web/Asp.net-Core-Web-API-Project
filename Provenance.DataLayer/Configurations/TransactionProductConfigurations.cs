using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{
	public class TransactionProductConfigurations : IEntityTypeConfiguration<TransactionProduct>
	{
		public void Configure (EntityTypeBuilder<TransactionProduct> builder)
		{

			builder.ToTable("TransactionProduct");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.CreateDate).IsRequired().HasColumnType("datetime");

			builder.HasOne(e => e.Transaction)
				.WithMany(e => e.TransactionProducts)
				.HasForeignKey(e => e.TransactionId);

			builder.HasOne(e => e.Product)
				.WithMany(e => e.TransactionProducts)
				.HasForeignKey(e => e.ProductId);


		}
	}
}
