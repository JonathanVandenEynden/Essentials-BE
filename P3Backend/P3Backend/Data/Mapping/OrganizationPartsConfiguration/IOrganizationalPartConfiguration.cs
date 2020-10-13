using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.OrganizationPartsConfiguration {
	public class IOrganizationalPartConfiguration : IEntityTypeConfiguration<IOrganizationPart> {
		public void Configure(EntityTypeBuilder<IOrganizationPart> builder) {

			builder.HasKey(o => o.Id);

			builder.Property(o => o.Name).IsRequired();
		}
	}
}
