using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
    public class PresetSurveyConfiguration : IEntityTypeConfiguration<PresetSurvey> {
        public void Configure(EntityTypeBuilder<PresetSurvey> builder) {
            
        }
    }
}