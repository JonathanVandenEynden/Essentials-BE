using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.ChangeTypes;

namespace P3Backend.Data.Mapping.ChangeTypesConfiguration {
	public class PersonalChangeTypeConfiguration : IEntityTypeConfiguration<PersonalChangeType> {
		public void Configure(EntityTypeBuilder<PersonalChangeType> builder) {

		}
	}
}
