using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{

	public class ProductHistoryConfigurations : IEntityTypeConfiguration<ProductHistory>
	{
		public void Configure (EntityTypeBuilder<ProductHistory> builder)
		{

			builder.ToTable("ProductHistory");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Title).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Content).IsRequired();
			builder.Property(e => e.Picture).IsRequired(false);
			builder.Property(e => e.LocationTitle).IsRequired(false).HasMaxLength(300);
			builder.Property(e => e.LocationAddress).IsRequired(false).HasMaxLength(2000);
			builder.Property(e => e.Date).IsRequired().HasColumnType("datetime");

			builder.HasOne(e => e.Product)
				.WithMany(e => e.ProductHistorys)
				.HasForeignKey(e => e.ProductId);

		}
	}
}
