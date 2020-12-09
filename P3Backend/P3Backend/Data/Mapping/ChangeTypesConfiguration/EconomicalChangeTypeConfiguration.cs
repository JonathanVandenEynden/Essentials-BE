using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.ChangeTypes;

namespace P3Backend.Data.Mapping.ChangeTypesConfiguration {
    public class EconomicalChangeTypeConfiguration : IEntityTypeConfiguration<EconomicalChangeType> {
        public void Configure(EntityTypeBuilder<EconomicalChangeType> builder) {

        }
    }
}
