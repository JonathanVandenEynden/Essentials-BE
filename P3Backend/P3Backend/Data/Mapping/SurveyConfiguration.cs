using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping {
	public class SurveyConfiguration : IEntityTypeConfiguration<Survey> {
		public void Configure(EntityTypeBuilder<Survey> builder) {
			builder.HasKey(s => s.Id);

			builder.Property(s => s.AmountSubmitted);

			builder.HasMany(s => s.Questions).WithOne();
			builder.HasOne(s => s.Feedback).WithOne();

		}
	}
}
