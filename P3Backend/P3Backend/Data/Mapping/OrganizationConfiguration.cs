using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping {
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization> {
        public void Configure(EntityTypeBuilder<Organization> builder) {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired();

            builder.HasMany(o => o.Employees).WithOne();
            builder.HasMany(o => o.ChangeManagers).WithOne();
            builder.HasOne(o => o.Portfolio);
        }
    }
}
