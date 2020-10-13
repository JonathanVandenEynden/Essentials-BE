using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping {
	public class OrganizationConfiguration : IEntityTypeConfiguration<Organization> {
		public void Configure(EntityTypeBuilder<Organization> builder) {

			builder.HasKey(o => o.Id);

			builder.Property(o => o.Name).IsRequired();

			builder.HasOne(o => o.Portfolio);
		}
	}
}
