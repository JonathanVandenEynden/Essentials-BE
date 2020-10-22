using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
	public class SurveyConfiguration : IEntityTypeConfiguration<Survey> {
		public void Configure(EntityTypeBuilder<Survey> builder) {

		}
	}
}
