using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Provenance.DataLayer.Entities;
using System;

namespace Provenance.DataLayer.Configurations
{
	public class RoleConfigurations : IEntityTypeConfiguration<Role>
	{
		public void Configure (EntityTypeBuilder<Role> builder)
		{

			builder.ToTable("Role");
			builder.HasKey(e => e.Id);
			builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

			builder.HasData(new Role { Id = new Guid("7CF9E80E-0FFC-4BD3-AA72-4EC80743545D"), Name = "Admin" });
			builder.HasData(new Role { Id = new Guid("68EEC1B9-BB00-4A92-A01D-0CD2305722F1"), Name = "User" });

		}
	}
}
