using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.ChangeTypes;

namespace P3Backend.Data.Mapping.ChangeTypesConfiguration {
	public class IChangeTypeConfiguration : IEntityTypeConfiguration<IChangeType> {
		public void Configure(EntityTypeBuilder<IChangeType> builder) {
			builder.HasKey(c => c.Id);
		}
	}
}
