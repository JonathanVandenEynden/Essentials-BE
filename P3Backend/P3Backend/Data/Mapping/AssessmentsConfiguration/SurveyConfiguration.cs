using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
	public class SurveyConfiguration : IEntityTypeConfiguration<Survey> {
		public void Configure(EntityTypeBuilder<Survey> builder) {

		}
	}
}
