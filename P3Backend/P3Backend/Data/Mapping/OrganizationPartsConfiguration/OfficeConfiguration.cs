using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.OrganizationParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.OrganizationPartsConfiguration {
	public class OfficeConfiguration : IEntityTypeConfiguration<Office> {
		public void Configure(EntityTypeBuilder<Office> builder) {

		}
	}
}
