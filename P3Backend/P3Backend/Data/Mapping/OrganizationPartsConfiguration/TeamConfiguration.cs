using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.OrganizationPartsConfiguration {
	public class TeamConfiguration : IEntityTypeConfiguration<Team> {
		public void Configure(EntityTypeBuilder<Team> builder) {

		}
	}
}
