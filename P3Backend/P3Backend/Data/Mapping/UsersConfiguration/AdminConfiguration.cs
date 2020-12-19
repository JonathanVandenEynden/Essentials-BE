using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Users;

namespace P3Backend.Data.Mapping.UsersConfiguration {
    public class AdminConfiguration : IEntityTypeConfiguration<Admin> {
        public void Configure(EntityTypeBuilder<Admin> builder) {


            builder.HasMany(a => a.Organizations);

        }
    }
}
