using P3Backend.Model.OrganizationParts;
using P3Backend.Model.Users;

namespace P3Backend.Model.TussenTabellen {
	public class EmployeeChangeGroup {

		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }
		public int ChangeGroupId { get; set; }
		public ChangeGroup ChangeGroup { get; set; }

		public EmployeeChangeGroup(Employee e, ChangeGroup cg) {
			Employee = e;
			EmployeeId = e.Id;
			ChangeGroup = cg;
			ChangeGroupId = cg.Id;
		}

		protected EmployeeChangeGroup() {
			//EF
		}
	}
}
