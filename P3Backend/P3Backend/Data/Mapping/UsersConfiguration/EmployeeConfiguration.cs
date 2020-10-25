using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3Backend.Data.Mapping.UsersConfiguration {
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee> {
		public void Configure(EntityTypeBuilder<Employee> builder) {

			builder.HasMany(e => e.EmployeeOrganizationParts).WithOne(eo => eo.Employee);

		}
	}
}
