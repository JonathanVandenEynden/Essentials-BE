using P3Backend.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace P3Backend.Model.DTO_s {
	public class OrganizationDTO {
		public string Name { get; set; }
		public List<EmployeeDTO> EmployeeDTOs { get; set; }
		public EmployeeDTO ChangeManager { get; set; }

	}
}
