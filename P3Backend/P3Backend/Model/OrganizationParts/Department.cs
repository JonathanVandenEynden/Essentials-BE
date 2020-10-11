namespace P3Backend.Model.OrganizationParts {
	public class Department : IOrganizationPart {
		// enum van maken?

		public Department(string name) : base(name) {
		}

		protected Department() {
			// EF
		}
	}
}