using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Users;

namespace P3Backend.Data.Mapping.UsersConfiguration {
	public class ChangeManagerConfiguration : IEntityTypeConfiguration<ChangeManager> {
		public void Configure(EntityTypeBuilder<ChangeManager> builder) {

			builder.HasMany(cm => cm.CreatedChangeInitiatives).WithOne();
		}
	}
}
