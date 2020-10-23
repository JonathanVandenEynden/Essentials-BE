using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.ChangeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.ChangeTypesConfiguration {
	public class PersonalChangeTypeConfiguration : IEntityTypeConfiguration<PersonalChangeType> {
		public void Configure(EntityTypeBuilder<PersonalChangeType> builder) {

		}
	}
}
