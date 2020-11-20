using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
	public class IAssessmentConfiguration : IEntityTypeConfiguration<IAssessment> {
		public void Configure(EntityTypeBuilder<IAssessment> builder) {
			builder.HasKey(s => s.Id);

			builder.HasMany(s => s.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(s => s.Feedback);

			// see FK in IAssesment class
			//builder.HasOne(a => a.RoadMapItem);


		}
	}
}
