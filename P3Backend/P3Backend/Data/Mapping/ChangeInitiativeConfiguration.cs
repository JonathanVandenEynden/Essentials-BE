using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping {
	public class ChangeInitiativeConfiguration : IEntityTypeConfiguration<ChangeInitiative> {
		public void Configure(EntityTypeBuilder<ChangeInitiative> builder) {

			builder.HasKey(ci => ci.Id);


			builder.Property(ci => ci.Name).IsRequired();
			builder.Property(ci => ci.Description).IsRequired();
			builder.Property(ci => ci.StartDate).IsRequired();
			builder.Property(ci => ci.EndDate).IsRequired();

			builder.HasOne(ci => ci.ChangeType);
			builder.HasOne(ci => ci.ChangeSponsor);
			builder.HasOne(ci => ci.ChangeGroup);

			builder.HasMany(ci => ci.RoadMap).WithOne();
		}
	}
}
