using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model;

namespace P3Backend.Data.Mapping.UsersConfiguration {
	public class IUserConfiguration : IEntityTypeConfiguration<IUser> {
		public void Configure(EntityTypeBuilder<IUser> builder) {

			builder.HasKey(a => a.Id);

			builder.Property(a => a.FirstName).IsRequired();
			builder.Property(a => a.LastName).IsRequired();
			builder.Property(a => a.Email).IsRequired();
		}
	}
}
