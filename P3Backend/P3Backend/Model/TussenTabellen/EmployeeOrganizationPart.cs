using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Users;

namespace P3Backend.Model.TussenTabellen {
	public class EmployeeOrganizationPart {

		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }
		public int OrganizationPartId { get; set; }
		public OrganizationPart OrganizationPart { get; set; }

		public EmployeeOrganizationPart(Employee e, OrganizationPart op) {
			Employee = e;
			EmployeeId = e.Id;
			OrganizationPart = op;
			OrganizationPartId = op.Id;
		}

		protected EmployeeOrganizationPart() {
			//EF
		}
	}
}
