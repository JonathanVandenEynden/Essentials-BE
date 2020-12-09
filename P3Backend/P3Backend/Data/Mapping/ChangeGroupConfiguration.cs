using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping {
    public class ChangeGroupConfiguration : IEntityTypeConfiguration<ChangeGroup> {
        public void Configure(EntityTypeBuilder<ChangeGroup> builder) {

            builder.HasKey(cg => cg.Id);

            builder.Property(cg => cg.Name).IsRequired();

            builder.HasMany(cg => cg.Users);
        }
    }
}
