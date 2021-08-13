using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;

namespace Provenance.DataLayer.Configurations
{

	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure (EntityTypeBuilder<User> builder)
		{

			builder.ToTable("User");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Email).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Firstname).IsRequired(false).HasMaxLength(300);
			builder.Property(e => e.Lastname).IsRequired(false).HasMaxLength(300);
			builder.Property(e => e.Name).IsRequired().HasMaxLength(300);
			builder.Property(e => e.Password).IsRequired(false).HasMaxLength(500);

			
			builder
				.HasOne(e => e.Role)
				.WithOne(u => u.User)
				.HasForeignKey<User>(c => c.RoleId);

			builder.HasData(new User
			{
				Id = new System.Guid("6AE7B75E-C101-4287-ACA5-38AFF787FD9C"),
				Firstname = "mohammadreza",
				Lastname = "Tarkhan",
				Email = "Mohammadreza.tarkhan@gmail.com",
				Name = "Mohammadreza Tarkhan",
				RoleId = new System.Guid("7CF9E80E-0FFC-4BD3-AA72-4EC80743545D")
			});


		}
	}

}
