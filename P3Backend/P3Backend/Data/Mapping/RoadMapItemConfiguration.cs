using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping {
	public class RoadMapItemConfiguration : IEntityTypeConfiguration<RoadMapItem> {
		public void Configure(EntityTypeBuilder<RoadMapItem> builder) {

			builder.HasKey(r => r.Id);

			builder.Property(r => r.Title).IsRequired();
			builder.Property(r => r.Done).IsRequired();
			builder.Property(r => r.StartDate).IsRequired();
			builder.Property(r => r.EndDate).IsRequired();

			builder.HasOne(r => r.Assessment);
		}
	}
}
