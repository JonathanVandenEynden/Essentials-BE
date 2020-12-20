using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping {
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio> {
        public void Configure(EntityTypeBuilder<Portfolio> builder) {

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Projects);
        }
    }
}
