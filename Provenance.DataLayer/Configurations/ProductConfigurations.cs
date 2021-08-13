using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{

	public class ProductConfigurations : IEntityTypeConfiguration<Product>
	{
		public void Configure (EntityTypeBuilder<Product> builder)
		{

			builder.ToTable("Product");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Code).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Name).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Description).IsRequired(false).HasMaxLength(2000);

		}
	}

}
