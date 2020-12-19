using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.ChangeTypes;

namespace P3Backend.Data.Mapping.ChangeTypesConfiguration {
    public class OrganizationalChangeTypeConfiguration : IEntityTypeConfiguration<OrganizationalChangeType> {
        public void Configure(EntityTypeBuilder<OrganizationalChangeType> builder) {

        }
    }
}
