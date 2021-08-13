using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{

	public class ProviderConfigurations : IEntityTypeConfiguration<Provider>
	{
		public void Configure (EntityTypeBuilder<Provider> builder)
		{

			builder.ToTable("Provider");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Code).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Name).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Description).IsRequired(false).HasMaxLength(2000);

			builder.HasOne(e => e.User)
				.WithOne(e => e.Provider)
				.HasForeignKey<Provider>(e => e.UserId);

		}
	}

}
