using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping.AssesmentConfiguration {
    public class IAssessmentConfiguration : IEntityTypeConfiguration<IAssessment> {
        public void Configure(EntityTypeBuilder<IAssessment> builder) {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.Feedback);
        }
    }
}
