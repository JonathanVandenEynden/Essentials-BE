using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping {
	public class ProjectConfiguration : IEntityTypeConfiguration<Project> {
		public void Configure(EntityTypeBuilder<Project> builder) {

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Name).IsRequired();

			builder.HasMany(p => p.ChangeInitiatives);
		}
	}
}
